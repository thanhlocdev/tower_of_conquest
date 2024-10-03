// https://github.com/laicasaane/tower_of_encosy/blob/main/Assets/Module.Core/Vaults/GlobalMap%7BTKey%2CTValue%7D.cs

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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Module.Core.Vaults
{
#if UNITY_EDITOR
    internal static partial class GlobalMapEditor
    {
        private readonly static Dictionary<TypeId, IDictionary> s_maps = new();

        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Init()
        {
            var maps = s_maps;

            foreach (var (_, map) in maps)
            {
                map?.Clear();
            }
        }

        public static void Register<TScope>(IDictionary map)
        {
            s_maps.TryAdd(TypeId.Get<TScope>(), map);
        }
    }
#endif

    public static partial class GlobalMap<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        private static readonly Dictionary<TKey, TValue> s_map = new(4);

#if UNITY_EDITOR
        static GlobalMap()
        {
            GlobalMapEditor.Register<Scope>(s_map);
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRemove(TKey key)
            => s_map.Remove(key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(TKey key, TValue value)
            => s_map[key] = value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAdd(TKey key, TValue value)
            => s_map.TryAdd(key, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGet(TKey key, out TValue value)
            => s_map.TryGetValue(key, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(TKey key)
            => s_map.ContainsKey(key);

        private readonly struct Scope { }
    }
}
