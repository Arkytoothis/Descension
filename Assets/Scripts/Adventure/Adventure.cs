using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class Adventure
    {
        [SerializeField] Quest quest = null;

        public Adventure()
        {
            quest = null;
        }

        public Adventure(Quest quest)
        {
            this.quest = new Quest(quest);
        }

        public Adventure(Adventure adventure)
        {
            quest = new Quest(adventure.quest);
        }
    }
}