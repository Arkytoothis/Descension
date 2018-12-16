using UnityEngine;
using System.Collections.Generic;
using Descension.Equipment;
using Descension.Core;

namespace Descension.Characters
{
    [System.Serializable]
    public class CharacterInventory
    {        
        private ItemData[] equippedItems;
        private List<ItemData> accessories;
        private int accessoryLimit;

        public static int MaximumAccessories = 7;

        public int AccessoryLimit
        {
            get { return accessoryLimit; }
            set { accessoryLimit = value; }
        }

        public ItemData[] EquippedItems
        {
            get { return equippedItems; }
        }

        public List<ItemData> Accessories
        {
            get { return accessories; }
        }

        public ItemData GetItem(int slot)
        {
            return EquippedItems[slot];
        }

        public CharacterInventory()
        {
            equippedItems = new ItemData[(int)EquipmentSlot.Number];
            accessories = new List<ItemData>(MaximumAccessories);

            for (int i = 0; i < EquippedItems.Length; i++)
            {
                EquippedItems[i] = null;
            }

            for (int i = 0; i < MaximumAccessories; i++)
            {
                accessories.Add(null);
            }
        }

        public CharacterInventory(CharacterInventory inventory)
        {
            AccessoryLimit = inventory.AccessoryLimit;
            equippedItems = new ItemData[(int)EquipmentSlot.Number];
            accessories = new List<ItemData>(MaximumAccessories);

            for (int i = 0; i < inventory.EquippedItems.Length; i++)
            {
                if (inventory.EquippedItems[i] != null)
                    EquippedItems[i] = new ItemData(inventory.EquippedItems[i]);
            }

            for (int i = 0; i < inventory.accessories.Count; i++)
            {
                if (inventory.accessories[i] != null)
                    accessories.Add(new ItemData(inventory.accessories[i]));
            }
        }

        public void EquipItem(ItemData item, EquipmentSlot slot)
        {
            if (item != null)
            {
                //Debug.Log("Equipping " + item.Name);
                EquippedItems[(int)slot] = new ItemData(item);
            }
            else
                EquippedItems[(int)slot] = null;

        }

        public void EquipAccessory(ItemData item, int slot)
        {
            if (slot != -1)
            {
                accessories[slot] = new ItemData(item);
            }
            else
            {
                for (int i = 0; i < accessories.Count; i++)
                {
                    if (accessories[i] == null)
                    {
                        accessories[i] = new ItemData(item);
                        break;
                    }
                }
            }
        }

        public bool TryEquip(ItemData item, EquipmentSlot slot)
        {
            bool success = true;

            if (item.Slot == slot)
            {
                success = true;
                EquipItem(item, slot);
            }

            return success;
        }
    }
}