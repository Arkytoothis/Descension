using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Characters;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class DamageEffectModifier : AbilityEffect
    {
        public DamageType DamageType;
        public int MinDuration;
        public int MaxDuration;
        public int MinValue;
        public int MaxValue;

        public DamageEffectModifier()
        {
            DamageType = DamageType.None;
            DamageType = 0;
            MinDuration = 0;
            MaxDuration = 0;
            MinValue = 0;
            MaxValue = 0;
        }

        public DamageEffectModifier(DamageType damage_type, int min_value, int max_value, int min_duration, int max_duration)
        {
            DamageType = damage_type;
            MinDuration = min_duration;
            MaxDuration = max_duration;
            MinValue = min_value;
            MaxValue = max_value;
        }

        public override string GetTooltipString()
        {
            string s = "";

            return s;
        }
    }
}