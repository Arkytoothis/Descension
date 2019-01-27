using Descension.Characters;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Descension
{
    public class ActionBar : MonoBehaviour
    {
        [SerializeField] TMP_Text label =null;

        public void SelectPc(PcData pcData)
        {
            label.text = pcData.Name.ShortName + " actions"; 
        }
    }
}