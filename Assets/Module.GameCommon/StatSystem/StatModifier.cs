using System;
using System.Runtime.CompilerServices;
using Module.Core;
using UnityEngine;

namespace Module.GameCommon.StatSystem
{
    public enum StatModifierType
    {
        Additive = 100,
        PercentAdditive = 200,
        PercentMultiplicative = 300
    }

    public readonly partial struct StatModifier : IEquatable<StatModifier>
       
    {
        public readonly StatModifierType ModifierType;
        public readonly int Order;
        public readonly float Value;
        public readonly Identifier Id;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StatModifier(Identifier id) : this()
        {
            Id = id;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StatModifier(StatModifierType modifierType, float value) : this()
        {
            ModifierType = modifierType;
            Value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StatModifier(StatModifierType modifierType, float value, int order) : this()
        {
            ModifierType = modifierType;
            Order = order;
            Value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(StatModifier other)
            => other.Equals(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is StatModifier other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => this.GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator == (in StatModifier lhs, in StatModifier rhs)
            => lhs.Equals(rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator != (in StatModifier lhs,in StatModifier rhs)
            => !lhs.Equals(rhs);

        [Serializable]
        public partial struct Serializable : IEquatable<Serializable>
        {
            [SerializeField]
            private StatModifierType _modifierType;
            [SerializeField]
            private float _value;
            [SerializeField]
            private int _order;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Serializable(StatModifierType modifierType, float value) : this()
            {
                _modifierType = modifierType;
                _value = value;
            }

            public bool Equals(Serializable other)
                => other._modifierType == _modifierType && other._value == _value;

            public override bool Equals(object obj)
                => obj is Serializable other && other._modifierType == _modifierType && _value == other._value;

            public override readonly int GetHashCode()
                => ((StatModifier)this).GetHashCode();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Serializable(StatModifier value)
                => new(value.ModifierType, value.Value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator StatModifier(Serializable value)
                => new StatModifier(value._modifierType, value._value);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator ==(Serializable lhs, Serializable rhs) 
                => lhs._modifierType == rhs._modifierType && lhs._value == rhs._value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator !=(Serializable lhs, Serializable rhs)
                => lhs._modifierType != rhs._modifierType || lhs._value != rhs._value;

            
        }
    }
}
