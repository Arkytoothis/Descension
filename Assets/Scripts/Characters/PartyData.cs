using Descension.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    [System.Serializable]
    public class PartyData
    {
        [SerializeField] List<PcData> pcs = new List<PcData>();
        public List<PcData> Pcs { get { return pcs; } }

        [SerializeField] int maxSize = 0;
        public int MaxSize { get { return maxSize; } }

        public PartyData()
        {
            pcs = new List<PcData>();
            maxSize = 0;
        }

        public PartyData(int maxSize)
        {
            pcs = new List<PcData>();
            this.maxSize = maxSize;
        }

        public void AddPc(PcData pc)
        {
            if (pcs.Count < maxSize)
            {
                pcs.Add(pc);
            }
        }

        public void RemovePc(int index)
        {
            pcs.RemoveAt(index);
        }

        public void UpdateIndexes()
        {
            for (int i = 0; i < pcs.Count; i++)
            {
                pcs[i].ListIndex = i;
            }
        }
    }
}