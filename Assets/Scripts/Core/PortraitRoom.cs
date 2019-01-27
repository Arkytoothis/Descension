using Descension.Characters;
using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Descension
{
    public class PortraitRoom : Singleton<PortraitRoom>
    {
        [SerializeField] PcManager pcManager = null;
        [SerializeField] GameObject mountPrefab = null;
        [SerializeField] List<CharacterPortrait> pcMounts = new List<CharacterPortrait>();
        [SerializeField] Transform mountsParent = null;

        public List<CharacterPortrait> PcMounts { get { return pcMounts;  } }

        void Awake()
        {
            Reload();
        }

        public void Initialize(int numMounts)
        {
            pcMounts = new List<CharacterPortrait>();

            for (int i = 0; i < numMounts; i++)
            {
                AddPcMount(new Vector3(-(i * 2), 0, 0), i);
            }
        }

        public void AddPcMount(Vector3 position, int index)
        {
            GameObject mountObject = Instantiate(mountPrefab);
            mountObject.transform.SetParent(mountsParent, false);
            mountObject.transform.localPosition = position;
            mountObject.SetActive(true);

            pcMounts.Add(mountObject.GetComponent<CharacterPortrait>());

            CharacterPortrait mountScript = mountObject.GetComponent<CharacterPortrait>();
            mountScript.Setup(index);

            AddModel(ModelManager.instance.SpawnCharacterModel(pcManager.PcDataList[index], mountObject.transform), index);
        }

        public void AddModel(GameObject model, int index)
        {
            model.transform.SetParent(pcMounts[index].Pivot);
            model.transform.localPosition = Vector3.zero;
            model.name = model.name;
        }

        public void SelectPc(int index)
        {
            for (int i = 0; i < pcMounts.Count; i++)
            {
                if (i == index)
                {
                    pcMounts[i].EnableFarCamera();
                }
                else
                {
                    pcMounts[i].DisableFarCamera();
                }
            }
        }
    }
}