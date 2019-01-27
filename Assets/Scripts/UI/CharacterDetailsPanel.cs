using Descension.Characters;
using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Descension
{
    public class CharacterDetailsPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text detailsLabel = null;
        [SerializeField] TMP_Text backgroundLabel = null;

        public void Initialize()
        {
            Clear();
        }

        public void SetData(PcData pcData)
        {
            nameLabel.text = pcData.Name.FullName;
            detailsLabel.text = pcData.RaceKey + " " + pcData.ProfessionKey;
            backgroundLabel.text = pcData.Background.ToString();
        }

        public void Clear()
        {
            nameLabel.text = "Full Name";
            detailsLabel.text = "Race and Profession";
            backgroundLabel.text = "Background";
        }
    }
}