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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using Module.Core.Logging;
using Module.Core.PubSub.Internals;

namespace Module.Core.PubSub
{
    partial class MessageSubscriber
    {
        public readonly partial struct Subscriber<TScope>
        {
            internal readonly MessageSubscriber _subscriber;

            public TScope Scope { get; }

            public bool IsValid => _subscriber != null;

            internal Subscriber([NotNull] MessageSubscriber subscriber, [NotNull] TScope scope)
            {
                _subscriber = subscriber;
                Scope = scope;
            }

            /// <summary>
            /// Remove empty handler groups to optimize performance.
            /// </summary>
#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Compress<TMessage>(ILogger logger = null)
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false)
                {
                    return;
                }
#endif

                if (_subscriber._brokers.TryGet<MessageBroker<TScope, TMessage>>(out var broker))
                {
                    broker.Compress(Scope, logger);
                }
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe<TMessage>(
                  [NotNull] Action handler
                , int order = 0
                , ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);
                TrySubscribe(new HandlerAction<TMessage>(handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe<TMessage>(
                  [NotNull] Action<TMessage> handler
                , int order = 0
                , ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);
                TrySubscribe(new HandlerActionMessage<TMessage>(handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe<TMessage>(
                  [NotNull] Action handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);

                if (TrySubscribe(new HandlerAction<TMessage>(handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe<TMessage>(
                  [NotNull] Action<TMessage> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);

                if (TrySubscribe(new HandlerActionMessage<TMessage>(handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe<TMessage>(
                  [NotNull] Action<PublishingContext> handler
                , int order = 0
                , ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);
                TrySubscribe(new ContextualHandlerAction<TMessage>(handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public ISubscription Subscribe<TMessage>(
                  [NotNull] Action<TMessage, PublishingContext> handler
                , int order = 0
                , ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);
                TrySubscribe(new ContextualHandlerActionMessage<TMessage>(handler), order, out var subscription, logger);
                return subscription;
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe<TMessage>(
                  [NotNull] Action<PublishingContext> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);

                if (TrySubscribe(new ContextualHandlerAction<TMessage>(handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }

#if __MODULE_CORE_PUBSUB_NO_VALIDATION__
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            public void Subscribe<TMessage>(
                  [NotNull] Action<TMessage, PublishingContext> handler
                , CancellationToken unsubscribeToken
                , int order = 0
                , ILogger logger = null
            )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
            {
                ThrowIfHandlerIsNull(handler);

                if (TrySubscribe(new ContextualHandlerActionMessage<TMessage>(handler), order, out var subscription, logger))
                {
                    subscription.RegisterTo(unsubscribeToken);
                }
            }

            internal bool TrySubscribe<TMessage>(
                  [NotNull] IHandler<TMessage> handler
                , int order
                , out Subscription<TMessage> subscription
                , ILogger logger
            )
            {
#if __MODULE_CORE_PUBSUB_VALIDATION__
                if (Validate(logger) == false)
                {
                    subscription = Subscription<TMessage>.None;
                    return false;
                }
#endif

                var taskArrayPool = _subscriber._taskArrayPool;
                var brokers = _subscriber._brokers;

                lock (brokers)
                {
                    if (brokers.TryGet<MessageBroker<TScope, TMessage>>(out var broker) == false)
                    {
                        broker = new MessageBroker<TScope, TMessage>();

                        if (brokers.TryAdd(broker) == false)
                        {
                            broker?.Dispose();
                            subscription = Subscription<TMessage>.None;
                            return false;
                        }
                    }

                    subscription = broker.Subscribe(Scope, handler, order, taskArrayPool, logger);
                    return true;
                }
            }

            [Conditional("__MODULE_CORE_PUBSUB_VALIDATION__"), DoesNotReturn]
            private static void ThrowIfHandlerIsNull(Delegate handler)
            {
                if (handler == null) throw new ArgumentNullException(nameof(handler));
            }

#if __MODULE_CORE_PUBSUB_VALIDATION__
            private bool Validate(ILogger logger)
            {
                if (_subscriber != null)
                {
                    return true;
                }

                (logger ?? DevLogger.Default).LogError(
                    $"{GetType()} must be retrieved via `{nameof(MessageSubscriber)}.{nameof(MessageSubscriber.Scope)}` API"
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
