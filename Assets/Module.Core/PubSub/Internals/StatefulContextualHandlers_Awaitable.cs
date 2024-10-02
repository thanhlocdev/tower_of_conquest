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
    internal sealed class StatefulContextualHandlerFuncMessageToken<TState, TMessage> : IHandler<TMessage> where TState : class
    {
        private readonly DelegateId _id;
        private readonly WeakReference<TState> _state;
        private Func<TState, TMessage, PublishingContext, CancellationToken, Awaitable> _handler;

        public StatefulContextualHandlerFuncMessageToken(TState state, Func<TState, TMessage, PublishingContext, CancellationToken, Awaitable> handler)
        {
            _state = new(state ?? throw new ArgumentNullException(nameof(state)));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _id = new(handler, state.GetHashCode());
        }

        public DelegateId Id => _id;

        public void Dispose()
        {
            _handler = null;
        }

        public Awaitable Handle(TMessage message, PublishingContext context, CancellationToken token)
        {
            if (token.IsCancellationRequested || _state.TryGetTarget(out var state) == false)
            {
                return Awaitables.GetCompleted();
            }

            return _handler?.Invoke(state, message, context, token) ?? Awaitables.GetCompleted();
        }
    }

    internal sealed class StatefulContextualHandlerFuncToken<TState, TMessage> : IHandler<TMessage> where TState : class
    {
        private readonly DelegateId _id;
        private readonly WeakReference<TState> _state;
        private Func<TState, PublishingContext, CancellationToken, Awaitable> _handler;

        public StatefulContextualHandlerFuncToken(TState state, Func<TState, PublishingContext, CancellationToken, Awaitable> handler)
        {
            _state = new(state ?? throw new ArgumentNullException(nameof(state)));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _id = new(handler, state.GetHashCode());
        }

        public DelegateId Id => _id;

        public void Dispose()
        {
            _handler = null;
        }

        public Awaitable Handle(TMessage message, PublishingContext context, CancellationToken token)
        {
            if (token.IsCancellationRequested || _state.TryGetTarget(out var state) == false)
            {
                return Awaitables.GetCompleted();
            }

            return _handler?.Invoke(state, context, token) ?? Awaitables.GetCompleted();
        }
    }

    internal sealed class StatefulContextualHandlerFuncMessage<TState, TMessage> : IHandler<TMessage> where TState : class
    {
        private readonly DelegateId _id;
        private readonly WeakReference<TState> _state;
        private Func<TState, TMessage, PublishingContext, Awaitable> _handler;

        public StatefulContextualHandlerFuncMessage(TState state, Func<TState, TMessage, PublishingContext, Awaitable> handler)
        {
            _state = new(state ?? throw new ArgumentNullException(nameof(state)));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _id = new(handler, state.GetHashCode());
        }

        public DelegateId Id => _id;

        public void Dispose()
        {
            _handler = null;
        }

        public Awaitable Handle(TMessage message, PublishingContext context, CancellationToken token)
        {
            if (token.IsCancellationRequested || _state.TryGetTarget(out var state) == false)
            {
                return Awaitables.GetCompleted();
            }

            return _handler?.Invoke(state, message, context) ?? Awaitables.GetCompleted();
        }
    }

    internal sealed class StatefulContextualHandlerFunc<TState, TMessage> : IHandler<TMessage> where TState : class
    {
        private readonly DelegateId _id;
        private readonly WeakReference<TState> _state;
        private Func<TState, PublishingContext, Awaitable> _handler;

        public StatefulContextualHandlerFunc(TState state, Func<TState, PublishingContext, Awaitable> handler)
        {
            _state = new(state ?? throw new ArgumentNullException(nameof(state)));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _id = new(handler, state.GetHashCode());
        }

        public DelegateId Id => _id;

        public void Dispose()
        {
            _handler = null;
        }

        public Awaitable Handle(TMessage message, PublishingContext context, CancellationToken token)
        {
            if (token.IsCancellationRequested || _state.TryGetTarget(out var state) == false)
            {
                return Awaitables.GetCompleted();
            }

            return _handler?.Invoke(state, context) ?? Awaitables.GetCompleted();
        }
    }

    internal sealed class StatefulContextualHandlerActionMessage<TState, TMessage> : IHandler<TMessage> where TState : class
    {
        private readonly DelegateId _id;
        private readonly WeakReference<TState> _state;
        private Action<TState, TMessage, PublishingContext> _handler;

        public StatefulContextualHandlerActionMessage(TState state, Action<TState, TMessage, PublishingContext> handler)
        {
            _state = new(state ?? throw new ArgumentNullException(nameof(state)));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _id = new(handler, state.GetHashCode());
        }

        public DelegateId Id => _id;

        public void Dispose()
        {
            _handler = null;
        }

        public Awaitable Handle(TMessage message, PublishingContext context, CancellationToken token)
        {
            if (token.IsCancellationRequested || _state.TryGetTarget(out var state) == false)
            {
                return Awaitables.GetCompleted();
            }

            _handler?.Invoke(state, message, context);
            return Awaitables.GetCompleted();
        }
    }

    internal sealed class StatefulContextualHandlerAction<TState, TMessage> : IHandler<TMessage> where TState : class
    {
        private readonly DelegateId _id;
        private readonly WeakReference<TState> _state;
        private Action<TState, PublishingContext> _handler;

        public StatefulContextualHandlerAction(TState state, Action<TState, PublishingContext> handler)
        {
            _state = new(state ?? throw new ArgumentNullException(nameof(state)));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _id = new(handler, state.GetHashCode());
        }

        public DelegateId Id => _id;

        public void Dispose()
        {
            _handler = null;
        }

        public Awaitable Handle(TMessage message, PublishingContext context, CancellationToken token)
        {
            if (token.IsCancellationRequested || _state.TryGetTarget(out var state) == false)
            {
                return Awaitables.GetCompleted();
            }

            _handler?.Invoke(state, context);
            return Awaitables.GetCompleted();
        }
    }
}

#endif
