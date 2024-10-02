#if UNITASK

using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Module.Core.Vaults
{
    public static partial class GlobalValueVault<TValue>
    {
        #region    ID<T>
        #endregion =====

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitUntilContains<T>(Identifier<T> id, CancellationToken token = default)
            => s_vault.WaitUntilContains(id, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitUntil<T>(Identifier<T> id, TValue value, CancellationToken token = default)
            => s_vault.WaitUntil(id, value, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<Option<TValue>> TryGetAsync<T>(Identifier<T> id, CancellationToken token = default)
            => s_vault.TryGetAsync(id, token);

        #region    PRESET_ID
        #endregion =========

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitUntilContains(PresetId id, CancellationToken token = default)
            => s_vault.WaitUntilContains(id, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitUntil(PresetId id, TValue value, CancellationToken token = default)
            => s_vault.WaitUntil(id, value, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<Option<TValue>> TryGetAsync(PresetId id, CancellationToken token = default)
            => s_vault.TryGetAsync(id, token);

        #region    ID2
        #endregion ===

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitUntilContains(ComplexId id, CancellationToken token = default)
            => s_vault.WaitUntilContains(id, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitUntil(ComplexId id, TValue value, CancellationToken token = default)
            => s_vault.WaitUntil(id, value, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<Option<TValue>> TryGetAsync(ComplexId id, CancellationToken token = default)
            => s_vault.TryGetAsync(id, token);
    }
}

#endif
