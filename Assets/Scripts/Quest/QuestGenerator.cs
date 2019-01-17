using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public static class QuestGenerator
    {
        static bool initialized = false;

        public static void Initialize()
        {
            if (initialized == false)
            {
                initialized = true;

            }
        }

        public static Quest Generate()
        {
            QuestType type = (QuestType)Random.Range(0, (int)QuestType.Number);
            QuestDifficulty difficulty = (QuestDifficulty)Random.Range(0, (int)QuestDifficulty.Number);
            Rarity rarity = (Rarity)Random.Range(0, (int)Rarity.Number);
            string name = QuestName.Generate(type, difficulty, rarity);
            string description = QuestDescription.Generate(type, difficulty, rarity);
            int gold = Random.Range(10, 100);
            int exp = Random.Range(10, 100);

            Quest quest = new Quest(name, description, type, gold, exp);

            return quest;
        }
    }
}