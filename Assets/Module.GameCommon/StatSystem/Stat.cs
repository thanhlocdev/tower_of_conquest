using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Module.GameCommon.StatSystem
{
    [System.Serializable]
    public class Stat : IStat
    {
        protected bool isDirty = true;
        protected float value;
        protected float alterablValue;

        [SerializeField]
        private List<StatModifier> _modifiers = new List<StatModifier>();


        public float Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return value;
            }
        }

        public float AlterableBaseValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => alterablValue;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => alterablValue = value;
        }

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
        }

        public void RemoveModifier(StatModifier modifier)
        {
            _modifiers.Remove(modifier);
        }

        protected virtual float CalculateValueInternal()
        {
            return value;
        }
    }
}
