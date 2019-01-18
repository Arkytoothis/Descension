using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Descension.Core;
using Descension.Name;
using Descension.Equipment;

namespace Descension.Characters
{
    public enum PcStatus
    {
        Idle, Adventuring, Resting, Camping, Working, Training,
        Number, None
    }

    [System.Serializable]
    public class PcDataList
    {
        public List<PcData> Pcs;

        public PcDataList()
        {
            Pcs = new List<PcData>();
        }
    }

    [System.Serializable]
    public class PcData : CharacterData
    {
        [SerializeField] UpkeepData upkeep;
        [SerializeField] int wealth;
        [SerializeField] PcStatus status;

        [SerializeField] int encounterIndex;
        [SerializeField] int worldIndex;
        [SerializeField] int partyIndex;
        [SerializeField] int partySlot;

        [SerializeField] int level;
        [SerializeField] int experience;
        [SerializeField] int expToLevel;
        [SerializeField] int maxExp;
        [SerializeField] float expBonus;
        [SerializeField] int maxAccessories;

        [SerializeField] CharacterAbilities abilities;

        public UpkeepData Upkeep { get { return upkeep; } }
        public int Wealth { get { return wealth; } }
        public PcStatus Status { get { return status; } }

        public int EncounterIndex { get { return encounterIndex; } }
        public int WorldIndex { get { return worldIndex; } }
        public int PartyIndex { get { return partyIndex; } }
        public int PartySlot { get { return partySlot; } }

        public int Level { get { return level; } }
        public int Experience { get { return experience; } }
        public int ExpToLevel { get { return expToLevel; } }
        public int MaxExp { get { return maxExp; } }
        public float ExpBonus { get { return expBonus; } }

        public int MaxAccessories { get { return maxAccessories; } }
        public CharacterAbilities Abilities { get { return abilities; } }

        public PcData()
        {
            wealth = 0;
            upkeep = new UpkeepData();
            name = new FantasyName();
            gender = Gender.None;
            background = null;
            status = PcStatus.Idle;
            faction = "Player";
            raceKey = "";
            professionKey = "";
            description = "";

            encounterIndex = -1;
            worldIndex = -1;
            partyIndex = -1;
            hair = "Hair 01";
            beard = "";
            maxAccessories = 1;

            level = 0;
            experience = 0;
            expToLevel = 0;
            maxExp = 0;
            expBonus = 0;

            attributes = new AttributeManager();

            for (int i = 0; i < (int)BaseAttribute.Number; i++)
                attributes.AddAttribute(AttributeListType.Base, new Attribute(AttributeType.Base, i, 1));

            for (int i = 0; i < (int)DerivedAttribute.Number; i++)
                attributes.AddAttribute(AttributeListType.Derived, new Attribute(AttributeType.Derived, i, 0));

            for (int i = 0; i < (int)DamageType.Number; i++)
                attributes.AddAttribute(AttributeListType.Resistance, new Attribute(AttributeType.Resistance, i, 0));

            abilities = new CharacterAbilities();
            inventory = new CharacterInventory();
        }

        public PcData(FantasyName name, Gender gender, int level, string raceKey, string professionKey, string hair, string beard, int worldIndex, int encounterIndex, int partyIndex,
            int power_slots, int spell_slots)
        {
            wealth = 0;
            upkeep = new UpkeepData();

            this.name = name;
            this.gender = gender;
            this.raceKey = raceKey;
            this.professionKey = professionKey;
            this.worldIndex = worldIndex;
            this.encounterIndex = encounterIndex;
            this.partyIndex = partyIndex;

            this.hair = hair;
            this.beard = beard;

            maxAccessories = Random.Range(1, 4);

            level = 0;
            experience = 0;
            expToLevel = 0;
            maxExp = 0;
            expBonus = 0.0f;

            attributes = new AttributeManager();

            for (int i = 0; i < (int)BaseAttribute.Number; i++)
            {
                attributes.AddAttribute(AttributeListType.Base, new Attribute(AttributeType.Base, i, 1));
            }

            for (int i = 0; i < (int)DerivedAttribute.Number; i++)
            {
                attributes.AddAttribute(AttributeListType.Derived, new Attribute(AttributeType.Derived, i, 0));
            }

            for (int i = 0; i < (int)DamageType.Number; i++)
            {
                attributes.AddAttribute(AttributeListType.Resistance, new Attribute(AttributeType.Resistance, i, 0));
            }

            abilities = new CharacterAbilities(this, power_slots, spell_slots);
            inventory = new CharacterInventory();
            faction = "Player";
        }

