using Descension.Characters;
using Descension.Equipment;
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

        [SerializeField] GameObject emptyPcPrefab = null;
        public GameObject EmptyPcPrefab { get { return emptyPcPrefab; } }

        [SerializeField] List<GameObject> characterPrefabList = new List<GameObject>();
        [SerializeField] Dictionary<string, GameObject> characterPrefabs = new Dictionary<string, GameObject>();

        [SerializeField] List<GameObject> itemPrefabList = new List<GameObject>();
        [SerializeField] Dictionary<string, GameObject> itemPrefabs = new Dictionary<string, GameObject>();

        [SerializeField] List<GameObject> hairPrefabList = new List<GameObject>();
        [SerializeField] Dictionary<string, GameObject> hairPrefabs = new Dictionary<string, GameObject>();

        [SerializeField] List<GameObject> beardPrefabList = new List<GameObject>();
        [SerializeField] Dictionary<string, GameObject> beardPrefabs = new Dictionary<string, GameObject>();

        public Dictionary<string, GameObject> HairPrefabs { get => hairPrefabs; }
        public Dictionary<string, GameObject> BeardPrefabs { get => beardPrefabs; }

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

                if (hairPrefabList.Count > 0)
                {
                    for (int i = 0; i < hairPrefabList.Count; i++)
                    {
                        hairPrefabs.Add(hairPrefabList[i].name, hairPrefabList[i]);
                    }
                }

                if (beardPrefabList.Count > 0)
                {
                    for (int i = 0; i < beardPrefabList.Count; i++)
                    {
                        beardPrefabs.Add(beardPrefabList[i].name, beardPrefabList[i]);
                    }
                }

                if (itemPrefabList.Count > 0)
                {
                    for (int i = 0; i < itemPrefabList.Count; i++)
                    {
                        itemPrefabs.Add(itemPrefabList[i].name, itemPrefabList[i]);
                    }
                }
            }
        }

        public GameObject SpawnCharacterModel(PcData pc, Transform parent)
        {
            GameObject model = Instantiate(characterPrefabs[pc.RaceKey + " " + pc.Gender.ToString()], parent);

            CharacterRenderer renderer = model.GetComponent<CharacterRenderer>();
            SpawnHairModel(pc, renderer.Mounts[(int)CharacterRenderSlot.Head]);
            SpawnBeardModel(pc, renderer.Mounts[(int)CharacterRenderSlot.Face]);

            SpawnItem(pc, renderer.Mounts[(int)CharacterRenderSlot.Right_Hand], (int)EquipmentSlot.Right_Hand);
            SpawnItem(pc, renderer.Mounts[(int)CharacterRenderSlot.Left_Hand], (int)EquipmentSlot.Left_Hand);
            SpawnItem(pc, renderer.Mounts[(int)CharacterRenderSlot.Head], (int)EquipmentSlot.Head);

            return model;
        }

        public void SpawnHairModel(PcData pc, Transform parent)
        {
            if (pc.Hair != "")
            {
                GameObject hairObject = Instantiate(hairPrefabs[pc.Gender.ToString() + " " + pc.Hair], parent);
                hairObject.name = pc.Gender.ToString() + " " + pc.Hair;
            }
        }

        public void SpawnBeardModel(PcData pc, Transform parent)
        {
            if (pc.Beard != "")
            {
                GameObject beardObject = Instantiate(beardPrefabs[pc.Beard], parent);
                beardObject.name = pc.Gender.ToString() + " " + pc.Beard;
            }
        }

        public void SpawnItem(PcData pcData, Transform parent, int equipmentSlot)
        {
            ItemData item = pcData.Inventory.EquippedItems[equipmentSlot];

            if (item != null && itemPrefabs.ContainsKey(item.MeshKey))
            {
                GameObject itemObject = Instantiate(itemPrefabs[item.MeshKey], parent);
            }
        }
    }
}