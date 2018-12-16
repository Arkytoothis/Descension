using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Abilities;
using Descension.Core;

namespace Descension.Characters
{
    [System.Serializable]
    public class SkillList
    {
        public List<SkillDefinition> Skills;

        public SkillList()
        {
            Skills = new List<SkillDefinition>();
        }
    }

    [System.Serializable]
    public class SkillDefinition
    {
        public SkillCategory Category;
        public Skill key;
        public string Name;
        public string ShortName;
        public string Abbreviated;
        public string Description;
        public string AttributeUsed;
        public int Minimum;
        public int Maximum;
        public List<AbilityUnlock> AbilityUnlocks;

        public SkillDefinition()
        {
            key = Skill.None;
            Name = "";
            ShortName = "";
            Abbreviated = "";
            Description = "";
            AttributeUsed = "";
            Minimum = 0;
            Maximum = 0;
            AbilityUnlocks = new List<AbilityUnlock>();
        }

        public SkillDefinition(SkillCategory category, Skill key, string name, string short_name, string abbreviated, string description, string attribute, int minimum, int maximum,
            List<AbilityUnlock> list)
        {
            this.key = key;
            Category = category;
            Name = name;
            ShortName = short_name;
            Abbreviated = abbreviated;
            Description = description;
            AttributeUsed = attribute;

            AbilityUnlocks = new List<AbilityUnlock>();
            for (int i = 0; i < list.Count; i++)
            {
                AbilityUnlocks.Add(new AbilityUnlock(list[i]));
            }

            Minimum = minimum;
            Maximum = maximum;
        }

        public SkillDefinition(SkillDefinition def)
        {
            key = def.key;
            Category = def.Category;
            Name = def.Name;
            ShortName = def.ShortName;
            Abbreviated = def.Abbreviated;
            Description = def.Description;
            AttributeUsed = def.AttributeUsed;

            AbilityUnlocks = new List<AbilityUnlock>();
            for (int i = 0; i < def.AbilityUnlocks.Count; i++)
            {
                AbilityUnlocks.Add(new AbilityUnlock(def.AbilityUnlocks[i]));
            }

            Minimum = def.Minimum;
            Maximum = def.Maximum;
        }
    }
}