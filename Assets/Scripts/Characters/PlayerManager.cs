using Descension.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Core
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        [SerializeField] List<GameObject> pcObjects = new List<GameObject>();
        [SerializeField] List<PcData> pcs = new List<PcData>();

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
            }
        }

        public void GeneratePcs()
        {
            GeneratePcData();
            GeneratePcObjects();
        }

        public void GeneratePcData()
        {
            pcs.Add(PcGenerator.Generate(0, Gender.Male, "Imperial", "Soldier"));
            pcs.Add(PcGenerator.Generate(1, Gender.Female, "Imperial", "Soldier"));
        }

        public void GeneratePcObjects()
        {
            for (int i = 0; i < pcs.Count; i++)
            {
                GameObject pcObject = ModelManager.instance.SpawnCharacterModel(pcs[i], transform);
                pcObject.name = pcs[i].Name.ShortName + " Model";

                pcObjects.Add(pcObject);
            }
        }
    }
}