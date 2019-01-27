using Descension.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class AdventureScreen : TownScreen
    {
        [SerializeField] QuestManager questManager = null;
        public QuestManager QuestManager { get { return questManager; } }

        [SerializeField] QuestPanel questPanel = null;
        public QuestPanel QuestPanel { get { return questPanel; } }

        [SerializeField] GameObject availablePcElementPrefab = null;
        [SerializeField] Transform availablePcElementsParent = null;
        [SerializeField] List<GameObject> availablePcElements = new List<GameObject>();

        [SerializeField] GameObject currentPcElementPrefab = null;
        [SerializeField] Transform currentPcElementsParent = null;
        [SerializeField] List<GameObject> currentPcElements = new List<GameObject>();

        public override void Initialize()
        {
            base.Initialize();
            questManager = TownManager.instance.QuestManager;
            UpdateQuestPanel();

            UpdateAvailablePcs();
            UpdateParty();
        }

        private void UpdateAvailablePcs()
        {
            ClearAvailablePcsList();

            for (int i = 0; i < PcManager.instance.PcDataList.Count; i++)
            {
                if (PcManager.instance.PcDataList[i].PartyIndex == -1)
                {
                    GameObject elementObject = Instantiate(availablePcElementPrefab, availablePcElementsParent);

                    AvailablePcElement elementScript = elementObject.GetComponent<AvailablePcElement>();
                    int index = i;
                    elementScript.Setup(PcManager.instance.PcDataList[i], delegate { AddPcToParty(index); });

                    availablePcElements.Add(elementObject);
                }
            }
        }

        private void UpdateParty()
        {
            ClearParty();

            for (int i = 0; i < PcManager.instance.PcDataList.Count; i++)
            {
                if (PcManager.instance.PcDataList[i].PartyIndex != -1)
                {
                    GameObject elementObject = Instantiate(currentPcElementPrefab, currentPcElementsParent);

                    PartyPcElement elementScript = elementObject.GetComponent<PartyPcElement>();
                    int index = i;
                    elementScript.Setup(PcManager.instance.PcDataList[i], delegate { RemovePcFromParty(index); });

                }
            }
        }

        private void UpdateQuestPanel() 
        {
            ClearQuestPanel();

            if (questManager.HasQuest == true)
            {
                questPanel.SelectQuest(questManager.CurrentQuest);
            }
        }

        private void ClearQuestPanel()
        {
            questPanel.Clear();
        }

        public override void Open()
        {
            base.Open();
            UpdateQuestPanel();
        }

        public void GenerateQuest()
        {
            questManager.AcceptQuest(QuestGenerator.Generate());
            UpdateQuestPanel();
        }

        private void AddPcToParty(int index)
        {
            if (PcManager.instance.PartySize < PcManager.instance.PartyData.MaxSize)
            {
                PcManager.instance.AddToParty(index);

                UpdateAvailablePcs();
                UpdateParty();
            }
        }

        private void RemovePcFromParty(int index)
        {
            PcManager.instance.RemoveFromParty(index);

            UpdateAvailablePcs();
            UpdateParty();
        }

        private void ClearAvailablePcsList()
        {
            foreach (Transform child in availablePcElementsParent)
            {
                Destroy(child.gameObject);
            }
        }

        private void ClearParty()
        {
            foreach (Transform child in currentPcElementsParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}