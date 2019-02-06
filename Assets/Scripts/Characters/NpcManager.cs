using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class NpcManager : Singleton<NpcManager>
    {
        private bool initialized = false;

        private void Awake()
        {
            Reload();
        }

        public void Initialize()
        {
            if (initialized == false)
            {
                initialized = true;
            }
        }
    }
}