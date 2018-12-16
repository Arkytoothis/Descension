using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class StunEffect : AbilityEffect
    {
        public GameValue Duration;

        public StunEffect()
        {
            EffectType = AbilityEffectType.Stun;
            Duration = new GameValue();
        }

        public StunEffect(GameValue duration)
        {
            EffectType = AbilityEffectType.Stun;
            Duration = new GameValue(duration);
        }

        public override string GetTooltipString()
        {
            string s = "";
            s += "Stunned for " + Duration.ToString() + " turns";

            return s;
        }
    }
}