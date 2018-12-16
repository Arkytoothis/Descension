using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Characters
{
    public class AttributeBonus
    {
        public string Attribute;
        public int MinValue;
        public int MaxValue;

        public AttributeBonus()
        {
            Attribute = "";
            MinValue = 0;
            MaxValue = 0;
        }

        public AttributeBonus(string attribute, int min_value, int max_value)
        {
        }
    }
}