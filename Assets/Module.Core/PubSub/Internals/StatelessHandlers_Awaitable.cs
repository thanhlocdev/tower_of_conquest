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
    internal sealed class HandlerFuncMessageToken<TMessage> : IHandler<TMessage>
    {
        private readonly DelegateId _id;
        private Func<TMessage, CancellationToken, Awaitable> _handler;

        public HandlerFuncMessageToken(Func<TMessage, CancellationToken, Awaitable> handler)
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

            return _handler?.Invoke(message, token) ?? Awaitables.GetCompleted();
        }
    }

    internal sealed class HandlerFuncToken<TMessage> : IHandler<TMessage>
    {
        private readonly DelegateId _id;
        private Func<CancellationToken, Awaitable> _handler;

        public HandlerFuncToken(Func<CancellationToken, Awaitable> handler)
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

            return _handler?.Invoke(token) ?? Awaitables.GetCompleted();
        }
    }

    internal sealed class HandlerFuncMessage<TMessage> : IHandler<TMessage>
    {
        private readonly DelegateId _id;
        private Func<TMessage, Awaitable> _handler;

        public HandlerFuncMessage(Func<TMessage, Awaitable> handler)
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

            return _handler?.Invoke(message) ?? Awaitables.GetCompleted();
        }
    }

    internal sealed class HandlerFunc<TMessage> : IHandler<TMessage>
    {
        private readonly DelegateId _id;
        private Func<Awaitable> _handler;

        public HandlerFunc(Func<Awaitable> handler)
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

            return _handler?.Invoke() ?? Awaitables.GetCompleted();
        }
    }

    internal sealed class HandlerActionMessage<TMessage> : IHandler<TMessage>
    {
        private readonly DelegateId _id;
        private Action<TMessage> _handler;

        public HandlerActionMessage(Action<TMessage> handler)
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

            _handler?.Invoke(message);
            return Awaitables.GetCompleted();
        }
    }

    internal sealed class HandlerAction<TMessage> : IHandler<TMessage>
    {
        private readonly DelegateId _id;
        private Action _handler;

        public HandlerAction(Action handler)
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

            _handler?.Invoke();
            return Awaitables.GetCompleted();
        }
    }
}

#endif
