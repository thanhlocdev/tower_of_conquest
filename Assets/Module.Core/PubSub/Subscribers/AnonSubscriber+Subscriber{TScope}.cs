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
#define __MODULE_CORE_PUBSUB_NO_VALIDATION__
#else
#define __MODULE_CORE_PUBSUB_VALIDATION__
#endif

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using Module.Core.Logging;
using Module.Core.PubSub.Internals;

namespace Module.Core.PubSub
{
    public partial class AnonSubscriber
    {
        /// <summary>
        /// Anonymous Subscriber allows registering handlers that take no message argument
        /// </summary>
        public readonly partial struct Subscriber<TScope>
        {
            internal readonly MessageSubscriber.Subscriber<TScope> _subscriber;

            public TScope Scope => _subscriber.Scope;

            public bool IsValid => _subscriber.IsValid;

            internal Subscriber(MessageSubscriber.Subscriber<TScope> subscriber)
            {
                _subscriber = subscriber;
            }

            /// <summary>
            /// Remove empty handler groups to optimize performance.
            /// </summary>
#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Compress(ILogger logger = null)
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false)
                {
                    return;
                }
#endif

                _subscriber.Compress<AnonMessage>(logger);
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe(
                  [NotNull] Action handler
                , int order = 0
                , ILogger logger = null
            )
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false)
                {
                    return Subscription<AnonMessage>.None;
                }
#endif

                return _subscriber.Subscribe<AnonMessage>(handler, order, logger);
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe(
                  [NotNull] Action handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , ILogger logger = null
            )
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false)
                {
                    return;
                }
#endif

                _subscriber.Subscribe<AnonMessage>(handler, unsubscribeToken, order, logger);
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe(
                  [NotNull] Action<PublishingContext> handler
                , int order = 0
                , ILogger logger = null
            )
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false)
                {
                    return Subscription<AnonMessage>.None;
                }
#endif

                return _subscriber.Subscribe<AnonMessage>(handler, order, logger);
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe(
                  [NotNull] Action<PublishingContext> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , ILogger logger = null
            )
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false)
                {
                    return;
                }
#endif

                _subscriber.Subscribe<AnonMessage>(handler, unsubscribeToken, order, logger);
            }

#if __MODULE_CORE_PUBSUB_VALIDATION__
            private bool Validate(ILogger logger)
            {
                if (_subscriber.IsValid == true)
                {
                    return true;
                }

                (logger ?? DevLogger.Default).LogError(
                    $"{GetType().Name} must be retrieved via `{nameof(AnonSubscriber)}.{nameof(AnonSubscriber.Scope)}` API"
                );

                return false;
            }
#endif

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            partial void RetainUsings();
        }
    }
}

#endif
