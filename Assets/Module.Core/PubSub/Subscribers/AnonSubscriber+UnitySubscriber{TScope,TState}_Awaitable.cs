#if !UNITASK && UNITY_6000_0_OR_NEWER

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
using System.Threading;
using Module.Core.PubSub.Internals;
using UnityEngine;

namespace Module.Core.PubSub
{
    public partial class AnonSubscriber
    {
        partial struct UnitySubscriber<TScope, TState>
        {
#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe(
                  [NotNull] Func<TState, Awaitable> handler
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false) return Subscription<AnonMessage>.None;
#endif

                ThrowIfHandlerIsNull(handler);

                _subscriber.TrySubscribe(new StatefulHandlerFunc<TState, AnonMessage>(State, handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe(
                  [NotNull] Func<TState, CancellationToken, Awaitable> handler
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false) return Subscription<AnonMessage>.None;
#endif

                ThrowIfHandlerIsNull(handler);

                _subscriber.TrySubscribe(new StatefulHandlerFuncToken<TState, AnonMessage>(State, handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe(
                  [NotNull] Func<TState, Awaitable> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false) return;
#endif

                ThrowIfHandlerIsNull(handler);

                if (_subscriber.TrySubscribe(new StatefulHandlerFunc<TState, AnonMessage>(State, handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe(
                  [NotNull] Func<TState, CancellationToken, Awaitable> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false) return;
#endif

                ThrowIfHandlerIsNull(handler);

                if (_subscriber.TrySubscribe(new StatefulHandlerFuncToken<TState, AnonMessage>(State, handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe(
                  [NotNull] Func<TState, PublishingContext, Awaitable> handler
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false) return Subscription<AnonMessage>.None;
#endif

                ThrowIfHandlerIsNull(handler);

                _subscriber.TrySubscribe(new StatefulContextualHandlerFunc<TState, AnonMessage>(State, handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe(
                  [NotNull] Func<TState, PublishingContext, CancellationToken, Awaitable> handler
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false) return Subscription<AnonMessage>.None;
#endif

                ThrowIfHandlerIsNull(handler);

                _subscriber.TrySubscribe(new StatefulContextualHandlerFuncToken<TState, AnonMessage>(State, handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe(
                  [NotNull] Func<TState, PublishingContext, Awaitable> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false) return;
#endif

                ThrowIfHandlerIsNull(handler);

                if (_subscriber.TrySubscribe(new StatefulContextualHandlerFunc<TState, AnonMessage>(State, handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe(
                  [NotNull] Func<TState, PublishingContext, CancellationToken, Awaitable> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false) return;
#endif

                ThrowIfHandlerIsNull(handler);

                if (_subscriber.TrySubscribe(new StatefulContextualHandlerFuncToken<TState, AnonMessage>(State, handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }
        }
    }
}

#endif
