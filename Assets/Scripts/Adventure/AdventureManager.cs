using Descension.Characters;
using Descension.Core;
using Descension.Equipment;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Descension
{
    public class AdventureManager : Singleton<AdventureManager>
    {
        [SerializeField] Camera mainCamera = null;
        [SerializeField] AdventureGuiManager guiManager = null;

        [SerializeField] Quest currentQuest = null;
        public Quest CurrentQuest { get { return currentQuest; } }

        [SerializeField] Adventure adventure = null;
        public Adventure Adventure { get { return adventure; } }

        [SerializeField] GameObject outdoorSceneSettings = null;
        [SerializeField] GameObject undergroundSceneSettings = null;
        [SerializeField] GameObject indoorsSceneSettings = null;

        [SerializeField] PartySpawner partySpawner = null;
        [SerializeField] Transform partyParent = null;

        private void Awake()
        {
            Reload();
            mainCamera = Camera.main;
        }

        private void Start()
        {
            StartCoroutine("Initialize");
        }

        IEnumerator Initialize()
        {
            Database.Initialize();
            SpriteManager.instance.Initialize();
            ItemGenerator.Initialize();
            PcGenerator.Initialize();
            ModelManager.instance.Initialize();

            Load();

            PcManager.instance.Initialize();
            PcManager.instance.Load();
            PcManager.instance.SetupParty();

            guiManager.Initialize(currentQuest);

            //AudioManager.instance.PlayMusic("Caves of Madness", 0, 5f);
            AudioManager.instance.PlayAmbient("Castle Abandoned");

            return null;
        }

        public void SetupPlayerSpawner()
        {
            partySpawner = FindObjectOfType<PartySpawner>();
            partySpawner.SpawnParty(partyParent);

            AdventurePathManager.instance.Initialize();
            //Invoke("ActivatePathfinding", 2f);
        }

        private void ActivatePathfinding()
        {
            //AdventurePathManager.instance.EnableAll();
        }

        public void Save()
        {
            string dataAsJson = JsonUtility.ToJson(currentQuest);
            string filePath = Database.DataPath + "current_quest.json"; 
            File.WriteAllText(filePath, dataAsJson);
        }

        public void Load()
        {
            string filePath = Database.DataPath + "current_quest.json";

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                currentQuest = JsonUtility.FromJson<Quest>(dataAsJson);
            }

            LoadAdventure();
        }

        public void LoadAdventure()
        {
            if (currentQuest.MapData.MapType == MapType.Outdoors)
            {
                outdoorSceneSettings.SetActive(true);
                indoorsSceneSettings.SetActive(false);
                undergroundSceneSettings.SetActive(false);
            }
            else if (currentQuest.MapData.MapType == MapType.Indoors)
            {
                outdoorSceneSettings.SetActive(false);
                indoorsSceneSettings.SetActive(true);
                undergroundSceneSettings.SetActive(false);
            }
            else if (currentQuest.MapData.MapType == MapType.Underground)
            {
                outdoorSceneSettings.SetActive(false);
                indoorsSceneSettings.SetActive(false);
                undergroundSceneSettings.SetActive(true);
            }
        }

        public void SelectPc(int index)
        {
            SelectPc(PcManager.instance.PartyData.Pcs[index]);
        }

        public void SelectPc(PcData pcData)
        {
            PcManager.instance.SelectPc(pcData.PartyIndex);
            guiManager.SelectPc(pcData);
            mainCamera.gameObject.GetComponent<CameraController>().SetTarget(PcManager.instance.PcObjects[pcData.PartyIndex].transform);
        }
    }
}