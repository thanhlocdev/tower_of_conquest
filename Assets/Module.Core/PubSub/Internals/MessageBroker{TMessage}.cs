#if UNITASK || UNITY_6000_0_OR_NEWER

// MIT License
// 
// Copyright (c) 2024 Laicasaane
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#if !(UNITY_EDITOR || DEBUG) || DISABLE_DEBUG
#define __MODULE_CORE_NO_VALIDATION__
#else
#define __MODULE_CORE_VALIDATION__
#endif

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Module.Core.Collections;
using Module.Core.Logging;
using Module.Core.Pooling;

namespace Module.Core.PubSub.Internals
{
    internal sealed partial class MessageBroker<TMessage> : MessageBroker
    {
        private readonly FasterList<int> _ordering = new(1);
        private readonly Dictionary<int, ArrayMap<DelegateId, IHandler<TMessage>>> _orderToHandlerMap = new(1);

        private long _refCount;

        public bool IsEmpty => _ordering.Count < 1;

        public bool IsCached => _refCount > 0;

        public Subscription<TMessage> Subscribe([NotNull] IHandler<TMessage> handler, int order, ILogger logger)
        {
            lock (_orderToHandlerMap)
            {
#if __MODULE_CORE_VALIDATION__
                try
#endif
                {
                    var orderToHandlerMap = _orderToHandlerMap;
                    var ordering = _ordering;

                    if (ordering.Contains(order) == false)
                    {
                        ordering.Add(order);
                        ordering.Sort(Comparer<int>.Default);
                    }

                    if (orderToHandlerMap.TryGetValue(order, out var handlerMap) == false)
                    {
                        handlerMap = new ArrayMap<DelegateId, IHandler<TMessage>>();
                        orderToHandlerMap.Add(order, handlerMap);
                    }

                    if (handlerMap.TryAdd(handler.Id, handler))
                    {
                        return new Subscription<TMessage>(handler, handlerMap);
                    }
                }
#if __MODULE_CORE_VALIDATION__
                catch (Exception ex)
                {
                    logger?.LogException(ex);
                }
#endif

                return Subscription<TMessage>.None;
            }
        }

        public sealed override void Dispose()
        {
            lock (_orderToHandlerMap)
            {
                var orderToHandlerMap = _orderToHandlerMap;
                var ordering = _ordering;

                foreach (var kvp in orderToHandlerMap)
                {
                    kvp.Value?.Dispose();
                }

                orderToHandlerMap.Clear();
            }
        }

        public void OnCache()
        {
            checked
            {
                _refCount++;
            }
        }

        public void OnUncache()
        {
            _refCount = Math.Max(0, _refCount - 1);
        }

        /// <summary>
        /// Remove empty handler groups to optimize performance.
        /// </summary>
        public sealed override void Compress(ILogger logger)
        {
            lock (_orderToHandlerMap)
            {
#if __MODULE_CORE_VALIDATION__
                try
#endif
                {
                    var orderToHandlerMap = _orderToHandlerMap;
                    var ordering = _ordering;
                    var orders = ordering.AsSpan();

                    for (var i = orders.Length - 1; i >= 0; i--)
                    {
                        var order = orders[i];

                        if (orderToHandlerMap.TryGetValue(order, out var handlerMap) == false)
                        {
                            continue;
                        }

                        if (handlerMap.Count > 0)
                        {
                            continue;
                        }

                        orderToHandlerMap.Remove(order);
                        ordering.RemoveAt(i);
                    }
                }
#if __MODULE_CORE_VALIDATION__
                catch (Exception ex)
                {
                    logger?.LogException(ex);
                }
#endif
            }
        }

        private FasterList<FasterList<IHandler<TMessage>>> GetHandlerListList(ILogger logger)
        {
            lock (_orderToHandlerMap)
            {
                var orderToHandlerMap = _orderToHandlerMap;
                var orders = _ordering.AsSpan();
                var handlerListList = GetHandlerListList();

#if __MODULE_CORE_VALIDATION__
                try
#endif
                {
                    for (var i = orders.Length - 1; i >= 0; i--)
                    {
                        var order = orders[i];

                        if (orderToHandlerMap.TryGetValue(order, out var handlerMap) == false
                            || handlerMap.Count < 1
                        )
                        {
                            continue;
                        }

                        var handlerArray = handlerMap.GetValues();
                        var handlerList = GetHandlerList();
                        handlerList.AddReplicateNoInit(handlerArray.Length);
                        handlerArray.CopyTo(handlerList.AsSpan());
                        handlerListList.Add(handlerList);
                    }
                }
#if __MODULE_CORE_VALIDATION__
                catch (Exception ex)
                {
                    logger?.LogException(ex);
                }
#endif

                return handlerListList;
            }
        }

        private static void Dispose(FasterList<FasterList<IHandler<TMessage>>> handlersList)
        {
            if (handlersList == null)
            {
                return;
            }

            var handlersArray = handlersList.AsSpan();
            var handlersArrayLength = handlersArray.Length;

            for (var i = 0; i < handlersArrayLength; i++)
            {
                ref var handlers = ref handlersArray[i];

                if (handlers == null) continue;

                Release(handlers);

                handlers = default;
            }

            Release(handlersList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FasterList<FasterList<IHandler<TMessage>>> GetHandlerListList()
            => FasterListPool<FasterList<IHandler<TMessage>>>.Get();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Release(FasterList<FasterList<IHandler<TMessage>>> item)
            => FasterListPool<FasterList<IHandler<TMessage>>>.Release(item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FasterList<IHandler<TMessage>> GetHandlerList()
            => FasterListPool<IHandler<TMessage>>.Get();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Release(FasterList<IHandler<TMessage>> item)
            => FasterListPool<IHandler<TMessage>>.Release(item);
    }
}

#endif
