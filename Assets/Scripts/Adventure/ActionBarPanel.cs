using Descension.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class ActionBarPanel : MonoBehaviour
    {
        [SerializeField] GameObject actionButtonPrefab = null;
        [SerializeField] Transform actionButtonsParent = null;
        [SerializeField] List<ActionButton> actionButtons = new List<ActionButton>();

        public void Initialize()
        {
            for (int i = 0; i < 12; i++)
            {
                GameObject buttonObject = Instantiate(actionButtonPrefab, actionButtonsParent);
                buttonObject.name = "Action Button " + i;

                ActionButton buttonScript = buttonObject.GetComponent<ActionButton>();
                buttonScript.SetData(null);

                actionButtons.Add(buttonScript);
            }
        }

        public void SetCharacterData(PcData pcData)
        {
        }

        public void SetCharacterData(NpcData npcData)
        {
        }
    }
}