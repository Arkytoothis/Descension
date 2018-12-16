using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Abilities
{
    [System.Serializable]
    public class AbilityModifier
    {
        public string Name;
        public string Key;
        public List<AbilityComponent> Modifiers;

        public AbilityModifier()
        {
            Name = "";
            Key = "";
            Modifiers = new List<AbilityComponent>();
        }

        public AbilityModifier(string name, string key)
        {
            Name = name;
            Key = key;
            Modifiers = new List<AbilityComponent>();
        }

        public AbilityModifier(string name, string key, List<AbilityModifier> modifiers)
        {
            Name = name;
            Key = key;
            Modifiers = new List<AbilityComponent>();
        }

        public AbilityModifier(AbilityModifier modifier)
        {
            Name = modifier.Name;
            Key = modifier.Key;
            Modifiers = new List<AbilityComponent>();
        }
    }
}