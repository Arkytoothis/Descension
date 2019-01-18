using DunGen;
using DunGen.Adapters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class DungeonFinisher : BaseAdapter
    {
        protected override void Run(DungeonGenerator generator)
        {
            AdventureManager.instance.SetupPlayerSpawner();
        }
    }
}