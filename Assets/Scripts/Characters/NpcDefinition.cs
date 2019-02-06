using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using Descension.Abilities;
using Descension.Core;
using Descension.Name;

namespace Descension.Characters
{

    [System.Serializable]
    public class NPCDefinition
    {
        public FantasyName name;
        public Gender gender;
        public Species species;
        public BodySize size;

        public string key;
        public string model;
        public Faction faction;

        public List<int> baseStart;
        public List<GameValue> BasePerLevel;
        public List<GameValue> derivedPerLevel;

        public List<int> baseResistances;
        public List<GameValue> resistancePerLevel;

        public List<int> skillStart;
        public List<GameValue> skillPerLevel;

        public int minLevel;
        public int maxLevel;
        public int expPerLevel;

        public List<Ability> abilities;
        public CharacterInventory inventory;


        public NPCDefinition()
        {
            name = new FantasyName();
            gender = Gender.None;
            species = Species.None;
            size = BodySize.None;
            faction = Faction.None;
            key = "";
            model = "";
            minLevel = 0;
            maxLevel = 0;
            expPerLevel = 0;

            baseStart = new List<int>();
            BasePerLevel = new List<GameValue>();

            derivedPerLevel = new List<GameValue>();

            skillStart = new List<int>();
            skillPerLevel = new List<GameValue>();

            baseResistances = new List<int>();
            resistancePerLevel = new List<GameValue>();

            abilities = new List<Ability>();
            inventory = new CharacterInventory();
        }

        public NPCDefinition(FantasyName name, Species species, BodySize size, Gender gender, string key, string model, Faction faction,
            int minLevel, int maxLevel, int expPerLevel)
        {
            this.name = name;
            this.species = species;
            this.size = size;
            this.gender = gender;
            this.key = key;
            this.model = model;
            this.faction = faction;
            this.minLevel = minLevel;
            this.maxLevel = maxLevel;
            this.expPerLevel = expPerLevel;

            baseStart = new List<int>();
            BasePerLevel = new List<GameValue>();
            for (int i = 0; i < (int)BaseAttribute.Number; i++)
            {
                baseStart.Add(0);
                BasePerLevel.Add(new GameValue());
            }

            derivedPerLevel = new List<GameValue>();
            for (int i = 0; i < (int)DerivedAttribute.Number; i++)
            {
                derivedPerLevel.Add(new GameValue());
            }

            skillStart = new List<int>();
            skillPerLevel = new List<GameValue>();
            for (int i = 0; i < (int)Skill.Number; i++)
            {
                skillStart.Add(0);
                skillPerLevel.Add(new GameValue());
            }

            baseResistances = new List<int>();
            resistancePerLevel = new List<GameValue>();
            for (int i = 0; i < (int)DamageType.Number; i++)
            {
                baseResistances.Add(0);
                resistancePerLevel.Add(new GameValue());
            }

            abilities = new List<Ability>();
            inventory = new CharacterInventory();
        }
    }
}