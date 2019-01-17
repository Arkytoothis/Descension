using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    [System.Serializable]
    public class Quest
    {
        [SerializeField] string name = "";
        public string Name { get { return name; } }
        [SerializeField] string description = "";
        public string Description { get { return description; } }
        [SerializeField] QuestType type = QuestType.Blank;
        public QuestType Type { get { return type; } }
        [SerializeField] int goldReward;
        public int GoldReward { get { return goldReward; } }
        [SerializeField] int experienceReward;
        public int ExperienceReward { get { return experienceReward; } }


        public Quest()
        {
            name = "";
            description = "";
            type = QuestType.Blank;
            goldReward = 0;
            experienceReward = 0;
        }

        public Quest(string name, string description, QuestType type, int gold, int experience)
        {
            this.name = name;
            this.description = description;
            this.type = type;
            this.goldReward = gold;
            this.experienceReward = experience;
        }
    }
}