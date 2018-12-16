using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class RangeComponent : AbilityComponent
    {
        public RangeType Type;
        public int Distance;

        public RangeComponent()
        {
            ComponentType = AbilityComponentType.Range;
            Type = RangeType.None;
            Distance = 0;
            Setup();
        }

        public RangeComponent(RangeType type, int distance = 0)
        {
            ComponentType = AbilityComponentType.Range;
            Type = type;
            Distance = distance;
            Setup();
        }

        public override void Setup()
        {
            Widgets = new List<AbilityPartWidgetType>();
            Widgets.Add(AbilityPartWidgetType.Dropdown);
            Widgets.Add(AbilityPartWidgetType.Input);
        }

        public override string GetTooltipString()
        {
            string s = "Range: ";

            if (Type == RangeType.Self)
                s += "from the caster";
            else if (Type == RangeType.Weapon)
                s += "hit by weapon";
            else if (Type == RangeType.Distance)
                s += "within " + Distance + " tiles";

            return s;
        }
    }
}