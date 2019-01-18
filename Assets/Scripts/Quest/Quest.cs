using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    [System.Serializable]
    public class QuestList
    {
        public List<Quest> Quests;

        public QuestList()
        {
            Quests = new List<Quest>();
        }
    }

    [System.Serializable]
    public class Quest
    {
        [SerializeField] string name = "";
        public string Name { get { return name; } }

        [SerializeField] string description = "";
        public string Description { get { return description; } }

        [SerializeField] QuestType type = QuestType.Blank;
        public QuestType Type { get { return type; } }

        [SerializeField] QuestDifficulty difficulty = QuestDifficulty.None;
        public QuestDifficulty Difficulty { get { return difficulty; } }

        [SerializeField] Rarity rarity = Rarity.None;
        public Rarity Rarity { get { return rarity; } }

        [SerializeField] int goldReward;
        public int GoldReward { get { return goldReward; } }

        [SerializeField] int experienceReward;
        public int ExperienceReward { get { return experienceReward; } }

        [SerializeField] MapData mapData = null;
        public MapData MapData { get { return mapData; } }

        [SerializeField] 
        public Quest()
        {
            name = "";
            description = "";
            type = QuestType.Blank;
            difficulty = QuestDifficulty.None;
            rarity = Rarity.None;
            goldReward = 0;
            experienceReward = 0;
            mapData = null;
        }

        public Quest(string name, string description, QuestType type, QuestDifficulty difficulty, Rarity rarity, int goldReward, int experienceReward, MapData mapData)
        {
            this.name = name;
            this.description = description;
            this.type = type;
            this.difficulty = difficulty;
            this.rarity = rarity;
            this.goldReward = goldReward;
            this.experienceReward = experienceReward;
            this.mapData = new MapData(mapData);
        }

        public Quest(Quest quest)
        {
            name = quest.name;
            description = quest.description;
            type = quest.type;
            difficulty = quest.difficulty;
            rarity = quest.rarity;
            goldReward = quest.goldReward;
            experienceReward = quest.experienceReward;
            mapData = new MapData(quest.mapData);
        }
    }
}