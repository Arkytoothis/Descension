using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class SpellLevelComponent : AbilityComponent
    {
        public SpellSchoolType School;
        public int Level;

        public SpellLevelComponent()
        {
            ComponentType = AbilityComponentType.Spell_Level;
            School = SpellSchoolType.None;
            Level = 0;
            Setup();
        }

        public SpellLevelComponent(SpellSchoolType school, int level)
        {
            ComponentType = AbilityComponentType.Spell_Level;
            School = school;
            Level = level;
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

            s += "\nLevel " + Level;
            s += "\nSchool " + School;

            return s;
        }
    }
}