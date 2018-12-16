using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class TargetComponent : AbilityComponent
    {
        public TargetType Type;
        public int MaxTargets;

        public TargetComponent()
        {
            ComponentType = AbilityComponentType.Target;
            Type = TargetType.None;
            MaxTargets = 0;
            Setup();
        }

        public TargetComponent(TargetType type, int max_targets = 1)
        {
            ComponentType = AbilityComponentType.Target;
            Type = type;
            MaxTargets = max_targets;
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
            string s = "Target: ";

            if (Type == TargetType.Enemy || Type == TargetType.Friend)
            {
                s += MaxTargets + " " + Type.ToString();

                if (MaxTargets > 1)
                    s += "s";
            }
            else if (Type == TargetType.Any)
            {
                s += "Any";
            }
            return s;
        }
    }
}