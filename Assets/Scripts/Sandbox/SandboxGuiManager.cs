using Descension.Characters;
using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Descension
{
    public class SandboxGuiManager : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown raceDropdown = null;
        [SerializeField] TMP_Dropdown hairDropdown = null;
        [SerializeField] TMP_Dropdown beardDropdown = null;

        public void Initialize()
        {
            raceDropdown.ClearOptions();
            foreach (KeyValuePair<string, Race> kvp in Database.Races)
            {
                raceDropdown.options.Add(new TMP_Dropdown.OptionData(kvp.Key));
            }

            hairDropdown.ClearOptions();
            foreach (KeyValuePair<string, GameObject> kvp in ModelManager.instance.HairPrefabs)
            {
                hairDropdown.options.Add(new TMP_Dropdown.OptionData(kvp.Key));
            }

            beardDropdown.ClearOptions();
            foreach (KeyValuePair<string, GameObject> kvp in ModelManager.instance.BeardPrefabs)
            {
                beardDropdown.options.Add(new TMP_Dropdown.OptionData(kvp.Key));
            }
        }
    }
}