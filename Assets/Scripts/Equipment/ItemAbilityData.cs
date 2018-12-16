using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Abilities;

namespace Descension.Equipment
{
    [System.Serializable]
    public class ItemAbilityData
    {
        public int UnlockValue;
        public bool Unlocked;
        public List<AbilityComponent> Components;
        public List<AbilityComponent> Effects;

        public ItemAbilityData()
        {
            UnlockValue = 0;
            Unlocked = false;
            Components = new List<AbilityComponent>();
            Effects = new List<AbilityComponent>();
        }

        public ItemAbilityData(ItemAbilityData data)
        {
            UnlockValue = data.UnlockValue;
            Unlocked = data.Unlocked;

            Components = new List<AbilityComponent>();
            for (int i = 0; i < data.Components.Count; i++)
            {
                Components.Add(data.Components[i]);

            }

            Effects = new List<AbilityComponent>();
            for (int i = 0; i < data.Effects.Count; i++)
            {
                Effects.Add(data.Effects[i]);

            }
        }
    }
}