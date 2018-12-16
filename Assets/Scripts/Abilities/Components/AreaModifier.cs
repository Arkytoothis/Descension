using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Abilities
{
    [System.Serializable]
    public class AreaModifier : AbilityComponent
    {
        public int Distance;
        public int Angle;

        public AreaModifier()
        {
            Distance = 0;
            Angle = 0;
            Setup();
        }

        public AreaModifier(int distance, int angle)
        {
            Distance = distance;
            Angle = angle;
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