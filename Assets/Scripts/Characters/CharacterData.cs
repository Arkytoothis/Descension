using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;
using Descension.Name;

namespace Descension.Characters
{
    [System.Serializable]
    public abstract class CharacterData
    {
        [SerializeField] protected FantasyName name;
        [SerializeField] protected Gender gender;
        [SerializeField] protected Background background;
        [SerializeField] protected CharacterPersonality personality;
        [SerializeField] protected Species species;
        [SerializeField] protected BodySize size;

        [SerializeField] protected string description;
        [SerializeField] protected string raceKey;
        [SerializeField] protected string professionKey;
        [SerializeField] protected Faction faction;
        [SerializeField] protected string hair;
        [SerializeField] protected string beard;

        [SerializeField] protected AttributeManager attributes;
        [SerializeField] protected CharacterInventory inventory;

        [SerializeField] protected bool isDead = false;
        [SerializeField] protected bool isExhausted = false;
        [SerializeField] protected bool isDrained = false;
        [SerializeField] protected bool isBroken = false;

        [SerializeField] protected int initiativeIndex = 0;

        [SerializeField] protected int xCell = 0;
        [SerializeField] protected int yCell = 0;

        protected CharacterData()
        {
        }

        public FantasyName Name { get { return name; } }
        public Gender Gender { get { return gender; } }
        public Background Background { get { return background; } set { background = value; } }
        public CharacterPersonality Personality { get { return personality; } set { personality = value; } }
        public Faction Faction { get { return faction; } }
        public Species Species { get { return species; } }
        public BodySize Size { get { return size; } }
        public string RaceKey { get { return raceKey; } }
        public string ProfessionKey { get { return professionKey; } }
        public AttributeManager Attributes { get { return attributes; } }
        public string Hair { get { return hair; } }
        public string Beard { get { return beard; } }
        public string Description { get { return description; } set { description = value; } }
        public CharacterInventory Inventory { get { return inventory; } }
        public bool IsDead { get { return isDead; } }
        public bool IsExhausted { get { return isExhausted; } }
        public bool IsDrained { get { return isDrained; } }
        public bool IsBroken { get { return isBroken; } }

        public int InitiativeIndex { get => initiativeIndex; set => initiativeIndex = value; }
        public int XCell { get => xCell; set => xCell = value; }
        public int YCell { get => yCell; set => yCell = value; }

        public void SetIsDead(bool isDead)
        {
            this.isDead = isDead;
        }

        public void SetStart(AttributeType type, int attribute, int start, int min, int max)
        {
            attributes.SetStart((AttributeListType)type, attribute, start, min, max);
        }

        public Attribute GetBase(int attribute)
        {
            return attributes.GetAttribute(AttributeListType.Base, attribute);
        }

        public Attribute GetDerived(int attribute)
        {
            return attributes.GetAttribute(AttributeListType.Derived, attribute);
        }

        public Attribute GetSkill(int index)
        {
            return attributes.GetSkill(index);
        }

        public Attribute GetResistance(int attribute)
        {
            return attributes.GetAttribute(AttributeListType.Resistance, attribute);
        }

        public void CalculateStartAttributes(bool randomize)
        {
            AttributeDefinition definition = null;
            int start = 10;

            for (int i = 0; i < (int)BaseAttribute.Number; i++)
            {
                definition = Database.GetBaseAttribute(i);

                attributes.SetStart(AttributeListType.Base, i, start, definition.Minimum, start);
            }

            if (randomize == true)
            {
                List<int> rolls = new List<int>((int)BaseAttribute.Number);

                for (int i = 0; i < (int)BaseAttribute.Number; i++)
                {
                    rolls.Add(Random.Range(5, 21));
                }

                if (Database.Professions[professionKey].AttributePriorities.Count > 0)
                {
                    rolls.Sort();
                    rolls.Reverse();
                    int total = 0;

                    for (int i = 0; i < (int)BaseAttribute.Number; i++)
                    {
                        total = rolls[(int)Database.Professions[professionKey].AttributePriorities[i]];
                        total += Database.GetRace(raceKey).StartingAttributes[i].Number;

                        if (Database.Professions[professionKey].MinimumAttributes[i] > 0 &&
                            total < Database.Professions[professionKey].MinimumAttributes[i])
                            total = Database.Professions[professionKey].MinimumAttributes[i];

                        attributes.SetStart(AttributeListType.Base, i, total, 0, total);
                    }
                }
                else
                {
                    int total = 0;

                    for (int i = 0; i < (int)BaseAttribute.Number; i++)
                    {
                        total = rolls[i];
                        total += Database.GetRace(raceKey).StartingAttributes[i].Number;

                        if (Database.Professions[professionKey].MinimumAttributes[i] > 0 &&
                            total < Database.Professions[professionKey].MinimumAttributes[i])
                            total = Database.Professions[professionKey].MinimumAttributes[i];

                        attributes.SetStart(AttributeListType.Base, i, total, 0, total);
                    }
                }
            }

            for (int i = 0; i < (int)BaseAttribute.Number; i++)
            {
                int total = attributes.GetAttributeValue(AttributeListType.Base, AttributeComponentType.Start, i) + attributes.GetAttributeValue(AttributeListType.Base, AttributeComponentType.Modifier, i);
                attributes.SetStart(AttributeListType.Base, i, total, 0, total);
            }
        }

