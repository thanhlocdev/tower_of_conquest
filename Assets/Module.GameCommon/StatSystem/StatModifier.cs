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

    [System.Serializable]
    public readonly partial struct StatModifier
    {
        public readonly StatModifierType ModifierType;
        public readonly int Order;
        public readonly float Value;
        public readonly Identifier Source;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StatModifier(StatModifierType modifierType, float value) : this()
        {
            ModifierType = modifierType;
            Value = value;
        }


        [Serializable]
        public partial struct Serializable
        {
            [SerializeField]
            private StatModifierType _modifierType;
            [SerializeField]
            private float _value;
            [SerializeField]
            private int _order;
        }
    }
}
