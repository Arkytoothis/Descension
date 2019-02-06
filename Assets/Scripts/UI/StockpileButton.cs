using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Descension.Equipment;
using UnityEngine.EventSystems;

namespace Descension
{
    public class StockpileButton : MonoBehaviour
    {
        [SerializeField] Button button = null;
        [SerializeField] Image itemImage = null;
        [SerializeField] Image borderImage = null;
        [SerializeField] TMP_Text stackSizeLabel = null;
        [SerializeField] string clickSound = "";
        [SerializeField] int index = -1;
        [SerializeField] ItemData itemData = null;

        public void Setup(int index, bool available)
        {
            this.index = index;
            Clear();
            SetAvailable(available);
        }

        public void SetData(ItemData item, bool available)
        {
            if (item != null)
            {
                itemData = item;
                itemImage.sprite = SpriteManager.instance.GetItemIcon(item.IconKey);

                //borderImage.sprite = null;
                stackSizeLabel.text = "1";
            }
            else
            {
                itemData = null;
                Clear();
            }

            SetAvailable(available);
        }

        public void Clear()
        {
            itemData = null;
            itemImage.sprite = SpriteManager.instance.GetItemIcon("blank");
            //borderImage.sprite = null;
            stackSizeLabel.text = "";
        }

        public void SetAvailable(bool available)
        {
            if (available == true)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }

        public void OnPointerClick(BaseEventData eventData)
        {
            PointerEventData pointer = (PointerEventData)eventData;
            if (pointer.button == PointerEventData.InputButton.Left)
            {
                AudioManager.instance.PlayUI(clickSound);
            }
            else if (pointer.button == PointerEventData.InputButton.Middle)
            {
                AudioManager.instance.PlayUI(clickSound);
            }
            else if (pointer.button == PointerEventData.InputButton.Right)
            {
                AudioManager.instance.PlayUI(clickSound);
            }
        }

        public void OnPointerUp(BaseEventData eventData)
        {
        }

        public void OnPointerDown(BaseEventData eventData)
        {
        }

        public void OnPointerEnter(BaseEventData eventData)
        {
            TooltipManager.instance.SetData(itemData);
        }

        public void OnPointerExit(BaseEventData eventData)
        {
            TooltipManager.instance.Hide();
        }
    }
}