using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Equipment
{
    public class WeaponSet
    {
        public ItemData[] Items;

        public WeaponSet()
        {
            for (int i = 0; i < 4; i++)
            {
                Items[i] = null;
            }
        }

        public WeaponSet(WeaponSet weaponSet)
        {
            for (int i = 0; i < weaponSet.Items.Length; i++)
            {
                if (weaponSet.Items[i] != null)
                {
                    Items[i] = new ItemData(weaponSet.Items[i]);
                }
                else
                {
                    Items[i] = null;
                }
            }
        }
    }
}