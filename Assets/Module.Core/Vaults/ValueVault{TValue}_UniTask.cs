#if UNITASK

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
using Cysharp.Threading.Tasks;

namespace Module.Core.Vaults
{
    public sealed partial class ValueVault<TValue>
    {
        #region    ID<T>
        #endregion =====

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask WaitUntilContains<T>(Identifier<T> id, CancellationToken token = default)
            => WaitUntilContains(ToComplexId(id), token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask WaitUntil<T>(Identifier<T> id, TValue value, CancellationToken token = default)
            => WaitUntil(ToComplexId(id), value, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask<Option<TValue>> TryGetAsync<T>(Identifier<T> id, CancellationToken token = default)
            => TryGetAsync(ToComplexId(id), token);

        #region    PRESET_ID
        #endregion =========

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask WaitUntilContains(PresetId id, CancellationToken token = default)
            => WaitUntilContains((ComplexId)id, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask WaitUntil(PresetId id, TValue other, CancellationToken token = default)
            => WaitUntil((ComplexId)id, other, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask<Option<TValue>> TryGetAsync(PresetId id, CancellationToken token = default)
            => TryGetAsync((ComplexId)id, token);

        #region    ID2
        #endregion ===

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async UniTask WaitUntilContains(ComplexId id, CancellationToken token = default)
        {
            var map = _map;

            while (map.ContainsKey(id) == false)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                await UniTask.NextFrame(token);

                if (token.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async UniTask WaitUntil(ComplexId id, TValue other, CancellationToken token = default)
        {
            var map = _map;

            while (map.TryGetValue(id, out var value) == false
                || EqualityComparer<TValue>.Default.Equals(value, other) == false
            )
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                await UniTask.NextFrame(token);

                if (token.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async UniTask<Option<TValue>> TryGetAsync(ComplexId id, CancellationToken token = default)
        {
            await WaitUntilContains(id, token);

            if (token.IsCancellationRequested)
            {
                return default;
            }

            return _map.TryGetValue(id, out var value) ? value : default;
        }
    }
}

#endif
