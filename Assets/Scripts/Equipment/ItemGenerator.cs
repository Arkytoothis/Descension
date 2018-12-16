using UnityEngine;
using System.Collections.Generic;
using Descension.Abilities;
using Descension.Core;

namespace Descension.Equipment
{
    public static class ItemGenerator
    {
        public static List<string> SoftMaterials = new List<string>();
        public static List<string> HardMaterials = new List<string>();
        public static List<string> SoftOrHardMaterials = new List<string>();
        public static List<string> ClothMaterials = new List<string>();
        public static List<string> LeatherMaterials = new List<string>();
        public static List<string> MetalMaterials = new List<string>();
        public static List<string> StoneMaterials = new List<string>();
        public static List<string> PotionMaterials = new List<string>();
        public static List<string> ScrollMaterials = new List<string>();
        public static List<string> FoodMaterials = new List<string>();

        public static List<string> AnyItem = new List<string>();
        public static List<string> Weapons = new List<string>();
        public static List<string> Ammo = new List<string>();
        public static List<string> Wearables = new List<string>();
        public static List<string> Shields = new List<string>();
        public static List<string> Consumables = new List<string>();

        public static List<string> AnyArtifact = new List<string>();
        public static List<string> WeaponArtifacts = new List<string>();
        public static List<string> AmmoArtifacts = new List<string>();
        public static List<string> WearableArtifacts = new List<string>();
        public static List<string> ShieldArtifacts = new List<string>();
        public static List<string> ConsumableArtifacts = new List<string>();

        public static List<string> Materials = new List<string>();
        public static List<string> Qualities = new List<string>();
        public static List<string> PreEnchants = new List<string>();
        public static List<string> PostEnchants = new List<string>();

        public static List<List<string>> ItemBySlot = new List<List<string>>();

        public static bool IgnoreUnlocks = true;

        public static void Initialize()
        {
            for (int i = 0; i < (int)EquipmentSlot.Number; i++)
            {
                ItemBySlot.Add(new List<string>());
            }

            foreach (KeyValuePair<string, ItemModifier> kvp in Database.ItemModifiers)
            {
                if (IgnoreUnlocks == true)// || PlayerManager.Instance.ItemModifiersUnlocked[kvp.Key] == true)
                {
                    if (kvp.Value.Type == ItemModifierType.Material)
                    {
                        if (kvp.Value.MaterialHardness == MaterialHardness.Any)
                        {
                            SoftMaterials.Add(kvp.Key);
                            HardMaterials.Add(kvp.Key);
                            SoftOrHardMaterials.Add(kvp.Key);
                            ClothMaterials.Add(kvp.Key);
                            LeatherMaterials.Add(kvp.Key);
                            MetalMaterials.Add(kvp.Key);
                            StoneMaterials.Add(kvp.Key);
                        }
                        else if (kvp.Value.MaterialHardness == MaterialHardness.Cloth)
                        {
                            ClothMaterials.Add(kvp.Key);
                        }
                        else if (kvp.Value.MaterialHardness == MaterialHardness.Leather)
                        {
                            LeatherMaterials.Add(kvp.Key);
                        }
                        else if (kvp.Value.MaterialHardness == MaterialHardness.Metal)
                        {
                            MetalMaterials.Add(kvp.Key);
                            HardMaterials.Add(kvp.Key);
                            SoftOrHardMaterials.Add(kvp.Key);
                        }
                        else if (kvp.Value.MaterialHardness == MaterialHardness.Soft)
                        {
                            SoftMaterials.Add(kvp.Key);
                            SoftOrHardMaterials.Add(kvp.Key);
                        }
                        else if (kvp.Value.MaterialHardness == MaterialHardness.Stone)
                        {
                            StoneMaterials.Add(kvp.Key);
                            SoftOrHardMaterials.Add(kvp.Key);
                        }
                        else if (kvp.Value.MaterialHardness == MaterialHardness.Liquid)
                        {
                            PotionMaterials.Add(kvp.Key);
                        }
                        else if (kvp.Value.MaterialHardness == MaterialHardness.Paper)
                        {
                            ScrollMaterials.Add(kvp.Key);
                        }
                        else if (kvp.Value.MaterialHardness == MaterialHardness.Food)
                        {
                            FoodMaterials.Add(kvp.Key);
                        }
                    }
                    else if (kvp.Value.Type == ItemModifierType.Quality || kvp.Value.Type == ItemModifierType.Plus_Enchant)
                    {
                        Qualities.Add(kvp.Key);
                    }
                    else if (kvp.Value.Type == ItemModifierType.Pre_Enchant)
                    {
                        PreEnchants.Add(kvp.Key);
                    }
                    else if (kvp.Value.Type == ItemModifierType.Post_Enchant)
                    {
                        PostEnchants.Add(kvp.Key);
                    }
                }
            }

            foreach (KeyValuePair<string, ItemDefinition> kvp in Database.Items)
            {
                if (IgnoreUnlocks == true)// || PlayerManager.Instance.ItemsUnlocked[kvp.Key] == true)
                {
                    AnyItem.Add(kvp.Key);

                    if (kvp.Value.Type == ItemType.Weapon)
                    {
                        Weapons.Add(kvp.Key);
                    }
                    else if (kvp.Value.Type == ItemType.Ammo)
                    {
                        Ammo.Add(kvp.Key);
                    }
                    else if (kvp.Value.Type == ItemType.Wearable)
                    {
                        Wearables.Add(kvp.Key);
                    }
                    else if (kvp.Value.Type == ItemType.Accessory)
                    {
                        Consumables.Add(kvp.Key);
                    }

                    //if (kvp.Value.Slot != EquipmentSlot.None)
                    //{
                    //    Debug.Log(kvp.Value.Name + " " + kvp.Value.Slot);
                    //    ItemBySlot[(int)kvp.Value.Slot].Add(kvp.Key);
                    //}
                }
            }
        }

