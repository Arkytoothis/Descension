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
    public class AdventureManager : Singleton<AdventureManager>
    {
        [SerializeField] AdventureGuiManager guiManager = null;
        [SerializeField] PlayerManagerTown playerManager = null;

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

            playerManager.Initialize();
            guiManager.Initialize();

            return null;
        }

        public void Setup()
        {
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }
}