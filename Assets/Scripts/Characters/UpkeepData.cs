using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Characters
{
    [System.Serializable]
    public class UpkeepData
    {
        public int Coin;
        public int Rations;
        public int Materials;
        public int Essence;

        public UpkeepData()
        {
            Coin = 0;
            Rations = 0;
            Materials = 0;
            Essence = 0;
        }

        public UpkeepData(int coin, int rations, int materials, int essence)
        {
            Coin = coin;
            Rations = rations;
            Materials = materials;
            Essence = essence;
        }

        public UpkeepData(UpkeepData data)
        {
            Coin = data.Coin;
            Rations = data.Rations;
            Materials = data.Materials;
            Essence = data.Essence;
        }
    }
}