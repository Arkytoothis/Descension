using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Characters;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class SavingThrowComponent : AbilityComponent
    {
        public BaseAttribute SaveType;
        public int Difficulty;

        public SavingThrowComponent()
        {
            ComponentType = AbilityComponentType.Saving_Throw;
            SaveType = BaseAttribute.None;
            Difficulty = 0;
            Setup();
        }

        public SavingThrowComponent(BaseAttribute save_type, int difficulty)
        {
            ComponentType = AbilityComponentType.Saving_Throw;
            SaveType = save_type;
            Difficulty = difficulty;
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

            s = SaveType.ToString() + " dif " + Difficulty;

            return s;
        }
    }
}