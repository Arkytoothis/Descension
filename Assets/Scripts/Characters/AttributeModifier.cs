using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Characters
{
    [System.Serializable]
    public class AttributeModifier
    {
        public AttributeModifierType Type;
        public int Attribute;

        public AttributeModifier()
        {
            Type = AttributeModifierType.None;
            Attribute = 0;
        }

        public AttributeModifier(AttributeModifierType type, int attribute)
        {
            Type = type;
            Attribute = attribute;
        }

        public AttributeModifier(AttributeModifier mod)
        {
            Type = mod.Type;
            Attribute = mod.Attribute;
        }
    }
}