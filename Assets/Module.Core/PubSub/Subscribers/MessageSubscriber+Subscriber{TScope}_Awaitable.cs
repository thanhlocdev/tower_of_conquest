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
    partial class MessageSubscriber
    {
        partial struct Subscriber<TScope>
        {
#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe<TMessage>(
                  [NotNull] Func<Awaitable> handler
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);
                TrySubscribe(new HandlerFunc<TMessage>(handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe<TMessage>(
                  [NotNull] Func<TMessage, Awaitable> handler
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);
                TrySubscribe(new HandlerFuncMessage<TMessage>(handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe<TMessage>(
                  [NotNull] Func<CancellationToken, Awaitable> handler
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);
                TrySubscribe(new HandlerFuncToken<TMessage>(handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe<TMessage>(
                  [NotNull] Func<TMessage, CancellationToken, Awaitable> handler
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);
                TrySubscribe(new HandlerFuncMessageToken<TMessage>(handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe<TMessage>(
                  [NotNull] Func<Awaitable> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);

                if (TrySubscribe(new HandlerFunc<TMessage>(handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe<TMessage>(
                  [NotNull] Func<TMessage, Awaitable> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);

                if (TrySubscribe(new HandlerFuncMessage<TMessage>(handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe<TMessage>(
                  [NotNull] Func<CancellationToken, Awaitable> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);

                if (TrySubscribe(new HandlerFuncToken<TMessage>(handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe<TMessage>(
                  [NotNull] Func<TMessage, CancellationToken, Awaitable> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);

                if (TrySubscribe(new HandlerFuncMessageToken<TMessage>(handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe<TMessage>(
                  [NotNull] Func<PublishingContext, Awaitable> handler
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);
                TrySubscribe(new ContextualHandlerFunc<TMessage>(handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe<TMessage>(
                  [NotNull] Func<TMessage, PublishingContext, Awaitable> handler
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);
                TrySubscribe(new ContextualHandlerFuncMessage<TMessage>(handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe<TMessage>(
                  [NotNull] Func<PublishingContext, CancellationToken, Awaitable> handler
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);
                TrySubscribe(new ContextualHandlerFuncToken<TMessage>(handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe<TMessage>(
                  [NotNull] Func<TMessage, PublishingContext, CancellationToken, Awaitable> handler
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);
                TrySubscribe(new ContextualHandlerFuncMessageToken<TMessage>(handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe<TMessage>(
                  [NotNull] Func<PublishingContext, Awaitable> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);

                if (TrySubscribe(new ContextualHandlerFunc<TMessage>(handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe<TMessage>(
                  [NotNull] Func<TMessage, PublishingContext, Awaitable> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);

                if (TrySubscribe(new ContextualHandlerFuncMessage<TMessage>(handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe<TMessage>(
                  [NotNull] Func<PublishingContext, CancellationToken, Awaitable> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);

                if (TrySubscribe(new ContextualHandlerFuncToken<TMessage>(handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe<TMessage>(
                  [NotNull] Func<TMessage, PublishingContext, CancellationToken, Awaitable> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , Module.Core.Logging.ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);

                if (TrySubscribe(new ContextualHandlerFuncMessageToken<TMessage>(handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }
        }
    }
}

#endif
