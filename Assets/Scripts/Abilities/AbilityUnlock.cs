using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Abilities
{
    public enum AbilityUnlockType
    {
        Race, Profession, Skill, Item,
        Number, None
    }

    [System.Serializable]
    public class AbilityUnlock
    {
        public AbilityType Type;
        public string Ability;
        public int Level;

        public AbilityUnlock()
        {
            Type = AbilityType.None;
            Ability = "";
            Level = 0;
        }

        public AbilityUnlock(AbilityType type, string ability, int level)
        {
            Type = type;
            Ability = ability;
            Level = level;
        }

        public AbilityUnlock(AbilityUnlock unlock)
        {
            Type = unlock.Type;
            Ability = unlock.Ability;
            Level = unlock.Level;
        }
    }
}