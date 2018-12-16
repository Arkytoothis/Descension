using UnityEngine;
using System.Collections.Generic;
using System;

namespace Descension.Abilities
{
    public enum AbilityComponentType
    {
        Area, Cooldown, Duration, Opposing_Trait, Range, Resource, Saving_Throw, Spell_Level, Target, Trait_Type, Trigger,
        Number, None
    }

    public enum AbilityComponentTypeShort
    {
        Area, Cooldown, Duration, Opp_Trait, Range, Resource, Save, Level, Target, Trait, Trigger,
        Number, None
    }

    public enum AbilityPartWidgetType
    {
        Dropdown, Label, Input,
        Number, None
    }

    [System.Serializable]
    public abstract class AbilityComponent
    {
        public AbilityComponentType ComponentType;
        public List<AbilityPartWidgetType> Widgets;

        abstract public void Setup();
        abstract public string GetTooltipString();
    }
}