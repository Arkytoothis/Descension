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
    public class BaseAttributeElement : GuiElement
    {
        [SerializeField] ButtonData skinData = null;
        [SerializeField] Image background = null;
        [SerializeField] Button button = null;
        [SerializeField] TMP_Text attributeLabel = null;
        [SerializeField] TMP_Text baseLabel = null;
        [SerializeField] TMP_Text currentLabel = null;
        [SerializeField] TMP_Text modifierLabel = null;
        [SerializeField] TMP_Text spentLabel = null;

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

        public void SetData(Attribute attribute)
        {
            if (attribute != null)
            {
                attributeLabel.text = Database.BaseAttributes[attribute.Index].Name;
                baseLabel.text = attribute.Start.ToString();
                currentLabel.text = attribute.Current.ToString();
                spentLabel.text = attribute.Spent.ToString();

                if (attribute.Modifier >= 0)
                    modifierLabel.text = "+";
                else
                    modifierLabel.text = "-";

                modifierLabel.text += attribute.Modifier.ToString();
            }
            else
            {
                Clear();
            }
        }

        public void Clear()
        {
            attributeLabel.text = "Attribute";
            baseLabel.text = "999";
            currentLabel.text = "999";
            modifierLabel.text = "999";
            spentLabel.text = "999";
        }
    }
}