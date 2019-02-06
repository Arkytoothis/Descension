using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class InitiativePanel : MonoBehaviour
    {
        [SerializeField] GameObject elementPrefab = null;
        [SerializeField] Transform elementsParent = null;
        [SerializeField] List<InitiativeElement> elements = new List<InitiativeElement>();

        public void Initialize()
        {
            Clear();
        }

        public void Clear()
        {
            Helper.ClearTransform(elementsParent);
            elements.Clear();
        }

        public void LoadInitiativeOrder()
        {
            Clear();

            foreach (InitiativeData initiative in EncounterManager.instance.InitiativeList)
            {
                GameObject elementObject = Instantiate(elementPrefab, elementsParent);

                InitiativeElement elementScript = elementObject.GetComponent<InitiativeElement>();
                elementScript.SetData(elements.Count, initiative);

                elements.Add(elementScript);
            }
        }

        public void Select(int index)
        {
            DeselectAll();
            elements[index].Select();
        }

        public void DeselectAll()
        {
            foreach (InitiativeElement element in elements)
            { 
                element.Deselect();
            }
        }
    }
}