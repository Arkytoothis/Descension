using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Descension.Characters;
using UnityEngine.Events;

namespace Descension
{
    public class PcListElement : MonoBehaviour
    {
        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text detailsLabel = null;

        public void Setup(PcData pcData, UnityAction action)
        {
            nameLabel.text = pcData.Name.ShortName;
            detailsLabel.text = "Lvl " + pcData.Level + " " + pcData.RaceKey + " " + pcData.ProfessionKey;

            GetComponent<Button>().onClick.AddListener(action);
        }
    }
}