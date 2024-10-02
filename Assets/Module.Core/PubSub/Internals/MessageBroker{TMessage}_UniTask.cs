#if UNITASK

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
using System.Threading;
using Cysharp.Threading.Tasks;
using Module.Core.Collections;
using Module.Core.Collections.Unsafe;
using Module.Core.Logging;

namespace Module.Core.PubSub.Internals
{
    partial class MessageBroker<TMessage>
    {
        private CappedArrayPool<UniTask> _taskArrayPool;

        public CappedArrayPool<UniTask> TaskArrayPool
        {
            get => _taskArrayPool ?? CappedArrayPool<UniTask>.Shared8Limit;
            set => _taskArrayPool = value ?? throw new ArgumentNullException(nameof(value));
        }

        public async UniTask PublishAsync(
              TMessage message
            , PublishingContext context
            , CancellationToken token
            , ILogger logger
        )
        {
            var handlerListList = GetHandlerListList(logger);

#if __MODULE_CORE_VALIDATION__
            try
#endif
            {
                handlerListList.GetBufferUnsafe(out var handlerListArray, out var length);

                for (var i = 0; i < length; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }

                    await PublishAsync(handlerListArray[i], message, context, token, TaskArrayPool, logger);

                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }
#if __MODULE_CORE_VALIDATION__
            catch (Exception ex)
            {
                logger?.LogException(ex);
            }
            finally
#endif
            {
                Dispose(handlerListList);
            }
        }

        private static async UniTask PublishAsync(
              FasterList<IHandler<TMessage>> handlerList
            , TMessage message
            , PublishingContext context
            , CancellationToken token
            , CappedArrayPool<UniTask> taskArrayPool
            , ILogger logger
        )
        {
            if (handlerList.Count < 1)
            {
                return;
            }

            var tasks = taskArrayPool.Rent(handlerList.Count);
            ToTasks(handlerList, message, context, token, logger, tasks);

#if __MODULE_CORE_VALIDATION__
            try
#endif
            {
                await UniTask.WhenAll(tasks);
            }
#if __MODULE_CORE_VALIDATION__
            catch (Exception ex)
            {
                logger?.LogException(ex);
            }
            finally
#endif
            {
                taskArrayPool.Return(tasks);
            }

            static void ToTasks(
                  FasterList<IHandler<TMessage>> handlerList
                , TMessage message
                , PublishingContext context
                , CancellationToken token
                , ILogger logger
                , UniTask[] result
            )
            {
                var handlers = handlerList.AsReadOnlySpan();
                var handlersLength = handlerList.Count;

                for (var i = 0; i < handlersLength; i++)
                {
#if __MODULE_CORE_VALIDATION__
                    try
#endif
                    {
                        result[i] = handlers[i]?.Handle(message, context, token) ?? UniTask.CompletedTask;
                    }
#if __MODULE_CORE_VALIDATION__
                    catch (Exception ex)
                    {
                        result[i] = UniTask.CompletedTask;
                        logger?.LogException(ex);
                    }
#endif
                }
            }
        }
    }
}

#endif
