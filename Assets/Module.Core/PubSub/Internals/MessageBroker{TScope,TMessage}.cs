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
using Module.Core.Collections;
using Module.Core.Logging;

namespace Module.Core.PubSub.Internals
{
    internal sealed partial class MessageBroker<TScope, TMessage> : MessageBroker
    {
        private readonly ArrayMap<TScope, MessageBroker<TMessage>> _scopedBrokers = new();
        private readonly FasterList<TScope> _scopesToRemove = new();

        public bool IsEmpty => _scopedBrokers.Count <= 0;

        public override void Dispose()
        {
            var scopedBrokers = _scopedBrokers;

            lock (scopedBrokers)
            {
                var brokers = scopedBrokers.GetValues();

                foreach (var broker in brokers)
                {
                    broker?.Dispose();
                }

                scopedBrokers.Dispose();
            }
        }

        public override void Compress(ILogger logger)
        {
            lock (_scopedBrokers)
            {
#if !__MODULE_CORE_NO_VALIDATION__
                try
#endif
                {
                    var scopedBrokers = _scopedBrokers;
                    var scopesToRemove = _scopesToRemove;

                    scopesToRemove.IncreaseCapacityTo(scopedBrokers.Count);

                    foreach (var (key, broker) in scopedBrokers)
                    {
                        broker.Compress(logger);

                        if (broker.IsEmpty)
                        {
                            broker.Dispose();
                            scopesToRemove.Add(key);
                        }
                    }

                    var scopes = scopesToRemove.AsSpan();

                    foreach (var scope in scopes)
                    {
                        scopedBrokers.Remove(scope);
                    }
                }
#if !__MODULE_CORE_NO_VALIDATION__
                catch (Exception ex)
                {
                    logger.LogException(ex);
                }
#endif
            }
        }

        /// <summary>
        /// Remove empty handler groups to optimize performance.
        /// </summary>
        public void Compress(TScope scope, ILogger logger)
        {
            var scopedBrokers = _scopedBrokers;

            lock (scopedBrokers)
            {
                if (scopedBrokers.TryGetValue(scope, out var broker) == false)
                {
                    return;
                }

                if (broker.IsCached)
                {
                    return;
                }

                broker.Compress(logger);

                if (broker.IsEmpty)
                {
                    scopedBrokers.Remove(scope);
                    broker.Dispose();
                }
            }
        }
    }
}

#endif
