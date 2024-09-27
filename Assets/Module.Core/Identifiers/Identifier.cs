namespace Module.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Serialization;


    public readonly partial struct Identifier
        : IEquatable<Identifier>
        , IComparable<Identifier>
    {
        private readonly uint _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Identifier(int value)
        {
            _value = new Union(value).uintValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Identifier(uint value)
        {
            _value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(Identifier other)
            => _value.CompareTo(other._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Identifier other)
            => _value == other._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Identifier other && _value == other._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => _value.GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => _value.ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryFormat(
              Span<char> destination
            , out int charsWritten
            , ReadOnlySpan<char> format = default
            , IFormatProvider provider = null
        )
            => _value.TryFormat(destination, out charsWritten, format, provider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Identifier(int value)
            => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Identifier(uint value)
            => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator uint(Identifier value)
            => value._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Identifier left, Identifier right)
            => left._value == right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Identifier left, Identifier right)
            => left._value != right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(Identifier left, Identifier right)
            => left._value < right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(Identifier left, Identifier right)
            => left._value > right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(Identifier left, Identifier right)
            => left._value <= right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(Identifier left, Identifier right)
            => left._value >= right._value;

        [Serializable]
        public partial struct Serializable : ITryConvert<Identifier>
            , IEquatable<Serializable>
            , IComparable<Serializable>
        {
            [SerializeField, FormerlySerializedAs("_id")]
            private uint _value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Serializable(int value)
            {
                _value = new Union(value).uintValue;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Serializable(uint value)
            {
                _value = value;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly bool TryConvert(out Identifier result)
            {
                result = new(_value);
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly bool Equals(Serializable other)
                => _value == other._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override readonly bool Equals(object obj)
                => obj is Serializable other && _value == other._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override readonly int GetHashCode()
                => _value.GetHashCode();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override string ToString()
                => _value.ToString();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int CompareTo(Serializable other)
                => _value.CompareTo(other._value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Serializable(int value)
                => new(value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Serializable(uint value)
                => new(value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Identifier(Serializable value)
                => new(value._value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Serializable(Identifier value)
                => new(value._value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator uint(Serializable value)
                => value._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator ==(Serializable left, Serializable right)
                => left._value == right._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator !=(Serializable left, Serializable right)
                => left._value != right._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator <(Serializable left, Serializable right)
                => left._value < right._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator <=(Serializable left, Serializable right)
                => left._value <= right._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator >(Serializable left, Serializable right)
                => left._value > right._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator >=(Serializable left, Serializable right)
                => left._value >= right._value;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct Union
        {
            [FieldOffset(0)]
            public int intValue;

            [FieldOffset(0)]
            public uint uintValue;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Union(int value) : this()
            {
                intValue = value;
            }
        }
    }
}

#if UNITY_COLLECTIONS

namespace Module.Core
{
    using System.Runtime.CompilerServices;
    using Newtonsoft.Json.Linq;
    using Unity.Collections;

    public readonly partial struct Identifier
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FixedString32Bytes ToFixedString()
        {
            var fs = new FixedString32Bytes();
            fs.Append(_value);
            return fs;
        }

        public partial struct Serializable
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public FixedString32Bytes ToFixedString()
            {
                var fs = new FixedString32Bytes();
                fs.Append(_value);
                return fs;
            }
        }
    }
}

#endif
