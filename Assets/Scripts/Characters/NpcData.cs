using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Descension.Characters;
using Descension.Abilities;
using Descension.Core;

namespace Descension
{
    public enum CombatStatus { Active, Unconcisous, Dead, Number, None }

    public enum NpcType
    {
        Boss, Mini_Boss, Boss_Guard, Objective_Enemy, Powerful_Enenmy, Enemy, Weak_Enemy,
        Citizen, Rescue_Target, Survivor, Neutral, Hireling, Story, Trader,
        Number, None
    }

    [System.Serializable]
    public class NpcData : CharacterData
    {
        [SerializeField] NpcType type;
        [SerializeField] string definition;
        [SerializeField] string model;
        [SerializeField] int index;
        [SerializeField] int level;
        [SerializeField] int expValue;
        [SerializeField] List<Ability> abilities;
        [SerializeField] CombatStatus combatStatus;

        public NpcType Type { get => type; set => type = value; }
        public string Definition { get => definition; set => definition = value; }
        public string Model { get => model; set => model = value; }
        public int Index { get => index; set => index = value; }
        public int Level { get => level; set => level = value; }
        public int ExpValue { get => expValue; set => expValue = value; }
        public List<Ability> Abilities { get => abilities; set => abilities = value; }
        public CombatStatus CombatStatus { get { return combatStatus; } }

        public new void SetStart(AttributeType type, int attribute, int start, int min, int max)
        {
            if (start == 0) return;
            Attributes.SetStart((AttributeListType)type, attribute, start, min, max);
        }

        //public event OnArmorChange onArmorChange;
        //public event OnHealthChange onHealthChange;
        //public event OnStaminaChange onStaminaChange;
        //public event OnEssenceChange onEssenceChange;
        //public event OnMoraleChange onMoraleChange;
        //public event OnDeath onDeath;
        //public event OnRevive onRevive;
        //public event OnInteract onInteract;
        //public event OnAttack onAttack;

        public NpcData()
        {
            type = NpcType.None;
            name = new FantasyName();
            gender = Gender.None;
            background = null;

            model = "";
            definition = "";
            combatStatus = CombatStatus.None;
            raceKey = "";
            professionKey = "";
            index = -1;
            hair = "";
            beard = "";
            faction = Faction.None;

            level = 0;
            expValue = 0;

            description = "";

            attributes = new AttributeManager();

            for (int i = 0; i < (int)BaseAttribute.Number; i++)
                attributes.AddAttribute(AttributeListType.Base, new Attribute(AttributeType.Base, i, 0));

            for (int i = 0; i < (int)DerivedAttribute.Number; i++)
                attributes.AddAttribute(AttributeListType.Derived, new Attribute(AttributeType.Derived, i, 0));

            for (int i = 0; i < (int)DamageType.Number; i++)
                attributes.AddAttribute(AttributeListType.Resistance, new Attribute(AttributeType.Resistance, i, 0));

            abilities = new List<Ability>();
            inventory = new CharacterInventory();
        }

        public NpcData(FantasyName name, Gender gender, string definition, string model, int index, int map_x, int map_y, Faction faction)
        {
            this.name = new FantasyName(name);
            this.background = new Background();

            this.definition = definition;
            this.gender = gender;
            this.index = index;
            this.model = model;
            this.faction = faction;

            combatStatus = CombatStatus.Active;
            attributes = new AttributeManager();

            for (int i = 0; i < (int)BaseAttribute.Number; i++)
            {
                attributes.AddAttribute(AttributeListType.Base, new Attribute(AttributeType.Base, i, 0));
            }

            for (int i = 0; i < (int)DerivedAttribute.Number; i++)
            {
                attributes.AddAttribute(AttributeListType.Derived, new Attribute(AttributeType.Derived, i, 0));
            }

            for (int i = 0; i < (int)DamageType.Number; i++)
            {
                attributes.AddAttribute(AttributeListType.Resistance, new Attribute(AttributeType.Resistance, i, 0));
            }

            abilities = new List<Ability>();
            inventory = new CharacterInventory();
        }

        public NpcData(NpcData npc)
        {
            name = new FantasyName(npc.Name);
            definition = npc.definition;
            index = npc.index;
            level = npc.level;
            expValue = npc.expValue;
            model = npc.model;
            faction = npc.faction;

            combatStatus = npc.combatStatus;
            attributes = new AttributeManager();

            for (int i = 0; i < (int)BaseAttribute.Number; i++)
            {
                attributes.AddAttribute(AttributeListType.Base, new Attribute(npc.attributes.GetAttribute(AttributeListType.Base, i)));
            }

            for (int i = 0; i < (int)DerivedAttribute.Number; i++)
            {
                attributes.AddAttribute(AttributeListType.Derived, new Attribute(npc.attributes.GetAttribute(AttributeListType.Derived, i)));
            }

            for (int i = 0; i < (int)DamageType.Number; i++)
            {
                attributes.AddAttribute(AttributeListType.Resistance, new Attribute(npc.attributes.GetAttribute(AttributeListType.Resistance, i)));
            }

            abilities = new List<Ability>();
            inventory = new CharacterInventory(npc.inventory);
        }

        public NpcData(NPCDefinition def)
        {
            name = new FantasyName(def.name);
            definition = def.key;
            index = -1;
            model = def.model;
            faction = def.faction;

            level = Random.Range(def.minLevel, def.maxLevel);
            expValue = def.expPerLevel * level;

            combatStatus = CombatStatus.Active;

            attributes = new AttributeManager();

            for (int i = 0; i < (int)BaseAttribute.Number; i++)
            {
                int value = def.baseStart[i] + (def.BasePerLevel[i].Roll(false) * level);
                attributes.AddAttribute(AttributeListType.Base, new Attribute(i, AttributeType.Base, value, Database.BaseAttributes[i].Minimum, value));
            }

            for (int i = 0; i < (int)DerivedAttribute.Number; i++)
            {
                int value = def.derivedPerLevel[i].Roll(false) * level;
                attributes.AddAttribute(AttributeListType.Derived, new Attribute(i, AttributeType.Derived, value, Database.DerivedAttributes[i].Minimum, value));
            }

            for (int i = 0; i < (int)DamageType.Number; i++)
            {
                //attributes.AddAttribute(AttributeListType.Resistance, new Attribute(def.attributes.GetAttribute(AttributeListType.Resistance, i)));
            }

            abilities = new List<Ability>();
            inventory = new CharacterInventory(def.inventory);
        }
    }
}