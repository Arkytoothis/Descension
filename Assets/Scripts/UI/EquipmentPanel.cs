using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Descension.Characters;
using Descension.Core;

namespace Descension
{
    public class EquipmentPanel : MonoBehaviour
    {
        [SerializeField] RawImage portrait = null;

        [SerializeField] GameObject equippedButtonPrefab = null;
        [SerializeField] Transform equippedButtonsParent = null;
        [SerializeField] List<EquippedItemButton> equippedButtons = new List<EquippedItemButton>();

        [SerializeField] GameObject accessoryButtonPrefab = null;
        [SerializeField] Transform accessoryButtonsParent = null;
        [SerializeField] List<AccessoryButton> accessoryButtons = new List<AccessoryButton>();

        public void Initialize()
        {
            for (int i = 0; i < (int)EquipmentSlot.Number; i++)
            {
                GameObject buttonObject = Instantiate(equippedButtonPrefab, equippedButtonsParent);
                buttonObject.name = "Equipped Item Button - " + i;

                EquippedItemButton buttonScript = buttonObject.GetComponent<EquippedItemButton>();
                buttonScript.Setup(i, true);

                equippedButtons.Add(buttonScript);
            }

            for (int i = 0; i < CharacterInventory.MaximumAccessories; i++)
            {
                GameObject buttonObject = Instantiate(accessoryButtonPrefab, accessoryButtonsParent);
                buttonObject.name = "Accessory Button - " + i;

                AccessoryButton buttonScript = buttonObject.GetComponent<AccessoryButton>();
                buttonScript.Setup(i, false);

                accessoryButtons.Add(buttonScript);
            }
        }

        public void SetData(PcData pcData)
        {
            portrait.texture = PortraitRoom.instance.PcMounts[pcData.ListIndex].RtFar;

            LoadEquippedItems(pcData);
            LoadAccessories(pcData);
        }

        private void LoadEquippedItems(PcData pcData)
        {
            for (int i = 0; i < pcData.Inventory.EquippedItems.Length; i++)
            {
                equippedButtons[i].SetData(pcData.Inventory.EquippedItems[i]);
            }
        }

        private void LoadAccessories(PcData pcData)
        {
            for (int i = 0; i < CharacterInventory.MaximumAccessories; i++)
            {
                if (i < pcData.Inventory.AccessoryLimit)
                {
                    if (pcData.Inventory.Accessories[i] != null)
                        accessoryButtons[i].SetData(pcData.Inventory.Accessories[i], true);
                    else
                        accessoryButtons[i].SetData(null, false);
                }
                else
                {
                    accessoryButtons[i].SetData(null, false);
                }
            }
        }
    }
}