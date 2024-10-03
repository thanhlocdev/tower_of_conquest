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

using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace Module.Core.Vaults
{
    public static partial class GlobalValueVault<TValue>
    {
        #region    ID<T>
        #endregion =====

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Awaitable WaitUntilContains<T>(Id<T> id, CancellationToken token = default)
            => s_vault.WaitUntilContains(id, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Awaitable WaitUntil<T>(Id<T> id, TValue value, CancellationToken token = default)
            => s_vault.WaitUntil(id, value, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Awaitable<Option<TValue>> TryGetAsync<T>(Id<T> id, CancellationToken token = default)
            => s_vault.TryGetAsync(id, token);

        #region    PRESET_ID
        #endregion =========

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Awaitable WaitUntilContains(PresetId id, CancellationToken token = default)
            => s_vault.WaitUntilContains(id, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Awaitable WaitUntil(PresetId id, TValue value, CancellationToken token = default)
            => s_vault.WaitUntil(id, value, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Awaitable<Option<TValue>> TryGetAsync(PresetId id, CancellationToken token = default)
            => s_vault.TryGetAsync(id, token);

        #region    ID2
        #endregion ===

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Awaitable WaitUntilContains(Id2 id, CancellationToken token = default)
            => s_vault.WaitUntilContains(id, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Awaitable WaitUntil(Id2 id, TValue value, CancellationToken token = default)
            => s_vault.WaitUntil(id, value, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Awaitable<Option<TValue>> TryGetAsync(Id2 id, CancellationToken token = default)
            => s_vault.TryGetAsync(id, token);
    }
}

#endif
