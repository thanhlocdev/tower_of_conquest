// https://github.com/laicasaane/tower_of_encosy/blob/main/Assets/Module.Core/ResourceKeys/ResourceKeyExtensions.cs

//MIT License
//
//Copyright (c) 2024 Laicasaane
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System.Runtime.CompilerServices;
using UnityEngine;

namespace Module.Core
{
    using UnityObject = UnityEngine.Object;

    public static partial class ResourceKeyExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Load<T>(this ResourceKey key) where T : UnityObject
            => ((ResourceKey<T>)key).Load();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> TryLoad<T>(this ResourceKey key) where T : UnityObject
            => ((ResourceKey<T>)key).TryLoad();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameObject Instantiate(
              this ResourceKey<GameObject> key
            , TransformOrScene parent = default
            , bool inWorldSpace = false
            , bool trimCloneSuffix = false
        )
        {
            return InstantiateInternal(key, parent, inWorldSpace, trimCloneSuffix)
                .ValueOrDefault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TComponent Instantiate<TComponent>(
              this ResourceKey<GameObject> key
            , TransformOrScene parent = default
            , bool inWorldSpace = false
            , bool trimCloneSuffix = false
        )
            where TComponent : Component
        {
            var result = InstantiateInternal(key, parent, inWorldSpace, trimCloneSuffix);
            return result.HasValue ? result.Value().GetComponent<TComponent>() : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<GameObject> TryInstantiate(
              this ResourceKey<GameObject> key
            , TransformOrScene parent = default
            , bool inWorldSpace = false
            , bool trimCloneSuffix = false
        )
        {
            return InstantiateInternal(key, parent, inWorldSpace, trimCloneSuffix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<TComponent> TryInstantiate<TComponent>(
              this ResourceKey<GameObject> key
            , TransformOrScene parent = default
            , bool inWorldSpace = false
            , bool trimCloneSuffix = false
        )
            where TComponent : Component
        {
            var result = InstantiateInternal(key, parent, inWorldSpace, trimCloneSuffix);

            if (result.HasValue)
            {
                if (result.Value().TryGetComponent<TComponent>(out var comp))
                {
                    return comp;
                }
            }

            return default;
        }
        private static Option<GameObject> InstantiateInternal(
              ResourceKey<GameObject> key
            , TransformOrScene parent
            , bool inWorldSpace
            , bool trimCloneSuffix
        )
        {
            if (key.IsValid == false) return default;

            var goOpt = key.TryLoad();

            if (goOpt.HasValue == false || goOpt.TryValue(out var prefab) == false)
            {
                return default;
            }

            var go = UnityObject.Instantiate(prefab, parent.Transform, inWorldSpace);

            if (go.IsValid())
            {
                if (parent.IsValid && parent.IsScene)
                {
                    go.MoveToScene(parent.Scene);
                }

                if (trimCloneSuffix)
                {
                    go.TrimCloneSuffix();
                }

                return go;
            }

            return default;
        }
    }
}
