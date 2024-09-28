using Module.GameCommon.StatSystem;
using UnityEngine;

namespace Test.Module.GameCommon
{
    public class StatSystemBehaviourTest : MonoBehaviour
    {
        [SerializeField]
        private Stat _stat;

        private void Start()
        {
            _stat = new();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                var modifier = new StatModifier(StatModifierType.PercentAdditive, 200);
                _stat.AddModifier(modifier);
            }
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                var modifier = new StatModifier(StatModifierType.PercentAdditive, 100);
                _stat.RemoveModifier(modifier);
            }
        }
    }
}
