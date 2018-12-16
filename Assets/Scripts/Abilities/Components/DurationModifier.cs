using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class DurationModifier : AbilityComponent
    {
        public int MinValue;
        public int MaxValue;

        public DurationModifier()
        {
            MinValue = 0;
            MaxValue = 0;
            Setup();
        }

        public DurationModifier(int min_value, int max_value = 0)
        {
            MinValue = min_value;
            MaxValue = max_value;
            Setup();
        }

        public override void Setup()
        {
            Widgets = new List<AbilityPartWidgetType>();
            Widgets.Add(AbilityPartWidgetType.Input);
            Widgets.Add(AbilityPartWidgetType.Input);
        }

        public override string GetTooltipString()
        {
            string s = "";

            return s;
        }
    }
}