        public PcData(PcData pc)
        {
            name = pc.Name;
            gender = pc.gender;

            wealth = pc.Wealth;
            upkeep = new UpkeepData(pc.Upkeep);
            background = new Background(pc.background);
            raceKey = pc.raceKey;
            professionKey = pc.professionKey;
            worldIndex = pc.WorldIndex;
            encounterIndex = pc.EncounterIndex;
            partyIndex = pc.PartyIndex;
            partySlot = pc.PartySlot;
            hair = pc.hair;
            beard= pc.beard;

            description = pc.description;

            level = pc.Level;
            experience = pc.Experience;
            expToLevel = pc.ExpToLevel;
            maxExp = pc.MaxExp;
            expBonus = pc.ExpBonus;

            attributes = new AttributeManager();

            for (int i = 0; i < (int)BaseAttribute.Number; i++)
            {
                attributes.AddAttribute(AttributeListType.Base, new Attribute(pc.attributes.GetAttribute(AttributeListType.Base, i)));
            }

            for (int i = 0; i < (int)DerivedAttribute.Number; i++)
            {
                attributes.AddAttribute(AttributeListType.Derived, new Attribute(pc.attributes.GetAttribute(AttributeListType.Derived, i)));
            }

            for (int i = 0; i < (int)DamageType.Number; i++)
            {
                attributes.AddAttribute(AttributeListType.Resistance, new Attribute(pc.attributes.GetAttribute(AttributeListType.Resistance, i)));
            }

            foreach (KeyValuePair<Skill, Attribute> kvp in pc.GetSkills())
            {
                attributes.AddSkill(kvp.Key, new Attribute(kvp.Value));
            }

            abilities = new CharacterAbilities(pc);
            inventory = new CharacterInventory(pc.inventory);
            faction = "Player";
        }

        public override void CalculateStartSkills()
        {
            for (int j = 0; j < Database.GetProfession(professionKey).SkillProficiencies.Count; j++)
            {
                int value = Database.GetProfession(professionKey).SkillProficiencies[j].Value;
                int result = GameValue.Roll(new GameValue(1, 2), false) * value;
                Skill key = Database.GetProfession(professionKey).SkillProficiencies[j].Skill;

                Attribute skill = new Attribute();
                skill.Type = AttributeType.Skill;
                skill.SetStart(result, 0, 100);
                attributes.AddSkill(key, skill);
            }

            for (int i = 0; i < Database.GetRace(raceKey).SkillProficiencies.Count; i++)
            {
                int value = Database.GetRace(raceKey).SkillProficiencies[i].Value;
                Skill key = Database.GetRace(raceKey).SkillProficiencies[i].Skill;

                Attribute skill = new Attribute();
                skill.Type = AttributeType.Skill;
                skill.SetStart(value, 0, 100);
                attributes.AddSkill(key, skill);
            }

            CalculateExpCosts();
        }           

        public bool CanEquip(ItemData item, EquipmentSlot slot)
        {
            bool canEquip = false;

            if (item.Slot != slot)
                canEquip = true;

            return canEquip;
        }

