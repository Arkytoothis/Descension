using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Descension.Characters;
using Descension.Core;
using UnityEngine.UI;

namespace Descension
{
    public class InitiativeElement : MonoBehaviour
    {
        [SerializeField] Image border = null;
        [SerializeField] TMP_Text initiativeLabel = null;
        [SerializeField] TMP_Text nameLabel = null;
        [SerializeField] TMP_Text detailsLabel = null;

        [SerializeField] int listIndex;
        [SerializeField] int characterIndex;
        [SerializeField] string text = "";
        [SerializeField] Color textColor = Color.white;

        public void SetData(int index, InitiativeData init)
        {
            name = "Initative Element " + index;

            listIndex = index;
            characterIndex = init.Index;

            switch (init.Faction)
            {
                case Faction.Player:
                    textColor = Color.cyan;
                    text = PcManager.instance.PartyData.Pcs[characterIndex].Name.ShortName;
                    detailsLabel.text = "HP: " + PcManager.instance.PartyData.Pcs[characterIndex].GetDerived((int)DerivedAttribute.Health).Current + "/" +
                        PcManager.instance.PartyData.Pcs[characterIndex].GetDerived((int)DerivedAttribute.Health).Maximum;
                    break;
                case Faction.Enemy:
                    textColor = Color.red;
                    text = EncounterManager.instance.Npcs[characterIndex].Name.ShortName;
                    detailsLabel.text = "HP: " + EncounterManager.instance.Npcs[characterIndex].GetDerived((int)DerivedAttribute.Health).Current + "/" +
                        EncounterManager.instance.Npcs[characterIndex].GetDerived((int)DerivedAttribute.Health).Maximum;
                    break;
                case Faction.Neutral:
                    textColor = Color.yellow;
                    break;
            }

            nameLabel.text = text;
            nameLabel.color = textColor;
            initiativeLabel.text = init.Initiative.ToString();
            border = GetComponent<Image>();
            border.color = Color.gray;
        }

        public void Select()
        {
            Debug.Log(border.gameObject.name + " Selected");
            border.color = Color.white;
        }

        public void Deselect()
        {
            GetComponent<Image>().color = Color.gray;
        }
    }
}