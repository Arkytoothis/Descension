using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Descension.Core;

namespace Descension.Characters
{
    [System.Serializable]
    public class AttributeDefinition
    {
        public AttributeDefinitionType Type;
        public string Name;
        public string ShortName;
        public string Abbreviated;
        public string Description;
        public int Minimum;
        public int Maximum;

        public AttributeCalculation Calculation;

        public AttributeDefinition()
        {
            Type = AttributeDefinitionType.None;
            Name = "";
            ShortName = "";
            Abbreviated = "";
            Description = "";
            Minimum = 0;
            Maximum = 0;
            Calculation = new AttributeCalculation();
        }

        public AttributeDefinition(string name, string short_name, string abbreviated, string description, int minimum, int maximum, AttributeDefinitionType type,
            AttributeCalculation calc)
        {
            Name = name;
            ShortName = short_name;
            Abbreviated = abbreviated;
            Description = description;
            Type = type;
            Minimum = minimum;
            Maximum = maximum;
            Calculation = new AttributeCalculation(calc);
        }

        public AttributeDefinition(AttributeDefinition def)
        {
            Name = def.Name;
            ShortName = def.ShortName;
            Abbreviated = def.Abbreviated;
            Description = def.Description;
            Type = def.Type;
            Minimum = def.Minimum;
            Maximum = def.Maximum;
            Calculation = new AttributeCalculation(def.Calculation);
        }
    }
}