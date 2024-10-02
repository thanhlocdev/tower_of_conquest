using System.Runtime.CompilerServices;

namespace Module.Core
{
    public readonly record struct PresetId(Identifier Value)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => Value.ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator PresetId(Identifier value)
            => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator PresetId(Identifier.Serializable value)
            => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Identifier(PresetId value)
            => value.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Identifier.Serializable(PresetId value)
            => value.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator ComplexId(PresetId value)
            => TypeId.Get<PresetId>().ToComplexId(value);
    }
}
