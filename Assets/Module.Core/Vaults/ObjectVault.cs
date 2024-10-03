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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Module.Core.Logging;
using UnityEngine;

namespace Module.Core.Vaults
{
    using UnityObject = UnityEngine.Object;

    public sealed partial class ObjectVault : IDisposable
    {
        private readonly Dictionary<ComplexId, object> _map = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            _map.Clear();
        }

        #region    ID<T>
        #endregion =====

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains<T>(Identifier<T> id)
            => Contains(ToComplexId(id));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryAdd<T>(Identifier<T> id, [NotNull] T obj)
            => TryAdd(ToComplexId(id), obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryRemove<T>(Identifier<T> id, out T obj)
            => TryRemove(ToComplexId(id), out obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGet<T>(Identifier<T> id, out T obj)
            => TryGet<T>(ToComplexId(id), out obj);

        #region    PRESET_ID
        #endregion =========

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(PresetId id)
            => Contains((ComplexId)id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryAdd<T>(PresetId id, [NotNull] T obj)
            => TryAdd((ComplexId)id, obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryRemove<T>(PresetId id, out T obj)
            => TryRemove((ComplexId)id, out obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGet<T>(PresetId id, out T obj)
            => TryGet<T>((ComplexId)id, out obj);

        #region    ID2
        #endregion ===

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(ComplexId id)
            => _map.ContainsKey(id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryAdd<T>(ComplexId id, [NotNull] T obj)
        {
            ThrowIfNotReferenceType<T>();
            ThrowIfUnityObjectIsDestroyed(obj);

            var map = _map;

            if (map.ContainsKey(id))
            {
                return false;
            }

            map[id] = obj;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryRemove<T>(ComplexId id, out T obj)
        {
            if (TryGet(id, out obj))
            {
                _map.Remove(id);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGet<T>(ComplexId id, out T obj)
        {
            ThrowIfNotReferenceType<T>();

            if (_map.TryGetValue(id, out var weakRef))
            {
                var result = TryCast<T>(id, weakRef);
                obj = result.ValueOrDefault();
                return result.HasValue;
            }

            obj = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ComplexId ToComplexId<T>(Identifier<T> id)
            => TypeId.Get<T>().ToComplexId(id);

        private static Option<T> TryCast<T>(ComplexId id, object obj, UnityObject context = null)
        {
            if (obj == null)
            {
                ErrorIfRegisteredObjectIsNull(id, context);
                return default;
            }

            if (obj is not UnityObject unityObj)
            {
                if (obj is T objT)
                {
                    return new(objT);
                }

                goto FAILED;
            }

            if (unityObj && obj is T unityObjT)
            {
                return new(unityObjT);
            }

            if (unityObj == false)
            {
                ErrorIfRegisteredObjectIsNull(id, context);
            }

        FAILED:
            ErrorIfTypeMismatch<T>(id, obj, context);
            return default;
        }

        [HideInCallstack, DoesNotReturn, Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        private static void ThrowIfNotReferenceType<T>()
        {
            if (TypeCache<T>.IsUnmanaged || TypeCache<T>.IsValueType)
            {
                throw new InvalidCastException(
                    $"\"{TypeCache<T>.Type}\" is not a reference type"
                );
            }
        }

        [HideInCallstack, DoesNotReturn, Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        private static void ThrowIfUnityObjectIsDestroyed<T>(T obj)
        {
            if (obj is UnityObject unityObj && unityObj == false)
            {
                throw new MissingReferenceException($"Unity Object of type {typeof(T)} is either missing or destroyed.");
            }
        }

        [HideInCallstack, DoesNotReturn, Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        private static void ErrorIfTypeMismatch<T>(ComplexId id, object obj, UnityObject context)
        {
            var message = "Id \"{0}\" is mapped to an object of type \"{1}\". " +
                  "However an object of type \"{2}\" is being requested from it. " +
                  "It might be a bug at the time of registering.";

            if (context)
            {
                DevLoggerAPI.LogErrorFormat(context, message, id, obj?.GetType(), typeof(T));
            }
            else
            {
                DevLoggerAPI.LogErrorFormat(message, id, obj?.GetType(), typeof(T));
            }
        }

        [HideInCallstack, DoesNotReturn, Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        private static void ErrorIfRegisteredObjectIsNull(ComplexId id, UnityObject context)
        {
            var message = "The object registered with id \"{0}\" is null.";

            if (context)
            {
                DevLoggerAPI.LogErrorFormat(context, message, id);
            }
            else
            {
                DevLoggerAPI.LogErrorFormat(message, id);
            }
        }
    }
}
