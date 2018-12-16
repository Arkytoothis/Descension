using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Characters
{
    [System.Serializable]
    public class SkillRequirement
    {
        public string Key;
        public int DefinitionIndex;
        public int Value;

        public SkillRequirement()
        {
            Key = "";
            DefinitionIndex = 0;
            Value = 0;
        }

        public SkillRequirement(int index, int value)
        {
            Key = Database.GetSkill(index).Name;
            DefinitionIndex = index;
            Value = value;
        }

        public SkillRequirement(string key, int index, int value)
        {
            Key = key;
            DefinitionIndex = index;
            Value = value;
        }

        public SkillRequirement(SkillRequirement skill_req)
        {
            Key = skill_req.Key;
            DefinitionIndex = skill_req.DefinitionIndex;
            Value = skill_req.Value;
        }
    }
}