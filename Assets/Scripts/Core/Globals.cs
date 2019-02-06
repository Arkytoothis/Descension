using UnityEngine;
using System.Collections.Generic;

namespace Descension.Core
{
    public static class Globals
    {
        public static string EmptyString = "empty";
        public static int AttributeMax = 999999;

        public static List<string> RarityColors = new List<string>
        {
            "#545454",    //Common
            "#FFFFFF",    //Uncommon
            "#117600",    //Rare
            "#00D2C0",    //Fabled        
            "#610057",    //Mythical
            "#B06500",    //Legendary
            "#E8EF00",    //Artifact
            "#13FF00"     //Set
        };
    }

    public enum BaseAttribute
    {
        Strength, Endurance, Agility, Speed, Senses, Intellect, Wisdom, Willpower, Charisma, Memory,
        Number, None
    }

    public enum DerivedAttribute
    {
        Actions, Movement, Armor, Health, Stamina, Essence, Morale,
        Might_Attack, Might_Damage, Finesse_Attack, Finesse_Damage, Spell_Attack, Spell_Power,
        Block, Dodge, Parry, Resistance, Initiative, Perception, Concentration,
        Bonus_Actions, Duration_Modifier, Range_Modifier, 
        Fumble, Critical_Strike, Critical_Damage,
        Number, None
    }

    public enum DamageType
    {
        Physical, Fire, Shock, Cold, Poison, Acid, Unholy, Holy, Psychic, Arcane,
        Number, None
    }

    public enum Skill
    {
        Slashing_Weapons, Piercing_Weapons, Blunt_Weapons, Unarmed, Thrown, Archery, Firearms, Explosives,
        Light_Armor, Medium_Armor, Heavy_Armor, Bucklers, Shields, Leadership, Tactics,
        Fire_Magic, Air_Magic, Water_Magic, Earth_Magic, Death_Magic, Life_Magic, Shadow_Magic, Arcane_Magic, Lore, Channeling,
        Sneaking, Scouting, Tricks, Evasion, Devices, Persuasion, Survival, Medicine,
        Number, None
    }

    public enum WeaponAttributes
    {
        Actions, Attack, Parry, Range,
        Number, None
    }

    public enum AmmoAttributes
    {
        Actions, Attack, Range,
        Number, None
    }

    public enum WearableAttributes
    {
        Actions, Armor, Block, Dodge,
        Number, None
    }

    public enum AccessoryAttributes
    {
        Actions, Cooldown,
        Number, None
    }

    public enum Rarity
    {
        Common, Uncommon, Rare, Fabled, Mythical, Legendary, Set, Artifact,
        Number, None
    };

    public enum AttributeDefinitionType
    {
        Base, Derived_Points, Derived_Score, Derived_Percent, Resistance, Skill, Party, Weapon, Ammo, Wearable, Accessory,
        Number, None
    }

    public enum AttributeComponentType
    {
        Current, Start, Minimum, Maximum, Modifier, Spent, Exp_Cost,
        Number, None
    }

    public enum AttributeType
    {
        Base, Derived, Resistance, Skill, Party, Weapon, Ammo, Wearable, Accessory,
        Number, None
    }

    public enum DerivedAttributeType
    {
        Derived_Points, Derived_Percent, Derived_Score,
        Number, None
    }

    public enum AttributeModifierType
    {
        Base_Attribute, Derived_Attribute, Resistance, Skill, Party_Attribute, Value, Race,
        Number, None
    }

    public enum AttributeField
    {
        Start, Current, Minimum, Maximum, Modifier
    }

    public enum Gender
    {
        Male, Female, Both, Either, Other, None, Number,
    };

    public enum SkillCategory
    {
        Combat, Magic, Misc
    };

    public enum EquipmentSlot
    {
        Right_Hand, Left_Hand, Body, Head, Shoulders, Hands, Feet, Back, Neck, Waist, Left_Finger, Right_Finger, Wrists, Ammo, 
        Number, None
    };

    public enum EquipmentSlotAbb
    {
        RH, LH, BD, HD, SH, HA, FT, BK, NK, WT, LF, RF, WR, AM, 
        Number, None
    };

    public enum CharacterRenderSlot
    {
        Right_Hand, Left_Hand, Ammo, Head, Body, Right_Shoulder, Left_Shoulder, Left_Wrist, Right_Wrist, Right_Glove, Left_Glove,
        Right_Foot, Left_Foot, Back, Neck, Waist, Hip, Right_Finger, Left_Finger, Hair, Beard, Face, 
        Number, None
    }

    public enum ItemType
    {
        Weapon, Ammo, Wearable, Accessory, Quest, Ingredient,
        Number, None
    };

    public enum WeaponType
    {
        Unarmed, One_Handed_Melee, Two_Handed_Melee, Polearm, Bow, Crossbow, Thrown, Firearm,
        None, Number
    }

