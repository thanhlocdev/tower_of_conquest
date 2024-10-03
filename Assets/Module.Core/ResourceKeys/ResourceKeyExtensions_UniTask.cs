#if UNITASK

// https://github.com/laicasaane/tower_of_encosy/blob/main/Assets/Module.Core/ResourceKeys/ResourceKeyExtensions_UniTask.cs

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
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Module.Core
{
    using UnityObject = UnityEngine.Object;

    public static partial class ResourceKeyExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<T> LoadAsync<T>(this ResourceKey key, CancellationToken token = default) where T : UnityObject
            => ((ResourceKey<T>)key).LoadAsync(token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<Option<T>> TryLoadAsync<T>(this ResourceKey key, CancellationToken token = default) where T : UnityObject
            => ((ResourceKey<T>)key).TryLoadAsync(token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<GameObject> InstantiateAsync(
              this ResourceKey<GameObject> key
            , TransformOrScene parent = default
            , bool inWorldSpace = false
            , bool trimCloneSuffix = false
            , CancellationToken token = default
        )
        {
            var result = await InstantiateAsyncInternal(key, parent, inWorldSpace, trimCloneSuffix, token);
            return result.ValueOrDefault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<TComponent> InstantiateAsync<TComponent>(
              this ResourceKey<GameObject> key
            , TransformOrScene parent = default
            , bool inWorldSpace = false
            , bool trimCloneSuffix = false
            , CancellationToken token = default
        )
            where TComponent : Component
        {
            var result = await InstantiateAsyncInternal(key, parent, inWorldSpace, trimCloneSuffix, token);
            return result.HasValue ? result.Value().GetComponent<TComponent>() : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<Option<GameObject>> TryInstantiateAsync(
              this ResourceKey<GameObject> key
            , TransformOrScene parent = default
            , bool inWorldSpace = false
            , bool trimCloneSuffix = false
            , CancellationToken token = default
        )
        {
            return await InstantiateAsyncInternal(key, parent, inWorldSpace, trimCloneSuffix, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<Option<TComponent>> TryInstantiateAsync<TComponent>(
              this ResourceKey<GameObject> key
            , TransformOrScene parent = default
            , bool inWorldSpace = false
            , bool trimCloneSuffix = false
            , CancellationToken token = default
        )
            where TComponent : Component
        {
            var result = await InstantiateAsyncInternal(key, parent, inWorldSpace, trimCloneSuffix, token);

            if (result.HasValue)
            {
                if (result.Value().TryGetComponent<TComponent>(out var comp))
                {
                    return comp;
                }
            }

            return default;
        }

        private static async UniTask<Option<GameObject>> InstantiateAsyncInternal(
              ResourceKey<GameObject> key
            , TransformOrScene parent
            , bool inWorldSpace
            , bool trimCloneSuffix
            , CancellationToken token
        )
        {
            if (key.IsValid == false) return default;

            var goOpt = await key.TryLoadAsync(token);

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

#endif
