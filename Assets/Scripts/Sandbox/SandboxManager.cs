using Descension.Characters;
using Descension.Core;
using Descension.Equipment;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Descension
{
    public class SandboxManager : Singleton<SandboxManager>
    {
        private void Awake()
        {
            Reload();
        }

        private void Start()
        {
            StartCoroutine("Initialize");
        }

        IEnumerator Initialize()
        {
            Database.Initialize();
            ItemGenerator.Initialize();
            PcGenerator.Initialize();

            ModelManager.instance.Initialize();
            PlayerManager.instance.Initialize();

            return null;
        }
    }
}