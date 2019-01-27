using Descension.Abilities;
using Descension.Characters;
using Descension.Core;
using Descension.Equipment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class TooltipManager : Singleton<TooltipManager>
    {
        [SerializeField] Tooltip tooltipMain = null;
        [SerializeField] Tooltip tooltipCompare1 = null;
        [SerializeField] Tooltip tooltipCompare2 = null;

        private void Awake()
        {
            Reload();
            Hide();

            tooltipMain.transform.localPosition = new Vector3(tooltipMain.transform.localPosition.x, -750f, 0f);
            tooltipCompare1.transform.localPosition = new Vector3(tooltipCompare1.transform.localPosition.x, -750f, 0f);
            tooltipCompare2.transform.localPosition = new Vector3(tooltipCompare2.transform.localPosition.x, -750f, 0f);
        }

        public void Hide()
        {
            tooltipMain.Hide();
            tooltipCompare1.Hide();
            tooltipCompare2.Hide();
        }

        public void SetData(Attribute attribute)
        {
            tooltipMain.SetData(attribute);
        }

        public void SetData(ItemData item)
        {
            tooltipMain.SetData(item);
        }

        public void SetData(Ability ability)
        {
            tooltipMain.SetData(ability);
        }
    }
}