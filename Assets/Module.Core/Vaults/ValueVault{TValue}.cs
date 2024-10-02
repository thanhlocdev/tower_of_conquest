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
