using Descension.Characters;
using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Descension
{
    public class PcManager : Singleton<PcManager>
    {
        [SerializeField] List<PcData> pcDataList = new List<PcData>();
        public List<PcData> PcDataList { get { return pcDataList; } }

        [SerializeField] PartyData partyData = null;
        public PartyData PartyData { get { return partyData; } }

        [SerializeField] List<GameObject> pcObjects = new List<GameObject>();
        public List<GameObject> PcObjects { get { return pcObjects; } set { pcObjects = value; } }

        [SerializeField] int partySize = 0;
        public int PartySize { get { return partySize; } }

        [SerializeField] int selectedPcIndex = -1;
        public int SelectedPcIndex { get { return selectedPcIndex; } }

        private bool initialized = false;

        private void Awake()
        {
            Reload();
        }

        public void Initialize()
        {
            if (initialized == false)
            {
                initialized = true;

                partyData = new PartyData(4);
                pcDataList = new List<PcData>();
                pcObjects = new List<GameObject>();
            }
        }

        public void Generate()
        {
            GeneratePcData();
            Save();
        }

        private void GeneratePcData()
        {
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Male, "Imperial", "Soldier"));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Female, "", "Scout"));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Female, "", "Priest"));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Male, "", "Apprentice"));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Female, "", "Rogue"));
            pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.Male, "", "Citizen"));

            for (int i = 0; i < 4; i++)
            {
                pcDataList.Add(PcGenerator.Generate(pcDataList.Count, Gender.None, "", ""));
            }
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

        public void AddToParty(int index)
        {
            pcDataList[index].PartyIndex = partySize;
            partySize++;
        }

        public void RemoveFromParty(int index)
        {
            pcDataList[index].PartyIndex = -1;
            partySize--;
        }

        public void SetupParty()
        {

            for (int i = 0; i < pcDataList.Count; i++)
            {
                if (pcDataList[i].PartyIndex != -1)
                {
                    partyData.AddPc(pcDataList[i]);
                    partyData.Pcs[pcDataList[i].PartyIndex].PartyIndex = i;
                }
            }

            partySize = partyData.Pcs.Count;
        }

        public void AddPcObject(GameObject pcObject)
        {
            pcObjects.Add(pcObject);
        }

        public void SelectPc(int index)
        {
            selectedPcIndex = index;
        }

        public GameObject GetSelectedPcObject()
        {
            if (selectedPcIndex != -1)
            {
                return pcObjects[selectedPcIndex];
            }
            else
            {
                return null;
            }
        }

        public PcData GetSelectedPc()
        {
            if (selectedPcIndex != -1)
            {
                return pcDataList[selectedPcIndex];
            }
            else
            {
                return null;
            }
        }

        public PcData GetSelectedPartyPc()
        {
            if (partyData != null && selectedPcIndex != -1)
            {
                return partyData.Pcs[selectedPcIndex];
            }
            else
            {
                return null;
            }
        }
    }
}