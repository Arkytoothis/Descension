using Descension.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class BarracksScreen : TownScreen
    {
        [SerializeField] List<GameObject> pcListElements = new List<GameObject>();
        [SerializeField] GameObject pcListElementPrefab = null;
        [SerializeField] Transform pcListElementsParent = null;

        public override void Initialize()
        {
            base.Initialize();

            SetupPcList();
        }

        public void SetupPcList()
        {
            int index = 0;
            foreach (PcData pc in TownManager.instance.PcManager.PcDataList)
            {
                GameObject go = Instantiate(pcListElementPrefab, pcListElementsParent);
                go.name = pc.Name.ShortName;

                PcListElement script = go.GetComponent<PcListElement>();
                int i = index;
                script.Setup(pc, delegate { SelectPc(i); });

                pcListElements.Add(go);
                index++;
            }
        }

        public void SelectPc(int index)
        {
            Debug.Log("Pc " + index + " selected");
        }
    }
}