        public static ItemShort CreateRandomItemShort(ItemTypeAllowed type, int pre_chance, int post_chance)
        {
            ItemData item = CreateRandomItem(type, pre_chance, post_chance);

            return new ItemShort(item);
        }

        static string GetItemKey(ItemTypeAllowed type)
        {
            string key = "";

            if (type == ItemTypeAllowed.Weapon)
            {
                key = Weapons[Random.Range(0, Weapons.Count)];
            }
            else if (type == ItemTypeAllowed.Ammo)
            {
                key = Ammo[Random.Range(0, Ammo.Count)];
            }
            else if (type == ItemTypeAllowed.Wearable)
            {
                key = Wearables[Random.Range(0, Wearables.Count)];
            }
            else if (type == ItemTypeAllowed.Accessory)
            {
                key = Consumables[Random.Range(0, Consumables.Count)];
            }
            else if (type == ItemTypeAllowed.Any)
            {
                key = AnyItem[Random.Range(0, AnyItem.Count)];
            }

            return key;
        }

        static string GetArtifactKey(ItemTypeAllowed type)
        {
            string key = "";

            if (type == ItemTypeAllowed.Weapon)
            {
                key = WeaponArtifacts[Random.Range(0, WeaponArtifacts.Count)];
            }
            else if (type == ItemTypeAllowed.Ammo)
            {
                key = AmmoArtifacts[Random.Range(0, AmmoArtifacts.Count)];
            }
            else if (type == ItemTypeAllowed.Wearable)
            {
                key = WearableArtifacts[Random.Range(0, WearableArtifacts.Count)];
            }
            else if (type == ItemTypeAllowed.Accessory)
            {
                key = ConsumableArtifacts[Random.Range(0, ConsumableArtifacts.Count)];
            }
            else if (type == ItemTypeAllowed.Any)
            {
                key = AnyArtifact[Random.Range(0, AnyArtifact.Count)];
            }

            return key;
        }

        static string GetItemKey(int slot)
        {
            string key = "";

            if (ItemBySlot[(int)slot].Count > 0)
                key = ItemBySlot[(int)slot][Random.Range(0, ItemBySlot[(int)slot].Count - 1)];

            return key;
        }

        public static ItemData CreateRandomItem(int slot, int pre_chance, int post_chance)
        {
            string itemKey = GetItemKey(slot);
            if (itemKey == "") return null;

            string material = GetMaterial(itemKey);
            string plus = "", pre = "", post = "";

            plus = Qualities[Random.Range(0, Qualities.Count)];

            if (Random.Range(0, 100) < pre_chance)
            {
                pre = PreEnchants[Random.Range(0, PreEnchants.Count)];
            }

            if (Random.Range(0, 100) < post_chance)
            {
                post = PostEnchants[Random.Range(0, PostEnchants.Count)];
            }

            ItemData item = CreateItem(itemKey, material, plus, pre, post, 1);

            return item;
        }

        public static ItemData CreateRandomItem(ItemTypeAllowed type, int pre_chance, int post_chance)
        {
            string itemKey = GetItemKey(type);
            string material = GetMaterial(itemKey);
            string plus = "", pre = "", post = "";

            plus = Qualities[Random.Range(0, Qualities.Count)];

            if (Random.Range(0, 100) < pre_chance)
            {
                pre = PreEnchants[Random.Range(0, PreEnchants.Count)];
            }

            if (Random.Range(0, 100) < post_chance)
            {
                post = PostEnchants[Random.Range(0, PostEnchants.Count)];
            }

            ItemData item = CreateItem(itemKey, material, plus, pre, post, 1);

            return item;
        }

        public static ItemData CreateArtifact(ItemTypeAllowed type)
        {
            bool setDataAllowed = true;
            string itemKey = GetArtifactKey(type);
            ItemData newItem = new ItemData(Database.GetItem(itemKey), 1);

            newItem.ArtifactData = GenerateArtifactData();

            if (setDataAllowed == true && Random.Range(1, 101) < 10)
                newItem.SetData = GenerateSetData();

            newItem.CalculateAttributes();
            newItem.SetText();

            return newItem;
        }

