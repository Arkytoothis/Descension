using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Descension
{
    public class AvailableQuestElement : MonoBehaviour
    {
        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text detailsLabel = null;

        public void Setup(Quest quest)
        {
            nameLabel.text = quest.Name;
            detailsLabel.text = quest.Description;
        }
    }
}