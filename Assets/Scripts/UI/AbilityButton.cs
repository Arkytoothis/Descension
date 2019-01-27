using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Descension.Equipment;
using Descension.Abilities;
using UnityEngine.EventSystems;

namespace Descension
{
    public class AbilityButton : MonoBehaviour
    {
        [SerializeField] Button button = null;
        [SerializeField] Image abilityImage = null;
        [SerializeField] Image borderImage = null;
        [SerializeField] TMP_Text cooldownLabel = null;

        [SerializeField] string clickSound = "";
        [SerializeField] int index = -1;
        [SerializeField] Ability ability = null;

        public void Setup(int index, bool available)
        {
            this.index = index;
            Clear();
            SetAvailable(available);
        }

        public void SetData(Ability ability, bool available)
        {
            if (ability != null)
            {
                this.ability = ability;
                abilityImage.sprite = SpriteManager.instance.GetAbilityIcon(ability.SpriteKey);

                //borderImage.sprite = null;
                cooldownLabel.text = "1";
            }
            else
            {
                this.ability = null;
                Clear();
            }

            SetAvailable(available);
        }

        public void Clear()
        {
            this.ability = null;
            abilityImage.sprite = SpriteManager.instance.GetItemIcon("blank");
            //borderImage.sprite = null;
            cooldownLabel.text = "";
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
            TooltipManager.instance.SetData(ability);
        }

        public void OnPointerExit(BaseEventData eventData)
        {
            TooltipManager.instance.Hide();
        }
    }
}