    public enum AmmoType
    {
        Arrow, Bolt, Bullet, Sling_Stone,
        None, Number
    }

    public enum WearableType
    {
        Clothing, Armor, Shield, Jewelry,
        None, Number
    }

    public enum AccessoryType
    {
        Consumable, Scroll, Throwable, Usable, Tool,
        None, Number
    }

    public enum IngredientType
    {
        Material, Reagent,
        None, Number
    }

    public enum ItemModifierType
    {
        Material, Quality, Plus_Enchant, Pre_Enchant, Post_Enchant,
        Number, None
    }

    public enum ItemTypeAllowed
    {
        Any,
        Weapon, One_Handed_Melee, Two_Handed_Melee, Polearm, Bow, Crossbow, Thrown, Firearm,
        Ammo, Arrow, Bolt, Bullet, Sling_Stone,
        Wearable, Clothing, Armor, Shield, Jewelry,
        Accessory, Consumable, Scroll, Throwable, Usable, Tool,
        Number, None
    }

    public enum MaterialHardness
    {
        Cloth, Leather, Soft, Stone, Metal,
        Liquid, Paper, Food,
        Number, Any, None
    };

    public enum ItemHardnessAllowed
    {
        Soft, Hard, Soft_or_Hard, Organic, Cloth, Leather, Metal, Stone,
        Potion, Scroll, Food,
        Number, Any, None
    };

    public enum MapDirection
    {
        North, North_East, East, South_East, South, South_West, West, North_West, Center
    };

    public enum QuestType
    {
        Story, Lore, Battle, Conquest, Defense, Siege, Rescue, Rumor, Merchant, Puzzle, Tutorial,
        Number, Blank
    };

    public enum QuestDifficulty
    {
        Very_Easy, Easy, Average, Hard, Very_Hard, Impossible, Number, None
    };

    public enum MapTheme
    {
        Bandits, Undead, Goblinoids, Animals, 
        Number, None
    };

    public enum MapLocation
    {
       Forest, Swamp, Desert, Tundra, Mountians, 
       Village, Town, Ruins, Camp,
       Dungeon, Tomb, Cavern, Mine, Hive, 
       Number, None
    };

    public enum MapWeather
    {
        Rain, Wind, Storm, Snow, Blizzard, Fog,
        Number, None
    };

    public enum MapType
    {
        Outdoors, Indoors, Underground,
        Number, None
    };

    public enum EventState
    {
        Active, Inactive, Deactivate
    };

    public enum ItemNameFormat
    {
        Material_First, Material_Middle, Material_Last, Artifact,
        Number, None
    };

    public enum ItemEffectType
    {
        Attribute, Skill, Damage, Resistance, Weapon_Attribute, Armor_Attribute, Trait, Power, Spell,
        Number, None
    };

    public enum AbilityClass
    {
        World, Encounter, Either, Number, None
    };

    public enum AbilityType
    {
        Power, Spell, Trait, Positive_Quirk, Neutral_Quirk, Negative_Quirk, Wound, Defect, Number, None
    };

    public enum SpellSchoolType
    {
        Fire, Air, Water, Earth, Death, Life, Shadow, Arcane, Number, None
    }

    public enum TraitType
    { Race, Profession, Wound, Background, Misc, Number, None }

    public enum AreaType
    { Single, Sphere, Rectangle, Cone, Beam, Number, None }

    public enum DurationType
    { Instant, Permanent, Duration, Number, None }

    public enum RangeType
    { Self, Distance, Touch, Weapon, Number, None }

    public enum TargetType
    { Self, Any, Friend, Enemy, Number, None }

    public enum TimeType
    { Minute, Hour, Day, Month, Year, Turn, Number, None }

    public enum TriggerType
    {
        Always_On, Use, Cast, Channel, On_Attack, On_Damage, On_Miss, On_Defense, On_Dodge, On_Block, On_Damaged,
        Number, None
    }

    public enum RewardType
    {
        Resource, Item, Exp, Unit, Unlock,
        Number, None
    };

    public enum RollType
    {
        Percentile, Attribute, Skill, Attack, Defense, Resistance,
        Number, None
    }
    public enum CheckType
    {
        Party_Highest, Party_Combined, Party_Individual, Character, Leader, Number, None
    }

    public enum ActionType
    {
        Movement, Weapon_Attack, Unarmed_Attack, Power, Spell, Item,
        Number, None
    }

    public enum BodyType
    {
        Large, Normal, Small,
        Number, None
    }

    public enum WeaponGripType
    {
        Right_Hand, Left_Hand, Either_Hand, Both_Hands,
        Number, None
    }

    public enum Species
    {
        Animal, Beast, Undead, Humanoid, Elemental, Draconic, Insect,
        Number, None

    }
    public enum BodySize
    {
        Miniscule, Tiny, Small, Medium, Large, Huge, Gigantic,
        Number, None
    }

    public enum Faction
    {
        Player, Enemy, Neutral,
        Number, None
    }
}