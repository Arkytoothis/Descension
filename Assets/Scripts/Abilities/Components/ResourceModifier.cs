using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Abilities
{
    [System.Serializable]
    public class ResourceModifier : AbilityComponent
    {
        public int MinValue;
        public int MaxValue;

        public ResourceModifier()
        {
            MinValue = 0;
            MaxValue = 0;
            Setup();
        }

        public ResourceModifier(int min_value, int max_value = 0)
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