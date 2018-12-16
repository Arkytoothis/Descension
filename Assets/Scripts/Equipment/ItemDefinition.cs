using UnityEngine;
using System.Collections.Generic;
using Descension.Characters;
using Descension.Core;

namespace Descension.Equipment
{
    [System.Serializable]
    public class ItemDefinition
    {
        public string Name;
        public string Key;
        public string Description;

        public string IconKey;
        public string MeshKey;
        public string TextureKey;

        public string particleEffect;
        public string equipSound;
        public string unequipSound;

        public Vector3 portraitRotation;
        public Vector3 worldRotation;

        public Vector3 portraitPosition;
        public Vector3 worldPosition;

        public ItemNameFormat NameFormat;
        public ItemType Type;
        public EquipmentSlot Slot;
        public ItemHardnessAllowed Hardness;

        public int Actions;
        public int BasePower;
        public int BaseBuildTime;
        public int DurabilityMax;

        public List<SkillRequirement> SkillRequirements;
        public int Value;

        public WeaponData WeaponData;
        public AmmoData AmmoData;
        public WearableData WearableData;
        public AccessoryData AccessoryData;
        public IngredientData IngredientData;
        public UsableData UsableData;

        public string TooltipText;

        public ItemDefinition()
        {
            Name = "";
            Key = "";
            Description = "empty";

            IconKey = "";
            MeshKey = "";
            TextureKey = "";

            particleEffect = "";
            equipSound = "";
            unequipSound = "";

            Type = ItemType.None;
            Slot = EquipmentSlot.None;
            Hardness = ItemHardnessAllowed.None;
            NameFormat = ItemNameFormat.None;

            Actions = 0;
            BasePower = 0;
            BaseBuildTime = 0;
            DurabilityMax = 0;

            WeaponData = null;
            AmmoData = null;
            WearableData = null;
            AccessoryData = null;
            IngredientData = null;
            UsableData = null;

            BaseBuildTime = 0;
            SkillRequirements = new List<SkillRequirement>();
            Value = 0;
            TooltipText = "";

            portraitRotation = Vector3.zero;
            portraitPosition = Vector3.zero;

            worldRotation = Vector3.zero;
            worldPosition = Vector3.zero;
        }

        public ItemDefinition(string name, string key, string iconKey, string meshKey, string textureKey, EquipmentSlot slot, int durability, int power, int hours, int value,
            int actions, ItemType type, ItemHardnessAllowed hardness, ItemNameFormat format,
            WeaponData weapon, AmmoData ammo, WearableData armor, AccessoryData accessory, IngredientData ingredient, UsableData usable,
            Vector3 portraitRotation, Vector3 worldRotation, Vector3 portraitPosition, Vector3 worldPosition, string particleEffect)
        {
            Name = name;
            Key = key;
            Description = "empty";

            Type = type;
            NameFormat = format;
            IconKey = iconKey;
            MeshKey = meshKey;
            TextureKey = textureKey;
            this.particleEffect = particleEffect;
            Value = value;

            Actions = actions;
            BasePower = power;
            BaseBuildTime = hours;
            Slot = slot;
            DurabilityMax = durability;
            Hardness = hardness;

            if (weapon != null)
            {
                WeaponData = new WeaponData(weapon);
            }
            else
                WeaponData = null;

            if (ammo != null)
            {
                AmmoData = new AmmoData(ammo);
            }
            else
                AmmoData = null;

            if (armor != null)
            {
                WearableData = new WearableData(armor);
            }
            else
                WearableData = null;

            if (accessory != null)
            {
                AccessoryData = new AccessoryData(accessory);
            }
            else
                AccessoryData = null;

            if (ingredient != null)
            {
                IngredientData = new IngredientData(ingredient);
            }
            else
                IngredientData = null;

            if (usable != null)
                UsableData = new UsableData(usable);
            else
                UsableData = null;

            SkillRequirements = new List<SkillRequirement>();
            SetTooltipText();

            this.portraitRotation = portraitRotation;
            this.portraitPosition = portraitPosition;

            this.worldRotation = worldRotation;
            this.worldPosition = worldPosition;
        }

        public ItemDefinition(ItemDefinition item)
        {
            Name = item.Name;
            Key = item.Key;
            Description = item.Description;

            Type = item.Type;
            NameFormat = item.NameFormat;
            IconKey = item.IconKey;
            MeshKey = item.MeshKey;
            TextureKey = item.TextureKey;
            particleEffect = item.particleEffect;

            Actions = item.Actions;
            BaseBuildTime = item.BaseBuildTime;
            BasePower = item.BasePower;
            Slot = item.Slot;
            DurabilityMax = item.DurabilityMax;

            Hardness = item.Hardness;

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

            if (item.UsableData != null)
                UsableData = new UsableData(item.UsableData);
            else
                UsableData = null;

            Value = item.Value;

            SkillRequirements = new List<SkillRequirement>();
            for (int i = 0; i < item.SkillRequirements.Count; i++)
            {
                SkillRequirements.Add(new SkillRequirement(item.SkillRequirements[i]));
            }

            SetTooltipText();

            portraitRotation = item.portraitRotation;
            portraitPosition = item.portraitPosition;

            worldRotation = item.worldRotation;
            worldPosition = item.worldPosition;
        }

        public void SetTooltipText()
        {

        }
    }
}