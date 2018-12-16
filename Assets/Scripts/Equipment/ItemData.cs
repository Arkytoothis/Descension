using UnityEngine;
using System.Collections.Generic;
using Descension.Characters;
using Descension.Core;

namespace Descension.Equipment
{
    [System.Serializable]
    public class ItemData
    {
        public string Name;
        public string Key;
        public string IconKey;
        public string MeshKey;
        public string TextureKey;
        public string particleEffect;

        public Vector3 portraitRotation;
        public Vector3 worldRotation;

        public Vector3 portraitPosition;
        public Vector3 worldPosition;

        public ItemNameFormat NameFormat;
        public ItemType Type;
        public EquipmentSlot Slot;
        public ItemHardnessAllowed Hardness;
        public List<SkillRequirement> SkillRequirements;

        public int StackSize;
        public float Actions;
        public int Power;
        public int DurabilityCur;
        public int DurabilityMax;
        public Rarity Rarity;

        public WeaponData WeaponData;
        public AmmoData AmmoData;
        public WearableData WearableData;
        public AccessoryData AccessoryData;
        public IngredientData IngredientData;

        public ItemModifier Material;
        public ItemModifier Quality;
        public ItemModifier PreEnchant;
        public ItemModifier PostEnchant;

        public ArtifactData ArtifactData;
        public ItemSetData SetData;

        public UsableData UsableData;

        public int HoursToBuild;
        public int Value;

        public string description;
        public string attributesTooltip;
        public string effectsTooltip;

        public ItemData()
        {
            Name = "";
            Key = "";
            Type = ItemType.None;
            Slot = EquipmentSlot.None;
            Hardness = ItemHardnessAllowed.None;
            NameFormat = ItemNameFormat.None;
            IconKey = "";
            MeshKey = "";
            TextureKey = "";
            particleEffect = "";

            StackSize = 0;
            Power = 0;
            Actions = 0;
            HoursToBuild = 0;

            WeaponData = null;
            AmmoData = null;
            WearableData = null;
            AccessoryData = null;
            IngredientData = null;

            Material = null;
            Quality = null;
            PreEnchant = null;
            PostEnchant = null;

            ArtifactData = null;
            SetData = null;

            UsableData = null;

            SkillRequirements = new List<SkillRequirement>();

            description = "";
            attributesTooltip = "";
            effectsTooltip = "";

            portraitRotation = Vector3.zero;
            portraitPosition = Vector3.zero;

            worldRotation = Vector3.zero;
            worldPosition = Vector3.zero;
        }

        public ItemData(ItemData item)
        {
            Name = item.Name;
            Key = item.Key;
            Type = item.Type;
            NameFormat = item.NameFormat;

            IconKey = item.IconKey;
            MeshKey = item.MeshKey;
            TextureKey = item.TextureKey;
            particleEffect = item.particleEffect;

            Slot = item.Slot;
            DurabilityCur = item.DurabilityCur;
            DurabilityMax = item.DurabilityMax;
            StackSize = item.StackSize;

            Hardness = item.Hardness;
            Power = item.Power;
            Actions = item.Actions;
            HoursToBuild = item.HoursToBuild;
            Rarity = item.Rarity;

            description = item.description;
            attributesTooltip = item.attributesTooltip;
            effectsTooltip = item.effectsTooltip;

            if (item.WeaponData != null)
            {
                WeaponData = new WeaponData(item.WeaponData);
            }
            else
                WeaponData = null;

            if (item.AmmoData != null)
            {
                AmmoData = new AmmoData(item.AmmoData);
            }
            else
                AmmoData = null;

            if (item.WearableData != null)
            {
                WearableData = new WearableData(item.WearableData);
            }
            else
                WearableData = null;

            if (item.AccessoryData != null)
            {
                AccessoryData = new AccessoryData(item.AccessoryData);
            }
            else
                AccessoryData = null;

            if (item.IngredientData != null)
            {
                IngredientData = new IngredientData(item.IngredientData);
            }
            else
                IngredientData = null;

            if (item.Material != null)
            {
                Material = new ItemModifier(item.Material);
            }
            else
                Material = null;

            if (item.Quality != null)
            {
                Quality = new ItemModifier(item.Quality);
            }
            else
                Quality = null;

            if (item.PreEnchant != null)
            {
                PreEnchant = new ItemModifier(item.PreEnchant);
            }
            else
                PreEnchant = null;

            if (item.PostEnchant != null)
            {
                PostEnchant = new ItemModifier(item.PostEnchant);
            }
            else
                PostEnchant = null;

            SkillRequirements = new List<SkillRequirement>();
            for (int i = 0; i < item.SkillRequirements.Count; i++)
            {
                SkillRequirements.Add(new SkillRequirement(item.SkillRequirements[i]));
            }

            if (item.ArtifactData != null)
                ArtifactData = new ArtifactData(item.ArtifactData);
            else
                ArtifactData = null;

            if (item.SetData != null)
                SetData = new ItemSetData(item.SetData);
            else
                SetData = null;

            if (item.UsableData != null)
                UsableData = new UsableData(item.UsableData);
            else
                UsableData = null;

            portraitRotation = item.portraitRotation;
            portraitPosition = item.portraitPosition;

            worldRotation = item.worldRotation;
            worldPosition = item.worldPosition;
        }

