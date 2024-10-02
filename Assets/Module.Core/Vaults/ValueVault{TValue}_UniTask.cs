#if UNITASK

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
