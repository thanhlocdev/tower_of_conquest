#if UNITASK

using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Module.Core.Vaults
{
    using UnityObject = UnityEngine.Object;

    public static partial class GlobalObjectVault
    {
        #region    ID<T>
        #endregion =====

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitUntilContains<T>(Identifier<T> id, CancellationToken token = default)
            => s_vault.WaitUntilContains(id, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<Option<T>> TryGetAsync<T>(Identifier<T> id, UnityObject context = null, CancellationToken token = default)
            => s_vault.TryGetAsync<T>(id, context, token);

        #region    PRESET_ID
        #endregion =========

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitUntilContains(PresetId id, CancellationToken token = default)
            => s_vault.WaitUntilContains(id, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<Option<T>> TryGetAsync<T>(PresetId id, UnityObject context = null, CancellationToken token = default)
            => s_vault.TryGetAsync<T>(id, context, token);

        #region    ID2
        #endregion ===

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitUntilContains<T>(ComplexId id, CancellationToken token = default)
            => s_vault.WaitUntilContains(id, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<Option<T>> TryGetAsync<T>(ComplexId id, UnityObject context = null, CancellationToken token = default)
            => s_vault.TryGetAsync<T>(id, context, token);
    }
}
#endif
