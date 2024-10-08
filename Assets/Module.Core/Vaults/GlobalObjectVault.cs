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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Module.Core.Vaults
{
    public static partial class GlobalObjectVault
    {
        private static ObjectVault s_vault = new();

#if UNITY_EDITOR
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Init()
        {
            s_vault?.Dispose();
            s_vault = new();
        }
#endif

        #region    ID<T>
        #endregion =====

        [MethodImpl(MethodImplOptions.AggressiveInlining)]  
        public static bool Contains<T>(Identifier<T> identifier)
            => s_vault.Contains(identifier);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAdd<T>(Identifier<T> id, [NotNull] T obj)
            => s_vault.TryAdd(id, obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRemove<T>(Identifier<T> id, out T obj)
            => s_vault.TryRemove(id, out obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGet<T>(Identifier<T> id, out T obj)
            => s_vault.TryGet<T>(id, out obj);

        #region    PRESET_ID
        #endregion =========

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(PresetId id)
            => s_vault.Contains(id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAdd<T>(PresetId id, [NotNull] T obj)
            => s_vault.TryAdd(id, obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRemove<T>(PresetId id, out T obj)
            => s_vault.TryRemove(id, out obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGet<T>(PresetId id, out T obj)
            => s_vault.TryGet<T>(id, out obj);

        #region    ComplexId

        #endregion =========

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(ComplexId id)
            => s_vault.Contains(id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAdd<T>(ComplexId id, [NotNull] T obj)
            => s_vault.TryAdd<T>(id, obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRemove<T>(ComplexId id, out T obj)
            => s_vault.TryRemove<T>(id, out obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGet<T>(ComplexId id, out T obj)
            => s_vault.TryGet<T>(id, out obj);


    }
}