        public static ItemData CreateItem(string item, string material, string plus, string pre, string post, int stack_size)
        {
            ItemData newItem = new ItemData(Database.Items[item], stack_size);

            bool plusAllowed = true;
            bool preAllowed = true;
            bool postAllowed = true;
            bool setDataAllowed = true;

            if (newItem.Type == ItemType.Accessory)
            {
                plusAllowed = false;
                preAllowed = false;
                postAllowed = false;
                setDataAllowed = false;
            }

            if (Database.ItemModifiers.ContainsKey(material))
                newItem.Material = new ItemModifier(Database.GetItemModifier(material));

            if (plusAllowed == true && plus != null && Database.ItemModifiers.ContainsKey(plus))
                newItem.Quality = new ItemModifier(Database.GetItemModifier(plus));

            if (preAllowed == true && pre != null && Database.ItemModifiers.ContainsKey(pre))
                newItem.PreEnchant = new ItemModifier(Database.GetItemModifier(pre));

            if (postAllowed == true && post != null && Database.ItemModifiers.ContainsKey(post))
                newItem.PostEnchant = new ItemModifier(Database.GetItemModifier(post));

            if (setDataAllowed == true && Random.Range(1, 101) < 5)
                newItem.SetData = GenerateSetData();

            newItem.CalculateAttributes();
            newItem.SetText();

            return newItem;
        }

        public static List<string> GetMaterialList(string item_key)
        {
            if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Cloth)
            {
                return ClothMaterials;
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Leather)
            {
                return LeatherMaterials;
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Metal)
            {
                return MetalMaterials;
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Hard)
            {
                return HardMaterials;
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Soft)
            {
                return SoftMaterials;
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Soft_or_Hard)
            {
                return SoftOrHardMaterials;
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Potion)
            {
                return PotionMaterials;
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Scroll)
            {
                return ScrollMaterials;
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Food)
            {
                return FoodMaterials;
            }
            else
            {
                return new List<string>();
            }
        }

        static string GetMaterial(string item_key)
        {
            string material = "";

            if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Cloth)
            {
                material = ClothMaterials[Random.Range(0, ClothMaterials.Count)];
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Leather)
            {
                material = LeatherMaterials[Random.Range(0, LeatherMaterials.Count)];
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Metal)
            {
                material = MetalMaterials[Random.Range(0, MetalMaterials.Count)];
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Hard)
            {
                material = HardMaterials[Random.Range(0, HardMaterials.Count)];
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Soft)
            {
                material = SoftMaterials[Random.Range(0, SoftMaterials.Count)];
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Soft_or_Hard)
            {
                material = SoftOrHardMaterials[Random.Range(0, SoftOrHardMaterials.Count)];
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Potion)
            {
                material = PotionMaterials[Random.Range(0, PotionMaterials.Count)];
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Scroll)
            {
                material = ScrollMaterials[Random.Range(0, ScrollMaterials.Count)];
            }
            else if (Database.GetItem(item_key).Hardness == ItemHardnessAllowed.Food)
            {
                material = FoodMaterials[Random.Range(0, FoodMaterials.Count)];
            }

            return material;
        }

        static ItemSetData GenerateSetData()
        {
            ItemSetData data = new ItemSetData();

            data.SetName = "Random Item Set Data";
            data.NumPieces = Random.Range(2, 9);

            data.LevelData = new List<ItemAbilityData>();
            for (int i = 0; i < data.NumPieces - 1; i++)
            {
                ItemAbilityData levelData = new ItemAbilityData();
                levelData.UnlockValue = i + 2;
                levelData.Effects.Add(GetRandomEffect());
                data.LevelData.Add(levelData);
            }

            return data;
        }

        static ArtifactData GenerateArtifactData()
        {
            ArtifactData data = new ArtifactData();

            data.Level = 1;
            data.Experience = 0;
            data.NextLevel = 1000;

            int numAbilitiesToGen = Random.Range(2, 6);
            data.LevelData = new List<ItemAbilityData>();
            for (int i = 0; i < numAbilitiesToGen; i++)
            {
                ItemAbilityData levelData = new ItemAbilityData();
                levelData.UnlockValue = i + 2;
                levelData.Effects.Add(GetRandomEffect());
                data.LevelData.Add(levelData);
            }

            return data;
        }

        static AbilityComponent GetRandomEffect()
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                //return new AlterCharacteristicEffect(TriggerType.Always_On, CharacteristicType.Base_Attribute,
                //Helper.RandomKey<string, AttributeDefinition>(Database.BaseAttributes), 1, 1);

                case 1:
                //return new AlterCharacteristicEffect(TriggerType.Always_On, CharacteristicType.Derived_Attribute,
                //Helper.RandomKey<string, AttributeDefinition>(Database.DerivedAttributes), 1, 1);
                case 2:
                //return new AlterCharacteristicEffect(TriggerType.Always_On, CharacteristicType.Skill,
                //Helper.RandomKey<string, SkillDefinition>(Database.Skills), 1, 1);
                case 3:
                //return new AlterCharacteristicEffect(TriggerType.Always_On, CharacteristicType.Resistance,
                //Helper.RandomKey<string, AttributeDefinition>(Database.DamageTypes), 1, 1);
                default:
                    return null;
            }
        }
    }
}