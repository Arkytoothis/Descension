using Descension.Characters;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Descension
{
    public class SelectedPcPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text detailsLabel = null;

        public void SelectPc(PcData pcData)
        {
            nameLabel.text = pcData.Name.ShortName;
            detailsLabel.text = "Level " + pcData.Level + " " + pcData.RaceKey + " " + pcData.ProfessionKey;
        }
    }
}