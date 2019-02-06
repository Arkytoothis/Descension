using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Descension.Characters;

namespace Descension
{
    public class CurrentCharacterPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text detailsLabel = null;

        public void SetCharacterData(PcData pcData)
        {
            nameLabel.text = pcData.Name.ShortName;
        }

        public void SetCharacterData(NpcData npcData)
        {
            nameLabel.text = npcData.Name.ShortName;
        }
    }
}