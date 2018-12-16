using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Abilities;
using Descension.Core;

namespace Descension.Equipment
{
    public enum CooldownType { Year, Month, Week, Day, Hour, Minute, Turn, Number, None };

    [System.Serializable]
    public class UsableData
    {
        public CooldownComponent Cooldown;

        public List<AbilityEffect> Effects;

        public UsableData()
        {
            Cooldown = new CooldownComponent(TimeType.None, 0);
            Effects = new List<AbilityEffect>();
        }

        public UsableData(TimeType time, int cooldown, List<AbilityEffect> effects)
        {
            Cooldown = new CooldownComponent(time, cooldown);
            Effects = new List<AbilityEffect>();
            for (int i = 0; i < effects.Count; i++)
            {
                Effects.Add(effects[i]);
            }
        }

        public UsableData(UsableData data)
        {
            Cooldown = new CooldownComponent(data.Cooldown.Type, data.Cooldown.Value);
            Effects = new List<AbilityEffect>();
            for (int i = 0; i < data.Effects.Count; i++)
            {
                Effects.Add(data.Effects[i]);
            }
        }
    }
}