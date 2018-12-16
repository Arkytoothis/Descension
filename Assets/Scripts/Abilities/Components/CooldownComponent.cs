using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class CooldownComponent : AbilityComponent
    {
        public TimeType Type;
        public int Value;

        public CooldownComponent()
        {
            ComponentType = AbilityComponentType.Cooldown;
            Type = TimeType.None;
            Value = 0;
            Setup();
        }

        public CooldownComponent(TimeType type, int value = 0)
        {
            ComponentType = AbilityComponentType.Cooldown;
            Type = type;
            Value = value;
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
            string s = "";

            if (Type == TimeType.None)
                s = "Cooldown: None";
            else
                s = "Cooldown : " + Value + " " + Type;

            if (Value > 1)
                s += "s";

            return s;
        }
    }
}