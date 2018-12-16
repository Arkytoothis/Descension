using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Abilities
{
    [System.Serializable]
    public class AlterCharacteristicEffectModifier : AbilityEffect
    {
        public int MinDuration;
        public int MaxDuration;
        public int MinValue;
        public int MaxValue;

        public AlterCharacteristicEffectModifier()
        {
            MinDuration = 0;
            MaxDuration = 0;
            MinValue = 0;
            MaxValue = 0;
        }

        public AlterCharacteristicEffectModifier(int min_value, int max_value, int min_duration, int max_duration)
        {
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