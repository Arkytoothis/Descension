using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Descension
{
    public class GameManager : Singleton<GameManager>
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

            return null;
        }
    }
}