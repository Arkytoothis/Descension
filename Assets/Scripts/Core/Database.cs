using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Descension.Abilities;
using Descension.Characters;
using Descension.Equipment;
using Descension.Name;

namespace Descension.Core
{
    public static class Database
    {
        static bool initialized = false;

        //private static Dictionary<string, TransitionDefinition> transitions = new Dictionary<string, TransitionDefinition>();
        //public static Dictionary<string, TransitionDefinition> Transitions { get { return transitions; } }    

        //private static Dictionary<string, Encounter.TerrainData> terrainDefinitions = new Dictionary<string, Encounter.TerrainData>();
        //public static Dictionary<string, Encounter.TerrainData> TerrainDefinitions { get { return terrainDefinitions; } }

        //private static Dictionary<string, WorldSiteDefinition> worldSites = new Dictionary<string, WorldSiteDefinition>();
        //public static Dictionary<string, WorldSiteDefinition> WorldSites { get { return worldSites; } }

        private static List<AttributeDefinition> baseAttributeDefinitions = new List<AttributeDefinition>();
        public static List<AttributeDefinition> BaseAttributes { get { return baseAttributeDefinitions; } }

        private static List<AttributeDefinition> derivedAttributeDefinitions = new List<AttributeDefinition>();
        public static List<AttributeDefinition> DerivedAttributes { get { return derivedAttributeDefinitions; } }

        private static List<SkillDefinition> skillDefinitions = new List<SkillDefinition>();
        public static List<SkillDefinition> Skills { get { return skillDefinitions; } }

        private static List<AttributeDefinition> damageTypeDefinitions = new List<AttributeDefinition>();
        public static List<AttributeDefinition> DamageTypes { get { return damageTypeDefinitions; } }

        private static List<AttributeDefinition> partyAttributeDefinitions = new List<AttributeDefinition>();
        public static List<AttributeDefinition> PartyAttributes { get { return partyAttributeDefinitions; } }

        private static List<AttributeDefinition> weaponAttributes = new List<AttributeDefinition>();
        public static List<AttributeDefinition> WeaponAttributes { get { return weaponAttributes; } }

        private static List<AttributeDefinition> ammoAttributes = new List<AttributeDefinition>();
        public static List<AttributeDefinition> AmmoAttributes { get { return ammoAttributes; } }

        private static List<AttributeDefinition> wearableAttributes = new List<AttributeDefinition>();
        public static List<AttributeDefinition> WearableAttributes { get { return wearableAttributes; } }

        private static List<AttributeDefinition> accessoryAttributes = new List<AttributeDefinition>();
        public static List<AttributeDefinition> AccessoryAttributes { get { return accessoryAttributes; } }

        private static Dictionary<string, Profession> professions = new Dictionary<string, Profession>();
        public static Dictionary<string, Profession> Professions { get { return professions; } }

        private static Dictionary<string, Race> races = new Dictionary<string, Race>();
        public static Dictionary<string, Race> Races { get { return races; } }

        private static Dictionary<string, ItemDefinition> itemDefinitions = new Dictionary<string, ItemDefinition>();
        public static Dictionary<string, ItemDefinition> Items { get { return itemDefinitions; } }

        private static Dictionary<string, ItemModifier> itemModifiers = new Dictionary<String, ItemModifier>();
        public static Dictionary<string, ItemModifier> ItemModifiers { get { return itemModifiers; } }

        private static Dictionary<string, Ability> abilities = new Dictionary<string, Ability>();
        public static Dictionary<string, Ability> Abilities { get { return abilities; } }

        private static Dictionary<string, AbilityModifier> runes = new Dictionary<string, AbilityModifier>();
        public static Dictionary<string, AbilityModifier> Runes { get { return runes; } }

        private static Dictionary<string, NPCDefinition> npcs = new Dictionary<string, NPCDefinition>();
        public static Dictionary<string, NPCDefinition> NPCs { get { return npcs; } }

        private static Dictionary<string, FactionData> factions = new Dictionary<string, FactionData>();
        public static Dictionary<string, FactionData> Factions { get { return factions; } }

        public static string DataPath;

        public static void Initialize()
        {
            if (initialized == false)
            {
                initialized = true;
                DataPath = Application.streamingAssetsPath + "/Data/";

                //LoadFiles();

                LoadData();
                Save();
            }
        }

        static void Save()
        {
            SaveAttributes();
            Save("items", itemDefinitions);
            Save("item_modifiers", itemModifiers);
            Save("professions", professions);
            Save("races", races);
            Save("abilities", abilities);
            Save("runes", runes);
            Save("npcs", npcs);
        }

        static void LoadData()
        {
            LoadAttributes();
            LoadSkills();
            LoadItems();
            LoadArtifacts();
            LoadItemMaterials();
            LoadItemQualities();
            LoadPreEnchants();
            LoadPostEnchants();
            LoadItemAttributes();
            LoadFactions();
            LoadProfessions();
            LoadRaces();
            LoadAbilities();
            LoadAbilityModifiers();
            LoadNPCs();
        }

        static void LoadFiles()
        {
            LoadBaseAttributeData();
            LoadDerivedAttributeData();
            LoadDerivedDamageTypeData();
            LoadSkillData();
            LoadItemData();
            LoadArtifacts();
            LoadItemModifiers();
            LoadProfessionData();
            LoadRaceData();
            LoadAbilityData();
            LoadRuneData();
            LoadNpcData();
        }

        public static void SaveAttributes()
        {
            SaveAttributes("base_attributes", baseAttributeDefinitions);
            SaveAttributes("derived_attributes", derivedAttributeDefinitions);
            SaveAttributes("damage_types", damageTypeDefinitions);
            SaveAttributes("party_attibutes", partyAttributeDefinitions);
            SaveSkills("skills", skillDefinitions);
        }

        public static void Save<TKey, TValue>(string file, Dictionary<TKey, TValue> dictionary)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            string dataAsJson = JsonConvert.SerializeObject(dictionary, settings);
            string filePath = DataPath + file + ".json";
            File.WriteAllText(filePath, dataAsJson);
        }

        public static void SaveAttributes(string file, List<AttributeDefinition> list)
        {
            AttributeDefinitionList al = new AttributeDefinitionList();
            foreach (AttributeDefinition def in list)
            {
                al.Attributes.Add(def);
            }

            string dataAsJson = JsonUtility.ToJson(al);
            string filePath = DataPath + file + ".json";
            File.WriteAllText(filePath, dataAsJson);
        }

        public static void SaveSkills(string file, List<SkillDefinition> list)
        {
            SkillList sl = new SkillList();
            foreach (SkillDefinition def in list)
            {
                sl.Skills.Add(def);
            }

            string dataAsJson = JsonUtility.ToJson(sl);
            string filePath = DataPath + file + ".json";
            File.WriteAllText(filePath, dataAsJson);
        }

