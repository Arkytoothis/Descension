using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public static class EncounterGenerator
    {
        public static EncounterData GenerateEncounter()
        {
            EncounterType type = (EncounterType)Random.Range(0, (int)EncounterType.Number);
            string name = type.ToString() + " Encounter";            
            int mapWidth = 9;
            int mapHeight = 9;
            int minNpcs = 1;
            int maxNpcs = 6;

            return new EncounterData(name, type, mapWidth, mapHeight, minNpcs, maxNpcs);
        }
    }
}