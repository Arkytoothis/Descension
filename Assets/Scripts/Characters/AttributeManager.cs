using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Characters
{
    public enum AttributeListType { Base, Derived, Resistance, Number, None };

    [System.Serializable]
    public class AttributeManager
    {
        private AttributeList[] lists;
        private Dictionary<Skill, Attribute> skills;

        public delegate void OnArmorChange(int current, int max);
        public delegate void OnHealthChange(int current, int max);
        public delegate void OnStaminaChange(int current, int max);
        public delegate void OnEssenceChange(int current, int max);
        public delegate void OnMoraleChange(int current, int max);

        public event OnArmorChange onArmorChange;
        public event OnHealthChange onHealthChange;
        public event OnStaminaChange onStaminaChange;
        public event OnEssenceChange onEssenceChange;
        public event OnMoraleChange onMoraleChange;

        public CharacterController controller;

        public AttributeManager()
        {
            lists = new AttributeList[(int)AttributeListType.Number];

            for (int i = 0; i < lists.Length; i++)
            {
                lists[i] = new AttributeList();
            }

            skills = new Dictionary<Skill, Attribute>();
        }

        public void ModifyAttribute(AttributeType type, int attribute, int value)
        {
            if (value == 0) return;

            ModifyAttributeInList(type, attribute, value);

            int cur = GetAttribute(AttributeListType.Derived, attribute).Current;
            int max = GetAttribute(AttributeListType.Derived, attribute).Maximum;

            if (attribute == (int)DerivedAttribute.Armor)
            {
                onArmorChange(cur, max);
            }
            else if (attribute == (int)DerivedAttribute.Health)
            {
                onHealthChange(cur, max);
            }
            else if (attribute == (int)DerivedAttribute.Stamina)
            {
                onStaminaChange(cur, max);
            }
            else if (attribute == (int)DerivedAttribute.Essence)
            {
                onEssenceChange(cur, max);
            }
            else if (attribute == (int)DerivedAttribute.Morale)
            {
                onMoraleChange(cur, max);
            }

            CheckVitals();
        }

        public void AddAttribute(AttributeListType listType, Attribute attribute)
        {
            lists[(int)listType].Attributes.Add(attribute);
        }

        public Attribute GetAttribute(AttributeListType listType, int attribute)
        {
            //Debug.Log(lists[(int)listType].Attributes.Count + " " + (DerivedAttribute)attribute);
            return lists[(int)listType].Attributes[attribute];
        }

        public int GetAttributeValue(AttributeListType listType, AttributeComponentType type, int attribute)
        {
            return lists[(int)listType].Attributes[attribute].Get(type);
        }

        public void SetStart(AttributeListType listType, int attribute, int value, int min, int max)
        {
            lists[(int)listType].Attributes[attribute].SetStart(value, min, max);
        }

        public void SetMaximum(AttributeListType listType, int attribute, int value)
        {
            lists[(int)listType].Attributes[attribute].SetMax(value, true);
        }

        public void AddSkill(Skill key, Attribute skill)
        {
            if (skills.ContainsKey(key) == false)
                skills.Add(key, skill);
            else
                skills[key].Current += skill.Current;
        }

        public Attribute GetSkill(Skill skill)
        {
            if (skills.ContainsKey(skill) == true)
            {
                return skills[skill];
            }
            else
                return null;
        }

        public Dictionary<Skill, Attribute> GetSkills()
        {
            return skills;
        }

        public int GetSkillValue(AttributeComponentType type, Skill skill)
        {
            if (skills.ContainsKey(skill) == true)
            {
                return skills[skill].Get(type);
            }
            else
                return 0;
        }

        private void ModifyAttributeInList(AttributeType type, int attribute, int value)
        {
            switch (type)
            {
                case AttributeType.Base:
                    lists[(int)AttributeListType.Base].Attributes[attribute].Modify(AttributeComponentType.Current, value);
                    break;
                case AttributeType.Derived:
                    lists[(int)AttributeListType.Derived].Attributes[attribute].Modify(AttributeComponentType.Current, value);
                    break;
                case AttributeType.Resistance:
                    lists[(int)AttributeListType.Resistance].Attributes[attribute].Modify(AttributeComponentType.Current, value);
                    break;
                case AttributeType.Skill:
                    //lists[(int)AttributeListType.ski].Attributes[attribute].Current += value;
                    break;
                default:
                    break;
            }
        }

        public void CheckVitals()
        {
            CheckHealth();
            CheckStamina();
            CheckEssence();
            CheckMorale();
        }

        public void CheckHealth()
        {
            //if (controller.CheckIsAlive() == true && GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Health).Current <= 0)
            //{
            //    controller.Death();
            //}
            //else if (controller.CheckIsAlive() == false && GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Health).Current > 0)
            //{
            //    controller.Revive();
            //}
        }

        public void CheckStamina()
        {
            //if (controller.CheckIsAlive() == true && GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Stamina).Current <= 0)
            //{
            //    //isExhausted = true;
            //    //Debug.Log(Name.FirstName + " is exhausted");
            //}
        }

        public void CheckEssence()
        {
            //if (controller.CheckIsAlive() == true && GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Essence).Current <= 0)
            //{
            //    //isDrained = true;
            //    //Debug.Log(Name.FirstName + " is out of essence");
            //}
        }

        public void CheckMorale()
        {
            //if (controller.CheckIsAlive() == true && GetAttribute(AttributeListType.Derived, (int)DerivedAttribute.Morale).Current <= 0)
            //{
            //    //isBroken = true;
            //    //Debug.Log(Name.FirstName + " is broken");
            //}
        }
    }
}