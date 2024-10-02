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

namespace Module.Core.PubSub
{
    /// <summary>
    /// Anonymous Publisher that allows invoking handlers that take no message argument
    /// </summary>
    public partial class AnonPublisher
    {
        private readonly MessagePublisher _publisher;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Publisher<GlobalScope> Global()
        {
            return new(_publisher.Global());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Publisher<TScope> Scope<TScope>()
            where TScope : struct
        {
            return new(_publisher.Scope(default(TScope)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Publisher<TScope> Scope<TScope>([NotNull] TScope scope)
        {
            return new(_publisher.Scope(scope));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CachedPublisher<AnonMessage> GlobalCache(ILogger logger = null)
        {
            return Global().Cache(logger);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CachedPublisher<AnonMessage> Cache<TScope>(ILogger logger = null)
            where TScope : struct
        {
            return Scope(default(TScope)).Cache(logger);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CachedPublisher<AnonMessage> Cache<TScope>([NotNull] TScope scope, ILogger logger = null)
        {
            return Scope(scope).Cache(logger);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UnityPublisher<TScope> UnityScope<TScope>([NotNull] TScope scope)
            where TScope : UnityEngine.Object
        {
            return new(_publisher.UnityScope(scope));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CachedPublisher<AnonMessage> UnityCache<TScope>([NotNull] TScope scope, ILogger logger = null)
            where TScope : UnityEngine.Object
        {
            return UnityScope(scope).Cache(logger);
        }
    }
}

#endif
