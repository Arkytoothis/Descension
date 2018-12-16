using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class AreaComponent : AbilityComponent
    {
        public AreaType Type;
        public int Distance;
        public int Angle;

        public AreaComponent()
        {
            ComponentType = AbilityComponentType.Area;
            Type = AreaType.Beam;
            Distance = 0;
            Angle = 0;
            Setup();
        }

        public AreaComponent(AreaType type, int distance = 0, int angle = 0)
        {
            ComponentType = AbilityComponentType.Area;
            Type = type;
            Distance = distance;
            Angle = angle;
            Setup();
        }

        public override void Setup()
        {
            Widgets = new List<AbilityPartWidgetType>();
            Widgets.Add(AbilityPartWidgetType.Dropdown);
            Widgets.Add(AbilityPartWidgetType.Input);
            Widgets.Add(AbilityPartWidgetType.Input);
        }

        public override string GetTooltipString()
        {
            string s = "Area: ";

            if (Type == AreaType.Beam)
                s += "in a " + Distance + " units long beam";
            else if (Type == AreaType.Cone)
                s += "in a " + Distance + " tile long cone";
            else if (Type == AreaType.Sphere)
                s += "in a " + Distance + " tile sphere";
            else if (Type == AreaType.Single)
                s += "";

            return s;
        }
    }
}