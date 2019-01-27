using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Descension
{
    public class AvailableQuestElement : GuiElement
    {
        [SerializeField] AvailableQuestElementData skinData = null;
        [SerializeField] AvailableQuestElementData selectedSkinData = null;
        [SerializeField] Image background = null;
        [SerializeField] Button button = null;
        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text detailsLabel = null;

        [SerializeField] int index = 0;

        public void Setup(int index, Quest quest, UnityAction action)
        {
            this.index = index;
            nameLabel.text = quest.Name;
            detailsLabel.text = quest.Rarity.ToString() +  ", Difficulty " + quest.Difficulty;
            detailsLabel.text += "\n" + quest.Description;

            GetComponent<Button>().onClick.AddListener(action);
        }

        protected override void OnSkinGui()
        {
            base.OnSkinGui();

            background.sprite = skinData.background;
            background.type = Image.Type.Sliced;

            button.transition = Selectable.Transition.ColorTint;
            button.colors = skinData.buttonColors;
        }

        public void OnPointerClick(BaseEventData bed)
        {
            AudioManager.instance.PlayUI(skinData.clickSound);
        }

        public void Select()
        {
            button.colors = selectedSkinData.buttonColors;
        }

        public void Deselect()
        {
            button.colors = skinData.buttonColors;
        }
    }
}