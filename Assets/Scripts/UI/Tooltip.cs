using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Descension.Characters;
using Descension.Equipment;
using Descension.Abilities;
using DG.Tweening;

namespace Descension
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] TMP_Text title = null;
        [SerializeField] TMP_Text description = null;
        [SerializeField] TMP_Text details = null;

        public void Clear()
        {
            title.text = "";
            description.text = "";
            details.text = null;
        }

        public void SetData(Attribute attribute)
        {
        }

        public void SetData(ItemData item)
        {
            if (item != null)
            {
                title.text = item.Name;
                description.text = item.description;
                details.text = item.attributesTooltip;
                Show();
            }
            else
            {
                Clear();
            }
        }

        public void SetData(Ability ability)
        {
            if (ability != null)
            {
                title.text = ability.Name;
                description.text = ability.Description;
                details.text = ability.ToString();
                Show();
            }
            else
            {
                Clear();
            }
        }

        private void Show()
        {
            //transform.DOMoveY(750f, 0f);
            transform.DOScale(1f, 0.1f);
        }

        public void Hide()
        {
            transform.DOScale(0f, 0.1f);
            //transform.DOMoveY(-750f, 0f);
        }
    }
}