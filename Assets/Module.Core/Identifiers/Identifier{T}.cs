namespace Module.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Serialization;

    public readonly partial struct Identifier<T>
        : IEquatable<Identifier<T>>
        , IComparable<Identifier<T>>
    {
        private readonly uint _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Identifier(int value)
        {
            _value = new Identifier.Union(value).uintValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Identifier(uint value)
        {
            _value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(Identifier<T> other)
            => _value.CompareTo(other._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Identifier<T> other)
            => _value == other._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Identifier<T> other && _value == other._value;

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
        public static implicit operator Identifier<T>(int value)
            => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Identifier<T>(uint value)
            => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Identifier<T>(Identifier value)
            => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Identifier(Identifier<T> value)
            => value._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator uint(Identifier<T> value)
            => value._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Identifier<T> left, Identifier<T> right)
            => left._value == right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Identifier<T> left, Identifier<T> right)
            => left._value != right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(Identifier<T> left, Identifier<T> right)
            => left._value < right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(Identifier<T> left, Identifier<T> right)
            => left._value > right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(Identifier<T> left, Identifier<T> right)
            => left._value <= right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(Identifier<T> left, Identifier<T> right)
            => left._value >= right._value;
    }

    partial struct Identifier
    {
        [Serializable]
        public partial struct Serializable<T> : ITryConvert<Identifier<T>>
            , IEquatable<Serializable<T>>
            , IComparable<Serializable<T>>
        {
            [SerializeField, FormerlySerializedAs("_id")]
            private uint _value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Serializable(int value)
            {
                _value = new Identifier.Union(value).uintValue;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Serializable(uint value)
            {
                _value = value;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly bool TryConvert(out Identifier<T> result)
            {
                result = new(_value);
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly bool Equals(Serializable<T> other)
                => _value == other._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override readonly bool Equals(object obj)
                => obj is Serializable<T> other && _value == other._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override readonly int GetHashCode()
                => _value.GetHashCode();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override string ToString()
                => _value.ToString();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int CompareTo(Serializable<T> other)
                => _value.CompareTo(other._value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Serializable<T>(int value)
                => new(value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Serializable<T>(uint value)
                => new(value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Identifier<T>(Serializable<T> value)
                => new(value._value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Identifier(Serializable<T> value)
                => new(value._value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Serializable<T>(Serializable value)
                => new((uint)value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Serializable(Serializable<T> value)
                => new(value._value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Serializable<T>(Identifier<T> value)
                => new((uint)value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator uint(Serializable<T> value)
                => value._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator ==(Serializable<T> left, Serializable<T> right)
                => left._value == right._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator !=(Serializable<T> left, Serializable<T> right)
                => left._value != right._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator <(Serializable<T> left, Serializable<T> right)
                => left._value < right._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator <=(Serializable<T> left, Serializable<T> right)
                => left._value <= right._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator >(Serializable<T> left, Serializable<T> right)
                => left._value > right._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator >=(Serializable<T> left, Serializable<T> right)
                => left._value >= right._value;
        }
    }
}

#if UNITY_COLLECTIONS

namespace Module.Core
{
    using System.Runtime.CompilerServices;
    using Newtonsoft.Json.Linq;
    using Unity.Collections;

    public readonly partial struct Identifier<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FixedString32Bytes ToFixedString()
        {
            var fs = new FixedString32Bytes();
            fs.Append(_value);
            return fs;
        }
    }

    partial struct Identifier
    {
        public partial struct Serializable<T>
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