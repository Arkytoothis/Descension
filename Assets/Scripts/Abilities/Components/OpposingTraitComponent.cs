using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Abilities
{
    [System.Serializable]
    public class OpposingTraitComponent : AbilityComponent
    {
        public string Key;

        public OpposingTraitComponent()
        {
            ComponentType = AbilityComponentType.Opposing_Trait;
            Key = "";
        }

        public OpposingTraitComponent(string key)
        {
            ComponentType = AbilityComponentType.Opposing_Trait;
            Key = key;
        }

        public override void Setup()
        {
        }

        public override string GetTooltipString()
        {
            string s = "";

            if (Key != "")
            {
                s = Key;
            }

            return s;
        }
    }
}