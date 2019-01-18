using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class TavernScreen : TownScreen
    {
        [SerializeField] QuestManager questManager = null;
        [SerializeField] List<GameObject> availableQuestElements = new List<GameObject>();
        [SerializeField] GameObject availableQuestElementPrefab = null;
        [SerializeField] Transform availableQuestElementsParent = null;

        [SerializeField] SelectedQuestPanel selectedQuestPanel = null;
        [SerializeField] CurrentQuestPanel currentQuestPanel = null;

        public override void Initialize()
        {
            base.Initialize();
            questManager = TownManager.instance.QuestManager;
            UpdateAvailableQuests();
            selectedQuestPanel.Clear();
            UpdateCurrentQuest();
        }

        private void UpdateAvailableQuests()
        {
            ClearAvailableQuests();

            for (int i = 0; i < questManager.AvailableQuests.Count; i++)
            {
                GameObject questObject = Instantiate(availableQuestElementPrefab, availableQuestElementsParent);
                questObject.name = "Quest Object";

                AvailableQuestElement questScript = questObject.GetComponent<AvailableQuestElement>();

                int index = i;
                questScript.Setup(i, questManager.AvailableQuests[i], delegate { SelectAvailableQuest(index); });

                availableQuestElements.Add(questObject);
            }
        }

        private void UpdateCurrentQuest()
        {
            currentQuestPanel.Clear();

            if (questManager.HasQuest == true)
            {
                currentQuestPanel.SelectQuest(questManager.CurrentQuest);
            }
        }

        private void ClearAvailableQuests()
        {
            foreach (Transform child in availableQuestElementsParent)
            {
                Destroy(child.gameObject);
            }

            availableQuestElements.Clear();
        }

        public void SelectAvailableQuest(int index)
        {
            Quest quest = questManager.AvailableQuests[index];

            if (quest != null)
            {
                selectedQuestPanel.Clear();
                selectedQuestPanel.SelectQuest(quest);
            }
        }

        public void AcceptQuest()
        {
            Quest quest = selectedQuestPanel.SelectedQuest;

            if (quest != null)
            {
                questManager.AcceptQuest(quest);
                selectedQuestPanel.Clear();
                questManager.AvailableQuests.Remove(quest);
                UpdateAvailableQuests();
                UpdateCurrentQuest();
            }
        }

        public void DeclineQuest()
        {
            Quest quest = selectedQuestPanel.SelectedQuest;

            if (quest != null)
            {
                selectedQuestPanel.Clear();
                questManager.AvailableQuests.Remove(quest);
                UpdateAvailableQuests();
                questManager.DeclineQuest(quest);
            }
        }

        public void CancelQuest()
        {
            if (questManager.HasQuest == true)
            {
                questManager.CancelQuest();
                UpdateCurrentQuest();
            }
        }
    }
}