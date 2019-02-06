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
        }

        public void ConstructPc(Transform parent, PcData pcData)
        {
            int index = EncounterRoom.instance.PcObjects.Count;
            GameObject baseObject = Instantiate(ModelManager.instance.EmptyPcPrefab);
            GameObject model = ModelManager.instance.SpawnCharacterModel(pcData, baseObject.transform);

            EncounterRoom.instance.AddPc(baseObject, index);
        }
    }
}