using Descension.Core;
using Descension.Equipment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class StockpileManager : Singleton<StockpileManager>
    {
        [SerializeField] List<ItemData> items = new List<ItemData>();

        [SerializeField] StockpileWindow stockpileWindow = null;

        public List<ItemData> Items { get { return items; } }

        public int MaxItems = 32;

        private void Awake()
        {
            Reload();
        }

        public void Initialize()
        {
            items = new List<ItemData>();

            for (int i = 0; i < MaxItems; i++)
            {
                items.Add(null);
            }

            GenerateRandomItems();
            stockpileWindow.Initialize();
        }

        public void GenerateRandomItems()
        {
            int numItems = 16;

            for (int i = 0; i < numItems; i++)
            {
                AddItem(ItemGenerator.CreateRandomItem(ItemTypeAllowed.Any, 10, 10));
                //Debug.Log(items[i].Name + " generated");
            }
        }

        public void AddItem(ItemData item)
        {
            for (int i = 0; i < MaxItems; i++)
            {
                if (items[i] == null)
                {
                    items[i] = new ItemData(item);
                    break;
                }
            }
        }
    }
}