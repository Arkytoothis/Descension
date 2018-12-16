using UnityEngine;
using System.Collections.Generic;
using Descension.Characters;
using Descension.Core;

namespace Descension.Abilities
{
    [System.Serializable]
    public class Ability
    {
        public AbilityClass Class;
        public AbilityType Type;

        public string Name;
        public string Key;
        public string Description;

        public string SpriteKey;
        public string StatusSpriteKey;

        public int ExpCost;
        public Skill SkillUsed;
        public int SkillRequired;

        public List<AbilityComponent> Components;
        public List<AbilityEffect> Effects;

        public string BoostRune;
        public string PreRune;
        public string PostRune;

        public float cooldown;

        public Ability()
        {
            Class = AbilityClass.None;
            Type = AbilityType.None;

            Name = "";
            Key = "";
            Description = "empty";

            ExpCost = 0;
            SkillUsed = Skill.None;
            SkillRequired = 0;

            SpriteKey = "";
            StatusSpriteKey = "";

            Components = new List<AbilityComponent>();
            Effects = new List<AbilityEffect>();

            BoostRune = "Blank";
            PreRune = "Blank";
            PostRune = "Blank";
            cooldown = 0;
        }

        public Ability(string name, string key, string sprite_key, AbilityClass ability_class, AbilityType ability_type, float cooldown = 0f, int exp = 0, Skill skill = Skill.None, int required = 0)
        {
            Name = name;
            Key = key;
            Description = "empty";

            ExpCost = exp;
            SkillUsed = skill;
            SkillRequired = required;

            SpriteKey = sprite_key;
            StatusSpriteKey = "";

            Class = ability_class;
            Type = ability_type;

            Components = new List<AbilityComponent>();
            Effects = new List<AbilityEffect>();
            this.cooldown = cooldown;
        }

        public Ability(Ability ability)
        {
            Name = ability.Name;
            Key = ability.Key;
            Description = ability.Description;

            ExpCost = ability.ExpCost;
            SkillUsed = ability.SkillUsed;
            SkillRequired = ability.SkillRequired;

            SpriteKey = ability.SpriteKey;
            StatusSpriteKey = ability.StatusSpriteKey;

            Class = ability.Class;
            Type = ability.Type;

            Components = new List<AbilityComponent>();

            for (int i = 0; i < ability.Components.Count; i++)
            {
                Components.Add(ability.Components[i]);
            }

            Effects = new List<AbilityEffect>();

            for (int i = 0; i < ability.Effects.Count; i++)
            {
                Effects.Add(ability.Effects[i]);
            }

            BoostRune = ability.BoostRune;
            PreRune = ability.PreRune;
            PostRune = ability.PostRune;
            cooldown = ability.cooldown;
        }

        public string GetName()
        {
            string name = "";

            if (BoostRune != "")
            {
                name = BoostRune;
            }

            name += " " + Name;

            return name;
        }

        public override string ToString()
        {
            string s = "";

            s = Name;
            s += "\nClass " + Class;
            s += "\nType " + Type;

            for (int i = 0; i < Components.Count; i++)
            {
                s += "\n" + Components[i].GetTooltipString();
            }

            for (int i = 0; i < Effects.Count; i++)
            {
                s += "\n" + Effects[i].GetTooltipString();
            }

            return s;
        }
    }
}