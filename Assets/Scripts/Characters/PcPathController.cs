using Pathfinding;
using Pathfinding.RVO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class PcPathController : MonoBehaviour
    {
        [SerializeField] Seeker seeker = null;
        [SerializeField] RichAI pathAI = null;
        [SerializeField] AIDestinationSetter destinationSetter = null;
        [SerializeField] PathLineController pathLine = null;

        [SerializeField] GameObject targetIndicator = null;

        private void Awake()
        {
            Setup(transform.parent);
        }

        public void FindPath(Transform destination)
        {
            if (destination != null)
            {
                targetIndicator.transform.position = destination.position;
                //Path path = seeker.StartPath(gameObject.transform.position, targetIndicator.transform.position);
                Path path = ABPath.Construct(gameObject.transform.position, targetIndicator.transform.position, null);

                if (path.vectorPath.Count > 0)
                {
                    Debug.Log("path.vectorPath.Count " + path.vectorPath.Count);
                }
            }
            else
            {
                this.targetIndicator.transform.position = Vector3.zero;
            }
        }

        public void MoveTo(Transform destination)
        {
            FindPath(destination);
            destinationSetter.target = targetIndicator.transform;
        }

        public void Setup(Transform parent)
        {
            targetIndicator = new GameObject("dest");
            targetIndicator.transform.SetParent(parent);
        }
    }
}