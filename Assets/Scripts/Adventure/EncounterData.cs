using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public enum EncounterType { Battle, Ambush, Dialog, Number, None }

    public class EncounterData
    {
        [SerializeField] EncounterType encounterType = EncounterType.None;
        [SerializeField] string name = "";
        [SerializeField] List<NpcData> npcs = new List<NpcData>();
        [SerializeField] int mapWidth = 0;
        [SerializeField] int mapHeight = 0;
        [SerializeField] int minNpcs = 0;
        [SerializeField] int maxNpcs = 0;

        public EncounterType Type { get => encounterType; set => encounterType = value; }
        public string Name { get => name; set => name = value; }
        public List<NpcData> Npcs { get => npcs; }
        public int MapWidth { get => mapWidth; set => mapWidth = value; }
        public int MapHeight { get => mapHeight; set => mapHeight = value; }
        public int MinNpcs { get => minNpcs; set => minNpcs = value; }
        public int MaxNpcs { get => maxNpcs; set => maxNpcs = value; }

        public EncounterData()
        {
            Name = "";
            encounterType = EncounterType.None;
            mapWidth = 6;
            mapHeight = 6;

            npcs = new List<NpcData>();
        }

        public EncounterData(string name, EncounterType type, int width, int height, int minNpcs, int maxNpcs)
        {
            this.name = name;
            this.minNpcs = minNpcs;
            this.maxNpcs = maxNpcs;
            encounterType = type;
            mapWidth = width;
            mapHeight = height;
        }

        public EncounterData(EncounterData data)
        {
            Name = data.Name;
            encounterType = data.Type;
            mapWidth = data.mapWidth;
            mapHeight = data.mapHeight;
            minNpcs = data.minNpcs;
            maxNpcs = data.maxNpcs;

            npcs = new List<NpcData>();
            for (int i = 0; i < data.npcs.Count; i++)
            {
                npcs.Add(new NpcData(Database.NPCs[Helper.RandomKey(Database.NPCs)]));
            }
        }
    }
}