using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Descension.Characters;
using Descension.Core;

namespace Descension
{
    public class DerivedAttributeElement : GuiElement
    {
        [SerializeField] ButtonData skinData = null;
        [SerializeField] Image background = null;
        [SerializeField] Button button = null;
        [SerializeField] TMP_Text attributeLabel = null;
        [SerializeField] TMP_Text valueLabel = null;

        [SerializeField] int index = -1;

        protected override void OnSkinGui()
        {
            base.OnSkinGui();

            background.sprite = skinData.background;
            background.type = Image.Type.Sliced;

            button.transition = Selectable.Transition.ColorTint;
            button.colors = skinData.buttonColors;

        }

        public void OnPointerEnter(BaseEventData eventData)
        {
        }

        public void OnPointerExit(BaseEventData eventData)
        {
        }

        public void SetData(string text, string value)
        {
            attributeLabel.text = text;
            valueLabel.text = value;
        }

        public void Clear()
        {
            attributeLabel.text = "Attribute";
            valueLabel.text = "";
        }
    }
}