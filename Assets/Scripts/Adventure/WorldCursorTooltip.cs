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
        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text positionLabel = null;
        [SerializeField] TMP_Text detailsLabel = null;

        private void Awake()
        {
            Reload();
        }

        //private void LateUpdate()
        //{
        //    if (EventSystem.current.IsPointerOverGameObject() == false)
        //    {
        //        transform.position = new Vector3(Input.mousePosition.x + 50, Input.mousePosition.y, 0);
        //    }
        //    else
        //    {
        //        transform.position = new Vector3(-1000, -1000, 0);
        //    }
        //}

        public void UpdateText(string name, string position, string details)
        {
            nameLabel.text = name;
            positionLabel.text = position;
            detailsLabel.text = details;
        }

        public void Clear()
        {
            nameLabel.text = "";
            positionLabel.text = "";
            detailsLabel.text = "";
        }
    }
}