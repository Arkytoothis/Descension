using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class RestoreEffectModifier : AbilityEffect
    {
        public GameValue Amount;
        public GameValue Duration;

        public RestoreEffectModifier()
        {
            Amount = new GameValue();
            Duration = new GameValue();
        }

        public RestoreEffectModifier(GameValue amount, GameValue duration)
        {
            Amount = new GameValue(amount);
            Duration = new GameValue(duration);
        }

        public override string GetTooltipString()
        {
            string s = "";

            return s;
        }
    }
}