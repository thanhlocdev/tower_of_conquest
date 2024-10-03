#if !UNITASK && UNITY_6000_0_OR_NEWER

//https://github.com/laicasaane/tower_of_encosy/blob/main/Assets/Module.Core/ResourceKeys/ResourceKey%2BLoad_Awaitable.cs

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
using UnityEngine;

namespace Module.Core
{
    partial record struct ResourceKey<T> : ILoadAsync<T>, ITryLoadAsync<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly async Awaitable<T> LoadAsync(CancellationToken token = default)
        {
            var result = await TryLoadAsync(token);
            return result.ValueOrDefault();
        }

        public readonly async Awaitable<Option<T>> TryLoadAsync(CancellationToken token = default)
        {
            if (IsValid == false) return default;

            try
            {
                var handle = Resources.LoadAsync<T>(Value);

                if (handle == null)
                {
                    return default;
                }

                while (handle.isDone == false)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }

                    await Awaitable.NextFrameAsync(token);

                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }

                if (token.IsCancellationRequested)
                {
                    return default;
                }

                var obj = handle.asset;

                if (obj is T asset && asset)
                {
                    return asset;
                }
            }
            catch { }

            return default;
        }
    }
}

#endif
