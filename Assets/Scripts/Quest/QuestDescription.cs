using UnityEngine;
using System.Collections.Generic;
using Descension.Core;
using Descension.Name;

namespace Descension
{
    public static class QuestDescription
    {
        public static string[] GroupSizes = new string[] { "Small", "Medium", "Large", "Huge", "Overwhelming" };
        public static string[] EnemyTypes = new string[] { "Human", "Goblin", "Orc", "Skeleton", "Zombie", "Dwarf" };
        public static string[] EnemyDescriptor = new string[] { "Treacherous", "Evil", "Savage", "Worthless", "Hated", "Feared" };
        public static string[] Items = new string[] { "Items", "Resources", "Information" };
        public static string[] Age = new string[] { "n Old", "n Ancient", " Forgotten", " Lost" };
        public static string[] RumorType = new string[] { "Treasure", "Enemy", "Cache", "Hideout" };

        public static string Generate(QuestType type, QuestDifficulty difficulty, Rarity rarity)
        {
            string description = "";

            if (type == QuestType.Battle)
            {
                description = "You must battle a " + GroupSizes[Random.Range(0, GroupSizes.Length)] + " group of " + EnemyTypes[Random.Range(0, EnemyTypes.Length)] + "s.";
            }
            else if (type == QuestType.Conquest)
            {
                description = "You must conquer " + AddLand(Addition.none) + " from the " + EnemyDescriptor[Random.Range(0, EnemyDescriptor.Length)] + " " + EnemyTypes[Random.Range(0, EnemyTypes.Length)] + "s.";
            }
            else if (type == QuestType.Siege)
            {
                description = "You must siege the city " + AddLand(Addition.of);
            }
            else if (type == QuestType.Defense)
            {
                description = "You must defend the land " + AddLand(Addition.of) + " from a " + GroupSizes[Random.Range(0, GroupSizes.Length)] + " group of " + EnemyTypes[Random.Range(0, EnemyTypes.Length)] + "s.";
            }
            else if (type == QuestType.Lore)
            {
                description = "Lore Quest";
            }
            else if (type == QuestType.Merchant)
            {
                FantasyName name = NameGenerator.Get(Gender.Male, "Northern Human", "Citizen");
                description = name.FirstName + " " + name.LastName + ", the famous merchant wants to sell you some " + Items[Random.Range(0, Items.Length)] + ".";
            }
            else if (type == QuestType.Puzzle)
            {
                description = "Puzzle Quest";
            }
            else if (type == QuestType.Rescue)
            {
                FantasyName name = NameGenerator.Get(Gender.Male, "Northern Human", "Citizen");
                description = "You must rescue " + name.FirstName + " " + name.LastName + " from a " + GroupSizes[Random.Range(0, GroupSizes.Length)] + " group of " +
                              EnemyDescriptor[Random.Range(0, EnemyDescriptor.Length)] + " " + EnemyTypes[Random.Range(0, EnemyTypes.Length)] + "s.";
            }
            else if (type == QuestType.Rumor)
            {
                description = "It is rumored that a" + Age[Random.Range(0, Age.Length)] + " " + RumorType[Random.Range(0, Age.Length)] + " was found in this area";
            }
            else if (type == QuestType.Story)
            {
                description = "Story Quest";
            }
            else if (type == QuestType.Tutorial)
            {
                description = "Tutorial Quest";
            }

            return description;
        }

        static string AddLand(Addition addition)
        {
            string land = "";

            if (addition == Addition.of)
                land = " of ";
            else if (addition == Addition._for == true)
                land = " for ";
            else if (addition == Addition.either == true)
            {
                if (Random.Range(0, 100) < 50)
                {
                    land = " of ";
                }
                else
                {
                    land = " for ";
                }
            }

            land += LandName.Generate();

            return land;
        }
    }
}