        public void CalculateAttributeModifiers()
        {
            for (int slot = 0; slot < (int)EquipmentSlot.Number; slot++)
            {
                if (inventory.EquippedItems[slot] != null)
                {
                    ItemData item = inventory.EquippedItems[slot];

                    if (item.WeaponData != null)
                    {
                        //if (item.WeaponData.AttackType == AttackType.Might)
                        //    DerivedAttributes[(int)DerivedAttribute.Might_Attack].AddToModifier(item.WeaponData.Attributes[(int)WeaponAttributes.Attack].Value);
                        //else if (item.WeaponData.AttackType == AttackType.Finesse)
                        //    DerivedAttributes[(int)DerivedAttribute.Finesse_Attack].AddToModifier(item.WeaponData.Attributes[(int)WeaponAttributes.Attack].Value);
                        //else if (item.WeaponData.AttackType == AttackType.Spell)
                        //    DerivedAttributes[(int)DerivedAttribute.Spell_Attack].AddToModifier(item.WeaponData.Attributes[(int)WeaponAttributes.Attack].Value);

                        //DerivedAttributes[(int)DerivedAttribute.Parry].AddToModifier(item.WeaponData.Attributes[(int)WeaponAttributes.Parry].Value);
                    }

                    if (item.WearableData != null)
                    {
                        //DerivedAttributes[(int)DerivedAttribute.Armor].AddToModifier(item.WearableData.Attributes[(int)WearableAttributes.Armor].Value);
                        //DerivedAttributes[(int)DerivedAttribute.Block].AddToModifier(item.WearableData.Attributes[(int)WearableAttributes.Block].Value);
                        //DerivedAttributes[(int)DerivedAttribute.Dodge].AddToModifier(item.WearableData.Attributes[(int)WearableAttributes.Dodge].Value);

                        //for (int r = 0; r < item.WearableData.Resistances.Count; r++)
                        //{
                        //    Resistances[(int)item.WearableData.Resistances[r].DamageType].AddToModifier(item.WearableData.Resistances[r].Value);
                        //}
                    }

                    //for (int m = 0; m < item.Modifiers.Count; m++)
                    //{
                    //    for (int e = 0; e < item.Modifiers[m].Effects.Count; e++)
                    //    {
                    //        if (item.Modifiers[m].Effects[e].GetType() == typeof(AlterCharacteristicEffect))
                    //        {
                    //            AlterCharacteristicEffect effect = (AlterCharacteristicEffect)item.Modifiers[m].Effects[e];

                    //            if (effect.Type == CharacteristicType.Base_Attribute)
                    //                BaseAttributes[effect.Characteristic].AddToModifier(effect.MaxValue);
                    //            else if (effect.Type == CharacteristicType.Derived_Attribute)
                    //                DerivedAttributes[effect.Characteristic].AddToModifier(effect.MaxValue);
                    //            else if (effect.Type == CharacteristicType.Skill)
                    //                Skills[effect.Characteristic].AddToModifier(effect.MaxValue);
                    //            else if (effect.Type == CharacteristicType.Resistance)
                    //                Resistances[effect.Characteristic].AddToModifier(effect.MaxValue);
                    //        }
                    //    }
                    //}
                }
            }
        }

        public void CalculateExp()
        {
            expToLevel = Level * 1000;
            maxExp = Level * 10000;
        }

        public void CalculateExpCosts()
        {
            //for (int i = 0; i < BaseAttributes.Count; i++)
            //{
            //    BaseAttributes[i].CalculateExpCost();
            //}

            //for (int i = 0; i < Skills.Count; i++)
            //{
            //    Skills[i].CalculateExpCost();
            //}
        }

        public void AddExperience(int amount, bool adjusted)
        {
            if (amount == 0) return;

            int expToAdd = 0;

            if (adjusted == true)
            {
                expToAdd = (int)((float)amount * Database.GetRace(raceKey).ExpModifier);
                expToAdd += (int)((float)amount * ExpBonus);
            }
            else
            {
                expToAdd = amount;
            }

            experience += expToAdd;

            if (experience >= expToLevel)
                onLevelUp();

            if(onExperienceChange != null)
                onExperienceChange(experience, expToLevel);
        }

        public void SpendExperience(int amount)
        {
            experience -= amount;
        }

        public void CalculateUpkeep()
        {
            upkeep = new UpkeepData();

            Race race = Database.GetRace(raceKey);
            Upkeep.Coin = race.Upkeep.Coin;
            Upkeep.Essence = race.Upkeep.Essence;
            Upkeep.Materials = race.Upkeep.Materials;
            Upkeep.Rations = race.Upkeep.Rations;

            Profession profession = Database.GetProfession(professionKey);
            Upkeep.Coin += profession.Upkeep.Coin;
            Upkeep.Essence += profession.Upkeep.Essence;
            Upkeep.Materials += profession.Upkeep.Materials;
            Upkeep.Rations += profession.Upkeep.Rations;

            wealth = Database.Races[raceKey].StartingWealth.Roll(false) + Database.Professions[professionKey].StartingWealth.Roll(false);
        }

        public void LevelUp()
        {
            SpendExperience(expToLevel);
            level++;
            CalculateExp();
            CalculateExpCosts();
            CalculateDerivedAttributes();
        }

        public delegate void OnExperienceChange(int current, int max);
        public event OnExperienceChange onExperienceChange;

        public delegate void OnLevelUp();
        public event OnLevelUp onLevelUp;
    }
}