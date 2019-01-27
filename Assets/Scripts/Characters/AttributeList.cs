using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Characters
{
    [System.Serializable]
    public class AttributeList
    {
        [SerializeField] List<Attribute> attributes;

        public List<Attribute> Attributes { get { return attributes; } }

        public AttributeList()
        {
            attributes = new List<Attribute>();
        }

        public void LoadAttributes(List<Attribute> attributes)
        {
            for (int i = 0; i < attributes.Count; i++)
            {
                this.attributes.Add(attributes[i]); 
            }
        }

        public void ModifyCurrent(int attribute, int value)
        {
            attributes[attribute].Current += value;
        }

        public void ModifyMaximum(int attribute, int value)
        {
            attributes[attribute].SetMax(attributes[attribute].Maximum + value, false);
        }
    }
}