        private static void LoadNpcData()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            string filePath = DataPath + "npcs.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                npcs = JsonConvert.DeserializeObject<Dictionary<string, NPCDefinition>>(dataAsJson, settings);
            }
        }

        private static void LoadBaseAttributeData()
        {
            string filePath = DataPath + "base_attributes.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                AttributeDefinitionList list = JsonUtility.FromJson<AttributeDefinitionList>(dataAsJson);

                foreach (AttributeDefinition def in list.Attributes)
                {
                    baseAttributeDefinitions.Add(def);
                }
            }
        }

        private static void LoadDerivedAttributeData()
        {
            string filePath = DataPath + "derived_attributes.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                AttributeDefinitionList list = JsonUtility.FromJson<AttributeDefinitionList>(dataAsJson);

                foreach (AttributeDefinition def in list.Attributes)
                {
                    derivedAttributeDefinitions.Add(def);
                }
            }
        }

        private static void LoadDerivedDamageTypeData()
        {
            string filePath = DataPath + "damage_types.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                AttributeDefinitionList list = JsonUtility.FromJson<AttributeDefinitionList>(dataAsJson);

                foreach (AttributeDefinition def in list.Attributes)
                {
                    damageTypeDefinitions.Add(def);
                }
            }
        }

        private static void LoadSkillData()
        {
            string filePath = DataPath + "skills.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                SkillList list = JsonUtility.FromJson<SkillList>(dataAsJson);

                foreach (SkillDefinition def in list.Skills)
                {
                    skillDefinitions.Add(def);
                }
            }
        }

        private static void LoadAbilityData()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            string filePath = DataPath + "abilities.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                abilities = JsonConvert.DeserializeObject<Dictionary<string, Ability>>(dataAsJson, settings);
            }
        }

        private static void LoadItemData()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            string filePath = DataPath + "items.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                itemDefinitions = JsonConvert.DeserializeObject<Dictionary<string, ItemDefinition>>(dataAsJson, settings);
            }
        }

        private static void LoadItemModifiers()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            string filePath = DataPath + "item_modifiers.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                itemModifiers = JsonConvert.DeserializeObject<Dictionary<string, ItemModifier>>(dataAsJson, settings);
            }
        }

        private static void LoadProfessionData()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            string filePath = DataPath + "professions.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                professions = JsonConvert.DeserializeObject<Dictionary<string, Profession>>(dataAsJson, settings);
            }
        }

        private static void LoadRaceData()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            string filePath = DataPath + "races.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                races = JsonConvert.DeserializeObject<Dictionary<string, Race>>(dataAsJson, settings);
            }

            //foreach (KeyValuePair<string, Race> kvp in races)
            //{
            //    Debug.Log(kvp.Value.Name);
            //}
        }

        private static void LoadRuneData()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            string filePath = DataPath + "runes.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                runes = JsonConvert.DeserializeObject<Dictionary<string, AbilityModifier>>(dataAsJson, settings);
            }
        }

        public static NPCDefinition GetNPC(string key)
        {
            if (npcs.ContainsKey(key) == false)
            {
                Debug.LogWarning("npcs key: " + key + " does not exist");
                return null;
            }
            else
            {
                return npcs[key];
            }
        }

        public static AttributeDefinition GetBaseAttribute(int index)
        {
            if (index >= 0 && index <= baseAttributeDefinitions.Count - 1)
            {
                return baseAttributeDefinitions[index];
            }
            else
            {
                Debug.Log("index out of range " + index);
                return null;
            }
        }

        public static AttributeDefinition GetDerivedAttribute(int index)
        {
            if (index >= 0 && index <= derivedAttributeDefinitions.Count - 1)
            {
                return derivedAttributeDefinitions[index];
            }
            else
            {
                Debug.Log("index out of range " + index);
                return null;
            }
        }

        public static AttributeDefinition GetDamageType(int index)
        {
            if (index >= 0 && index <= damageTypeDefinitions.Count - 1)
            {
                return damageTypeDefinitions[index];
            }
            else
            {
                Debug.Log("index out of range " + index);
                return null;
            }
        }

        public static SkillDefinition GetSkill(int index)
        {
            if (index >= 0 && index <= skillDefinitions.Count - 1)
            {
                return skillDefinitions[index];
            }
            else
            {
                Debug.Log("index out of range " + index);
                return null;
            }
        }

        public static AttributeDefinition GetPartyAttribute(int index)
        {
            if (index >= 0 && index <= partyAttributeDefinitions.Count - 1)
            {
                return partyAttributeDefinitions[index];
            }
            else
            {
                Debug.Log("index out of range " + index);
                return null;
            }
        }

        public static Profession GetProfession(string key)
        {
            if (professions.ContainsKey(key) == false)
            {
                Debug.LogWarning("Profession key: " + key + " does not exist");
                return null;
            }
            else
            {
                return professions[key];
            }
        }

        public static Race GetRace(string key)
        {
            if (races.ContainsKey(key) == false)
            {
                Debug.LogWarning("Race key: " + key + " does not exist");
                return null;
            }
            else
            {
                return races[key];
            }
        }

        public static string GetRaceAt(int index)
        {
            string race = Globals.EmptyString;
            int i = 0;

            foreach (KeyValuePair<string, Race> kvp in races)
            {
                i++;
                if (index == i)
                {
                    race = kvp.Key;
                    break;
                }
            }

            return race;
        }

        public static int GetRaceIndex(string race)
        {
            int index = 0;

            foreach (KeyValuePair<string, Race> kvp in races)
            {
                index++;
                if (race == kvp.Key)
                    break;
            }

            return index;
        }

        public static int GetProfessionIndex(string profession)
        {
            int index = 0;

            foreach (KeyValuePair<string, Profession> kvp in professions)
            {
                index++;
                if (profession == kvp.Key)
                    break;
            }

            return index;
        }

        public static Ability GetAbility(string key)
        {
            if (abilities.ContainsKey(key) == false)
            {
                Debug.LogWarning("traits key: " + key + " does not exist");
                return null;
            }
            else
            {
                return abilities[key];
            }
        }

        public static ItemModifier GetItemModifier(string key)
        {
            if (itemModifiers.ContainsKey(key) == false)
            {
                Debug.LogWarning("itemModifiers key: " + key + " does not exist");
                return null;
            }
            else
            {
                return itemModifiers[key];
            }
        }

        public static ItemDefinition GetItem(string key)
        {
            if (itemDefinitions.ContainsKey(key) == true)
            {
                return itemDefinitions[key];
            }
            else
            {
                Debug.LogWarning("Item key: " + key + " does not exist");
                return null;
            }
        }

        static void LoadItemQualities()
        {
            ItemModifier enchant = new ItemModifier("Inferior", "Inferior", 2, 100, 1, 20, 0, ItemModifierType.Quality, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("Average", "Average", 2, 100, 1, 20, 0, ItemModifierType.Quality, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Uncommon);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("Superior", "Superior", 2, 100, 1, 20, 0, ItemModifierType.Quality, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Rare);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("Masterwork", "Masterwork", 2, 100, 1, 20, 0, ItemModifierType.Quality, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Fabled);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("Magnificent", "Magnificent", 2, 100, 1, 20, 0, ItemModifierType.Quality, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("Exalted", "Exalted", 2, 100, 1, 20, 0, ItemModifierType.Quality, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Legendary);
            itemModifiers.Add(enchant.Key, enchant);
        }

        static void LoadPreEnchants()
        {
            ItemModifier enchant = new ItemModifier("Flaming", "Flaming", 2, 100, 1, 20, 0, ItemModifierType.Pre_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Rare);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("Icey", "Icey", 2, 100, 1, 20, 0, ItemModifierType.Pre_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Rare);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("Venomous", "Venomous", 2, 100, 1, 20, 0, ItemModifierType.Pre_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Rare);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("Shocking", "Shocking", 2, 100, 1, 20, 0, ItemModifierType.Pre_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Rare);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("Warriors", "Warriors", 4, 100, 1, 100, 0, ItemModifierType.Pre_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("Wizards", "Wizards", 4, 100, 1, 25, 0, ItemModifierType.Pre_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("Priests", "Priests", 4, 100, 1, 75, 0, ItemModifierType.Pre_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(enchant.Key, enchant);
        }

        static void LoadPostEnchants()
        {
            ItemModifier enchant = new ItemModifier("of Strength", "Strength", 1, 100, 1, 10, 0, ItemModifierType.Post_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Rare);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("of Endurance", "Endurance", 1, 100, 1, 10, 0, ItemModifierType.Post_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Rare);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("of Might", "Might", 2, 100, 1, 20, 0, ItemModifierType.Post_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Rare);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("of Insight", "Insight", 2, 100, 1, 20, 0, ItemModifierType.Post_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Rare);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("of the Wolf", "Wolf", 3, 100, 1, 50, 0, ItemModifierType.Post_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("of the Bear", "Bear", 3, 100, 1, 75, 0, ItemModifierType.Post_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("of the Snake", "Snake", 3, 100, 1, 30, 0, ItemModifierType.Post_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("of Shadow", "Shadow", 3, 100, 1, 30, 0, ItemModifierType.Post_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("of Light", "Light", 3, 100, 1, 30, 0, ItemModifierType.Post_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("of Death", "Death", 3, 100, 1, 30, 0, ItemModifierType.Post_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("of Arcana", "Arcana", 3, 100, 1, 30, 0, ItemModifierType.Post_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(enchant.Key, enchant);

            enchant = new ItemModifier("of Destiny", "Destiny", 3, 100, 1, 30, 0, ItemModifierType.Post_Enchant, MaterialHardness.None, ItemTypeAllowed.Any, Rarity.Legendary);
            itemModifiers.Add(enchant.Key, enchant);
        }

        static void LoadItemMaterials()
        {
            ItemModifier material = new ItemModifier("Copper", "Copper", 0, 0, 1, 0, 0, ItemModifierType.Material, MaterialHardness.Metal, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Rusty", "Rusty", -1, -1, 1, -1, 0, ItemModifierType.Material, MaterialHardness.Metal, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Iron", "Iron", 1, 1, 1, 1, 0, ItemModifierType.Material, MaterialHardness.Metal, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Bronze", "Bronze", 1, 5, 1, 5, 0, ItemModifierType.Material, MaterialHardness.Metal, ItemTypeAllowed.Any, Rarity.Uncommon);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Steel", "Steel", 2, 10, 1, 12, 0, ItemModifierType.Material, MaterialHardness.Metal, ItemTypeAllowed.Any, Rarity.Rare);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Black Steel", "Black Steel", 3, 20, 1, 25, 0, ItemModifierType.Material, MaterialHardness.Metal, ItemTypeAllowed.Any, Rarity.Fabled);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Mythril", "Mythril", 4, 100, 1, 300, 0, ItemModifierType.Material, MaterialHardness.Metal, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Arcanite", "Arcanite", 5, 150, 1, 500, 0, ItemModifierType.Material, MaterialHardness.Metal, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Dragon Scale", "Dragon Scale", 6, 250, 1, 1000, 0, ItemModifierType.Material, MaterialHardness.Metal, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Wood", "Wood", 0, 0, 1, 0, 0, ItemModifierType.Material, MaterialHardness.Soft, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Bone", "Bone", -1, 1, 1, -1, -1, ItemModifierType.Material, MaterialHardness.Soft, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Ironbark", "Ironbark", 1, 5, 1, 15, 0, ItemModifierType.Material, MaterialHardness.Soft, ItemTypeAllowed.Any, Rarity.Uncommon);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Dragon Bone", "Dragon Bone", 6, 100, 1, 1000, 0, ItemModifierType.Material, MaterialHardness.Soft, ItemTypeAllowed.Any, Rarity.Legendary);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Stone", "Stone", 0, 0, 1, -1, 0, ItemModifierType.Material, MaterialHardness.Stone, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Flint", "Flint", 1, 1, 1, 0, 0, ItemModifierType.Material, MaterialHardness.Stone, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Obsidian", "Obsidian", 1, 5, 1, 3, 0, ItemModifierType.Material, MaterialHardness.Stone, ItemTypeAllowed.Any, Rarity.Uncommon);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Sun Stone", "Sun Stone", 3, 30, 1, 75, 0, ItemModifierType.Material, MaterialHardness.Stone, ItemTypeAllowed.Any, Rarity.Rare);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Moon Stone", "Moon Stone", 3, 30, 1, 75, 0, ItemModifierType.Material, MaterialHardness.Stone, ItemTypeAllowed.Any, Rarity.Rare);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Elder Clay", "Elder Clay", 5, 45, 1, 120, 0, ItemModifierType.Material, MaterialHardness.Stone, ItemTypeAllowed.Any, Rarity.Fabled);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Linen", "Linen", 0, 0, 1, 0, 0, ItemModifierType.Material, MaterialHardness.Cloth, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Silk", "Silk", 1, 10, 1, 0, 0, ItemModifierType.Material, MaterialHardness.Cloth, ItemTypeAllowed.Any, Rarity.Uncommon);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Steelweave", "Steelweave", 2, 25, 1, 20, 0, ItemModifierType.Material, MaterialHardness.Cloth, ItemTypeAllowed.Any, Rarity.Rare);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Spellcloth", "Spellcloth", 3, 35, 1, 25, 0, ItemModifierType.Material, MaterialHardness.Cloth, ItemTypeAllowed.Any, Rarity.Fabled);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Rockleaf", "Rockleaf", 4, 20, 1, 50, 0, ItemModifierType.Material, MaterialHardness.Cloth, ItemTypeAllowed.Any, Rarity.Mythical);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Fur", "Fur", -1, 1, 1, 0, 0, ItemModifierType.Material, MaterialHardness.Leather, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Hide", "Hide", 0, 3, 1, 1, 0, ItemModifierType.Material, MaterialHardness.Leather, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Hardened Leather", "Hardened Leather", 1, 10, 1, 5, 0, ItemModifierType.Material, MaterialHardness.Leather, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Reinforced Leather", "Reinforced Leather", 2, 15, 1, 12, 0, ItemModifierType.Material, MaterialHardness.Leather, ItemTypeAllowed.Any, Rarity.Uncommon);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Troll Skin", "Troll Skin", 3, 20, 1, 150, 0, ItemModifierType.Material, MaterialHardness.Leather, ItemTypeAllowed.Any, Rarity.Rare);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Dragon Hide", "Dragon Hide", 10, 100, 1, 750, 0, ItemModifierType.Material, MaterialHardness.Leather, ItemTypeAllowed.Any, Rarity.Legendary);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Thick", "Thick", 1, 10, 1, 0, 0, ItemModifierType.Material, MaterialHardness.Liquid, ItemTypeAllowed.Any, Rarity.Uncommon);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Bubbling", "Bubbling", 2, 10, 1, 0, 0, ItemModifierType.Material, MaterialHardness.Liquid, ItemTypeAllowed.Any, Rarity.Uncommon);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Paper", "Paper", 0, 10, 1, 0, 0, ItemModifierType.Material, MaterialHardness.Paper, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Parchment", "Parchment", 1, 15, 1, 0, 0, ItemModifierType.Material, MaterialHardness.Paper, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Plain", "Plain", 0, 0, 1, 0, 0, ItemModifierType.Material, MaterialHardness.Food, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);

            material = new ItemModifier("Cooked", "Cooked", 1, 1, 1, 0, 0, ItemModifierType.Material, MaterialHardness.Food, ItemTypeAllowed.Any, Rarity.Common);
            itemModifiers.Add(material.Key, material);
        }

        static void LoadArtifacts()
        {
        }

        static void LoadItems()
        {
            ItemDefinition item = new ItemDefinition("Knife", "Knife", "Knife", "Knife", "Knife", EquipmentSlot.Right_Hand, 10, 1, 10, 1, 3,
                ItemType.Weapon, ItemHardnessAllowed.Soft_or_Hard, ItemNameFormat.Material_First,
                new WeaponData(WeaponType.One_Handed_Melee, AmmoType.None, AttackType.Finesse, WeaponGripType.Either_Hand, 15, 1, 5, 5,
                new List<DamageData> { new DamageData(DamageType.Physical, (int)DerivedAttribute.Health, new GameValue(1, 4), GameValue.Zero, 0, 0) }, "", "", "", "", ""),
                null, null, null, null, null, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(-8.696f, 4.995f, -179.488f), new Vector3(0.156f, 0.052f, -0.033f), "");
            itemDefinitions.Add(item.Key, item);

            item = new ItemDefinition("Short Sword", "Short Sword", "Short Sword", "Short Sword", "Short Sword", EquipmentSlot.Right_Hand, 10, 1, 10, 1, 4,
                ItemType.Weapon, ItemHardnessAllowed.Soft_or_Hard, ItemNameFormat.Material_First,
                new WeaponData(WeaponType.One_Handed_Melee, AmmoType.None, AttackType.Finesse, WeaponGripType.Right_Hand, 15, 1, 7, 5,
                new List<DamageData> { new DamageData(DamageType.Physical, (int)DerivedAttribute.Health, new GameValue(1, 4), GameValue.Zero, 0, 0) }, "", "", "", "", ""),
                null, null, null, null, null, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(7.232f, -9.425f, 171.125f), new Vector3(0.064f, 0.084f, 0.031f), "");
            itemDefinitions.Add(item.Key, item);

            item = new ItemDefinition("Short Bow", "Short Bow", "Short Bow", "Short Bow", "Short Bow", EquipmentSlot.Right_Hand, 10, 1, 10, 1, 8,
                ItemType.Weapon, ItemHardnessAllowed.Soft_or_Hard, ItemNameFormat.Material_First,
                new WeaponData(WeaponType.One_Handed_Melee, AmmoType.None, AttackType.Finesse, WeaponGripType.Both_Hands, 15, 5, 5, 5,
                new List<DamageData> { new DamageData(DamageType.Physical, (int)DerivedAttribute.Health, new GameValue(1, 4), GameValue.Zero, 0, 0) }, "", "", "", "", ""),
                null, null, null, null, null, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0.0f, -195.245f, 8.925f), new Vector3(-0.003f, -0.096f, 0.0010f), "");
            itemDefinitions.Add(item.Key, item);

            item = new ItemDefinition("Club", "Club", "Club", "Club", "Club", EquipmentSlot.Right_Hand, 10, 1, 10, 1, 3,
                ItemType.Weapon, ItemHardnessAllowed.Soft_or_Hard, ItemNameFormat.Material_First,
                new WeaponData(WeaponType.One_Handed_Melee, AmmoType.None, AttackType.Finesse, WeaponGripType.Right_Hand, 15, 1, 5, 5,
                new List<DamageData> { new DamageData(DamageType.Physical, (int)DerivedAttribute.Health, new GameValue(1, 4), GameValue.Zero, 0, 0) }, "", "", "", "", ""),
                null, null, null, null, null, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0f, 0f, -179.976f), new Vector3(0.011f, 0.045f, -0.009f), "");
            itemDefinitions.Add(item.Key, item);

            item = new ItemDefinition("Staff", "Staff", "Staff", "Staff", "Staff", EquipmentSlot.Right_Hand, 10, 1, 10, 1, 5,
                ItemType.Weapon, ItemHardnessAllowed.Soft_or_Hard, ItemNameFormat.Material_First,
                new WeaponData(WeaponType.One_Handed_Melee, AmmoType.None, AttackType.Finesse, WeaponGripType.Both_Hands, 15, 5, 5, 5,
                new List<DamageData> { new DamageData(DamageType.Physical, (int)DerivedAttribute.Health, new GameValue(1, 4), GameValue.Zero, 0, 0) }, "", "", "", "", ""),
                null, null, null, null, null, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0.0f, 0.0f, -189.922f), new Vector3(-0.004f, 0.08f, 0.013f), "");
            itemDefinitions.Add(item.Key, item);

            item = new ItemDefinition("Mace", "Mace", "Mace", "Mace", "Mace", EquipmentSlot.Right_Hand, 10, 1, 10, 1, 5,
                ItemType.Weapon, ItemHardnessAllowed.Soft_or_Hard, ItemNameFormat.Material_First,
                new WeaponData(WeaponType.One_Handed_Melee, AmmoType.None, AttackType.Finesse, WeaponGripType.Both_Hands, 15, 5, 5, 5,
                new List<DamageData> { new DamageData(DamageType.Physical, (int)DerivedAttribute.Health, new GameValue(1, 4), GameValue.Zero, 0, 0) }, "", "", "", "", ""),
                null, null, null, null, null, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0.0f, 0.0f, -189.922f), new Vector3(-0.004f, 0.08f, 0.013f), "");
            itemDefinitions.Add(item.Key, item);

            item = new ItemDefinition("Buckler", "Buckler", "Buckler", "Buckler", "Buckler", EquipmentSlot.Left_Hand, 10, 1, 10, 1, 2,
                ItemType.Weapon, ItemHardnessAllowed.Soft_or_Hard, ItemNameFormat.Material_First,
                new WeaponData(WeaponType.One_Handed_Melee, AmmoType.None, AttackType.Finesse, WeaponGripType.Both_Hands, 15, 5, 5, 5,
                new List<DamageData> { new DamageData(DamageType.Physical, (int)DerivedAttribute.Health, new GameValue(1, 4), GameValue.Zero, 0, 0) }, "", "", "", "", ""),
                null,
                new WearableData(WearableType.Shield, 1, 0, 5, 0, new List<ResistanceData> { new ResistanceData(DamageType.Physical, 2) }, "", ""),
                null, null, null, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(122.756f, 117.561f, -1.37f), new Vector3(-0.021f, 0.014f, 0.029f), "");
            itemDefinitions.Add(item.Key, item);

            item = new ItemDefinition("Small Shield", "Small Shield", "Small Shield", "Small Shield", "Small Shield", EquipmentSlot.Left_Hand, 10, 1, 10, 1, 5,
                ItemType.Weapon, ItemHardnessAllowed.Soft_or_Hard, ItemNameFormat.Material_First,
                new WeaponData(WeaponType.One_Handed_Melee, AmmoType.None, AttackType.Finesse, WeaponGripType.Both_Hands, 15, 5, 5, 5,
                new List<DamageData> { new DamageData(DamageType.Physical, (int)DerivedAttribute.Health, new GameValue(1, 4), GameValue.Zero, 0, 0) }, "", "", "", "", ""),
                null,
                new WearableData(WearableType.Shield, 1, 0, 5, 0, new List<ResistanceData> { new ResistanceData(DamageType.Physical, 2) }, "", ""),
                null, null, null, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(122.756f, 117.561f, -1.37f), new Vector3(-0.021f, 0.014f, 0.029f), "");
            itemDefinitions.Add(item.Key, item);

            item = new ItemDefinition("Hat", "Hat", "Hat", "Hat", "Hat", EquipmentSlot.Head, 10, 1, 10, 1, 0,
                ItemType.Weapon, ItemHardnessAllowed.Soft_or_Hard, ItemNameFormat.Material_First,
                null, null, new WearableData(WearableType.Armor, 1, 0, 5, 0, new List<ResistanceData> { new ResistanceData(DamageType.Physical, 2) }, "", ""),
                null, null, null, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(122.756f, 117.561f, -1.37f), new Vector3(-0.021f, 0.014f, 0.029f), "");
            itemDefinitions.Add(item.Key, item);

            item = new ItemDefinition("Leather Helm", "Leather Helm", "Leather Helm", "Leather Helm", "Leather Helm", EquipmentSlot.Head, 10, 1, 10, 1, -1,
                ItemType.Weapon, ItemHardnessAllowed.Soft_or_Hard, ItemNameFormat.Material_First,
                null, null, new WearableData(WearableType.Armor, 1, 0, 5, 0, new List<ResistanceData> { new ResistanceData(DamageType.Physical, 2) }, "", ""),
                null, null, null, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(122.756f, 117.561f, -1.37f), new Vector3(-0.021f, 0.014f, 0.029f), "");
            itemDefinitions.Add(item.Key, item);

            item = new ItemDefinition("Helmet", "Helmet", "Helmet", "Helmet", "Helmet", EquipmentSlot.Head, 10, 1, 10, 1, -2,
                ItemType.Weapon, ItemHardnessAllowed.Soft_or_Hard, ItemNameFormat.Material_First,
                null, null, new WearableData(WearableType.Armor, 1, 0, 5, 0, new List<ResistanceData> { new ResistanceData(DamageType.Physical, 2) }, "", ""),
                null, null, null, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(122.756f, 117.561f, -1.37f), new Vector3(-0.021f, 0.014f, 0.029f), "");
            itemDefinitions.Add(item.Key, item);

            item = new ItemDefinition("Small Healing Potion", "Small Healing Potion", "Small Healing Potion", "Small Healing Potion", "Small Healing Potion",
                EquipmentSlot.None, 1, 2, 20, 1, 10,
                ItemType.Accessory, ItemHardnessAllowed.Potion, ItemNameFormat.Material_First, null, null, null,
                new AccessoryData(AccessoryType.Consumable, 5, "", "", ""), null, new UsableData(TimeType.None, 0, new List<AbilityEffect> { }), new Vector3(0, 0, 0), new Vector3(0, 0, 0),
                new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), "");
            itemDefinitions.Add(item.Key, item);

            item = new ItemDefinition("Small Energy Potion", "Small Energy Potion", "Small Energy Potion", "Small Energy Potion", "Small Energy Potion",
                EquipmentSlot.None, 1, 2, 20, 1, 10,
                ItemType.Accessory, ItemHardnessAllowed.Potion, ItemNameFormat.Material_First, null, null, null,
                new AccessoryData(AccessoryType.Consumable, 5, "", "", ""), null,
                new UsableData(TimeType.None, 0, new List<AbilityEffect>
                {
                }), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), "");
            itemDefinitions.Add(item.Key, item);

            item = new ItemDefinition("Apple", "Apple", "Apple", "Apple", "Apple",
                EquipmentSlot.None, 1, 2, 20, 1, 10,
                ItemType.Accessory, ItemHardnessAllowed.Food, ItemNameFormat.Material_First, null, null, null,
                new AccessoryData(AccessoryType.Consumable, 5, "", "", ""), null, new UsableData(TimeType.None, 0, new List<AbilityEffect> { }), new Vector3(0, 0, 0), new Vector3(0, 0, 0),
                new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), "");
            itemDefinitions.Add(item.Key, item);

            item = new ItemDefinition("Water", "Water", "Water", "Water", "Water",
                EquipmentSlot.None, 1, 2, 20, 1, 10,
                ItemType.Accessory, ItemHardnessAllowed.Food, ItemNameFormat.Material_First, null, null, null,
                new AccessoryData(AccessoryType.Consumable, 5, "", "", ""), null, new UsableData(TimeType.None, 0, new List<AbilityEffect> { }), new Vector3(0, 0, 0), new Vector3(0, 0, 0),
                new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), "");
            itemDefinitions.Add(item.Key, item);
        }

        static void LoadProfessions()
        {
            Profession temp = new Profession("Citizen", "Citizen", "", 25, new UpkeepData(1, 0, 0, 0), new GameValue(10, 20));
            temp.HealthPerLevel = new GameValue(1); temp.StaminaPerLevel = new GameValue(1); temp.EssencePerLevel = new GameValue(1);
            temp.SkillProficiencies.Add(SkillProficiency.Randomize(new GameValue(1, 4)));
            temp.SkillProficiencies.Add(SkillProficiency.Randomize(new GameValue(1, 4)));
            temp.SkillProficiencies.Add(SkillProficiency.Randomize(new GameValue(1, 4)));
            temp.SkillProficiencies.Add(SkillProficiency.Randomize(new GameValue(1, 4)));
            temp.MinimumAttributes[(int)BaseAttribute.Strength] = 15;
            professions.Add(temp.Key, temp);

            temp = new Profession("Soldier", "Soldier", "", 100, new UpkeepData(2, 1, 1, 0), new GameValue(20, 30));
            temp.HealthPerLevel = new GameValue(10); temp.StaminaPerLevel = new GameValue(5); temp.EssencePerLevel = new GameValue(1);
            temp.StartingItems.Add(new ItemShort("Short Sword", "Copper", "", "", ""));
            temp.StartingItems.Add(new ItemShort("Small Shield", "Wood", "", "", ""));
            temp.StartingItems.Add(new ItemShort("Helmet", "Copper", "", "", ""));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Slashing_Weapons, 3, 6));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Piercing_Weapons, 2, 0));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Blunt_Weapons, 2, 0));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Shields, 3, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Heavy_Armor, 3, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Leadership, 2, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Survival, 1, 0));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Archery, 1, 0));
            temp.AttributePriorities.Add(BaseAttribute.Strength);
            temp.AttributePriorities.Add(BaseAttribute.Endurance);
            temp.AttributePriorities.Add(BaseAttribute.Speed);
            temp.AttributePriorities.Add(BaseAttribute.Agility);
            temp.AttributePriorities.Add(BaseAttribute.Senses);
            temp.AttributePriorities.Add(BaseAttribute.Willpower);
            temp.AttributePriorities.Add(BaseAttribute.Memory);
            temp.AttributePriorities.Add(BaseAttribute.Intellect);
            temp.AttributePriorities.Add(BaseAttribute.Wisdom);
            temp.AttributePriorities.Add(BaseAttribute.Charisma);
            temp.MinimumAttributes[(int)BaseAttribute.Strength] = 18;
            temp.MinimumAttributes[(int)BaseAttribute.Endurance] = 18;
            temp.MinimumAttributes[(int)BaseAttribute.Agility] = 15;
            temp.Powers.Add(new AbilityUnlock(AbilityType.Power, "Taunt", 1));
            professions.Add(temp.Key, temp);

            temp = new Profession("Scout", "Scout", "", 90, new UpkeepData(2, 0, 1, 0), new GameValue(20, 30));
            temp.HealthPerLevel = new GameValue(5); temp.StaminaPerLevel = new GameValue(5); temp.EssencePerLevel = new GameValue(3);
            temp.StartingItems.Add(new ItemShort("Short Bow", "Wood", "", "", ""));
            temp.StartingItems.Add(new ItemShort("Hat", "Leather", "", "", ""));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Archery, 3, 6));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Scouting, 3, 3));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Slashing_Weapons, 2, 0));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Light_Armor, 2, 0));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Survival, 2, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Air_Magic, 1, 0));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Bucklers, 1, 0));
            temp.Powers.Add(new AbilityUnlock(AbilityType.Power, "Eagle Eye", 1));
            temp.AttributePriorities.Add(BaseAttribute.Senses);
            temp.AttributePriorities.Add(BaseAttribute.Agility);
            temp.AttributePriorities.Add(BaseAttribute.Speed);
            temp.AttributePriorities.Add(BaseAttribute.Willpower);
            temp.AttributePriorities.Add(BaseAttribute.Strength);
            temp.AttributePriorities.Add(BaseAttribute.Endurance);
            temp.AttributePriorities.Add(BaseAttribute.Intellect);
            temp.AttributePriorities.Add(BaseAttribute.Wisdom);
            temp.AttributePriorities.Add(BaseAttribute.Charisma);
            temp.AttributePriorities.Add(BaseAttribute.Memory);
            temp.MinimumAttributes[(int)BaseAttribute.Senses] = 18;
            temp.MinimumAttributes[(int)BaseAttribute.Agility] = 18;
            temp.MinimumAttributes[(int)BaseAttribute.Speed] = 15;
            professions.Add(temp.Key, temp);

            temp = new Profession("Rogue", "Rogue", "", 120, new UpkeepData(3, 0, 1, 0), new GameValue(40, 50));
            temp.HealthPerLevel = new GameValue(7); temp.StaminaPerLevel = new GameValue(5); temp.EssencePerLevel = new GameValue(1);
            temp.StartingItems.Add(new ItemShort("Knife", "Copper", "", "", ""));
            temp.StartingItems.Add(new ItemShort("Knife", "Copper", "", "", ""));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Slashing_Weapons, 3, 6));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Tricks, 3, 6));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Devices, 3, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Scouting, 2, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Persuasion, 2, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Survival, 2, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Shadow_Magic, 1, 0));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Light_Armor, 1, 0));
            temp.Powers.Add(new AbilityUnlock(AbilityType.Power, "Stealth", 1));
            temp.Powers.Add(new AbilityUnlock(AbilityType.Power, "Pickpocket", 1));
            temp.AttributePriorities.Add(BaseAttribute.Speed);
            temp.AttributePriorities.Add(BaseAttribute.Agility);
            temp.AttributePriorities.Add(BaseAttribute.Senses);
            temp.AttributePriorities.Add(BaseAttribute.Memory);
            temp.AttributePriorities.Add(BaseAttribute.Willpower);
            temp.AttributePriorities.Add(BaseAttribute.Intellect);
            temp.AttributePriorities.Add(BaseAttribute.Strength);
            temp.AttributePriorities.Add(BaseAttribute.Endurance);
            temp.AttributePriorities.Add(BaseAttribute.Wisdom);
            temp.AttributePriorities.Add(BaseAttribute.Charisma);
            temp.MinimumAttributes[(int)BaseAttribute.Agility] = 18;
            temp.MinimumAttributes[(int)BaseAttribute.Speed] = 18;
            temp.MinimumAttributes[(int)BaseAttribute.Senses] = 15;
            professions.Add(temp.Key, temp);

            temp = new Profession("Priest", "Priest", "", 80, new UpkeepData(2, 0, 1, 1), new GameValue(5, 10));
            temp.HealthPerLevel = new GameValue(5); temp.StaminaPerLevel = new GameValue(3); temp.EssencePerLevel = new GameValue(6);
            temp.StartingItems.Add(new ItemShort("Mace", "Copper", "", "", "")); ;
            temp.StartingItems.Add(new ItemShort("Buckler", "Wood", "", "", ""));
            temp.StartingItems.Add(new ItemShort("Leather Helm", "Leather", "", "", ""));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Persuasion, 3, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Life_Magic, 3, 6));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Medium_Armor, 3, 0));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Leadership, 2, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Shields, 2, 0));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Water_Magic, 2, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Channeling, 1, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Blunt_Weapons, 1, 0));
            temp.Spells.Add(new AbilityUnlock(AbilityType.Spell, "Bless", 1));
            temp.AttributePriorities.Add(BaseAttribute.Wisdom);
            temp.AttributePriorities.Add(BaseAttribute.Charisma);
            temp.AttributePriorities.Add(BaseAttribute.Willpower);
            temp.AttributePriorities.Add(BaseAttribute.Memory);
            temp.AttributePriorities.Add(BaseAttribute.Speed);
            temp.AttributePriorities.Add(BaseAttribute.Senses);
            temp.AttributePriorities.Add(BaseAttribute.Strength);
            temp.AttributePriorities.Add(BaseAttribute.Endurance);
            temp.AttributePriorities.Add(BaseAttribute.Agility);
            temp.AttributePriorities.Add(BaseAttribute.Intellect);
            temp.MinimumAttributes[(int)BaseAttribute.Wisdom] = 18;
            temp.MinimumAttributes[(int)BaseAttribute.Willpower] = 18;
            temp.MinimumAttributes[(int)BaseAttribute.Charisma] = 15;
            professions.Add(temp.Key, temp);

            temp = new Profession("Apprentice", "Apprentice", "", 110, new UpkeepData(1, 0, 1, 1), new GameValue(20, 30));
            temp.HealthPerLevel = new GameValue(1); temp.StaminaPerLevel = new GameValue(1); temp.EssencePerLevel = new GameValue(10);
            temp.StartingItems.Add(new ItemShort("Staff", "Wood", "", "", ""));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Channeling, 3, 6));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Fire_Magic, 2, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Air_Magic, 2, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Water_Magic, 2, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Earth_Magic, 2, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Lore, 1, 1));
            temp.SkillProficiencies.Add(new SkillProficiency(Skill.Arcane_Magic, 1, 1));
            temp.Powers.Add(new AbilityUnlock(AbilityType.Power, "Empower Spell", 1));
            temp.AttributePriorities.Add(BaseAttribute.Intellect);
            temp.AttributePriorities.Add(BaseAttribute.Memory);
            temp.AttributePriorities.Add(BaseAttribute.Speed);
            temp.AttributePriorities.Add(BaseAttribute.Willpower);
            temp.AttributePriorities.Add(BaseAttribute.Senses);
            temp.AttributePriorities.Add(BaseAttribute.Agility);
            temp.AttributePriorities.Add(BaseAttribute.Wisdom);
            temp.AttributePriorities.Add(BaseAttribute.Charisma);
            temp.AttributePriorities.Add(BaseAttribute.Strength);
            temp.AttributePriorities.Add(BaseAttribute.Endurance);
            temp.MinimumAttributes[(int)BaseAttribute.Intellect] = 18;
            temp.MinimumAttributes[(int)BaseAttribute.Memory] = 18;
            temp.MinimumAttributes[(int)BaseAttribute.Willpower] = 15;
            professions.Add(temp.Key, temp);

            //temp = new Profession("Veteran", "Veteran", "", 150, new UpkeepData(5, 0, 0, 0), new GameValue(20, 30));
            //temp.HealthPerLevel = new GameValue(12); temp.StaminaPerLevel = new GameValue(6); temp.EssencePerLevel = new GameValue(1);
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.One_Hand_Melee, 3, 12));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Heavy_Armor, 3, 10));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Shields, 3, 10));
            //temp.MinimumAttributes[(int)BaseAttribute.Strength] = 25;
            //temp.MinimumAttributes[(int)BaseAttribute.Endurance] = 20;
            //temp.MinimumAttributes[(int)BaseAttribute.Speed] = 20;
            //professions.Add(temp.Key, temp);

            //temp = new Profession("Skirmisher", "Skirmisher", "", 125, new UpkeepData(5, 0, 0, 0), new GameValue(20, 30));
            //temp.HealthPerLevel = new GameValue(8); temp.StaminaPerLevel = new GameValue(5); temp.EssencePerLevel = new GameValue(1);
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.One_Hand_Melee, 3, 12));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Two_hand_Melee, 3, 10));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Medium_Armor, 3, 10));
            //temp.MinimumAttributes[(int)BaseAttribute.Speed] = 25;
            //temp.MinimumAttributes[(int)BaseAttribute.Strength] = 20;
            //temp.MinimumAttributes[(int)BaseAttribute.Endurance] = 20;
            //professions.Add(temp.Key, temp);

            //temp = new Profession("Archer", "Archer", "", 160, new UpkeepData(5, 0, 0, 0), new GameValue(20, 30));
            //temp.HealthPerLevel = new GameValue(6); temp.StaminaPerLevel = new GameValue(6); temp.EssencePerLevel = new GameValue(1);
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Archery, 3, 12));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Medium_Armor, 3, 10));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Scouting, 3, 10));
            //temp.MinimumAttributes[(int)BaseAttribute.Speed] = 25;
            //temp.MinimumAttributes[(int)BaseAttribute.Agility] = 20;
            //temp.MinimumAttributes[(int)BaseAttribute.Senses] = 20;
            //professions.Add(temp.Key, temp);

            //temp = new Profession("Explorer", "Explorer", "", 140, new UpkeepData(5, 0, 0, 0), new GameValue(20, 30));
            //temp.HealthPerLevel = new GameValue(3); temp.StaminaPerLevel = new GameValue(5); temp.EssencePerLevel = new GameValue(1);
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Navigation, 3, 12));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Scouting, 3, 10));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Survival, 3, 10));
            //professions.Add(temp.Key, temp);

            //temp = new Profession("Burglar", "Burglar", "", 165, new UpkeepData(5, 0, 0, 0), new GameValue(20, 30));
            //temp.HealthPerLevel = new GameValue(3); temp.StaminaPerLevel = new GameValue(5); temp.EssencePerLevel = new GameValue(2);
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Stealth, 3, 12));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Devices, 3, 10));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Tricks, 3, 10));
            //temp.MinimumAttributes[(int)BaseAttribute.Speed] = 25;
            //temp.MinimumAttributes[(int)BaseAttribute.Agility] = 20;
            //temp.MinimumAttributes[(int)BaseAttribute.Senses] = 20;
            //professions.Add(temp.Key, temp);

            //temp = new Profession("Mercenary", "Mercenary", "", 150, new UpkeepData(5, 0, 0, 0), new GameValue(20, 30));
            //temp.HealthPerLevel = new GameValue(6); temp.StaminaPerLevel = new GameValue(7); temp.EssencePerLevel = new GameValue(1);
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.One_Hand_Melee, 3, 12));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Medium_Armor, 3, 10));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Stealth, 3, 10));
            //temp.MinimumAttributes[(int)BaseAttribute.Speed] = 25;
            //temp.MinimumAttributes[(int)BaseAttribute.Strength] = 20;
            //temp.MinimumAttributes[(int)BaseAttribute.Agility] = 20;
            //professions.Add(temp.Key, temp);

            //temp = new Profession("Cleric", "Cleric", "", 100, new UpkeepData(5, 0, 0, 0), new GameValue(20, 30));
            //temp.HealthPerLevel = new GameValue(8); temp.StaminaPerLevel = new GameValue(5); temp.EssencePerLevel = new GameValue(5);
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Life_Magic, 3, 12));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Persuasion, 3, 10));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Medium_Armor, 3, 10));
            //temp.MinimumAttributes[(int)BaseAttribute.Wisdom] = 25;
            //temp.MinimumAttributes[(int)BaseAttribute.Charisma] = 20;
            //temp.MinimumAttributes[(int)BaseAttribute.Willpower] = 20;
            //professions.Add(temp.Key, temp);

            //temp = new Profession("Druid", "Druid", "", 85, new UpkeepData(5, 0, 0, 0), new GameValue(20, 30));
            //temp.HealthPerLevel = new GameValue(6); temp.StaminaPerLevel = new GameValue(4); temp.EssencePerLevel = new GameValue(6);
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Water_Magic, 3, 12));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Survival, 3, 10));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Scouting, 3, 10));
            //temp.MinimumAttributes[(int)BaseAttribute.Wisdom] = 25;
            //temp.MinimumAttributes[(int)BaseAttribute.Willpower] = 20;
            //temp.MinimumAttributes[(int)BaseAttribute.Senses] = 20;
            //professions.Add(temp.Key, temp);

            //temp = new Profession("Wizard", "Wizard", "", 175, new UpkeepData(5, 0, 0, 0), new GameValue(20, 30));
            //temp.HealthPerLevel = new GameValue(2); temp.StaminaPerLevel = new GameValue(2); temp.EssencePerLevel = new GameValue(10);
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Channeling, 3, 12));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Arcane_Magic, 3, 10));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Lore, 3, 10));
            //temp.MinimumAttributes[(int)BaseAttribute.Intellect] = 25;
            //temp.MinimumAttributes[(int)BaseAttribute.Memory] = 20;
            //temp.MinimumAttributes[(int)BaseAttribute.Speed] = 20;
            //professions.Add(temp.Key, temp);

            //temp = new Profession("Elementalist", "Elementalist", "", 175, new UpkeepData(5, 0, 0, 0), new GameValue(20, 30));
            //temp.HealthPerLevel = new GameValue(2); temp.StaminaPerLevel = new GameValue(2); temp.EssencePerLevel = new GameValue(10);
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Fire_Magic, 3, 10));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Air_Magic, 3, 10));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Water_Magic, 3, 10));
            //temp.SkillProficiencies.Add(new SkillProficiency(Skill.Earth_Magic, 3, 10));
            //temp.MinimumAttributes[(int)BaseAttribute.Intellect] = 25;
            //temp.MinimumAttributes[(int)BaseAttribute.Memory] = 20;
            //temp.MinimumAttributes[(int)BaseAttribute.Willpower] = 20;
            //professions.Add(temp.Key, temp);
        }

        static void LoadRaces()
        {
            Race race = new Race("Halfling", "Halfling", "Halfling Male", "Halfling Female", true, "", "", true, false,
                new GameValue(1, 10), new GameValue(1, 10), new GameValue(1, 10), 10, 1.0f, new UpkeepData(0, 12, 0, 0), new GameValue(1, 5),
                new Vector3(0.7f, 0.8f, 0.8f), "Hair 1", "", "Hair 1", "");
            race.StartingAttributes[(int)BaseAttribute.Endurance].Number = 2;
            race.StartingAttributes[(int)BaseAttribute.Agility].Number = 4;
            race.StartingAttributes[(int)BaseAttribute.Charisma].Number = 4;
            race.StartingAttributes[(int)BaseAttribute.Memory].Number = 2;
            races.Add(race.Key, race);

            race = new Race("High Elf", "High Elf", "High Elf Male", "High Elf Female", false, "", "", true, true,
                new GameValue(1, 10), new GameValue(1, 10), new GameValue(1, 10), 10, 0.5f, new UpkeepData(0, 5, 0, 0), new GameValue(1, 5),
                new Vector3(0.65f, 0.9f, 0.8f), "Hair 1", "", "Hair 1", "");
            race.StartingAttributes[(int)BaseAttribute.Endurance].Number = -2;
            race.StartingAttributes[(int)BaseAttribute.Agility].Number = 4;
            race.StartingAttributes[(int)BaseAttribute.Intellect].Number = 4;
            race.StartingAttributes[(int)BaseAttribute.Memory].Number = 2;
            races.Add(race.Key, race);

            race = new Race("Mountain Dwarf", "Mountain Dwarf", "Mountain Dwarf Male", "Mountain Dwarf Female", true, "", "", true, true,
                new GameValue(1, 10), new GameValue(1, 10), new GameValue(1, 10), 9, 1.0f, new UpkeepData(0, 8, 0, 0), new GameValue(1, 5),
                new Vector3(1, 0.75f, 1.1f), "Hair 1", "Beard 1", "Hair 1", "");
            race.StartingAttributes[(int)BaseAttribute.Strength].Number = 4;
            race.StartingAttributes[(int)BaseAttribute.Endurance].Number = 4;
            race.StartingAttributes[(int)BaseAttribute.Agility].Number = -4;
            race.Traits.Add(new AbilityUnlock(AbilityType.Trait, "Dark Vision", 1));
            races.Add(race.Key, race);

            race = new Race("Imperial", "Imperial", "Imperial Male", "Imperial Female", false, "", "", true, true,
                new GameValue(1, 10), new GameValue(1, 10), new GameValue(1, 10), 10, 1.1f, new UpkeepData(0, 7, 0, 0), new GameValue(1, 5),
                new Vector3(1, 1, 1), "Hair 1", "Beard 1", "Hair 1", "");
            race.Traits.Add(new AbilityUnlock(AbilityType.Trait, "Fast Learner", 1));
            races.Add(race.Key, race);

            race = new Race("Half Ogre", "Half Ogre", "Half Ogre Male", "Half Ogre Female", false, "", "", true, true,
                new GameValue(1, 10), new GameValue(1, 10), new GameValue(1, 10), 10, 1.1f, new UpkeepData(0, 7, 0, 0), new GameValue(1, 5),
                new Vector3(1, 1, 1), "Hair 1", "Beard 1", "Hair 1", "");
            race.StartingAttributes[(int)BaseAttribute.Strength].Number = 8;
            race.StartingAttributes[(int)BaseAttribute.Endurance].Number = 4;
            race.StartingAttributes[(int)BaseAttribute.Agility].Number = -5;
            race.StartingAttributes[(int)BaseAttribute.Speed].Number = -5;
            race.StartingAttributes[(int)BaseAttribute.Intellect].Number = -5;
            race.Traits.Add(new AbilityUnlock(AbilityType.Trait, "Fast Learner", 1));
            races.Add(race.Key, race);
        }

        static void LoadTraits()
        {
            Ability ability = new Ability("Regeneration", "Regeneration", "trait", AbilityClass.None, AbilityType.Trait);
            ability.Components.Add(new TraitTypeComponent(TraitType.Misc));
            ability.Components.Add(new DurationComponent(DurationType.Permanent));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Fast Learner", "Fast Learner", "trait", AbilityClass.None, AbilityType.Trait);
            ability.Components.Add(new TraitTypeComponent(TraitType.Misc));
            ability.Components.Add(new DurationComponent(DurationType.Permanent));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Undead", "Undead", "trait", AbilityClass.None, AbilityType.Trait);
            ability.Components.Add(new TraitTypeComponent(TraitType.Misc));
            ability.Components.Add(new DurationComponent(DurationType.Permanent));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Camoflage", "Camoflage", "trait", AbilityClass.None, AbilityType.Trait);
            ability.Components.Add(new TraitTypeComponent(TraitType.Misc));
            ability.Components.Add(new DurationComponent(DurationType.Permanent));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Dark Vision", "Dark Vision", "trait", AbilityClass.None, AbilityType.Trait);
            ability.Components.Add(new TraitTypeComponent(TraitType.Misc));
            ability.Components.Add(new DurationComponent(DurationType.Permanent));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Flight", "Flight", "trait", AbilityClass.None, AbilityType.Trait, 0);
            ability.Components.Add(new TraitTypeComponent(TraitType.Misc));
            ability.Components.Add(new DurationComponent(DurationType.Permanent));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Strong", "Strong", "trait", AbilityClass.None, AbilityType.Trait, 0);
            ability.Components.Add(new TraitTypeComponent(TraitType.Misc));
            ability.Components.Add(new DurationComponent(DurationType.Permanent));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Scaly Hide", "Scaly Hide", "trait", AbilityClass.None, AbilityType.Trait, 5f, 0, Skill.None);
            ability.Components.Add(new TraitTypeComponent(TraitType.Misc));
            ability.Components.Add(new DurationComponent(DurationType.Permanent));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Weak", "Weak", "trait", AbilityClass.None, AbilityType.Trait);
            ability.Components.Add(new TraitTypeComponent(TraitType.Misc));
            ability.Components.Add(new DurationComponent(DurationType.Permanent));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Crippled", "Crippled", "trait", AbilityClass.None, AbilityType.Trait);
            ability.Components.Add(new TraitTypeComponent(TraitType.Wound));
            ability.Components.Add(new DurationComponent(DurationType.Permanent));
            abilities.Add(ability.Key, ability);
        }

        static void LoadPowers()
        {
            Ability ability = new Ability("Strike", "Strike", "strike", AbilityClass.Encounter, AbilityType.Power, 1f, 0, Skill.Slashing_Weapons, 1);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Power Strike", "Power Strike", "power strike", AbilityClass.Encounter, AbilityType.Power, 5f, 1000, Skill.Slashing_Weapons, 5);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Reckless Strike", "Reckless Strike", "reckless strike", AbilityClass.Encounter, AbilityType.Power, 5f, 1000, Skill.Slashing_Weapons, 5);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Whirlwind", "Whirlwind", "whirlwind", AbilityClass.Encounter, AbilityType.Power, 5f, 1000, Skill.Slashing_Weapons, 10);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Sphere));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Long Strike", "Long Strike", "long strike", AbilityClass.Encounter, AbilityType.Power, 5f, 0, Skill.Piercing_Weapons, 1);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 3));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Beam));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Piercing Strike", "Piercing Strike", "piercing strike", AbilityClass.Encounter, AbilityType.Power, 5f, 1000, Skill.Piercing_Weapons, 5);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 3));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Beam));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Stunning Blow", "Stunning Blow", "stunning blow", AbilityClass.Encounter, AbilityType.Power, 5f, 1000, Skill.Blunt_Weapons, 5);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);


            ability = new Ability("Taunt", "Taunt", "taunt", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Provoke", "Provoke", "provoke", AbilityClass.Encounter, AbilityType.Power, 5f, 1000);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 2));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Sphere));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Stealth", "Stealth", "stealth", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Pickpocket", "Pickpocket", "pickpocket", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Empower Spell", "Empower Spell", "metamagic empower", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Overcharge Spell", "Overcharge Spell", "metamagic overcharge", AbilityClass.Encounter, AbilityType.Power, 5f, 1000);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Metamagic - Quicken", "Metamagic - Quicken", "metamagic quicken", AbilityClass.Encounter, AbilityType.Power, 5f, 1000);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Eagle Eye", "Eagle Eye", "eagle eye", AbilityClass.Encounter, AbilityType.Power, 5f, 1000);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Repair", "Repair", "repair", AbilityClass.Encounter, AbilityType.Power, 5f, 1000, Skill.None, 5);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Rebuild", "Rebuild", "rebuild", AbilityClass.Encounter, AbilityType.Power, 5f, 1000, Skill.None, 5);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Careful Strike", "Careful Strike", "careful strike", AbilityClass.Encounter, AbilityType.Power, 5f, 1000, Skill.Slashing_Weapons, 5);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Shoot", "Shoot", "shoot", AbilityClass.Encounter, AbilityType.Power, 5f, 0, Skill.Archery, 1);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Accurate Shot", "Accurate Shot", "accurate shot", AbilityClass.Encounter, AbilityType.Power, 5f, 1000, Skill.Archery, 5);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Fire", "Fire", "fire", AbilityClass.Encounter, AbilityType.Power, 5f, 0, Skill.Firearms, 1);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Throw", "Throw", "throw", AbilityClass.Encounter, AbilityType.Power, 5f, 0, Skill.Thrown, 1);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Trick Throw", "Trick Throw", "trick throw", AbilityClass.Encounter, AbilityType.Power, 5f, 1000, Skill.Tricks, 5);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Point Blank Shot", "Point Blank Shot", "point blank shot", AbilityClass.Encounter, AbilityType.Power, 5f, 1000, Skill.Firearms, 5);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Weapon));
            ability.Components.Add(new TargetComponent(TargetType.Enemy));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Bestial Rage", "Bestial Rage", "bestial rage", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Self));
            ability.Components.Add(new TargetComponent(TargetType.Friend));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Buckler Block", "Buckler Block", "buckler block", AbilityClass.Encounter, AbilityType.Power, 5f, 1000, Skill.Bucklers, 1);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Self));
            ability.Components.Add(new TargetComponent(TargetType.Friend));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Shield Block", "Shield Block", "shield block", AbilityClass.Encounter, AbilityType.Power, 5f, 1000, Skill.Shields, 1);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Self));
            ability.Components.Add(new TargetComponent(TargetType.Friend));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Rally", "Rally", "rally", AbilityClass.Encounter, AbilityType.Power, 5f, 1000, Skill.Leadership, 1);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.Turn, 5));
            ability.Components.Add(new RangeComponent(RangeType.Self));
            ability.Components.Add(new TargetComponent(TargetType.Friend));
            ability.Components.Add(new AreaComponent(AreaType.Single));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Fire Breath", "Fire Breath", "", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 5));
            ability.Components.Add(new AreaComponent(AreaType.Cone));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Lava Breath", "Lava Breath", "", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 5));
            ability.Components.Add(new AreaComponent(AreaType.Cone));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Lightning Breath", "Lightning Breath", "", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 5));
            ability.Components.Add(new AreaComponent(AreaType.Cone));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Ice Breath", "Ice Breath", "", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 5));
            ability.Components.Add(new AreaComponent(AreaType.Cone));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Poison Breath", "Poison Breath", "", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 5));
            ability.Components.Add(new AreaComponent(AreaType.Cone));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Acid Breath", "Acid Breath", "", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 5));
            ability.Components.Add(new AreaComponent(AreaType.Cone));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Holy Breath", "Holy Breath", "", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 5));
            ability.Components.Add(new AreaComponent(AreaType.Cone));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Unholy Breath", "Unholy Breath", "", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 5));
            ability.Components.Add(new AreaComponent(AreaType.Cone));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Shadow Breath", "Shadow Breath", "", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 5));
            ability.Components.Add(new AreaComponent(AreaType.Cone));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Arcane Breath", "Arcane Breath", "", AbilityClass.Encounter, AbilityType.Power, 5f);
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Stamina, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 5));
            ability.Components.Add(new AreaComponent(AreaType.Cone));
            abilities.Add(ability.Key, ability);
        }

        static void LoadSpells()
        {
            Ability ability = new Ability("Torchlight", "Torchlight", "torchlight", AbilityClass.Encounter, AbilityType.Spell, 5f, 0, Skill.Fire_Magic, 1);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Fire, 1));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new TargetComponent(TargetType.Self, 1));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Firebolt", "Firebolt", "firebolt", AbilityClass.Encounter, AbilityType.Spell, 5f, 1000, Skill.Fire_Magic, 5);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Fire, 1));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 5));
            ability.Components.Add(new TargetComponent(TargetType.Enemy, 1));
            ability.Effects.Add(new DamageEffect(DamageType.Fire, (int)DerivedAttribute.Health, new GameValue(1, 6), GameValue.Zero, 0, 0, 0f, 3));
            ability.Effects.Add(new DamageEffect(DamageType.Fire, (int)DerivedAttribute.Health, new GameValue(1), new GameValue(1, 3), 0, 0, 0f, 3));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Fireball", "Fireball", "fireball", AbilityClass.Encounter, AbilityType.Spell, 5f, 1000, Skill.Fire_Magic, 10);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Fire, 3));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 20));
            ability.Components.Add(new DurationComponent(DurationType.Instant));
            ability.Components.Add(new CooldownComponent(TimeType.Hour, 1));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 10));
            ability.Components.Add(new TargetComponent(TargetType.Any));
            ability.Components.Add(new AreaComponent(AreaType.Sphere, 3));
            ability.Effects.Add(new DamageEffect(DamageType.Fire, (int)DerivedAttribute.Health, new GameValue(2, 4), GameValue.Zero, 0, 0, 0f, 3));
            ability.Effects.Add(new DamageEffect(DamageType.Fire, (int)DerivedAttribute.Health, new GameValue(1, 2), new GameValue(1, 4), 0, 0, 0f, 3));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Hasten", "Hasten", "hasten", AbilityClass.Encounter, AbilityType.Spell, 5f, 0, Skill.Air_Magic, 1);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Fire, 1));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new TargetComponent(TargetType.Self, 1));
            //ability.Effects.Add(new RestoreEffect(RestoreType.Actions, new GameValue(2, 6), GameValue.Zero, true, 3));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Shockbolt", "Shockbolt", "shockbolt", AbilityClass.Encounter, AbilityType.Spell, 5f, 1000, Skill.Air_Magic, 5);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Air, 1));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 5));
            ability.Components.Add(new TargetComponent(TargetType.Enemy, 1));
            ability.Effects.Add(new DamageEffect(DamageType.Shock, (int)DerivedAttribute.Health, new GameValue(1, 4), GameValue.Zero, 0, 0, 0f, 3));
            ability.Effects.Add(new DamageEffect(DamageType.Shock, (int)DerivedAttribute.Stamina, new GameValue(1, 6), GameValue.Zero, 0, 0, 0f, 3));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Lightning Bolt", "Lightning Bolt", "lightning bolt", AbilityClass.Encounter, AbilityType.Spell, 5f, 1000, Skill.Air_Magic, 10);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Air, 3));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 25));
            ability.Components.Add(new DurationComponent(DurationType.Instant, TimeType.None));
            ability.Components.Add(new CooldownComponent(TimeType.None));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 5));
            ability.Components.Add(new TargetComponent(TargetType.Any));
            ability.Components.Add(new AreaComponent(AreaType.Beam, 10));
            ability.Effects.Add(new DamageEffect(DamageType.Shock, (int)DerivedAttribute.Health, new GameValue(1, 6), GameValue.Zero, 0, 0, 0f, 3));
            ability.Effects.Add(new DamageEffect(DamageType.Shock, (int)DerivedAttribute.Stamina, new GameValue(1, 6), GameValue.Zero, 0, 0, 0f, 3));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Armor!", "Armor!", "armor!", AbilityClass.Encounter, AbilityType.Spell, 5f, 1000, Skill.Earth_Magic, 1);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Earth, 1));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant));
            ability.Components.Add(new CooldownComponent(TimeType.Hour, 1));
            ability.Components.Add(new RangeComponent(RangeType.Touch));
            ability.Components.Add(new TargetComponent(TargetType.Friend));
            //ability.Effects.Add(new RestoreEffect(RestoreType.Armor, new GameValue(1, 4), GameValue.Zero, true, 3));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Stone Skin", "Stone Skin", "stone skin", AbilityClass.Encounter, AbilityType.Spell, 5f, 1000, Skill.Earth_Magic, 5);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Earth, 1));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant));
            ability.Components.Add(new CooldownComponent(TimeType.Hour, 1));
            ability.Components.Add(new RangeComponent(RangeType.Touch));
            ability.Components.Add(new TargetComponent(TargetType.Friend));
            ability.Effects.Add(new AlterCharacteristicEffect(AttributeType.Derived, (int)DerivedAttribute.Armor, new GameValue(1, 4), new GameValue(2, 6)));
            ability.Effects.Add(new AlterCharacteristicEffect(AttributeType.Resistance, (int)DamageType.Physical, new GameValue(10), new GameValue(2, 6)));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Lesser Regen", "Lesser Regen", "lesser regen", AbilityClass.Encounter, AbilityType.Spell, 5f, 0, Skill.Water_Magic, 1);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Water, 2));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 15));
            ability.Components.Add(new DurationComponent(DurationType.Duration));
            ability.Components.Add(new CooldownComponent(TimeType.Hour, 1));
            ability.Components.Add(new RangeComponent(RangeType.Touch));
            ability.Components.Add(new TargetComponent(TargetType.Friend, 1));
            //ability.Effects.Add(new RestoreEffect(RestoreType.Health, new GameValue(1, 4), new GameValue(2, 6), false, 5));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Lesser Drain", "Lesser Drain", "lesser drain", AbilityClass.Encounter, AbilityType.Spell, 5f, 1000, Skill.Death_Magic, 5);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Death, 1));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 10));
            ability.Components.Add(new DurationComponent(DurationType.Duration));
            ability.Components.Add(new CooldownComponent(TimeType.Hour, 1));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 10));
            ability.Components.Add(new TargetComponent(TargetType.Friend, 1));
            ability.Effects.Add(new DamageEffect(DamageType.Unholy, (int)DerivedAttribute.Health, new GameValue(1, 6), GameValue.Zero, 0, 0, 0f, 3));
            //ability.Effects.Add(new RestoreEffect(RestoreType.Health, new GameValue(1, 6), GameValue.Zero, true, 3));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Curse", "Curse", "curse", AbilityClass.Encounter, AbilityType.Spell, 5f, 1000, Skill.Death_Magic, 1);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Death, 1));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant));
            ability.Components.Add(new CooldownComponent(TimeType.Hour, 1));
            ability.Components.Add(new TargetComponent(TargetType.Friend));
            ability.Components.Add(new AreaComponent(AreaType.Sphere, 10));
            ability.Effects.Add(new AlterCharacteristicEffect(AttributeType.Derived, (int)DerivedAttribute.Might_Attack, new GameValue(-1, -6), new GameValue(2, 6)));
            ability.Effects.Add(new AlterCharacteristicEffect(AttributeType.Derived, (int)DerivedAttribute.Finesse_Attack, new GameValue(-1, -6), new GameValue(2, 6)));
            //ability.Effects.Add(new RestoreEffect(RestoreType.Morale, new GameValue(-1, -4), GameValue.Zero, false, 2));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Lesser Heal", "Lesser Heal", "lesser heal", AbilityClass.Encounter, AbilityType.Spell, 5f, 0, Skill.Life_Magic, 1);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Life, 1));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 10));
            ability.Components.Add(new DurationComponent(DurationType.Duration));
            ability.Components.Add(new CooldownComponent(TimeType.Hour, 1));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 10));
            ability.Components.Add(new TargetComponent(TargetType.Friend, 1));
            //ability.Effects.Add(new RestoreEffect(RestoreType.Health, new GameValue(1, 6), GameValue.Zero, false, 2));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Lesser Courage", "Lesser Courage", "lesser courage", AbilityClass.Encounter, AbilityType.Spell, 5f, 1000, Skill.Life_Magic, 5);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Life, 2));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 10));
            ability.Components.Add(new DurationComponent(DurationType.Duration));
            ability.Components.Add(new CooldownComponent(TimeType.Hour, 1));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 10));
            ability.Components.Add(new TargetComponent(TargetType.Friend, 1));
            //ability.Effects.Add(new RestoreEffect(RestoreType.Morale, new GameValue(1, 6), GameValue.Zero, false, 2));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Bless", "Bless", "bless", AbilityClass.Encounter, AbilityType.Spell, 5f, 1000, Skill.Life_Magic, 1);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Life, 1));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 10));
            ability.Components.Add(new DurationComponent(DurationType.Instant));
            ability.Components.Add(new CooldownComponent(TimeType.Hour, 1));
            ability.Components.Add(new TargetComponent(TargetType.Friend));
            ability.Components.Add(new AreaComponent(AreaType.Sphere, 10));
            ability.Effects.Add(new AlterCharacteristicEffect(AttributeType.Derived, (int)DerivedAttribute.Might_Attack, new GameValue(1, 4), new GameValue(2, 6)));
            ability.Effects.Add(new AlterCharacteristicEffect(AttributeType.Derived, (int)DerivedAttribute.Finesse_Attack, new GameValue(1, 4), new GameValue(2, 6)));
            //ability.Effects.Add(new RestoreEffect(RestoreType.Morale, new GameValue(1, 4), GameValue.Zero, false, 2));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Mirror Image", "Mirror Image", "mirror image", AbilityClass.World, AbilityType.Spell, 5f, 1000, Skill.Shadow_Magic, 5);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Shadow, 1));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 20));
            ability.Components.Add(new DurationComponent(DurationType.Instant));
            ability.Components.Add(new CooldownComponent(TimeType.Minute, 1));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Detect Illusion", "Detect Illusion", "detect illusion", AbilityClass.World, AbilityType.Spell, 5f, 1000, Skill.Shadow_Magic, 1);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Shadow, 1));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 20));
            ability.Components.Add(new DurationComponent(DurationType.Instant));
            ability.Components.Add(new CooldownComponent(TimeType.Minute, 1));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Arcane Missile", "Arcane Missile", "arcane missile", AbilityClass.Encounter, AbilityType.Spell, 5f, 1000, Skill.Arcane_Magic, 5);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Arcane, 1));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 20));
            ability.Components.Add(new DurationComponent(DurationType.Instant));
            ability.Components.Add(new CooldownComponent(TimeType.Hour, 1));
            ability.Components.Add(new RangeComponent(RangeType.Distance, 10));
            ability.Components.Add(new TargetComponent(TargetType.Any));
            ability.Components.Add(new AreaComponent(AreaType.Sphere, 3));
            ability.Effects.Add(new DamageEffect(DamageType.Arcane, (int)DerivedAttribute.Health, new GameValue(1, 4), GameValue.Zero, 0, 0, 0f, 3));
            ability.Effects.Add(new DamageEffect(DamageType.Arcane, (int)DerivedAttribute.Essence, new GameValue(1, 4), GameValue.Zero, 0, 0, 0f, 3));
            abilities.Add(ability.Key, ability);

            ability = new Ability("Identify", "Identify", "identify", AbilityClass.World, AbilityType.Spell, 5f, 1000, Skill.Arcane_Magic, 1);
            ability.Components.Add(new SpellLevelComponent(SpellSchoolType.Arcane, 1));
            ability.Components.Add(new ResourceComponent(DerivedAttribute.Essence, 20));
            ability.Components.Add(new DurationComponent(DurationType.Instant));
            ability.Components.Add(new CooldownComponent(TimeType.Minute, 1));
            abilities.Add(ability.Key, ability);
        }

        static void LoadAbilities()
        {
            Ability ability = new Ability("Blank", "Blank", "blank", AbilityClass.None, AbilityType.None);
            abilities.Add(ability.Key, ability);

            LoadQuirks();
            LoadTraits();
            LoadPowers();
            LoadSpells();
        }

        public static void LoadAbilityModifiers()
        {
            AbilityModifier rune = new AbilityModifier("Weak", "Weak");
            rune.Modifiers.Add(new ResourceModifier(-10));
            rune.Modifiers.Add(new DurationModifier(-2));
            rune.Modifiers.Add(new CooldownModifier(-2));
            rune.Modifiers.Add(new RangeModifier(-2));
            rune.Modifiers.Add(new AreaModifier(-2, -15));

            runes.Add(rune.Key, rune);

            rune = new AbilityModifier("Dim", "Dim");
            rune.Modifiers.Add(new ResourceModifier(-5));
            rune.Modifiers.Add(new DurationModifier(-1));
            rune.Modifiers.Add(new CooldownModifier(-1));
            rune.Modifiers.Add(new RangeModifier(-1));
            rune.Modifiers.Add(new AreaModifier(-1, -10));

            runes.Add(rune.Key, rune);

            rune = new AbilityModifier("Empowered", "Empowered");
            rune.Modifiers.Add(new ResourceModifier(10));
            rune.Modifiers.Add(new DurationModifier(2));
            rune.Modifiers.Add(new CooldownModifier(2));
            rune.Modifiers.Add(new RangeModifier(2));
            rune.Modifiers.Add(new AreaModifier(2, 15));

            runes.Add(rune.Key, rune);

            rune = new AbilityModifier("Devastating", "Devastating");
            rune.Modifiers.Add(new ResourceModifier(100));
            rune.Modifiers.Add(new DurationModifier(5));
            rune.Modifiers.Add(new CooldownModifier(5));
            rune.Modifiers.Add(new RangeModifier(5));
            rune.Modifiers.Add(new AreaModifier(5, 15));

            runes.Add(rune.Key, rune);
        }

        public static void LoadAttributes()
        {
            AttributeDefinition bA = new AttributeDefinition("Strength", "Strength", "Str", "", 0, 999, AttributeDefinitionType.Base, null);
            baseAttributeDefinitions.Add(bA);

            bA = new AttributeDefinition("Endurance", "Endurance", "End", "", 0, 999, AttributeDefinitionType.Base, null);
            baseAttributeDefinitions.Add(bA);

            bA = new AttributeDefinition("Agility", "Agility", "Agi", "", 0, 999, AttributeDefinitionType.Base, null);
            baseAttributeDefinitions.Add(bA);

            bA = new AttributeDefinition("Speed", "Speed", "Spd", "", 0, 999, AttributeDefinitionType.Base, null);
            baseAttributeDefinitions.Add(bA);

            bA = new AttributeDefinition("Senses", "Senses", "Sns", "", 0, 999, AttributeDefinitionType.Base, null);
            baseAttributeDefinitions.Add(bA);

            bA = new AttributeDefinition("Intellect", "Intellect", "Int", "", 0, 999, AttributeDefinitionType.Base, null);
            baseAttributeDefinitions.Add(bA);

            bA = new AttributeDefinition("Wisdom", "Wisdom", "Wis", "", 0, 999, AttributeDefinitionType.Base, null);
            baseAttributeDefinitions.Add(bA);

            bA = new AttributeDefinition("Willpower", "Willpower", "Wil", "", 0, 999, AttributeDefinitionType.Base, null);
            baseAttributeDefinitions.Add(bA);

            bA = new AttributeDefinition("Charisma", "Charisma", "Cha", "", 0, 999, AttributeDefinitionType.Base, null);
            baseAttributeDefinitions.Add(bA);

            bA = new AttributeDefinition("Memory", "Memory", "Mem", "", 0, 999, AttributeDefinitionType.Base, null);
            baseAttributeDefinitions.Add(bA);

            AttributeDefinition dA = new AttributeDefinition("Actions", "Actions", "Act", "", 0, 999, AttributeDefinitionType.Derived_Points, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Agility),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Senses), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Movement", "Movement", "Mov", "", 0, 999, AttributeDefinitionType.Derived_Points, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Speed),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Willpower), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Armor", "Armor", "Arm", "", 0, 999, AttributeDefinitionType.Derived_Points, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Value, 0), null, null,
                    AttributeCalculationOpperator.None,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Health", "Health", "Hp", "", 0, 999, AttributeDefinitionType.Derived_Points, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Strength),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Endurance), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Stamina", "Stamina", "Sta", "", 0, 999, AttributeDefinitionType.Derived_Points, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Endurance),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Willpower), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Essence", "Essence", "Ess", "", 0, 999, AttributeDefinitionType.Derived_Points, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Intellect),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Memory), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Morale", "Morale", "Mor", "", 0, 999, AttributeDefinitionType.Derived_Points, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Value, 100), null, null,
                    AttributeCalculationOpperator.None,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Might Attack", "Might Att", "Matt", "", 0, 999, AttributeDefinitionType.Derived_Score, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Strength),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Agility), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Might Damage", "Might Dmg", "Mdmg", "", -1000, 1000, AttributeDefinitionType.Derived_Percent, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Strength),
                    new AttributeModifier(AttributeModifierType.Value, 12), null,
                    AttributeCalculationOpperator.Subtract,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Finesse Attack", "Finesse Att", "Fatt", "", 0, 999, AttributeDefinitionType.Derived_Score, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Agility),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Senses), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Finesse Damage", "Finesse Dmg", "FDmg", "", -1000, 1000, AttributeDefinitionType.Derived_Percent, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Agility),
                    new AttributeModifier(AttributeModifierType.Value, 12), null,
                    AttributeCalculationOpperator.Subtract,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Spell Attack", "Spell Att", "Satt", "", 1, 100, AttributeDefinitionType.Derived_Score, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Intellect),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Wisdom), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Spell Power", "Spell Pow", "Spow", "", -1000, 1000, AttributeDefinitionType.Derived_Percent, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Intellect),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Memory), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Block", "Block", "Blk", "", 0, 999, AttributeDefinitionType.Derived_Score, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Endurance),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Agility), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Dodge", "Dodge", "Ddg", "", 0, 999, AttributeDefinitionType.Derived_Score, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Agility),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Senses), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Parry", "Parry", "Par", "", 0, 999, AttributeDefinitionType.Derived_Score, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Agility),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Speed), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Resistance", "Resistance", "Res", "", 0, 99, AttributeDefinitionType.Derived_Percent, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Endurance),
                    new AttributeModifier(AttributeModifierType.Value, 20), null,
                    AttributeCalculationOpperator.Subtract,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Initiative", "Initiative", "Ini", "", 0, 99, AttributeDefinitionType.Derived_Score, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Agility),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Speed), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Perception", "Perception", "Per", "", 0, 99, AttributeDefinitionType.Derived_Score, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Senses), null, null,
                    AttributeCalculationOpperator.None,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Concentration", "Concentration", "Con", "", 0, 999, AttributeDefinitionType.Derived_Score, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Willpower),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Memory), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);


            dA = new AttributeDefinition("Bonus Actions", "Bonus Actions", "Bact", "", -1000, 1000, AttributeDefinitionType.Derived_Percent, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Speed),
                    new AttributeModifier(AttributeModifierType.Value, 20),
                    new AttributeModifier(AttributeModifierType.Value, 1),
                    AttributeCalculationOpperator.Subtract,
                    AttributeCalculationOpperator.Multiply_Neg));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Duration Modifier", "Duration Mod", "Dur", "", -100, 100, AttributeDefinitionType.Derived_Percent, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Wisdom),
                    new AttributeModifier(AttributeModifierType.Value, 20), null,
                    AttributeCalculationOpperator.Subtract,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Range Modifier", "Range Mod", "Rng", "", -100, 100, AttributeDefinitionType.Derived_Percent, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Senses),
                    new AttributeModifier(AttributeModifierType.Value, 20), null,
                    AttributeCalculationOpperator.Subtract,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);



            dA = new AttributeDefinition("Fumble", "Fumble", "Fum", "", 1, 20, AttributeDefinitionType.Derived_Score, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Value, 10), null, null,
                    AttributeCalculationOpperator.None,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Critical Hit", "Crit", "CH", "", 75, 99, AttributeDefinitionType.Derived_Score, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Value, 90), null, null,
                    AttributeCalculationOpperator.None,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);

            dA = new AttributeDefinition("Critical Dmg", "Crit Dmg", "CD", "", 0, 1000, AttributeDefinitionType.Derived_Percent, new AttributeCalculation(
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Speed),
                    new AttributeModifier(AttributeModifierType.Base_Attribute, (int)BaseAttribute.Senses), null,
                    AttributeCalculationOpperator.Add,
                    AttributeCalculationOpperator.None));
            derivedAttributeDefinitions.Add(dA);


            AttributeDefinition res = new AttributeDefinition("Physical", "Physical", "Phy", "", 0, 9999, AttributeDefinitionType.Resistance, null);
            damageTypeDefinitions.Add(res);

            res = new AttributeDefinition("Fire", "Fire", "Fir", "", 0, 9999, AttributeDefinitionType.Resistance, null);
            damageTypeDefinitions.Add(res);

            res = new AttributeDefinition("Cold", "Cold", "cld", "", 0, 9999, AttributeDefinitionType.Resistance, null);
            damageTypeDefinitions.Add(res);

            res = new AttributeDefinition("Shock", "Shock", "Shk", "", 0, 9999, AttributeDefinitionType.Resistance, null);
            damageTypeDefinitions.Add(res);

            res = new AttributeDefinition("Poison", "Poison", "Poi", "", 0, 9999, AttributeDefinitionType.Resistance, null);
            damageTypeDefinitions.Add(res);

            res = new AttributeDefinition("Acid", "Acid", "Acd", "", 0, 9999, AttributeDefinitionType.Resistance, null);
            damageTypeDefinitions.Add(res);

            res = new AttributeDefinition("Unholy", "Unholy", "Unh", "", 0, 9999, AttributeDefinitionType.Resistance, null);
            damageTypeDefinitions.Add(res);

            res = new AttributeDefinition("Holy", "Holy", "Hly", "", 0, 9999, AttributeDefinitionType.Resistance, null);
            damageTypeDefinitions.Add(res);

            res = new AttributeDefinition("Psychic", "Psychic", "Psy", "", 0, 9999, AttributeDefinitionType.Resistance, null);
            damageTypeDefinitions.Add(res);

            res = new AttributeDefinition("Arcane", "Arcane", "Arc", "", 0, 9999, AttributeDefinitionType.Resistance, null);
            damageTypeDefinitions.Add(res);
        }

        public static void LoadSkills()
        {
            SkillDefinition skill = new SkillDefinition(SkillCategory.Combat, Skill.Slashing_Weapons, "Slashing Weapons", "Slash", "SlW", "", "strength", 0, 999,
                new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Combat, Skill.Piercing_Weapons, "Piercing Weapons", "Pierce", "PiW", "", "strength", 0, 999,
                new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Combat, Skill.Blunt_Weapons, "Blunt Weapons", "Blunt", "BlW", "", "strength", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Combat, Skill.Unarmed, "Unarmed", "Unarmed", "Una", "", "strength", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Combat, Skill.Thrown, "Thrown", "Thrown", "Thr", "", "dexterity", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Combat, Skill.Archery, "Archery", "Archery", "Arc", "", "dexterity", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Combat, Skill.Firearms, "Firearms", "Firarm", "Fir", "", "dexterity", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Combat, Skill.Explosives, "Explosives", "Explosive", "Exp", "", "dexterity", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Combat, Skill.Light_Armor, "Light Armor", "L Armor", "LAr", "", "agility", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Combat, Skill.Medicine, "Medium Armor", "M Armor", "MAr", "", "endurance", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Combat, Skill.Heavy_Armor, "Heavy Armor", "H Armor", "HAr", "", "endurance", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Combat, Skill.Bucklers, "Bucklers", "Buckler", "Buc", "", "agility", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Combat, Skill.Shields, "Shields", "Shield", "Shi", "", "endurance", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Combat, Skill.Leadership, "Leadership", "Leader", "Lea", "", "charisma", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Combat, Skill.Tactics, "Tactics", "Tactics", "Tac", "", "intellect", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Magic, Skill.Firearms, "Fire Magic", "Fire", "FMa", "", "intellect", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Magic, Skill.Air_Magic, "Air Magic", "Air", "AMa", "", "intellect", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Magic, Skill.Water_Magic, "Water Magic", "Water", "WMa", "", "intellect", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Magic, Skill.Earth_Magic, "Earth Magic", "Earth", "EMa", "", "intellect", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Magic, Skill.Death_Magic, "Death Magic", "Death", "DMa", "", "intellect", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Magic, Skill.Life_Magic, "Life Magic", "Life", "LMa", "", "intellect", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Magic, Skill.Shadow_Magic, "Shadow Magic", "Shadow", "SMa", "", "intellect", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Magic, Skill.Arcane_Magic, "Arcane Magic", "Arcane", "AMa", "", "intellect", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Magic, Skill.Lore, "Lore", "Lore", "Lor", "", "intellect", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Misc, Skill.Channeling, "Channeling", "Channel", "Cha", "", "intellect", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Misc, Skill.Sneaking, "Sneaking", "Sneak", "Snk", "", "agility", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Misc, Skill.Scouting, "Scounting", "Scout", "Sct", "", "senses", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Misc, Skill.Tricks, "Tricks", "Tricks", "Tri", "", "dexterity", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Misc, Skill.Evasion, "Evasion", "Evasion", "Eva", "", "agility", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Misc, Skill.Devices, "Devices", "Device", "Dev", "", "dexterity", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Misc, Skill.Persuasion, "Persuasion", "Persuade", "Pes", "", "charisma", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Misc, Skill.Survival, "Survival", "Survival", "Sur", "", "endurance", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);

            skill = new SkillDefinition(SkillCategory.Misc, Skill.Medicine, "Medicine", "Medic", "Med", "", "intellect", 0, 999,
                 new List<AbilityUnlock> { });
            skillDefinitions.Add(skill);
        }

        static List<string> npcKeys = new List<string>();

        public static void LoadNPCs()
        {
            NPCDefinition npc = new NPCDefinition(new FantasyName("Giant Rat", "", ""), Species.Animal, BodySize.Small, Gender.Either, "giant_rat", "", "",
                "Enemy", 1, 2, 10, 10, "", "", "", "");
            npc.baseStart[(int)BaseAttribute.Strength] = 10; npc.baseStart[(int)BaseAttribute.Intellect] = 10;
            npc.baseStart[(int)BaseAttribute.Endurance] = 10; npc.baseStart[(int)BaseAttribute.Wisdom] = 10;
            npc.baseStart[(int)BaseAttribute.Agility] = 10; npc.baseStart[(int)BaseAttribute.Willpower] = 10;
            npc.baseStart[(int)BaseAttribute.Speed] = 10; npc.baseStart[(int)BaseAttribute.Charisma] = 10;
            npc.baseStart[(int)BaseAttribute.Senses] = 10; npc.baseStart[(int)BaseAttribute.Memory] = 10;
            npc.BasePerLevel[(int)BaseAttribute.Strength] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Intellect] = new GameValue(0, 0);
            npc.BasePerLevel[(int)BaseAttribute.Endurance] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Wisdom] = new GameValue(0, 0);
            npc.BasePerLevel[(int)BaseAttribute.Agility] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Willpower] = new GameValue(0, 0);
            npc.BasePerLevel[(int)BaseAttribute.Speed] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Charisma] = new GameValue(0, 0);
            npc.BasePerLevel[(int)BaseAttribute.Senses] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Memory] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Armor] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Health] = new GameValue(1, 2); npc.derivedPerLevel[(int)DerivedAttribute.Stamina] = new GameValue(1, 1);
            npc.derivedPerLevel[(int)DerivedAttribute.Essence] = new GameValue(0, 1); npc.derivedPerLevel[(int)DerivedAttribute.Morale] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Might_Attack] = new GameValue(0, 0); npc.derivedPerLevel[(int)DerivedAttribute.Finesse_Attack] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Block] = new GameValue(0, 0); npc.derivedPerLevel[(int)DerivedAttribute.Dodge] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Parry] = new GameValue(0, 0); npc.derivedPerLevel[(int)DerivedAttribute.Initiative] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Critical_Strike] = new GameValue(0, 0); npc.derivedPerLevel[(int)DerivedAttribute.Critical_Damage] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Perception] = new GameValue(0, 0);

            npcs.Add(npc.key, npc); npcKeys.Add(npc.key);

            npc = new NPCDefinition(new FantasyName("Giant Black Rat", "", ""), Species.Animal, BodySize.Small, Gender.Either, "giant_black_rat", "", "",
                "Enemy", 1, 2, 10, 10, "", "", "", "");
            npc.baseStart[(int)BaseAttribute.Strength] = 10; npc.baseStart[(int)BaseAttribute.Intellect] = 10;
            npc.baseStart[(int)BaseAttribute.Endurance] = 10; npc.baseStart[(int)BaseAttribute.Wisdom] = 10;
            npc.baseStart[(int)BaseAttribute.Agility] = 10; npc.baseStart[(int)BaseAttribute.Willpower] = 10;
            npc.baseStart[(int)BaseAttribute.Speed] = 10; npc.baseStart[(int)BaseAttribute.Charisma] = 10;
            npc.baseStart[(int)BaseAttribute.Senses] = 10; npc.baseStart[(int)BaseAttribute.Memory] = 10;
            npc.BasePerLevel[(int)BaseAttribute.Strength] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Intellect] = new GameValue(0, 0);
            npc.BasePerLevel[(int)BaseAttribute.Endurance] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Wisdom] = new GameValue(0, 0);
            npc.BasePerLevel[(int)BaseAttribute.Agility] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Willpower] = new GameValue(0, 0);
            npc.BasePerLevel[(int)BaseAttribute.Speed] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Charisma] = new GameValue(0, 0);
            npc.BasePerLevel[(int)BaseAttribute.Senses] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Memory] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Armor] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Health] = new GameValue(1, 2); npc.derivedPerLevel[(int)DerivedAttribute.Stamina] = new GameValue(1, 1);
            npc.derivedPerLevel[(int)DerivedAttribute.Essence] = new GameValue(0, 1); npc.derivedPerLevel[(int)DerivedAttribute.Morale] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Might_Attack] = new GameValue(0, 0); npc.derivedPerLevel[(int)DerivedAttribute.Finesse_Attack] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Block] = new GameValue(0, 0); npc.derivedPerLevel[(int)DerivedAttribute.Dodge] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Parry] = new GameValue(0, 0); npc.derivedPerLevel[(int)DerivedAttribute.Initiative] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Critical_Strike] = new GameValue(0, 0); npc.derivedPerLevel[(int)DerivedAttribute.Critical_Damage] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Perception] = new GameValue(0, 0);
            npcs.Add(npc.key, npc); npcKeys.Add(npc.key);

            npc = new NPCDefinition(new FantasyName("Skeleton", "", ""), Species.Undead, BodySize.Small, Gender.Either, "Skeleton", "", "",
                 "Enemy", 2, 10, 20, 10, "", "", "", "");
            npc.baseStart[(int)BaseAttribute.Strength] = 10; npc.baseStart[(int)BaseAttribute.Intellect] = 10;
            npc.baseStart[(int)BaseAttribute.Endurance] = 10; npc.baseStart[(int)BaseAttribute.Wisdom] = 10;
            npc.baseStart[(int)BaseAttribute.Agility] = 10; npc.baseStart[(int)BaseAttribute.Willpower] = 10;
            npc.baseStart[(int)BaseAttribute.Speed] = 10; npc.baseStart[(int)BaseAttribute.Charisma] = 10;
            npc.baseStart[(int)BaseAttribute.Senses] = 10; npc.baseStart[(int)BaseAttribute.Memory] = 10;
            npc.BasePerLevel[(int)BaseAttribute.Strength] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Intellect] = new GameValue(0, 0);
            npc.BasePerLevel[(int)BaseAttribute.Endurance] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Wisdom] = new GameValue(0, 0);
            npc.BasePerLevel[(int)BaseAttribute.Agility] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Willpower] = new GameValue(0, 0);
            npc.BasePerLevel[(int)BaseAttribute.Speed] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Charisma] = new GameValue(0, 0);
            npc.BasePerLevel[(int)BaseAttribute.Senses] = new GameValue(0, 0); npc.BasePerLevel[(int)BaseAttribute.Memory] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Armor] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Health] = new GameValue(12, 15); npc.derivedPerLevel[(int)DerivedAttribute.Stamina] = new GameValue(1, 1);
            npc.derivedPerLevel[(int)DerivedAttribute.Essence] = new GameValue(0, 1); npc.derivedPerLevel[(int)DerivedAttribute.Morale] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Might_Attack] = new GameValue(0, 0); npc.derivedPerLevel[(int)DerivedAttribute.Finesse_Attack] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Block] = new GameValue(0, 0); npc.derivedPerLevel[(int)DerivedAttribute.Dodge] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Parry] = new GameValue(0, 0); npc.derivedPerLevel[(int)DerivedAttribute.Initiative] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Critical_Strike] = new GameValue(0, 0); npc.derivedPerLevel[(int)DerivedAttribute.Critical_Damage] = new GameValue(0, 0);
            npc.derivedPerLevel[(int)DerivedAttribute.Perception] = new GameValue(0, 0);
            npcs.Add(npc.key, npc); npcKeys.Add(npc.key);
        }

        public static void LoadQuirks()
        {
            Ability quirk = new Ability("Adventurous", "adventurous", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Attractive", "attractive", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("ugly"));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Alert", "alert", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Athletic", "athletic", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("lazy"));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Articulate", "articulate", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Benevolent", "benevolanet", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("cruel"));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Calm", "calm", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Charming", "charming", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Cheerful", "Cheerful", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Clever", "clever", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Courageous", "courageous", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("cowardly"));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Dignified", "dignified", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Disciplined", "disciplined", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Educated", "educated", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("uneducated"));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Eloquent", "eloquent", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Friendly", "friendly", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Generous", "generous", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("greedy"));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Honorable", "Honorable", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Humorous", "humorous", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Idealistic", "idealistic", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Intuitive", "Intuitive", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Kind", "kind", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Logical", "logical", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Loyal", "loyal", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Observant", "observant", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Optimistic", "optimistic", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("pesimistic"));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Peaceful", "peaceful", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Persuasive", "persuasive", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Protective", "protective", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Resourceful", "resourceful", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Romantic", "romantic", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Selfless", "selfless", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Sexy", "sexy", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Simple", "simple", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Social", "social", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Sweet", "sweet", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Wity", "wity", "trait", AbilityClass.Either, AbilityType.Positive_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);


            quirk = new Ability("Frugal", "frugal", "trait", AbilityClass.Either, AbilityType.Neutral_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Moralistic", "moralistic", "trait", AbilityClass.Either, AbilityType.Neutral_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Outspoken", "outspoken", "trait", AbilityClass.Either, AbilityType.Neutral_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Political", "political", "trait", AbilityClass.Either, AbilityType.Neutral_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Quiet", "quiet", "trait", AbilityClass.Either, AbilityType.Neutral_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Religious", "religious", "trait", AbilityClass.Either, AbilityType.Neutral_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Proud", "proud", "trait", AbilityClass.Either, AbilityType.Neutral_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Reserved", "reserved", "trait", AbilityClass.Either, AbilityType.Neutral_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Sarcastic", "sarcastic", "trait", AbilityClass.Either, AbilityType.Neutral_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Skeptical", "skeptical", "trait", AbilityClass.Either, AbilityType.Neutral_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Strict", "strict", "trait", AbilityClass.Either, AbilityType.Neutral_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Whimsical", "whimsical", "trait", AbilityClass.Either, AbilityType.Neutral_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);


            quirk = new Ability("Abrasive", "abrasive", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Apathetic", "Apathetic", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Arrogant", "arrogant", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Antisocial", "antisocial", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("social"));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Blunt", "blunt", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Careless", "careless", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Childish", "childish", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Compulsive", "compulsive", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Cowardly", "cowardly", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("courageous"));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Crazy", "crazy", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Crude", "crude", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Cruel", "cruel", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("benevolent"));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Deceptive", "deceptive", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Destructive", "destructive", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Disobedient", "disobedient", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Egocentric", "egocentric", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("selfless"));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Fanatical", "fanatical", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Fearful", "fearful", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Forgetful", "forgetful", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Hateful", "hateful", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Insecure", "insecure", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Lazy", "lazy", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("athletic"));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Moody", "moody", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Obsessive", "obsessive", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Paranoid", "paranoid", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Pesimitic", "pesimitic", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("optimistic"));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Secretive", "secretive", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Shy", "shy", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Superstitious", "superstitious", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Timid", "timid", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent(""));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Ugly", "ugly", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("attractive"));
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Uneducated", "uneducated", "trait", AbilityClass.Either, AbilityType.Negative_Quirk);
            quirk.Components.Add(new OpposingTraitComponent("educated"));
            abilities.Add(quirk.Key, quirk);


            quirk = new Ability("Minor Limp", "minor_limp", "trait", AbilityClass.Either, AbilityType.Wound);
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Major Limp", "major_limp", "trait", AbilityClass.Either, AbilityType.Wound);
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Lisp", "lisp", "trait", AbilityClass.Either, AbilityType.Wound);
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Missing Finger", "missing_finger", "trait", AbilityClass.Either, AbilityType.Wound);
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Missing Toe", "missing_toe", "trait", AbilityClass.Either, AbilityType.Wound);
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Missing Foot", "missing_foot", "trait", AbilityClass.Either, AbilityType.Wound);
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Missing Hand", "missing_hand", "trait", AbilityClass.Either, AbilityType.Wound);
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Deep Scar", "deep_scar", "trait", AbilityClass.Either, AbilityType.Wound);
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Missing Ear", "missing_ear", "trait", AbilityClass.Either, AbilityType.Wound);
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Missing Eye", "missing_eye", "trait", AbilityClass.Either, AbilityType.Wound);
            abilities.Add(quirk.Key, quirk);

            quirk = new Ability("Head Wound", "head_wound", "trait", AbilityClass.Either, AbilityType.Wound);
            abilities.Add(quirk.Key, quirk);
        }

        public static void LoadItemAttributes()
        {
            AttributeDefinition att = new AttributeDefinition("Attack", "Attack", "Acc", "", 0, 9999, AttributeDefinitionType.Weapon, null);
            weaponAttributes.Add(att);
            att = new AttributeDefinition("Range", "Range", "Ran", "", 0, 9999, AttributeDefinitionType.Weapon, null);
            weaponAttributes.Add(att);
            att = new AttributeDefinition("Actions", "Actions", "Act", "", 0, 9999, AttributeDefinitionType.Weapon, null);
            weaponAttributes.Add(att);
            att = new AttributeDefinition("Parry", "Parry", "Pry", "", 0, 9999, AttributeDefinitionType.Weapon, null);
            weaponAttributes.Add(att);

            att = new AttributeDefinition("Armor", "Armor", "Arm", "", 0, 9999, AttributeDefinitionType.Wearable, null);
            wearableAttributes.Add(att);
            att = new AttributeDefinition("Dodge", "Dodge", "Ddg", "", 0, 9999, AttributeDefinitionType.Wearable, null);
            wearableAttributes.Add(att);
            att = new AttributeDefinition("Block", "Block", "Blk", "", 0, 9999, AttributeDefinitionType.Wearable, null);
            wearableAttributes.Add(att);
            att = new AttributeDefinition("Actions", "Actions", "Act", "", 0, 9999, AttributeDefinitionType.Wearable, null);
            wearableAttributes.Add(att);

            att = new AttributeDefinition("Attack", "Attack", "Attack", "", 0, 9999, AttributeDefinitionType.Ammo, null);
            ammoAttributes.Add(att);
            att = new AttributeDefinition("Range", "Range", "Ran", "", 0, 9999, AttributeDefinitionType.Ammo, null);
            ammoAttributes.Add(att);
            att = new AttributeDefinition("Actions", "Actions", "Act", "", 0, 9999, AttributeDefinitionType.Ammo, null);
            ammoAttributes.Add(att);

            att = new AttributeDefinition("Actions", "Actions", "Act", "", 0, 9999, AttributeDefinitionType.Accessory, null);
            accessoryAttributes.Add(att);
            att = new AttributeDefinition("Cooldown", "Cooldown", "Cd", "", 0, 9999, AttributeDefinitionType.Accessory, null);
            accessoryAttributes.Add(att);
        }

        public static void LoadFactions()
        {
            FactionData faction = new FactionData("Player", "Player");
            faction.reputations = new Dictionary<string, Reputation>();
            faction.reputations.Add("Player", new Reputation(ReputationLevel.Friendly));
            faction.reputations.Add("Neutral", new Reputation(ReputationLevel.Neutral));
            faction.reputations.Add("Enemy", new Reputation(ReputationLevel.Hated));
            factions.Add(faction.key, faction);

            faction = new FactionData("Neutral", "Neutral");
            faction.reputations = new Dictionary<string, Reputation>();
            faction.reputations.Add("Player", new Reputation(ReputationLevel.Neutral));
            faction.reputations.Add("Neutral", new Reputation(ReputationLevel.Friendly));
            faction.reputations.Add("Enemy", new Reputation(ReputationLevel.Hated));
            factions.Add(faction.key, faction);

            faction = new FactionData("Enemy", "Enemy");
            faction.reputations = new Dictionary<string, Reputation>();
            faction.reputations.Add("Player", new Reputation(ReputationLevel.Hated));
            faction.reputations.Add("Neutral", new Reputation(ReputationLevel.Hated));
            faction.reputations.Add("Enemy", new Reputation(ReputationLevel.Friendly));
            factions.Add(faction.key, faction);
        }
    }
}