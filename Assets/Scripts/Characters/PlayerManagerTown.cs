using Descension.Characters;
using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class PlayerManagerTown : MonoBehaviour
    {
        [SerializeField] List<PcData> pcDataList = new List<PcData>();
        public List<PcData> PcDataList { get { return pcDataList; } }

        //[SerializeField] int numPcsToGenerate = 6;

        private bool initialized = false;


        public void Initialize()
        {
            if (initialized == false)
            {
                initialized = true;

                GeneratePcData();
            }
        }

        public void GeneratePcData()
        {

            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Male, "Imperial", "Soldier"));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Female, "Imperial", "Soldier"));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Female, "Imperial", "Soldier"));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Male, "Imperial", "Soldier"));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Female, "Imperial", "Soldier"));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Male, "Imperial", "Soldier"));

        }
    }
}