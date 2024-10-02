#if UNITASK

using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Module.Core.Vaults
{
    using UnityObject = UnityEngine.Object;

    public sealed partial class ObjectVault
    {
        #region    ID<T>
        #endregion =====

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask WaitUntilContains<T>(Identifier<T> id, CancellationToken token = default)
            => WaitUntilContains(ToComplexId(id), token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask<Option<T>> TryGetAsync<T>(Identifier<T> id, UnityObject context = null, CancellationToken token = default)
           => TryGetAsync<T>(ToComplexId(id), context, token);

        #region    PRESET_ID
        #endregion =========

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask WaitUntilContains(PresetId id, CancellationToken token = default)
            => WaitUntilContains((ComplexId)id, token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask<Option<T>> TryGetAsync<T>(PresetId id, UnityObject context = null, CancellationToken token = default)
            => TryGetAsync<T>((ComplexId)id, context, token);

        #region    CompledId
        #endregion =========

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async UniTask WaitUntilContains(ComplexId id, CancellationToken token = default)
        {
            var map = _map;

            while(map.ContainsKey(id) == false)
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
        public async UniTask<Option<T>> TryGetAsync<T>(
             ComplexId id
           , UnityObject context
           , CancellationToken token
       )
        {
            ThrowIfNotReferenceType<T>();

            await WaitUntilContains(id, token);

            if (token.IsCancellationRequested)
            {
                return default;
            }

            return TryCast<T>(id, _map[id], context);
        }
    }
}
#endif
