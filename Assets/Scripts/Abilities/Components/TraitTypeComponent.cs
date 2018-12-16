using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class TraitTypeComponent : AbilityComponent
    {
        public TraitType Type;

        public TraitTypeComponent()
        {
            ComponentType = AbilityComponentType.Trait_Type;
            Type = TraitType.None;
            Setup();
        }

        public TraitTypeComponent(TraitType type)
        {
            ComponentType = AbilityComponentType.Trait_Type;
            Type = type;
            Setup();
        }

        public override void Setup()
        {
            Widgets = new List<AbilityPartWidgetType>();
            Widgets.Add(AbilityPartWidgetType.Dropdown);
        }

        public override string GetTooltipString()
        {
            string s = "";

            s = "Trait Type" + Type.ToString();

            return s;
        }
    }
}