        public ItemData(ItemDefinition def, int stack_size)
        {
            Name = def.Name;
            Key = def.Key;
            Type = def.Type;
            NameFormat = def.NameFormat;

            IconKey = def.IconKey;
            MeshKey = def.MeshKey;
            TextureKey = def.TextureKey;
            particleEffect = def.particleEffect;

            StackSize = stack_size;
            Slot = def.Slot;
            DurabilityMax = def.DurabilityMax;
            Hardness = def.Hardness;

            if (def.WeaponData != null && def.WeaponData.Type != WeaponType.None)
            {
                WeaponData = new WeaponData(def.WeaponData);
            }
            else
                WeaponData = null;

            if (def.AmmoData != null && def.AmmoData.Type != AmmoType.None)
            {
                AmmoData = new AmmoData(def.AmmoData);
            }
            else
                AmmoData = null;

            if (def.WearableData != null && def.WearableData.Type != WearableType.None)
            {
                WearableData = new WearableData(def.WearableData);
            }
            else
                WearableData = null;

            if (def.AccessoryData != null && def.AccessoryData.Type != AccessoryType.None)
            {
                AccessoryData = new AccessoryData(def.AccessoryData);
            }
            else
                AccessoryData = null;

            if (def.IngredientData != null && def.IngredientData.Type != IngredientType.None)
            {
                IngredientData = new IngredientData(def.IngredientData);
            }
            else
                AccessoryData = null;

            if (UsableData != null)
            {
                UsableData = new UsableData(def.UsableData);
            }
            else
            {
                UsableData = null;
            }

            SkillRequirements = new List<SkillRequirement>();
            for (int i = 0; i < def.SkillRequirements.Count; i++)
            {
                SkillRequirements.Add(new SkillRequirement(def.SkillRequirements[i]));
            }

            Actions = def.Actions;
            HoursToBuild = def.BaseBuildTime;
            Power = def.BasePower;

            description = "";
            attributesTooltip = "";
            effectsTooltip = "";

            portraitRotation = def.portraitRotation;
            portraitPosition = def.portraitPosition;

            worldRotation = def.worldRotation;
            worldPosition = def.worldPosition;
        }

        public void SetMaterial(ItemModifier material)
        {
            if (material == null)
                return;

            Material = new ItemModifier(material);
        }

