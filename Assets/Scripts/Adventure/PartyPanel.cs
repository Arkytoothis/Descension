using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class PartyPanel : MonoBehaviour
    {
        [SerializeField] PcManager pcManager = null;

        [SerializeField] GameObject partyMemberElementPrefab = null;
        [SerializeField] Transform partyMemberElementsParent = null;
        [SerializeField] List<GameObject> partyMemberElements = new List<GameObject>();

        public void Setup()
        {
            for (int i = 0; i < pcManager.PartyData.Pcs.Count; i++)
            {
                GameObject elementObject = Instantiate(partyMemberElementPrefab, partyMemberElementsParent);

                PartyMemberElement elementScript = elementObject.GetComponent<PartyMemberElement>();
                int index = i;
                elementScript.Setup(pcManager.PartyData.Pcs[i], delegate { SelectPc(index); } );

                partyMemberElements.Add(elementObject);
            }
        }

        public void SelectPc(int index)
        {
            AdventureManager.instance.SelectPc(pcManager.PartyData.Pcs[index]);
        }
    }
}