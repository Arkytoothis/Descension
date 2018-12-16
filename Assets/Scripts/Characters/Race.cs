using UnityEngine;
using System.Collections.Generic;
using Descension.Abilities;
using Descension.Equipment;
using Descension.Core;

namespace Descension.Characters
{
    [System.Serializable]
    public class Race
    {
        public string Name;
        public string Key;
        public string Description;

        public string maleModelPath;
        public string femaleModelPath;
        public string wingsPath;
        public string hornsPath;

        public int BaseSpeed;
        public float ExpModifier;
        public GameValue HealthPerLevel;
        public GameValue StaminaPerLevel;
        public GameValue EssencePerLevel;

        public bool Short;

        public string maleDefaultHair;
        public string maleDefaultBeard;
        public string femaleDefaultHair;
        public string femaleDefaultBeard;

        public bool HelmAllowed;
        public bool ShoesAllowed;

        public List<GameValue> StartingAttributes;
        public List<SkillProficiency> SkillProficiencies;
        public List<ResistanceData> Resistances;

        public List<AbilityUnlock> Traits;
        public List<AbilityUnlock> Powers;
        public List<AbilityUnlock> Spells;

        public UpkeepData Upkeep;
        public GameValue StartingWealth;

        public Vector3 scale;

        public Race()
        {
            Name = "";
            Key = "";
            Description = "empty";

            StartingAttributes = new List<GameValue>();
            SkillProficiencies = new List<SkillProficiency>();
            Resistances = new List<ResistanceData>();

            Traits = new List<AbilityUnlock>();
            Powers = new List<AbilityUnlock>();
            Spells = new List<AbilityUnlock>();

            ExpModifier = 1.0f;
            BaseSpeed = 10;

            Upkeep = new UpkeepData();
            StartingWealth = new GameValue();
            scale = Vector3.one;

            maleDefaultHair = "";
            maleDefaultBeard = "";
            femaleDefaultHair = "";
            femaleDefaultBeard = "";
        }

        public Race(string name, string key, string male, string female, bool small, string wings, string horns, bool helm, bool shoes,
                    GameValue health, GameValue stamina, GameValue essence, int movement, float exp_mod, UpkeepData upkeep, GameValue wealth,
                    Vector3 scale, string maleHair, string maleBeard, string femaleHair, string femaleBeard)
        {
            Name = name;
            Key = key;
            Description = "empty";
            maleModelPath = male;
            femaleModelPath = female;

            Short = small;
            wingsPath = wings;
            hornsPath = horns;

            maleDefaultHair = maleHair;
            maleDefaultBeard = maleBeard;
            femaleDefaultHair = femaleHair;
            femaleDefaultBeard = femaleBeard;

            HelmAllowed = helm;
            ShoesAllowed = shoes;
            this.scale = scale;

            StartingAttributes = new List<GameValue>();
            for (int i = 0; i < (int)BaseAttribute.Number; i++)
            {
                StartingAttributes.Add(new GameValue(5, 4));
            }

            SkillProficiencies = new List<SkillProficiency>();
            Resistances = new List<ResistanceData>();

            Traits = new List<AbilityUnlock>();
            Powers = new List<AbilityUnlock>();
            Spells = new List<AbilityUnlock>();

            HealthPerLevel = new GameValue(health.Number, health.Die);
            StaminaPerLevel = new GameValue(stamina.Number, stamina.Die);
            EssencePerLevel = new GameValue(essence.Number, essence.Die);

            ExpModifier = exp_mod;
            BaseSpeed = movement;

            Upkeep = new UpkeepData(upkeep);
            StartingWealth = new GameValue(wealth);
        }

        public bool ArmorSlotAllowed(EquipmentSlot slot)
        {
            bool allowed = true;

            if (slot == EquipmentSlot.Head && HelmAllowed == false)
                allowed = false;

            if (slot == EquipmentSlot.Feet && ShoesAllowed == false)
                allowed = false;

            return allowed;
        }
    }
}