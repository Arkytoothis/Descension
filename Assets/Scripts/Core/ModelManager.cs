using Descension.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Core
{
    public class ModelManager : Singleton<ModelManager>
    {
        private void Awake()
        {
            Reload();
        }

        private bool initialized = false;

        [SerializeField] List<GameObject> characterPrefabList = new List<GameObject>();
        [SerializeField] Dictionary<string, GameObject> characterPrefabs = new Dictionary<string, GameObject>();

        [SerializeField] List<GameObject> itemPrefabList = new List<GameObject>();
        [SerializeField] Dictionary<string, GameObject> itemPrefabs = new Dictionary<string, GameObject>();

        [SerializeField] List<GameObject> hairPrefabList = new List<GameObject>();
        [SerializeField] Dictionary<string, GameObject> hairPrefabs = new Dictionary<string, GameObject>();

        [SerializeField] List<GameObject> beardPrefabList = new List<GameObject>();
        [SerializeField] Dictionary<string, GameObject> beardPrefabs = new Dictionary<string, GameObject>();

        public void Initialize()
        {
            if (initialized == false)
            {
                initialized = true;

                if (characterPrefabList.Count > 0)
                {
                    for (int i = 0; i < characterPrefabList.Count; i++)
                    {
                        characterPrefabs.Add(characterPrefabList[i].name, characterPrefabList[i]);
                    }
                }
            }
        }

        public GameObject SpawnCharacterModel(PcData pc, Transform parent)
        {
            GameObject model = Instantiate(characterPrefabs[pc.RaceKey + " " + pc.Gender.ToString()], parent);

            return model;
        }
    }
}