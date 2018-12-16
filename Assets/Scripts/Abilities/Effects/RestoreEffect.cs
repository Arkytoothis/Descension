using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    public enum RestoreType
    {
        Armor, Barrier, Actions, Health, Energy, Mana, Morale,
        Number, None
    }

    [System.Serializable]
    public class RestoreEffect : AbilityEffect
    {
        public RestoreType RestoreType;
        public GameValue Amount;
        public GameValue Duration;
        public int LevelModifier;
        public bool OverCharge;

        public RestoreEffect()
        {
            EffectType = AbilityEffectType.Restore;
            RestoreType = RestoreType.None;
            Amount = new GameValue();
            Duration = new GameValue();
            LevelModifier = 0;
            OverCharge = false;
        }

        public RestoreEffect(RestoreType type, GameValue amount, GameValue duration, bool overcharge, int level = 0)
        {
            EffectType = AbilityEffectType.Restore;
            RestoreType = type;
            Amount = new GameValue(amount);
            Duration = new GameValue(duration);
            LevelModifier = level;
            OverCharge = overcharge;
        }

        public override string GetTooltipString()
        {
            string s = "";

            s += "Restores " + Amount.ToString();
            s += " " + RestoreType.ToString();

            if (OverCharge == true) s += "(overcharge)";

            if (LevelModifier != 0)
            {
                s += " per " + LevelModifier + " level";
                if (LevelModifier > 1)
                    s += "s";
            }

            if (Duration.Number != 0)
            {
                s += " for " + Duration.ToString() + " turns";
            }

            return s;
        }
    }
}