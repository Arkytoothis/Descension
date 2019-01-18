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
        [SerializeField] AdventureGuiManager guiManager = null;
        [SerializeField] PcManager pcManager = null;

        [SerializeField] Quest currentQuest = null;
        public Quest CurrentQuest { get { return currentQuest; } }

        [SerializeField] Adventure adventure = null;
        public Adventure Adventure { get { return adventure; } }

        [SerializeField] PlayerSpawner playerSpawner = null;

        private void Awake()
        {
            Reload();
        }

        private void Start()
        {
            StartCoroutine("Initialize");
        }

        IEnumerator Initialize()
        {
            //Debug.Log("AdventureManager.Initialize()");
            Database.Initialize();
            ItemGenerator.Initialize();
            PcGenerator.Initialize();
            ModelManager.instance.Initialize();

            Load();
            pcManager.Initialize();
            guiManager.Initialize(currentQuest);

            return null;
        }

        public void SetupPlayerSpawner()
        {
            playerSpawner = FindObjectOfType<PlayerSpawner>();
            playerSpawner.SpawnPlayer();
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
                //Debug.Log(currentQuest.Name + " loaded");
            }
        }

        public void LoadAdventure()
        {
        }
    }
}