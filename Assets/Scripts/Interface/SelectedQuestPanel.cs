using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Descension
{
    public class SelectedQuestPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text difficultyLabel = null;
        [SerializeField] TMP_Text rarityLabel = null;
        [SerializeField] TMP_Text rewardsLabel = null;
        [SerializeField] TMP_Text descriptionLabel = null;
        [SerializeField] TMP_Text objectivesLabel = null;
        [SerializeField] TMP_Text mapDataLabel = null;

        [SerializeField] Button acceptButton = null;
        [SerializeField] Button declineButton = null;

        [SerializeField] Quest selectedQuest = null;
        public Quest SelectedQuest { get { return selectedQuest; } }

        public void SelectQuest(Quest quest)
        {
            if (quest != null)
            {
                selectedQuest = quest;

                nameLabel.text = quest.Name;
                difficultyLabel.text = quest.Difficulty.ToString();
                rarityLabel.text = quest.Rarity.ToString();
                rewardsLabel.text = "Gold: " + quest.GoldReward + " Experience: " + quest.ExperienceReward;
                descriptionLabel.text = quest.Description.ToString();
                objectivesLabel.text = "Objectives";
                mapDataLabel.text = quest.MapData.GetString();

                acceptButton.gameObject.SetActive(true);
                declineButton.gameObject.SetActive(true);
            }
        }

        public void Clear()
        {
            selectedQuest = null;

            nameLabel.text = "Select a Quest";
            difficultyLabel.text = "";
            rarityLabel.text = "";
            rewardsLabel.text = "";
            descriptionLabel.text = "";
            objectivesLabel.text = "";
            mapDataLabel.text = "";

            acceptButton.gameObject.SetActive(false);
            declineButton.gameObject.SetActive(false);
        }
    }
}