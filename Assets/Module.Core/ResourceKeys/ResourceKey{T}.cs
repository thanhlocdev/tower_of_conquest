// https://github.com/laicasaane/tower_of_encosy/blob/main/Assets/Module.Core/ResourceKeys/ResourceKey%7BT%7D.cs

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

using Module.Core.TypeWrap;

namespace Module.Core
{
    [WrapRecord]
    public readonly partial record struct ResourceKey<T>(AssetKey<T> Value)
        where T : UnityEngine.Object
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator ResourceKey<T>(ResourceKey value)
            => new(value.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator ResourceKey(ResourceKey<T> value)
            => new(value.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator ResourceKey<T>(AssetKey.Serializable<T> value)
            => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator AssetKey.Serializable<T>(ResourceKey<T> value)
            => value.Value;
    }
}
