using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class AdventureScreen : TownScreen
    {
        [SerializeField] QuestManager questManager = null;
        [SerializeField] QuestPanel questPanel = null;
        public QuestPanel QuestPanel { get { return questPanel; } }

        public override void Initialize()
        {
            base.Initialize();
            questManager = TownManager.instance.QuestManager;
            UpdateQuestPanel();
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
    }
}