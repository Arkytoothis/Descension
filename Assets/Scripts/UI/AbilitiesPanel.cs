using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Descension.Characters;
using Descension.Core;

namespace Descension
{
    public class AbilitiesPanel : MonoBehaviour
    {
        [SerializeField] GameObject abilityButtonPrefab = null;

        [SerializeField] Transform powerButtonsParent = null;
        [SerializeField] List<AbilityButton> powerButtons = new List<AbilityButton>();

        [SerializeField] Transform spellButtonsParent = null;
        [SerializeField] List<AbilityButton> spellButtons = new List<AbilityButton>();

        [SerializeField] Transform traitButtonsParent = null;
        [SerializeField] List<AbilityButton> traitButtons = new List<AbilityButton>();

        public void Initialize()
        {
            for (int i = 0; i < CharacterAbilities.MaxPowerSlots; i++)
            {
                GameObject buttonObject = Instantiate(abilityButtonPrefab, powerButtonsParent);
                buttonObject.name = "Power Button - " + i;

                AbilityButton buttonScript = buttonObject.GetComponent<AbilityButton>();
                buttonScript.Setup(i, true);

                powerButtons.Add(buttonScript);
            }

            for (int i = 0; i < CharacterAbilities.MaxSpellSlots; i++)
            {
                GameObject buttonObject = Instantiate(abilityButtonPrefab, spellButtonsParent);
                buttonObject.name = "Spell Button - " + i;

                AbilityButton buttonScript = buttonObject.GetComponent<AbilityButton>();
                buttonScript.Setup(i, true);

                spellButtons.Add(buttonScript);
            }

            for (int i = 0; i < CharacterAbilities.MaxTraitSlots; i++)
            {
                GameObject buttonObject = Instantiate(abilityButtonPrefab, traitButtonsParent);
                buttonObject.name = "Trait Button - " + i;

                AbilityButton buttonScript = buttonObject.GetComponent<AbilityButton>();
                buttonScript.Setup(i, true);

                traitButtons.Add(buttonScript);
            }
        }

        public void SetData(PcData pcData)
        {
            LoadPowers(pcData);
            LoadSpells(pcData);
            LoadTraits(pcData);
        }

        private void LoadPowers(PcData pcData)
        {
            for (int i = 0; i < powerButtons.Count; i++)
            {
                if (i < pcData.Abilities.AvailablePowers.Count)
                {
                    if (pcData.Abilities.AvailablePowers[i] != null)
                        powerButtons[i].SetData(pcData.Abilities.AvailablePowers[i], true);
                    else
                        powerButtons[i].SetData(null, true);
                }
                else
                {
                    powerButtons[i].SetData(null, true);
                }
            }
        }

        private void LoadSpells(PcData pcData)
        {
            for (int i = 0; i < spellButtons.Count; i++)
            {
                if (i < pcData.Abilities.AvailableSpells.Count)
                {
                    if (pcData.Abilities.AvailableSpells[i] != null)
                        spellButtons[i].SetData(pcData.Abilities.AvailableSpells[i], true);
                    else
                        spellButtons[i].SetData(null, true);
                }
                else
                {
                    spellButtons[i].SetData(null, true);
                }
            }
        }

        private void LoadTraits(PcData pcData)
        {
            for (int i = 0; i < traitButtons.Count; i++)
            {
                if (i < pcData.Abilities.Traits.Count)
                {
                    if (pcData.Abilities.Traits[i] != null)
                        traitButtons[i].SetData(pcData.Abilities.Traits[i], true);
                    else
                        traitButtons[i].SetData(null, true);
                }
                else
                {
                    traitButtons[i].SetData(null, true);
                }
            }
        }
    }
}