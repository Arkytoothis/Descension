using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Descension
{
    public class QuestPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text difficultyLabel = null;
        [SerializeField] TMP_Text rarityLabel = null;
        [SerializeField] TMP_Text rewardsLabel = null;
        [SerializeField] TMP_Text descriptionLabel = null;
        [SerializeField] TMP_Text objectivesLabel = null;
        [SerializeField] TMP_Text mapThemeLabel = null;
        [SerializeField] TMP_Text mapTypeLabel = null;

        public void SelectQuest(Quest quest)
        {
            nameLabel.text = quest.Name;
            difficultyLabel.text = quest.Difficulty.ToString();
            rarityLabel.text = quest.Rarity.ToString();
            rewardsLabel.text = "Gold: " + quest.GoldReward + " Experience: " + quest.ExperienceReward;
            descriptionLabel.text = quest.Description.ToString();
            objectivesLabel.text = "Objectives";
            mapThemeLabel.text = "Map Theme";
            mapTypeLabel.text = "Map Type";
        }

        public void Clear()
        {
            nameLabel.text = "Accept a Quest";
            difficultyLabel.text = "";
            rarityLabel.text = "";
            rewardsLabel.text = "";
            descriptionLabel.text = "";
            objectivesLabel.text = "";
            mapThemeLabel.text = "";
            mapTypeLabel.text = "";
        }
    }
}