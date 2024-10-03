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

namespace Module.Core.Vaults
{
    public sealed partial class ValueVault<TValue> : IClearable
        where TValue : struct
    {
        private readonly Dictionary<ComplexId, TValue> _map = new();

        public void Clear()
        {
            _map.Clear();
        }

        #region    ID<T>
        #endregion =====

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains<T>(Identifier<T> id)
            => Contains(ToComplexId(id));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryRemove<T>(Identifier<T> id)
            => TryRemove(ToComplexId(id));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGet<T>(Identifier<T> id, out TValue value)
            => TryGet(ToComplexId(id), out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TrySet<T>(Identifier<T> id, TValue value)
            => TrySet(ToComplexId(id), value);

        #region    PRESET_ID
        #endregion =========

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(PresetId id)
            => Contains((ComplexId)id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryRemove(PresetId id)
            => TryRemove((ComplexId)id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGet(PresetId id, out TValue value)
            => TryGet((ComplexId)id, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TrySet(PresetId id, TValue value)
            => TrySet((ComplexId)id, value);

        #region    ID2
        #endregion ===

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(ComplexId id)
            => _map.ContainsKey(id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryRemove(ComplexId id)
            => _map.Remove(id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGet(ComplexId id, out TValue value)
            => _map.TryGetValue(id, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TrySet(ComplexId id, TValue value)
        {
            _map[id] = value;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ComplexId ToComplexId<T>(Identifier<T> id)
            => TypeId.Get<T>().ToComplexId(id);
    }
}
