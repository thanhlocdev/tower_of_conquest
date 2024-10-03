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

namespace Module.Core.Vaults
{
#if UNITY_EDITOR
    internal static partial class GlobalValueVaultEditor
    {
        private readonly static System.Collections.Generic.Dictionary<TypeId, IClearable> s_vaults = new();

        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Init()
        {
            var vaults = s_vaults;

            foreach (var (_, disposable) in vaults)
            {
                disposable?.Clear();
            }
        }

        public static void Register<T>(ValueVault<T> vault)
            where T : struct
        {
            s_vaults.TryAdd(TypeId.Get<T>(), vault);
        }
    }
#endif

    public static partial class GlobalValueVault<TValue>
        where TValue : struct
    {
        private readonly static ValueVault<TValue> s_vault = new();

#if UNITY_EDITOR
        static GlobalValueVault()
        {
            GlobalValueVaultEditor.Register(s_vault);
        }
#endif

        #region    ID<T>
        #endregion =====

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(Identifier<T> id)
            => s_vault.Contains(id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRemove<T>(Identifier<T> id)
            => s_vault.TryRemove(id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGet<T>(Identifier<T> id, out TValue value)
            => s_vault.TryGet(id, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TrySet<T>(Identifier<T> id, TValue value)
            => s_vault.TrySet(id, value);

        #region    PRESET_ID
        #endregion =========

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(PresetId id)
            => s_vault.Contains(id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRemove(PresetId id)
            => s_vault.TryRemove(id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGet(PresetId id, out TValue value)
            => s_vault.TryGet(id, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TrySet(PresetId id, TValue value)
            => s_vault.TrySet(id, value);

        #region    ComplexId
        #endregion ===

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(ComplexId id)
            => s_vault.Contains(id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRemove(ComplexId id)
            => s_vault.TryRemove(id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGet(ComplexId id, out TValue value)
            => s_vault.TryGet(id, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TrySet(ComplexId id, TValue value)
            => s_vault.TrySet(id, value);
    }
}
