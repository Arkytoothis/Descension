using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class DurationComponent : AbilityComponent
    {
        public DurationType Type;
        public TimeType TimeType;
        public int MinValue;
        public int MaxValue;

        public DurationComponent()
        {
            ComponentType = AbilityComponentType.Duration;
            Type = DurationType.None;
            TimeType = TimeType.None;
            MinValue = 0;
            MaxValue = 0;
            Setup();
        }

        public DurationComponent(DurationType duration, TimeType time_type = TimeType.None, int min_value = 0, int max_value = 0)
        {
            ComponentType = AbilityComponentType.Duration;
            Type = duration;
            TimeType = time_type;
            MinValue = min_value;
            MaxValue = max_value;
            Setup();
        }

        public override void Setup()
        {
            Widgets = new List<AbilityPartWidgetType>();
            Widgets.Add(AbilityPartWidgetType.Dropdown);
            Widgets.Add(AbilityPartWidgetType.Dropdown);
            Widgets.Add(AbilityPartWidgetType.Input);
            Widgets.Add(AbilityPartWidgetType.Input);
        }

        public override string GetTooltipString()
        {
            string s = "";

            if (Type == DurationType.Instant)
            {
                s = "Duration: Instant";
            }
            else if (Type == DurationType.Duration)
            {
                s = "Duration: " + MinValue;

                if (MaxValue > MinValue)
                    s += " " + MaxValue + " " + Type;

                if (MinValue > 1)
                    s += "s";
            }
            else if (Type == DurationType.Permanent)
            {
                s = "Duration: Permanent";
            }

            return s;
        }
    }
}