using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Descension.Gui
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(TMP_Text))]
    public class GenericButton : GuiElement
    {
        public enum ButtonType { Standard, Text, Icon, Text_Icon, Resource }
        public ButtonType buttonType;

        public ButtonData skinData;

        public Image background;
        public UnityEngine.UI.Button button;
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
    }
}