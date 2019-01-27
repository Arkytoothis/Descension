using Descension.Characters;
using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Descension
{
    public class BaseAttributesPanel : MonoBehaviour
    {
        [SerializeField] GameObject attributePrefab = null;
        [SerializeField] Transform attributesParent = null;
        [SerializeField] List<GameObject> attributeElements = new List<GameObject>();
        
        public void Initialize()
        {
            for (int i = 0; i < (int)BaseAttribute.Number; i++)
            {
                GameObject attributeObject = Instantiate(attributePrefab, attributesParent);
                attributeObject.name = "Base Attribute Element - " + ((BaseAttribute)i).ToString();

                BaseAttributeElement attributeScript = attributeObject.GetComponent<BaseAttributeElement>();
                attributeScript.SetData(null);

                attributeElements.Add(attributeObject);
            }
        }

        public void SetData(PcData pcData)
        {
            for (int i = 0; i < (int)BaseAttribute.Number; i++)
            {
                attributeElements[i].GetComponent<BaseAttributeElement>().SetData(pcData.Attributes.GetAttribute(AttributeListType.Base, i));
            }
        }
    }
}