using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class TavernScreen : TownScreen
    {
        [SerializeField] List<GameObject> availableQuestElements = new List<GameObject>();
        [SerializeField] GameObject availableQuestElementPrefab = null;
        [SerializeField] Transform availableQuestElementsParent = null;

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}