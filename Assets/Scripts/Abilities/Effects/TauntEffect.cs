using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class TauntEffect : AbilityEffect
    {
        public GameValue Duration;
        public float DamageModifier;

        public TauntEffect()
        {
            EffectType = AbilityEffectType.Taunt;
            Duration = GameValue.Zero;
            DamageModifier = 0;
        }

        public TauntEffect(GameValue duration, float modifier)
        {
            EffectType = AbilityEffectType.Taunt;
            Duration = new GameValue(duration);
            DamageModifier = modifier;
        }

        public override string GetTooltipString()
        {
            string s = "";

            s += "Taunted for " + Duration.ToString() + "turns,  -" + DamageModifier + "% damage";

            return s;
        }
    }
}