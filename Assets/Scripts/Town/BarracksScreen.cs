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

        [SerializeField] CharacterDetailsPanel detailsPanel = null;
        [SerializeField] BaseAttributesPanel baseAttributesPanel = null;
        [SerializeField] DerivedAttributesPanel derivedAttributesPanel = null;
        [SerializeField] SkillsPanel skillsPanel = null;
        [SerializeField] EquipmentPanel equipmentPanel = null;
        [SerializeField] AbilitiesPanel abilitiesPanel = null;


        public override void Initialize()
        {
            base.Initialize();

            detailsPanel.Initialize();
            baseAttributesPanel.Initialize();
            derivedAttributesPanel.Initialize();
            skillsPanel.Initialize();
            equipmentPanel.Initialize();
            abilitiesPanel.Initialize();

            SetupPcList();
            SelectPc(0);
        }

        public void SetupPcList()
        {
            int index = 0;
            foreach (PcData pc in PcManager.instance.PcDataList)
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
            PortraitRoom.instance.SelectPc(index);

            detailsPanel.SetData(PcManager.instance.PcDataList[index]);
            baseAttributesPanel.SetData(PcManager.instance.PcDataList[index]);
            derivedAttributesPanel.SetData(PcManager.instance.PcDataList[index]);
            skillsPanel.SetData(PcManager.instance.PcDataList[index]);
            equipmentPanel.SetData(PcManager.instance.PcDataList[index]);
            abilitiesPanel.SetData(PcManager.instance.PcDataList[index]);
        }
    }
}