        public void CalculateDerivedAttributes()
        {
            AttributeDefinition definition = null;
            int start = 0;
            for (int i = 0; i < (int)DerivedAttribute.Number; i++)
            {
                definition = Database.GetDerivedAttribute(i);

                if (definition.Calculation.Attribute1 != null)
                {
                    if (definition.Calculation.Attribute1.Type == AttributeModifierType.Base_Attribute)
                    {
                        start = GetBase(definition.Calculation.Attribute1.Attribute).Current;
                    }
                    else if (definition.Calculation.Attribute1.Type == AttributeModifierType.Race)
                    {
                        start = Database.GetRace(raceKey).StartingAttributes[definition.Calculation.Attribute1.Attribute].Roll(false);
                    }
                    else if (definition.Calculation.Attribute1.Type == AttributeModifierType.Value)
                    {
                        start = definition.Calculation.Attribute1.Attribute;
                    }
                }

                if (definition.Calculation.Attribute2 != null)
                {
                    if (definition.Calculation.Attribute2.Type == AttributeModifierType.Base_Attribute)
                    {
                        if (definition.Calculation.Operator1 == AttributeCalculationOpperator.Add)
                        {
                            start += GetBase(definition.Calculation.Attribute2.Attribute).Current;
                        }
                        else if (definition.Calculation.Operator1 == AttributeCalculationOpperator.Subtract)
                        {
                            start -= GetBase(definition.Calculation.Attribute2.Attribute).Current;
                        }
                    }
                    else if (definition.Calculation.Attribute2.Type == AttributeModifierType.Value)
                    {
                        if (definition.Calculation.Operator1 == AttributeCalculationOpperator.Add)
                        {
                            start += definition.Calculation.Attribute2.Attribute;
                        }
                        else if (definition.Calculation.Operator1 == AttributeCalculationOpperator.Subtract)
                        {
                            start -= definition.Calculation.Attribute2.Attribute;
                        }
                    }
                }

                if (start < 0) start = 0;

                attributes.SetStart(AttributeListType.Derived, i, start, definition.Minimum, start);
            }
        }

        public virtual void CalculateStartSkills()
        {
            //for (int i = 0; i < (int)Skill.Number; i++)
            //{
            //    attributeManager.SetStart(AttributeListType.Skill, i, 0, 0, 100);
            //}

            //int result = 0;
            //for (int i = 0; i < Database.GetProfession(ProfessionKey).SkillProficiencies.Count; i++)
            //{
            //    int skill = (int)Database.GetProfession(ProfessionKey).SkillProficiencies[i].Skill;
            //    int value = Database.GetProfession(ProfessionKey).SkillProficiencies[i].Value;
            //    result = GameValue.Roll(new GameValue(1, 2), false) * value;
            //    attributeManager.SetStart(AttributeListType.Skill, i, result, 0, 100);
            //}

            //for (int i = 0; i < Database.GetRace(RaceKey).SkillProficiencies.Count; i++)
            //{
            //    int skill = (int)Database.GetRace(RaceKey).SkillProficiencies[i].Skill;
            //    result += Database.GetRace(RaceKey).SkillProficiencies[i].Value;
            //}

            //for (int i = 0; i < (int)Skill.Number; i++)
            //{
            //}
        }

        public void CalculateResistances()
        {
            for (int i = 0; i < Database.Races[raceKey].Resistances.Count; i++)
            {
                //int resistance = (int)Database.Races[RaceKey].Resistances[i].DamageType;
                int value = Database.Races[raceKey].Resistances[i].Value;
                //Resistances[resistance].SetStart(value, 0, 100);
                attributes.SetStart(AttributeListType.Resistance, i, value, 0, 100);
            }
        }
    }
}