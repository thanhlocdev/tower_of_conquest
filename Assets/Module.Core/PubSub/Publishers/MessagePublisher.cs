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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Module.Core.Logging;
using Module.Core.PubSub.Internals;
using Module.Core.Vaults;
using static Module.Core.PubSub.AnonPublisher;

namespace Module.Core.PubSub
{
    public partial class MessagePublisher
    {
        private readonly SingletonVault<MessageBroker> _brokers;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Publisher<GlobalScope> Global()
        {
            return new(this, default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Publisher<TScope> Scope<TScope>()
            where TScope : struct
        {
            return new(this, default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Publisher<TScope> Scope<TScope>([NotNull] TScope scope)
        {
            return new(this, scope);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CachedPublisher<TMessage> GlobalCache<TMessage>(ILogger logger = null)
#if MODULE_CORE_PUBSUB_RELAX_MODE
            where TMessage : new()
#else
            where TMessage : IMessage, new()
#endif
        {
            return Global().Cache<TMessage>(logger);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CachedPublisher<TMessage> Cache<TScope, TMessage>(ILogger logger = null)
            where TScope : struct
#if MODULE_CORE_PUBSUB_RELAX_MODE
            where TMessage : new()
#else
            where TMessage : IMessage, new()
#endif
        {
            return Scope(default(TScope)).Cache<TMessage>(logger);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CachedPublisher<TMessage> Cache<TScope, TMessage>([NotNull] TScope scope, ILogger logger = null)
#if MODULE_CORE_PUBSUB_RELAX_MODE
            where TMessage : new()
#else
            where TMessage : IMessage, new()
#endif
        {
            return Scope(scope).Cache<TMessage>(logger);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UnityPublisher<TScope> UnityScope<TScope>([NotNull] TScope scope)
            where TScope : UnityEngine.Object
        {
            return new(this, scope);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CachedPublisher<TMessage> UnityCache<TScope, TMessage>([NotNull] TScope scope, ILogger logger = null)
            where TScope : UnityEngine.Object
#if MODULE_CORE_PUBSUB_RELAX_MODE
            where TMessage : new()
#else
            where TMessage : IMessage, new()
#endif
        {
            return UnityScope(scope).Cache<TMessage>(logger);
        }
    }
}

#endif
