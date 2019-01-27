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
        [SerializeField] TMP_Text mapDataLabel = null;

        public void SelectQuest(Quest quest)
        {
            nameLabel.text = quest.Name;
            difficultyLabel.text = quest.Difficulty.ToString();
            rarityLabel.text = quest.Rarity.ToString();
            rewardsLabel.text = "Gold: " + quest.GoldReward + " Experience: " + quest.ExperienceReward;
            descriptionLabel.text = quest.Description.ToString();
            objectivesLabel.text = "Objectives";
            mapDataLabel.text = quest.MapData.GetString();
        }

        public void Clear()
        {
            nameLabel.text = "Accept a Quest";
            difficultyLabel.text = "";
            rarityLabel.text = "";
            rewardsLabel.text = "";
            descriptionLabel.text = "";
            objectivesLabel.text = "";
            mapDataLabel.text = "";
        }
    }
}