using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Descension.Characters;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Descension
{
    public class PcListElement : GuiElement
    {
        [SerializeField] PcListElementData skinData = null;
        [SerializeField] PcListElementData selectedSkinData = null;
        [SerializeField] Image background = null;
        [SerializeField] Button button = null;
        [SerializeField] RawImage portrait = null;

        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text detailsLabel = null;

        [SerializeField] GameObject expBar = null;
        [SerializeField] int index = -1;

        public void Setup(PcData pcData, UnityAction clickAction)
        {
            nameLabel.text = pcData.Name.ShortName;
            detailsLabel.text = "Lvl " + pcData.Level + " " + pcData.RaceKey + " " + pcData.ProfessionKey;

            GetComponent<Button>().onClick.AddListener(clickAction);

            portrait.texture = PortraitRoom.instance.PcMounts[pcData.ListIndex].RtClose;
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