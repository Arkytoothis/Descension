using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Descension.Characters;

namespace Descension
{
    public class AdventureGuiManager : MonoBehaviour
    {
        [SerializeField] PartyPanel partyPanel = null;
        [SerializeField] SelectedPcPanel pcPanel = null;
        [SerializeField] ActionBar actionBar = null;

        [SerializeField] TMP_Text questLabel = null;

        public void Initialize(Quest quest)
        {
            partyPanel.Setup();

            questLabel.text = quest.Name + "\nDifficulty" + quest.Difficulty + "\nRarity" + quest.Rarity + "\nGold " + quest.GoldReward + "\nExp " + quest.ExperienceReward;
            questLabel.text += "\n" + quest.MapData.GetString();
        }

        public void SelectPc(PcData pcData)
        {
            pcPanel.SelectPc(pcData);
            actionBar.SelectPc(pcData);
        }
    }
}