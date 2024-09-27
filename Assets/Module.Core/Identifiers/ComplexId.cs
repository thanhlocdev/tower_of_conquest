namespace Module.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [StructLayout(LayoutKind.Explicit)]
    public readonly partial struct ComplexId
        : IEquatable<ComplexId>
        , IComparable<ComplexId>
    {
        [FieldOffset(0)]
        private readonly ulong _value;

        [FieldOffset(0)]
        public readonly Identifier Id;

        [FieldOffset(4)]
        public readonly Identifier Kind;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ComplexId(ulong value) : this()
        {
            _value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ComplexId(Identifier kind, Identifier id) : this()
        {
            Id = id;
            Kind = kind;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out Identifier kind, out Identifier id)
        {
            kind = this.Kind;
            id = this.Id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ComplexId other)
            => _value == other._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is ComplexId other && _value == other._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => _value.GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(ComplexId other)
            => _value.CompareTo(other._value);

#if !UNITY_COLLECTIONS
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => $"({Kind}, {Id})";
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryFormat(
              Span<char> destination
            , out int charsWritten
            , ReadOnlySpan<char> format = default
            , IFormatProvider provider = null
        )
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static bool False(out int value)
            {
                value = 0;
                return false;
            }

            if (destination.Length < 1)
            {
                return False(out charsWritten);
            }

            var openQuoteChars = 0;
            destination[openQuoteChars++] = '(';
            destination = destination[openQuoteChars..];

            if (Kind.TryFormat(destination, out var kindChars, format, provider) == false)
            {
                return False(out charsWritten);
            }

            destination = destination[kindChars..];

            if (destination.Length < 2)
            {
                return False(out charsWritten);
            }

            var delimiterChars = 0;
            destination[delimiterChars++] = ',';
            destination[delimiterChars++] = ' ';
            destination = destination[delimiterChars..];

            if (Id.TryFormat(destination, out var idChars, format, provider) == false)
            {
                return False(out charsWritten);
            }

            destination = destination[idChars..];

            if (destination.Length < 1)
            {
                return False(out charsWritten);
            }

            var closeQuoteChars = 0;
            destination[closeQuoteChars++] = ')';

            charsWritten = openQuoteChars + kindChars + delimiterChars + idChars + closeQuoteChars;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator ulong(in ComplexId id)
            => id._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator ComplexId(in (Identifier kind, Identifier id) tuple)
            => new(tuple.kind, tuple.id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(in ComplexId left, in ComplexId right)
            => left._value == right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(in ComplexId left, in ComplexId right)
            => left._value != right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(in ComplexId left, in ComplexId right)
            => left._value > right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(in ComplexId left, in ComplexId right)
            => left._value >= right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(in ComplexId left, in ComplexId right)
            => left._value < right._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(in ComplexId left, in ComplexId right)
            => left._value <= right._value;

        [Serializable]
        public partial struct Serializable : ITryConvert<ComplexId>
            , IEquatable<Serializable>
            , IComparable<Serializable>
        {
            [SerializeField]
            private Identifier.Serializable _kind;

            [SerializeField]
            private Identifier.Serializable _id;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Serializable(Identifier.Serializable kind, Identifier.Serializable id)
            {
                _kind = kind;
                _id = id;
            }

            public bool TryConvert(out ComplexId result)
            {
                if (_kind.TryConvert(out var kind) && _id.TryConvert(out var id))
                {
                    result = new(kind, id);
                    return true;
                }

                result = default;
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly bool Equals(Serializable other)
                => _kind == other._kind && _id == other._id;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override readonly bool Equals(object obj)
                => obj is Serializable other && _kind == other._kind && _id == other._id;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override readonly int GetHashCode()
                => ((ComplexId)this).GetHashCode();

#if !UNITY_COLLECTIONS
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override string ToString()
                => $"({_kind}, {_id})";
#endif

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int CompareTo(Serializable other)
                => ((ComplexId)this).CompareTo((ComplexId)other);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator ComplexId(Serializable value)
                => new(value._kind, value._id);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Serializable(ComplexId value)
                => new(value.Kind, value.Id);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator ==(Serializable left, Serializable right)
                => left._kind == right._kind && left._id == right._id;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator !=(Serializable left, Serializable right)
                => left._kind != right._kind || left._id != right._id;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator >(in Serializable left, in Serializable right)
                => (ComplexId)left > (ComplexId)right;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator >=(in Serializable left, in Serializable right)
                => (ComplexId)left >= (ComplexId)right;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator <(in Serializable left, in Serializable right)
                => (ComplexId)left < (ComplexId)right;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator <=(in Serializable left, in Serializable right)
                => (ComplexId)left <= (ComplexId)right;
        }
    }
}

#if UNITY_COLLECTIONS

namespace Module.Core
{
    using System.Runtime.CompilerServices;
    using Unity.Collections;

    public readonly partial struct ComplexId
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ToFixedString().ToString();

        public FixedString32Bytes ToFixedString()
        {
            var fs = new FixedString32Bytes();
            fs.Append('(');
            fs.Append(Kind);
            fs.Append(',');
            fs.Append(' ');
            fs.Append(Id);
            fs.Append(')');
            return fs;
        }

        public partial struct Serializable
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override string ToString()
                => ToFixedString().ToString();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public FixedString32Bytes ToFixedString()
            {
                var fs = new FixedString32Bytes();
                fs.Append('(');
                fs.Append(_kind);
                fs.Append(',');
                fs.Append(' ');
                fs.Append(_id);
                fs.Append(')');
                return fs;
            }
        }
    }
}

#endif
