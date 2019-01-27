using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class PathLineController : MonoBehaviour
    {
        [SerializeField] LineRenderer pathLine = null;
        [SerializeField] LineRenderer attackLine = null;
        [SerializeField] LineRenderer abilityLine = null;

        public void UpdatePath(Seeker seeker)
        {
            Path path = seeker.GetCurrentPath();

            if (path != null)
            {
                Debug.Log("Path Count: " + path.vectorPath.Count);
            }
        }
    }
}