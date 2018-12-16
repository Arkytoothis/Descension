using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Equipment
{
    [System.Serializable]
    public class ItemSetData
    {
        public string SetName;
        public int NumPieces;

        public List<ItemAbilityData> LevelData;

        public ItemSetData()
        {
            SetName = "";
            NumPieces = 0;
            LevelData = new List<ItemAbilityData>();
        }

        public ItemSetData(string name, int num_pieces, List<ItemAbilityData> level_data)
        {
            SetName = name;
            NumPieces = num_pieces;

            LevelData = new List<ItemAbilityData>();
            for (int i = 0; i < level_data.Count; i++)
            {
                LevelData.Add(new ItemAbilityData(level_data[i]));
            }
        }

        public ItemSetData(ItemSetData data)
        {
            SetName = data.SetName;
            NumPieces = data.NumPieces;

            LevelData = new List<ItemAbilityData>();
            for (int i = 0; i < data.LevelData.Count; i++)
            {
                LevelData.Add(new ItemAbilityData(data.LevelData[i]));
            }
        }

        public override string ToString()
        {
            string s = "";

            s = "\n<color=" + Globals.RarityColors[(int)Rarity.Set] + ">" + SetName + ", " + NumPieces + " piece set";

            for (int i = 0; i < LevelData.Count; i++)
            {
                //s += "\n" + LevelData[i].UnlockValue + " pieces: " + LevelData[i].Effects[0].GetTooltipString();
            }

            return s;
        }
    }
}