using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Abilities
{
    [System.Serializable]
    public class KnockbackEffect : AbilityEffect
    {
        public int MinDistance;
        public int MaxDistance;

        public KnockbackEffect()
        {
            EffectType = AbilityEffectType.Knockback;
            MinDistance = 0;
            MaxDistance = 0;
        }

        public KnockbackEffect(int min, int max)
        {
            EffectType = AbilityEffectType.Stun;
            MinDistance = min;
            MaxDistance = max;
        }

        public override string GetTooltipString()
        {
            string s = "";
            s += "Knocked back " + MinDistance + " to " + MaxDistance + " tiles";

            return s;
        }
    }
}