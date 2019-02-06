using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Descension.Abilities;
using Descension.Core;
using Descension.Name;
using Descension.Equipment;
using Descension.Characters;

namespace Descension
{
    public static class NpcGenerator
    {
        static bool initialized = false;

        //static bool ignoreUnlocks = false;

        public static void Initialize()
        {
            if (initialized == false)
            {
                initialized = true;
            }
        }

        public static NpcData Generate(int listIndex, string definition)
        {
            NpcData npc = new NpcData(Database.GetNPC(definition));
            npc.Index = listIndex;

            return npc;
        }
    }
}