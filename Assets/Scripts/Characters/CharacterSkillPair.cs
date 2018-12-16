using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Characters
{
    [System.Serializable]
    public class CharacterSkillPair
    {
        public int CharacterIndex;
        public int SkillValue;

        public CharacterSkillPair(int character, int skill)
        {
            CharacterIndex = character;
            SkillValue = skill;
        }
    }
}