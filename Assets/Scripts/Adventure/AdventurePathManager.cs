using Descension.Core;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class AdventurePathManager : Singleton<AdventurePathManager>
    {
        [SerializeField] PcManager pcManager = null;

        [SerializeField] AstarPath graph = null;

        public void Initialize()
        {
            UpdateGraph();
        }

        public void SetDestination(Transform destination)
        {
            for (int i = 0; i < pcManager.PcObjects.Count; i++)
            {
                if (pcManager.SelectedPcIndex == i)
                {
                    PcPathController controller = pcManager.PcObjects[i].GetComponent<PcPathController>();
                    controller.enabled = true;
                    controller.MoveTo(destination);
                }
            }
        }

        public void UpdateGraph()
        {
            graph.Scan();
        }
    }
}