using System;
using System.Runtime.CompilerServices;
using Module.GameCommon.StatSystem.Interfaces;
using UnityEngine;

namespace Module.GameCommon.StatSystem
{
    public readonly partial struct StatModifierData : IStatModifierData
    {
        public readonly StatType StatType;
        public readonly StatModifier Modifier;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StatModifierData(StatType statType, StatModifier modifier) : this()
        {
            StatType = statType;
            Modifier = modifier;
        }

        [Serializable]
        public partial struct Serializable
        {
            [SerializeField]
            private StatType _statType;
            [SerializeField]
            private StatModifier.Serializable _modifier;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Serializable(StatType statType, StatModifier.Serializable modifier) : this()
            {
                _statType = statType;
                _modifier = modifier;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Serializable(StatModifierData value)
                => new(value.StatType, value.Modifier);


            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator StatModifierData(Serializable value)
                => new(value._statType, value._modifier);
        }
    }
}
