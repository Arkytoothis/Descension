using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Descension.Characters;
using UnityEngine.Events;

namespace Descension
{
    public class PartyPcElement : MonoBehaviour
    {
        //[SerializeField] Image portrait = null;
        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text detailsLabel = null;
        [SerializeField] Button removePcButton = null;

        [SerializeField] int index = 0;

        public void Setup(PcData pcData, UnityAction action)
        {
            this.index = pcData.PartyIndex;
            nameLabel.text = pcData.Name.ShortName;
            detailsLabel.text = "Lvl " + pcData.Level + " " + pcData.RaceKey + " " + pcData.ProfessionKey;
            detailsLabel.text += "\nIndex " + this.index + ",   ListIndex " + pcData.ListIndex + ",   PartyIndex " + pcData.PartyIndex;

            removePcButton.onClick.AddListener(action);
        }
    }
}