using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Abilities;
using Descension.Core;
using Descension.Name;
using Descension.Equipment;

namespace Descension.Characters
{
    public static class PcGenerator
    {
        static List<string> positiveQuirks;
        static List<string> neutralQuirks;
        static List<string> negativeQuirks;
        static List<string> woundQuirks;

        static List<string> availableRaces;
        static List<string> availableProfessions;

        static bool initialized = false;

        //static bool ignoreUnlocks = false;

        public static void Initialize()
        {
            if (initialized == false)
            {
                initialized = true;

                availableRaces = new List<string>();
                availableProfessions = new List<string>();

                foreach (KeyValuePair<string, Race> kvp in Database.Races)
                {
                    //if (ignoreUnlocks == true || PlayerManager.Instance.RacesUnlocked[kvp.Key] == true)
                    //{
                    //    availableRaces.Add(kvp.Key);
                    //}
                }

                foreach (KeyValuePair<string, Profession> kvp in Database.Professions)
                {
                    //if (ignoreUnlocks == true || PlayerManager.Instance.ProfessionsUnlocked[kvp.Key] == true)
                    //{
                    //    availableProfessions.Add(kvp.Key);
                    //}
                }

                positiveQuirks = new List<string>();
                neutralQuirks = new List<string>();
                negativeQuirks = new List<string>();
                woundQuirks = new List<string>();

                foreach (KeyValuePair<string, Ability> kvp in Database.Abilities)
                {
                    switch (kvp.Value.Type)
                    {
                        case AbilityType.Positive_Quirk:
                            positiveQuirks.Add(kvp.Key);
                            break;
                        case AbilityType.Neutral_Quirk:
                            neutralQuirks.Add(kvp.Key);
                            break;
                        case AbilityType.Negative_Quirk:
                            negativeQuirks.Add(kvp.Key);
                            break;
                        case AbilityType.Wound:
                            woundQuirks.Add(kvp.Key);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static PcData Generate(int index, Gender gender, string r, string p)
        {
            //if (availableRaces == null || availableProfessions == null) return null;

            string race_key = "";
            if (r == "") race_key = availableRaces[Random.Range(0, availableRaces.Count)];
            else race_key = r;

            string professionKey = "";
            if (p == "") professionKey = availableProfessions[Random.Range(0, availableProfessions.Count)];
            else professionKey = p;

            Race race = Database.GetRace(race_key);
            Profession profession = Database.GetProfession(professionKey);

            string hair = "";
            string beard = "";

            if (gender == Gender.None)
            {
                if (Random.Range(0, 100) < 50)
                {
                    gender = Gender.Male;
                    hair = race.maleDefaultHair;
                    beard = race.maleDefaultBeard;
                }
                else
                {
                    gender = Gender.Female;
                    hair = race.femaleDefaultHair;
                    beard = race.femaleDefaultBeard;
                }
            }
            else if (gender == Gender.Male)
            {
                gender = Gender.Male;
                hair = race.maleDefaultHair;
                beard = race.maleDefaultBeard;
            }
            else if (gender == Gender.Female)
            {
                gender = Gender.Female;
                hair = race.femaleDefaultHair;
                beard = race.femaleDefaultBeard;
            }

            PcData pc = new PcData(NameGenerator.Get(gender, race_key, professionKey),
                gender, 1, race_key, professionKey, hair, beard, index, index, index,
                3 + GameValue.Roll(new GameValue(1, 3), false), 3 + GameValue.Roll(new GameValue(1, 3), false));

            pc.Background = BackgroundGenerator.Generate();
            pc.Personality = GeneratePersonality();
            pc.Description = GenerateDescription(pc);

            pc.LevelUp();
            pc.CalculateExp();
            pc.AddExperience(Random.Range(0, 999), false);

            if (profession.StartingItems.Count > 0)
            {
                for (int i = 0; i < profession.StartingItems.Count; i++)
                {
                    ItemData item = ItemGenerator.CreateItem(profession.StartingItems[i].ItemKey, profession.StartingItems[i].MaterialKey, profession.StartingItems[i].PlusKey,
                        profession.StartingItems[i].PreKey, profession.StartingItems[i].PostKey, profession.StartingItems[i].StackSize);

                    if (race.ArmorSlotAllowed((EquipmentSlot)item.Slot) == true)
                        pc.Inventory.EquipItem(item, ((EquipmentSlot)item.Slot));
                }
            }
            else
            {
                for (int i = 0; i < pc.Inventory.EquippedItems.Length; i++)
                {
                    if (Database.GetRace(race_key).ArmorSlotAllowed(((EquipmentSlot)i)) == true)
                    {
                        int chanceForItem = 25;

                        if (i == (int)EquipmentSlot.Right_Hand)
                            chanceForItem = 100;
                        else if (i == (int)EquipmentSlot.Body)
                            chanceForItem = 100;
                        else if (i == (int)EquipmentSlot.Feet)
                            chanceForItem = 100;

                        if (Random.Range(1, 101) <= chanceForItem)
                        {
                            ItemData item = ItemGenerator.CreateRandomItem(i, 25, 25);

                            if (item != null)
                                pc.Inventory.EquippedItems[i] = new ItemData(item);
                        }
                    }
                }
            }

            int numAccessories = Random.Range(1, 3);
            pc.Inventory.AccessoryLimit = numAccessories + Random.Range(0, 3);

            for (int i = 0; i < numAccessories; i++)
            {
                pc.Inventory.EquipAccessory(ItemGenerator.CreateRandomItem(ItemTypeAllowed.Accessory, 0, 0), -1);
            }

            for (int spell = 0; spell < pc.Abilities.KnownSpells.Count; spell++)
            {
                if (Random.Range(0, 100) < 100)
                {
                    pc.Abilities.KnownSpells[spell].BoostRune = Helper.RandomValues<string, AbilityModifier>(Database.Runes).Key;
                }
            }

            string key = positiveQuirks[Random.Range(0, positiveQuirks.Count)];
            pc.Abilities.AddTrait(new Ability(Database.GetAbility(key)));

            key = neutralQuirks[Random.Range(0, neutralQuirks.Count)];
            pc.Abilities.AddTrait(new Ability(Database.GetAbility(key)));

            key = negativeQuirks[Random.Range(0, negativeQuirks.Count)];
            pc.Abilities.AddTrait(new Ability(Database.GetAbility(key)));

            if (GameValue.Roll(1, 100) < 20)
            {
                key = woundQuirks[Random.Range(0, woundQuirks.Count)];
                pc.Abilities.AddTrait(new Ability(Database.GetAbility(key)));
            }

            pc.CalculateAttributeModifiers();
            pc.CalculateStartAttributes(true);
            pc.CalculateDerivedAttributes();
            pc.CalculateStartSkills();
            pc.CalculateResistances();
            pc.CalculateExpCosts();

            pc.Abilities.PowerSlots = (pc.Attributes.GetAttribute(AttributeListType.Derived, (int)BaseAttribute.Memory).Current / 5) + 1;
            pc.Abilities.SpellSlots = (pc.Attributes.GetAttribute(AttributeListType.Derived, (int)BaseAttribute.Memory).Current / 5) + 1;

            pc.Abilities.FindTraits();
            pc.Abilities.FindAvailableAbilities();

            //pc.AddExperience(Random.Range(0, 500) * 10, false);

            pc.CalculateUpkeep();

            return pc;
        }

        public static string GenerateDescription(PcData pc)
        {
            string description = "";

            return description;
        }

        public static CharacterPersonality GeneratePersonality()
        {
            CharacterPersonality p = new CharacterPersonality();

            p.Order = Random.Range(-50, 51);
            p.Morality = Random.Range(-15, 76);
            p.Bravery = Random.Range(-50, 51);
            p.Ego = Random.Range(-50, 51);
            p.Faith = Random.Range(-50, 51);

            return p;
        }
    }
}