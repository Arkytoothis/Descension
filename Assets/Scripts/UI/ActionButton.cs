using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Descension
{
    public class ActionButton : MonoBehaviour
    {
        [SerializeField] Image iconImage = null;
        [SerializeField] Image cooldownImage = null;
        [SerializeField] TMP_Text hotkeyLabel = null;
        [SerializeField] TMP_Text cooldownLabel = null;
 
        public void SetData(GameAction action)
        {

        }
    }
}