using UnityEngine;
using System.Collections;
using Descension.Core;

namespace Descension.Equipment
{
    public class IngredientData
    {
        public string Name;
        public string Key;

        public IngredientType Type;

        public IngredientData()
        {
            Name = "";
            Key = "";
        }

        public IngredientData(IngredientType type, string name, string key)
        {
            Type = type;
            Name = name;
            Key = key;
        }

        public IngredientData(IngredientData data)
        {
            Type = data.Type;
            Name = data.Name;
            Key = data.Key;
        }
    }
}