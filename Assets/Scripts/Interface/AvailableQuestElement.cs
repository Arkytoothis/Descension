using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Descension
{
    public class AvailableQuestElement : MonoBehaviour
    {
        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text detailsLabel = null;

        [SerializeField] int index = 0;

        public void Setup(int index, Quest quest, UnityAction action)
        {
            this.index = index;
            nameLabel.text = quest.Name;
            detailsLabel.text = quest.Rarity.ToString() +  ", Difficulty " + quest.Difficulty;
            detailsLabel.text += "\n" + quest.Description;

            GetComponent<Button>().onClick.AddListener(action);
        }
    }
}