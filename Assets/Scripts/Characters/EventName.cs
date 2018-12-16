using UnityEngine;
using System.Collections.Generic;
using Descension.Core;

namespace Descension.Name
{
    public enum Addition { of, _for, either, none }

    public static class EventName
    {
        public static string[] BattleEventNames = new string[] { "Battle", "Fight", "War" };

        public static string Generate(GameEventType type, EventDifficulty difficulty, Rarity rarity)
        {
            string name = "";

            if (type == GameEventType.Battle)
            {
                name = BattleEventNames[Random.Range(0, BattleEventNames.Length)] + AddLand(Addition.either);
            }
            else if (type == GameEventType.Conquest)
            {
                name = "Conquest" + AddLand(Addition.either);
            }
            else if (type == GameEventType.Siege)
            {
                name = "Siege" + AddLand(Addition.either);
            }
            else if (type == GameEventType.Conquest)
            {
                name = "Conquest" + AddLand(Addition.either);
            }
            else if (type == GameEventType.Defense)
            {
                name = "Defense" + AddLand(Addition._for);
            }
            else if (type == GameEventType.Lore)
            {
                name = "Lore Event";
            }
            else if (type == GameEventType.Merchant)
            {
                name = "Merchant Event";
            }
            else if (type == GameEventType.Puzzle)
            {
                name = "Puzzle Event";
            }
            else if (type == GameEventType.Quest)
            {
                name = "Quest Event";
            }
            else if (type == GameEventType.Rescue)
            {
                name = "Rescue Event";
            }
            else if (type == GameEventType.Rumor)
            {
                name = "Rumor Event";
            }
            else if (type == GameEventType.Story)
            {
                name = "Story Event";
            }
            else if (type == GameEventType.Tutorial)
            {
                name = "Tutorial Event";
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