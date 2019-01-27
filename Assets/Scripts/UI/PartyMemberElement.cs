using Descension.Characters;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Descension
{
    public class PartyMemberElement : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text detailsLabel = null;
        [SerializeField] Button button = null;
        [SerializeField] string clickSound = "Button 02";

        public void Setup(PcData pcData, UnityAction action)
        {
            nameLabel.text = pcData.Name.ShortName;
            detailsLabel.text = "Lvl " + pcData.Level + " " + pcData.RaceKey + " " + pcData.ProfessionKey;
            button.onClick.AddListener(action);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            AudioManager.instance.PlayUI(clickSound);
        }
    }
}