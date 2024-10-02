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

using System;
using System.Threading;
using UnityEngine;

namespace Module.Core.PubSub.Internals
{
    internal sealed class ContextualHandlerFuncMessageToken<TMessage> : IHandler<TMessage>
    {
        private readonly DelegateId _id;
        private Func<TMessage, PublishingContext, CancellationToken, Awaitable> _handler;

        public ContextualHandlerFuncMessageToken(Func<TMessage, PublishingContext, CancellationToken, Awaitable> handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _id = new(handler);
        }

        public DelegateId Id => _id;

        public void Dispose()
        {
            _handler = null;
        }

        public Awaitable Handle(TMessage message, PublishingContext context, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return Awaitables.GetCompleted();
            }

            return _handler?.Invoke(message, context, token) ?? Awaitables.GetCompleted();
        }
    }

    internal sealed class ContextualHandlerFuncToken<TMessage> : IHandler<TMessage>
    {
        private readonly DelegateId _id;
        private Func<PublishingContext, CancellationToken, Awaitable> _handler;

        public ContextualHandlerFuncToken(Func<PublishingContext, CancellationToken, Awaitable> handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _id = new(handler);
        }

        public DelegateId Id => _id;

        public void Dispose()
        {
            _handler = null;
        }

        public Awaitable Handle(TMessage message, PublishingContext context, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return Awaitables.GetCompleted();
            }

            return _handler?.Invoke(context, token) ?? Awaitables.GetCompleted();
        }
    }

    internal sealed class ContextualHandlerFuncMessage<TMessage> : IHandler<TMessage>
    {
        private readonly DelegateId _id;
        private Func<TMessage, PublishingContext, Awaitable> _handler;

        public ContextualHandlerFuncMessage(Func<TMessage, PublishingContext, Awaitable> handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _id = new(handler);
        }

        public DelegateId Id => _id;

        public void Dispose()
        {
            _handler = null;
        }

        public Awaitable Handle(TMessage message, PublishingContext context, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return Awaitables.GetCompleted();
            }

            return _handler?.Invoke(message, context) ?? Awaitables.GetCompleted();
        }
    }

    internal sealed class ContextualHandlerFunc<TMessage> : IHandler<TMessage>
    {
        private readonly DelegateId _id;
        private Func<PublishingContext, Awaitable> _handler;

        public ContextualHandlerFunc(Func<PublishingContext, Awaitable> handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _id = new(handler);
        }

        public DelegateId Id => _id;

        public void Dispose()
        {
            _handler = null;
        }

        public Awaitable Handle(TMessage message, PublishingContext context, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return Awaitables.GetCompleted();
            }

            return _handler?.Invoke(context) ?? Awaitables.GetCompleted();
        }
    }

    internal sealed class ContextualHandlerActionMessage<TMessage> : IHandler<TMessage>
    {
        private readonly DelegateId _id;
        private Action<TMessage, PublishingContext> _handler;

        public ContextualHandlerActionMessage(Action<TMessage, PublishingContext> handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _id = new(handler);
        }

        public DelegateId Id => _id;

        public void Dispose()
        {
            _handler = null;
        }

        public Awaitable Handle(TMessage message, PublishingContext context, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return Awaitables.GetCompleted();
            }

            _handler?.Invoke(message, context);
            return Awaitables.GetCompleted();
        }
    }

    internal sealed class ContextualHandlerAction<TMessage> : IHandler<TMessage>
    {
        private readonly DelegateId _id;
        private Action<PublishingContext> _handler;

        public ContextualHandlerAction(Action<PublishingContext> handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _id = new(handler);
        }

        public DelegateId Id => _id;

        public void Dispose()
        {
            _handler = null;
        }

        public Awaitable Handle(TMessage message, PublishingContext context, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return Awaitables.GetCompleted();
            }

            _handler?.Invoke(context);
            return Awaitables.GetCompleted();
        }
    }
}

#endif
