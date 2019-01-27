using Descension.Characters;
using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class PartySpawner : MonoBehaviour
    {
        public void SpawnParty(Transform parent)
        {
            foreach (PcData pc in PcManager.instance.PartyData.Pcs)
            {
                ConstructPc(parent, pc);
            }

            AdventureManager.instance.SelectPc(0);
        }

        public void ConstructPc(Transform parent, PcData pcData)
        {
            int index = PcManager.instance.PcObjects.Count;
            Vector3 spawnPosition = transform.position + new Vector3((index % 2) - 0.5f, 0.01f, -(index / 2) + 0.5f);

            GameObject baseObject = Instantiate(ModelManager.instance.EmptyPcPrefab, parent);
            baseObject.transform.SetParent(parent);
            baseObject.transform.position = spawnPosition;

            GameObject model = ModelManager.instance.SpawnCharacterModel(pcData, baseObject.transform);

            PcManager.instance.AddPcObject(baseObject);
        }
    }
}