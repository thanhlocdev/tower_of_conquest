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

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace Module.Core.Vaults
{
    public static partial class GlobalMap<TKey, TValue>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Awaitable WaitUntilContains(TKey key, CancellationToken token = default)
        {
            var map = s_map;

            while (map.ContainsKey(key) == false)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                await Awaitable.NextFrameAsync(token);

                if (token.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Awaitable WaitUntil(TKey key, TValue value, CancellationToken token = default)
        {
            var map = s_map;

            while (map.TryGetValue(key, out var result) == false
                || EqualityComparer<TValue>.Default.Equals(result, value) == false
            )
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                await Awaitable.NextFrameAsync(token);

                if (token.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Awaitable<Option<TValue>> TryGetAsync(TKey key, CancellationToken token = default)
        {
            var map = s_map;
            TValue value;

            while (map.TryGetValue(key, out value) == false)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                await Awaitable.NextFrameAsync(token);

                if (token.IsCancellationRequested)
                {
                    break;
                }
            }

            if (token.IsCancellationRequested)
            {
                return default;
            }

            return new(value);
        }
    }
}

#endif
