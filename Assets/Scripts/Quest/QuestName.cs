using UnityEngine;
using System.Collections.Generic;
using Descension.Core;
using Descension.Name;

namespace Descension
{
    public enum Addition { of, _for, either, none }

    public static class QuestName
    {
        public static string[] BattleEventNames = new string[] { "Battle", "Fight", "War" };

        public static string Generate(QuestType type, QuestDifficulty difficulty, Rarity rarity)
        {
            string name = "";

            if (type == QuestType.Battle)
            {
                name = BattleEventNames[Random.Range(0, BattleEventNames.Length)] + AddLand(Addition.either);
            }
            else if (type == QuestType.Conquest)
            {
                name = "Conquest" + AddLand(Addition.either);
            }
            else if (type == QuestType.Siege)
            {
                name = "Siege" + AddLand(Addition.either);
            }
            else if (type == QuestType.Conquest)
            {
                name = "Conquest" + AddLand(Addition.either);
            }
            else if (type == QuestType.Defense)
            {
                name = "Defense" + AddLand(Addition._for);
            }
            else if (type == QuestType.Lore)
            {
                name = "Lore Quest";
            }
            else if (type == QuestType.Merchant)
            {
                name = "Merchant Quest";
            }
            else if (type == QuestType.Puzzle)
            {
                name = "Puzzle Quest";
            }
            else if (type == QuestType.Rescue)
            {
                name = "Rescue Quest";
            }
            else if (type == QuestType.Rumor)
            {
                name = "Rumor Quest";
            }
            else if (type == QuestType.Story)
            {
                name = "Story Quest";
            }
            else if (type == QuestType.Tutorial)
            {
                name = "Tutorial Quest";
            }

            return name;
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