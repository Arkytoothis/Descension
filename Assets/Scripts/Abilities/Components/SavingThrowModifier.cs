using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Abilities
{
    [System.Serializable]
    public class SavingThrownModifier : AbilityComponent
    {
        public int Difficulty;

        public SavingThrownModifier()
        {
            Difficulty = 0;
            Setup();
        }

        public SavingThrownModifier(int difficulty)
        {
            Difficulty = difficulty;
            Setup();
        }

        public override void Setup()
        {
            Widgets = new List<AbilityPartWidgetType>();
            Widgets.Add(AbilityPartWidgetType.Input);
            Widgets.Add(AbilityPartWidgetType.Input);
        }

        public override string GetTooltipString()
        {
            string s = "";

            s = Difficulty.ToString();

            return s;
        }
    }
}