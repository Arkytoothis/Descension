using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Descension
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] List<Quest> availableQuests = new List<Quest>();
        public List<Quest> AvailableQuests { get { return availableQuests; } }

        [SerializeField] Quest currentQuest = null;
        public Quest CurrentQuest { get { return currentQuest; } }

        public bool HasQuest = false;

        public void Initialize()
        {
            GenerateQuests(5);
            //Load();
        }

        public void GenerateQuests(int numToGenerate)
        {
            for (int i = 0; i < numToGenerate; i++)
            {
                availableQuests.Add(QuestGenerator.Generate());
            }
        }

        public void AcceptQuest(Quest quest)
        {
            if (quest != null)
            {
                //Debug.Log("Quest " + quest.Name + " accepted");
                currentQuest = quest;
                HasQuest = true;
            }
        }

        public void DeclineQuest(Quest quest)
        {
            //Debug.Log("Quest " + quest.Name + " declined");
        }

        public void CancelQuest()
        {
            //Debug.Log("Quest " + currentQuest.Name + " canceled");
            currentQuest = null;
            HasQuest = false;
        }

        public void Save()
        {
            SaveAvailableQuests();
            SaveCurrentQuest();
        }

        private void SaveAvailableQuests()
        {
            QuestList ql = new QuestList();

            foreach (Quest q in availableQuests)
            {
                ql.Quests.Add(q);
            }

            string dataAsJson = JsonUtility.ToJson(ql);
            string filePath = Database.DataPath + "available_quests.json";
            File.WriteAllText(filePath, dataAsJson);
        }

        private void SaveCurrentQuest()
        {
            string dataAsJson = JsonUtility.ToJson(currentQuest);
            string filePath = Database.DataPath + "current_quest.json";
            File.WriteAllText(filePath, dataAsJson);
        }

        public void Load()
        {
            LoadAvailableQuests();
            LoadCurrentQuest();
        }

        private void LoadAvailableQuests()
        {
            string filePath = Database.DataPath + "available_quests.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                QuestList list = JsonUtility.FromJson<QuestList>(dataAsJson);

                foreach (Quest q in list.Quests)
                {
                    availableQuests.Add(q);
                }
            }
        }

        private void LoadCurrentQuest()
        {
            string filePath = Database.DataPath + "current_quest.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                currentQuest = JsonUtility.FromJson<Quest>(dataAsJson);
                HasQuest = true;
                Debug.Log(currentQuest.Name + " loaded");
            }
        }
    }
}