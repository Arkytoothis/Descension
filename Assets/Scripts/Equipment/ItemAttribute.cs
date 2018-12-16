using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Equipment
{
    [System.Serializable]
    public class ItemAttribute
    {
        public int Index;
        public int Value;

        public ItemAttribute()
        {
            Index = 0;
            Value = 0;
        }

        public ItemAttribute(int index, int value)
        {
            Index = index;
            Value = value;
        }

        public ItemAttribute(ItemAttribute attribute)
        {
            Index = attribute.Index;
            Value = attribute.Value;
        }
    }
}