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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using Module.Core.Collections;

namespace Module.Core.PubSub.Internals
{
    internal sealed class Subscription<TMessage> : ISubscription
    {
        public static readonly Subscription<TMessage> None = new(default, default);

        private IHandler<TMessage> _handler;
        private readonly WeakReference<ArrayMap<DelegateId, IHandler<TMessage>>> _handlers;

        public Subscription(IHandler<TMessage> handler, ArrayMap<DelegateId, IHandler<TMessage>> handlers)
        {
            _handler = handler;
            _handlers = new WeakReference<ArrayMap<DelegateId, IHandler<TMessage>>>(handlers);
        }

        public void Dispose()
        {
            if (_handler == null)
            {
                return;
            }

            var id = _handler.Id;
            _handler.Dispose();
            _handler = null;

            if (_handlers.TryGetTarget(out var handlers))
            {
                handlers.Remove(id);
            }
        }
    }

    internal static class SubscriptionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RegisterTo<TMessage>(
              [NotNull] this Subscription<TMessage> subscription
            , CancellationToken unsubscribeToken
        )
#if !MODULE_CORE_PUBSUB_RELAX_MODE
                where TMessage : IMessage
#endif
        {
            unsubscribeToken.Register(static x => ((Subscription<TMessage>)x)?.Dispose(), subscription);
        }
    }
}

#endif
