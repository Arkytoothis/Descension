using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Characters
{
    public class BackgroundDefinition
    {
        public string Name;
        public string Key;
        public string Description;

        public BackgroundDefinition()
        {
            Name = "";
            Key = "";
            Description = "";
        }
        public BackgroundDefinition(string name, string key, string description)
        {
            Name = name;
            Key = key;
            Description = description;
        }

        public BackgroundDefinition(BackgroundDefinition def)
        {
            Name = def.Name;
            Key = def.Key;
            Description = def.Description;
        }
    }
}