        public void SetName()
        {
            if (NameFormat != ItemNameFormat.Artifact)
            {
                Name = "";

                if (Quality != null)
                {
                    Name = Quality.Name + " ";
                }

                if (PreEnchant != null)
                {
                    Name += PreEnchant.Name + " ";
                }

                if (NameFormat == ItemNameFormat.Material_First)
                {
                    if (Material != null)
                        Name += Database.GetItemModifier(Material.Key).Name + " " + Database.GetItem(Key).Name;
                }
                else if (NameFormat == ItemNameFormat.Material_Middle)
                {
                    string[] strings = Database.GetItem(Key).Name.Split(' ');

                    if (strings.Length > 1)
                    {
                        Name += strings[0];
                        if (Material != null)
                            Name += " " + Database.GetItemModifier(Material.Key).Name;

                        Name = Database.GetItemModifier(Material.Key).Name + " " + Name;
                    }
                    else
                    {
                        Name = Database.GetItemModifier(Material.Key).Name + " " + Database.GetItem(Key).Name;
                    }
                }

                if (PostEnchant != null)
                {
                    Name += " " + PostEnchant.Name;
                }
            }
        }

        public void CalculatePower()
        {
            if (Database.Items.ContainsKey(Key) == true)
            {
                if (Material != null && Material.Key != "")
                    Power += Database.GetItemModifier(Material.Key).Power;

                if (Quality != null && Quality.Key != null && Quality.Key != "" && Database.ItemModifiers.ContainsKey(Quality.Key))
                {
                    Power += Database.GetItemModifier(Quality.Key).Power;
                }

                if (PreEnchant != null && PreEnchant.Key != null && PreEnchant.Key != "" && Database.ItemModifiers.ContainsKey(PreEnchant.Key))
                {
                    Power += Database.GetItemModifier(PreEnchant.Key).Power;
                }

                if (PostEnchant != null && PostEnchant.Key != null && PostEnchant.Key != "" && Database.ItemModifiers.ContainsKey(PostEnchant.Key))
                {
                    Power += Database.GetItemModifier(PostEnchant.Key).Power;
                }

                if (Power < 1)
                    Power = 1;
            }
        }

        public void CalculateHoursToBuild()
        {
            if (Material != null && Database.ItemModifiers.ContainsKey(Material.Key))
                HoursToBuild += Database.ItemModifiers[Material.Key].HoursToBuild;

            if (Quality != null && Quality.Key != null && Quality.Key != "" && Database.ItemModifiers.ContainsKey(Quality.Key))
                HoursToBuild += Database.ItemModifiers[Quality.Key].HoursToBuild;

            if (PreEnchant != null && PreEnchant.Key != null && PreEnchant.Key != "" && Database.ItemModifiers.ContainsKey(PreEnchant.Key))
                HoursToBuild += Database.ItemModifiers[PreEnchant.Key].HoursToBuild;

            if (PostEnchant != null && PostEnchant.Key != null && PostEnchant.Key != "" && Database.ItemModifiers.ContainsKey(PostEnchant.Key))
                HoursToBuild += Database.ItemModifiers[PostEnchant.Key].HoursToBuild;
        }

        public void CalculateAttributes()
        {
            CalculatePower();
            CalculateHoursToBuild();
            CalculateTotalResources();
            CalculateDurability();
            CalculateTotalResources();
            CalculateRarity();
        }

        public void CalculateTotalResources()
        {
            if (Database.Items.ContainsKey(Key) == false)
                return;
        }

        public void CalculateDurability()
        {
            if (Database.Items.ContainsKey(Key) == false)
                return;

            if (Material != null)
                DurabilityMax += Material.DurabilityModifier;

            if (Quality != null)
                DurabilityMax += Quality.DurabilityModifier;

            if (PreEnchant != null)
                DurabilityMax += PreEnchant.DurabilityModifier;

            if (PostEnchant != null)
                DurabilityMax += PostEnchant.DurabilityModifier;

            if (DurabilityMax < 1)
                DurabilityMax = 1;

            DurabilityCur = DurabilityMax;
        }

