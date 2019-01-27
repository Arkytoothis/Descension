using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace Descension
{
    public class WorldCursorTooltip : Singleton<WorldCursorTooltip>
    {
        [SerializeField] TMP_Text distanceLabel = null;
        [SerializeField] TMP_Text actionsLabel = null;
        [SerializeField] TMP_Text attackLabel = null;

        private void Awake()
        {
            Reload();
        }

        private void LateUpdate()
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                transform.position = new Vector3(Input.mousePosition.x + 50, Input.mousePosition.y, 0);
            }
            else
            {
                transform.position = new Vector3(-1000, -1000, 0);
            }
        }

        public void UpdateDistance(string distance)
        {
            distanceLabel.text = distance;
        }

        public void UpdateText(string distance, string actions, string attack)
        {
            distanceLabel.text = distance;
            actionsLabel.text = actions;
            attackLabel.text = attack;
        }

        public void Clear()
        {
            distanceLabel.text = "";
            actionsLabel.text = "";
            attackLabel.text = "";
        }
    }
}