using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Descension
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(TMP_Text))]
    public class GenericButton : GuiElement
    {
        public enum ButtonType { Standard, Text, Icon, Text_Icon, Resource }
        public ButtonType buttonType;

        public ButtonData skinData;
        public string clickSound = "";
        public string dragSound = "";
        public Image background;
        public Button button;
        public Image icon;
        public TMP_Text label;

        protected override void OnSkinGui()
        {
            base.OnSkinGui();

            background.sprite = skinData.background;
            background.type = Image.Type.Sliced;

            button.transition = Selectable.Transition.ColorTint;
            button.colors = skinData.buttonColors;

            icon.sprite = skinData.icon;
            //label.text = skinData.text;

            switch (buttonType)
            {
                case ButtonType.Standard:
                    label.enabled = true;
                    icon.enabled = false;
                    break;
                case ButtonType.Text:
                    label.enabled = true;
                    icon.enabled = false;
                    break;
                case ButtonType.Icon:
                    label.enabled = false;
                    icon.enabled = true;
                    break;
                case ButtonType.Text_Icon:
                    label.enabled = true;
                    icon.enabled = true;
                    break;
                case ButtonType.Resource:
                    label.enabled = true;
                    icon.enabled = true;
                    break;
                default:
                    break;
            }
        }

        public void OnPointerClick(BaseEventData eventData)
        {
            if(clickSound == "")
                AudioManager.instance.PlayUI(skinData.clickSound);
            else
                AudioManager.instance.PlayUI(clickSound);
        }

        public void OnPointerUp(BaseEventData eventData)
        {
        }

        public void OnPointerDown(BaseEventData eventData)
        {
        }

        public void OnPointerEnter(BaseEventData eventData)
        {
        }

        public void OnPointerExit(BaseEventData eventData)
        {
        }
    }
}