        public void SetText()
        {
            SetName();

            description = "Description for " + Name;
            string attributes = "";
            //string effects = "";

            attributes = "Item Type " + Type + ", " + Rarity.ToString() + " Rarity";

            if (WeaponData != null)
            {
                if (WeaponData.Damage != null)
                {
                    for (int i = 0; i < WeaponData.Damage.Count; i++)
                    {
                        attributes += "\n" + WeaponData.Damage[i].ToString();
                    }
                }

                attributes += "\n" + WeaponData.Attributes[(int)WeaponAttributes.Attack].Value + "% " + WeaponData.AttackType + " Attack Modifier";
                attributes += "\n" + WeaponData.Attributes[(int)WeaponAttributes.Range].Value + " Attack Range";

                attributes += "\n" + WeaponData.Attributes[(int)WeaponAttributes.Actions].Value + " Actions to Use, " + WeaponData.Attributes[(int)WeaponAttributes.Actions].Value + " Action to Equip";

                if (WeaponData.Attributes[(int)WeaponAttributes.Parry].Value != 0)
                    attributes += "\n" + WeaponData.Attributes[(int)WeaponAttributes.Parry].Value + "% Parry Chance";
            }      

            if (WearableData != null)
            {
                attributes += " - " + WearableData.Type;

                attributes += "\n" + WearableData.Attributes[(int)WearableAttributes.Armor].Value + " Armor";

                if (WearableData.Attributes[(int)WearableAttributes.Dodge].Value != 0)
                    attributes += "\n" + WearableData.Attributes[(int)WearableAttributes.Dodge].Value + "% Dodge Chance";

                if (WearableData.Attributes[(int)WearableAttributes.Block].Value != 0)
                    attributes += "\n" + WearableData.Attributes[(int)WearableAttributes.Block].Value + "% Block Chance";


                attributes += "\n";

                if (WearableData.Resistances != null)
                {
                    for (int i = 0; i < WearableData.Resistances.Count; i++)
                    {
                        attributes += WearableData.Resistances[i].Value + "% " + WearableData.Resistances[i].ToString() + " ";
                    }
                }
            }

            if (SkillRequirements.Count > 0)
            {
                attributes += "Skill Requirements";

                for (int i = 0; i < SkillRequirements.Count; i++)
                {
                    attributes += "\n" + Database.Skills[SkillRequirements[i].DefinitionIndex].Name + " " + SkillRequirements[i].Value;

                    if (i != SkillRequirements.Count)
                        attributes += ", ";
                }
            }

            if (UsableData != null)
            {
                attributes += "Usable once every ";
                attributes += UsableData.Cooldown.Type;
            }

            if (AccessoryData != null)
            {
                attributes += " - " + AccessoryData.Type;
            }

            if (Quality != null)
            {
                attributes += "\n";
                attributes += Quality.GetText();
            }

            if (Material != null)
            {
                attributes += "\n";
                attributes += Material.GetText();
            }

            if (PreEnchant != null)
            {
                attributes += "\n";
                attributes += PreEnchant.GetText();
            }

            if (PostEnchant != null)
            {
                attributes += "\n";
                attributes += PostEnchant.GetText();
            }

            if (ArtifactData != null)
            {
                attributes += ArtifactData.ToString();
            }

            if (SetData != null)
            {
                attributes += SetData.ToString();
            }

            attributesTooltip = attributes;
        }

        //public CharacterAction GetAction()
        //{
        //    CharacterAction action = new CharacterAction();

        //    if (WeaponData != null)
        //    {
        //        action.Type = ActionType.Weapon_Attack;
        //        action.DamageList = new List<DamageData>();

        //        for (int i = 0; i < WeaponData.Damage.Count; i++)
        //        {
        //            action.DamageList.Add(new DamageData(WeaponData.Damage[i]));
        //        }
        //    }

        //    return action;
        //}

        public void CalculateRarity()
        {
            if (ArtifactData == null)
            {
                Rarity = Rarity.Common;

                if (Material != null)
                {
                    Rarity = Material.Rarity;
                }

                if (Quality != null)
                {
                    if (Quality.Rarity > Rarity)
                        Rarity = Quality.Rarity;
                }

                if (PreEnchant != null)
                {
                    if (PreEnchant.Rarity > Rarity)
                        Rarity = PreEnchant.Rarity;
                }

                if (PostEnchant != null)
                {
                    if (PostEnchant.Rarity > Rarity)
                        Rarity = PostEnchant.Rarity;
                }

                if (SetData != null)
                {
                    Rarity = Rarity.Set;
                }
            }
            else Rarity = Rarity.Artifact;
        }
    }
}