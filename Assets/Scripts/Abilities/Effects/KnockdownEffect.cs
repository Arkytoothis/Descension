using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class KnockdownEffect : AbilityEffect
    {
        public GameValue Duration;

        public KnockdownEffect()
        {
            EffectType = AbilityEffectType.Knockdown;
            Duration = new GameValue();
        }

        public KnockdownEffect(GameValue duration)
        {
            EffectType = AbilityEffectType.Knockdown;
            Duration = new GameValue(duration);
        }

        public override string GetTooltipString()
        {
            string s = "";
            s += "Knocked prone for " + Duration.ToString() + " turns";

            return s;
        }
    }
}