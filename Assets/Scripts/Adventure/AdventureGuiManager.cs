using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Descension
{
    public class AdventureGuiManager : MonoBehaviour
    {
        [SerializeField] TMP_Text questLabel = null;

        public void Initialize(Quest quest)
        {
            questLabel.text = quest.Name + "\nDifficulty" + quest.Difficulty + "\nRarity" + quest.Rarity + "\nGold " + quest.GoldReward + "\nExp " + quest.ExperienceReward;
            questLabel.text += "\n" + quest.MapData.GetString();
        }
    }
}