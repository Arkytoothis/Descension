using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class BlindEffect : AbilityEffect
    {
        public GameValue Duration;

        public BlindEffect()
        {
            EffectType = AbilityEffectType.Blind;
            Duration = new GameValue();
        }

        public BlindEffect(GameValue duration)
        {
            EffectType = AbilityEffectType.Blind;
            Duration = new GameValue(duration);
        }

        public override string GetTooltipString()
        {
            string s = "";

            s += "Blinded for " + Duration.ToString();

            return s;
        }
    }
}