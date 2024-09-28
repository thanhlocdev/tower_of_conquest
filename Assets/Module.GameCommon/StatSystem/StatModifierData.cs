using System;
using UnityEngine;

namespace Module.GameCommon.StatSystem
{
    public readonly partial struct StatModifierData
    {
        public readonly string Identifier;
        public readonly StatModifier Modifier;

        [Serializable]
        public partial struct Serializable
        {
            [SerializeField]
            private string _identifier;
            [SerializeField]
            private StatModifier.Serializable _modifier;
        }
    }
}
