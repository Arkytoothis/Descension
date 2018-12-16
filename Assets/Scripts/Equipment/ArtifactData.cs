using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Equipment
{
    [System.Serializable]
    public class ArtifactData
    {
        public int Level;
        public int Experience;
        public int NextLevel;
        public int Power;

        public List<ItemAbilityData> LevelData;

        public ArtifactData()
        {
            Level = 0;
            Experience = 0;
            NextLevel = 0;
            Power = 0;

            LevelData = new List<ItemAbilityData>();
        }
        public ArtifactData(int level, int exp, int next_level, int power, List<ItemAbilityData> level_data)
        {
            Level = level;
            Experience = exp;
            NextLevel = next_level;
            Power = power;

            LevelData = new List<ItemAbilityData>();
            for (int i = 0; i < level_data.Count; i++)
            {
                LevelData.Add(new ItemAbilityData(level_data[i]));
            }
        }

        public ArtifactData(ArtifactData data)
        {
            Level = data.Level;
            Experience = data.Experience;
            NextLevel = data.NextLevel;
            Power = data.Power;

            LevelData = new List<ItemAbilityData>();
            for (int i = 0; i < data.LevelData.Count; i++)
            {
                LevelData.Add(new ItemAbilityData(data.LevelData[i]));
            }
        }

        public override string ToString()
        {
            string s = "";

            s = "\nLvl " + Level + ", Exp " + Experience + "/" + NextLevel;

            for (int i = 0; i < LevelData.Count; i++)
            {
                //s += "\nAt level " + LevelData[i].UnlockValue + " " + LevelData[i].Effects[0].GetTooltipString();
            }

            return s;
        }
    }
}