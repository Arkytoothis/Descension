using Descension.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class EncounterCell : MonoBehaviour
    {
        [SerializeField] int x = 0;
        [SerializeField] int y = 0;
        [SerializeField] bool walkable = true;
        [SerializeField] int pcIndex = -1;
        [SerializeField] int npcIndex = -1;

        public int X { get => x; }
        public int Y { get => y; }
        public bool Walkable { get => walkable; }
        public int PcIndex { get => pcIndex; set => pcIndex = value; }
        public int NpcIndex { get => npcIndex; set => npcIndex = value; }

        public void Setup(int x, int y, bool walkable)
        {
            this.x = x;
            this.y = y;
            this.walkable = walkable;
            pcIndex = -1;
            npcIndex = -1;
        }
    }
}