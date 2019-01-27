using Descension.Characters;
using Descension.Core;
using Descension.Equipment;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Descension
{
    public class TownManager : Singleton<TownManager>
    {
        [SerializeField] TownGuiManager guiManager = null;

        [SerializeField] QuestManager questManager = null;
        public QuestManager QuestManager { get { return questManager; } }

        [SerializeField] List<GameObject> buildingObjects = new List<GameObject>();

        public Dictionary<string, bool> racesUnlocked = new Dictionary<string, bool>();
        public Dictionary<string, bool> professionsUnlocked = new Dictionary<string, bool>();

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
            Database.Initialize();
            SpriteManager.instance.Initialize();
            LoadUnlocks();

            ItemGenerator.Initialize();
            PcGenerator.Initialize();
            QuestGenerator.Initialize();

            ModelManager.instance.Initialize();

            PcManager.instance.Initialize();
            //PcManager.instance.Load();
            PcManager.instance.Generate();

            questManager.Initialize();

            PortraitRoom.instance.Initialize(PcManager.instance.PcDataList.Count);

            guiManager.Initialize(buildingObjects);

            //AudioManager.instance.PlayMusic("Back Home", 0, 5f);
            AudioManager.instance.PlayAmbient("Town Day");

            return null;
        }

        private void LoadUnlocks()
        {
            foreach (KeyValuePair<string, Race> kvp in Database.Races)
            {
                racesUnlocked.Add(kvp.Key, true);
            }

            foreach (KeyValuePair<string, Profession> kvp in Database.Professions)
                professionsUnlocked.Add(kvp.Key, true);
        }

        public bool IsRaceUnlocked(string key)
        {
            bool unlocked = false;

            if (racesUnlocked.ContainsKey(key) == true)
            {
                if (racesUnlocked[key] == true)
                    unlocked = true;
                else
                    unlocked = false;
            }
            else
            {
                Debug.Log("Key " + key + " does not exist");
                unlocked = false;
            }

            return unlocked;
        }

        public bool IsProfessionUnlocked(string key)
        {
            bool unlocked = false;

            if (professionsUnlocked.ContainsKey(key) == true)
            {
                if (professionsUnlocked[key] == true)
                    unlocked = true;
                else
                    unlocked = false;
            }
            else
            {
                Debug.Log("Key " + key + " does not exist");
                unlocked = false;
            }

            return unlocked;
        }

        public void StartAdventure()
        {
            PcManager.instance.Save();
            questManager.Save();

            SceneManager.LoadSceneAsync(2);
        }
    }
}