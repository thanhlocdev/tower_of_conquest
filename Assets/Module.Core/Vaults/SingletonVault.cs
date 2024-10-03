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
using System.Collections.Generic;

namespace Module.Core.Vaults
{
    public class SingletonVault<TBase> : IDisposable
    {
        private readonly Dictionary<TypeHash, TBase> _singletons = new();

        public bool Contains<T>()
            where T : TBase
            => _singletons.ContainsKey(TypeCache<T>.Hash);

        public bool Contains<T>(T instance)
            where T : TBase
            => _singletons.TryGetValue(TypeCache<T>.Hash, out var obj);

        public bool TryAdd<T>()
            where T : TBase, new()
        {
            if (_singletons.ContainsKey(TypeCache<T>.Hash))
            {
#if ENABLE_DEBUG_CHECKS
                UnityEngine.Debug.LogError($"An instance of {TypeCache<T>.Type.Name} has already been existing");
#endif

                return false;
            }

            _singletons.Add(TypeCache<T>.Hash, new T());
            return true;
        }

        public bool TryAdd<T>(T instance)
            where T : TBase
        {
            if (instance == null)
            {
#if ENABLE_DEBUG_CHECKS
                throw new ArgumentNullException(nameof(instance));
#else
                return false;
#endif
            }

            if (_singletons.ContainsKey(TypeCache<T>.Hash))
            {
#if ENABLE_DEBUG_CHECKS
                UnityEngine.Debug.LogError($"An instance of {TypeCache<T>.Type} has already been existing");
#endif

                return false;
            }

            _singletons.Add(TypeCache<T>.Hash, instance);
            return true;
        }

        public bool TryGetOrAdd<T>(out T instance)
            where T : TBase, new()
        {
            if (_singletons.TryGetValue(TypeCache<T>.Hash, out var obj))
            {
                if (obj is T inst)
                {
                    instance = inst;
                    return true;
                }
#if ENABLE_DEBUG_CHECKS
                else
                {
                    throw new InvalidCastException(
                        $"Cannot cast an instance of type {obj.GetType()} to {TypeCache<T>.Type}" +
                        $"even though it is registered for {TypeCache<T>.Type}"
                    );
                }
#endif
            }

            _singletons[TypeCache<T>.Hash] = instance = new();
            return true;
        }

        public bool TryGet<T>(out T instance)
            where T : TBase
        {
            if (_singletons.TryGetValue(TypeCache<T>.Hash, out var obj))
            {
                if (obj is T inst)
                {
                    instance = inst;
                    return true;
                }
#if ENABLE_DEBUG_CHECKS
                else
                {
                    throw new InvalidCastException(
                        $"Cannot cast an instance of type {obj.GetType()} to {TypeCache<T>.Type}"
                    );
                }
#endif
            }

            instance = default;
            return false;
        }

        public void Dispose()
        {
            var singletons = _singletons;

            foreach (var (_, obj) in singletons)
            {
                if (obj is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            singletons.Clear();
        }
    }
}
