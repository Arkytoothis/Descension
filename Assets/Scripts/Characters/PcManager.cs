using Descension.Characters;
using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Descension
{
    public class PcManager : MonoBehaviour
    {
        [SerializeField] List<PcData> pcDataList = new List<PcData>();
        public List<PcData> PcDataList { get { return pcDataList; } }

        private bool initialized = false;

        public void Initialize()
        {
            if (initialized == false)
            {
                initialized = true;

                Load();
                //GeneratePcData();
                //Save();
            }
        }

        public void GeneratePcData()
        {
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Male, "Imperial", "Soldier"));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Female, "", ""));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Female, "", ""));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Male, "", ""));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Female, "", ""));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Male, "", ""));
        }

        public void Save()
        {
            SavePcList();
        }

        public void Load()
        {
            LoadPcList();
        }

        private void SavePcList()
        {
            PcDataList list = new PcDataList();

            foreach (PcData pc in pcDataList)
            {
                list.Pcs.Add(pc);
            }

            string dataAsJson = JsonUtility.ToJson(list);
            string filePath = Database.DataPath + "player_character_list.json";
            File.WriteAllText(filePath, dataAsJson);
        }

        private void LoadPcList()
        {
            string filePath = Database.DataPath + "player_character_list.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                PcDataList list = JsonUtility.FromJson<PcDataList>(dataAsJson);

                foreach (PcData pc in list.Pcs)
                {
                    pcDataList.Add(pc);
                }
            }
        }
    }
}