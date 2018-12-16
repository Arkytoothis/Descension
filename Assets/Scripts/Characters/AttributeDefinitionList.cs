using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Characters
{
    [System.Serializable]
    public class AttributeDefinitionList
    {
        private List<AttributeDefinition> attributes;

        public List<AttributeDefinition> Attributes { get { return attributes; } }

        public AttributeDefinitionList()
        {
            attributes = new List<AttributeDefinition>();
        }
    }
}