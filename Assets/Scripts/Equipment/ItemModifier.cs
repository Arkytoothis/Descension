using UnityEngine;
using System.Collections.Generic;
using Descension.Abilities;
using Descension.Core;

namespace Descension.Equipment
{
    [System.Serializable]
    public class ItemModifier
    {
        public string Name;
        public string Key;
        public string Description;

        public int Actions;
        public int Power;
        public int DurabilityModifier;
        public int HoursToBuild;

        public Rarity Rarity;
        public ItemModifierType Type;
        public ItemTypeAllowed AllowedItemType;
        public MaterialHardness MaterialHardness;

        public GameColor MainColor;
        public GameColor SecondaryColor;
        public int Value;

        public List<AbilityComponent> Components;
        public List<AbilityEffect> Effects;

        public ItemModifier()
        {
            Name = "";
            Key = "";
            Description = "empty";

            Power = 0;
            HoursToBuild = 0;
            Actions = 0;
            Rarity = Rarity.Common;
            Type = ItemModifierType.None;
            AllowedItemType = ItemTypeAllowed.None;
            MaterialHardness = MaterialHardness.None;

            MainColor = new GameColor();
            SecondaryColor = new GameColor();
            Components = new List<AbilityComponent>();
            Effects = new List<AbilityEffect>();
        }

        public ItemModifier(string name, string key, int power, int hours, int value, int dur_mod, int actions,
            ItemModifierType type, MaterialHardness hardness, ItemTypeAllowed item_type, Rarity rarity)
        {
            Name = name;
            Key = key;
            Description = "empty";

            Type = type;

            Actions = actions;
            Power = power;
            HoursToBuild = hours;
            DurabilityModifier = dur_mod;
            Value = value;
            Rarity = rarity;
            MaterialHardness = hardness;
            AllowedItemType = item_type;

            MainColor = new GameColor();
            SecondaryColor = new GameColor();

            Components = new List<AbilityComponent>();
            Effects = new List<AbilityEffect>();
        }

        public ItemModifier(ItemModifier data)
        {
            if (data != null)
            {
                Name = data.Name;
                Key = data.Key;
                Description = data.Description;

                Type = data.Type;
                MaterialHardness = data.MaterialHardness;
                AllowedItemType = data.AllowedItemType;
                HoursToBuild = data.HoursToBuild;
                DurabilityModifier = data.DurabilityModifier;
                Actions = data.Actions;
                Rarity = data.Rarity;

                MainColor = new GameColor(data.MainColor);
                SecondaryColor = new GameColor(data.SecondaryColor);

                Value = data.Value;

                Components = new List<AbilityComponent>();

                for (int i = 0; i < data.Components.Count; i++)
                {
                    Components.Add(data.Components[i]);
                }

                Effects = new List<AbilityEffect>();

                for (int i = 0; i < data.Effects.Count; i++)
                {
                    Effects.Add(data.Effects[i]);
                }
            }
            else
            {
                Debug.Log("ItemModifier(ItemModifier data) data == null");
                Name = "";
                Key = "";
                Power = 0;
                HoursToBuild = 0;
                Actions = 0;
                Rarity = Rarity.Common;
                Type = ItemModifierType.None;
                AllowedItemType = ItemTypeAllowed.None;
                MaterialHardness = MaterialHardness.None;

                MainColor = new GameColor();
                SecondaryColor = new GameColor();

                Components = new List<AbilityComponent>();
                Effects = new List<AbilityEffect>();
            }
        }

        public string GetText()
        {
            string text = "\n<b><color=" + Globals.RarityColors[(int)Rarity] + ">" + Name + "</b>\n";

            for (int i = 0; i < Effects.Count; i++)
            {
                if (i != 0)
                    text += ", ";

                text += Effects[i].GetTooltipString();
            }

            return text;
        }
    }
}