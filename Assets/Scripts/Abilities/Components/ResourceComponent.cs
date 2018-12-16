using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class ResourceComponent : AbilityComponent
    {
        public DerivedAttribute Resource;
        public int Value;

        public ResourceComponent()
        {
            ComponentType = AbilityComponentType.Resource;
            Resource = DerivedAttribute.None;
            Value = 0;
            Setup();
        }

        public ResourceComponent(DerivedAttribute resource, int value = 0)
        {
            ComponentType = AbilityComponentType.Resource;
            Resource = resource;
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

            if (Resource != DerivedAttribute.None)
            {
                s = Value + " " + Resource;
            }

            return s;
        }
    }
}