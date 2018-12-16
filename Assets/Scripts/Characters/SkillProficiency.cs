using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Core;

namespace Descension.Characters
{
    public class SkillProficiency
    {
        public Skill Skill;
        public int Value;
        public int Minimum;

        public SkillProficiency()
        {
            Skill = 0;
            Value = 0;
            Minimum = 0;
        }

        public SkillProficiency(Skill skill, int value, int minimum)
        {
            Skill = skill;
            Value = value;
            Minimum = minimum;
        }

        public static SkillProficiency Randomize(GameValue value)
        {
            SkillProficiency sp = new SkillProficiency();
            sp.Skill = (Skill)Random.Range(0, (int)Skill.Number);
            sp.Value = value.Roll(false);
            sp.Minimum = 0;
            return sp;
        }
    }
}