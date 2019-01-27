using Descension.Core;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class WorldCursor : Singleton<WorldCursor>
    {
        [SerializeField] bool isEnabled = true;
        [SerializeField] CameraRaycaster raycaster = null;
        [SerializeField] GameObject sphere = null;

        [SerializeField] RaycastHit hit;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }

        void Awake()
        {
            raycaster = Camera.main.GetComponent<CameraRaycaster>();
        }

        void Start()
        {
            raycaster.onMouseOverWalkable += OnMouseOverWalkable;
            raycaster.onMouseOverObstacle += OnMouseOverObstacle;

            //InvokeRepeating("UpdatePathLine", 0.1f, 0.1f);
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(1) == true)
            {
                AdventurePathManager.instance.SetDestination(transform);
            }
        }

        public bool OnMouseOverWalkable(RaycastHit hit)
        {
            transform.position = hit.point;
            PcManager.instance.GetSelectedPcObject().GetComponentInChildren<PcPathController>().FindPath(hit.transform);
            //Vector3 pc = PcManager.instance.GetSelectedPcObject().transform.position;
            //string distance =  Vector3.Distance(pc, this.hit.point).ToString();

            //WorldCursorTooltip.instance.UpdateDistance(distance);

            return true;
        }

        public bool OnMouseOverObstacle(RaycastHit hit)
        {
            WorldCursorTooltip.instance.UpdateDistance("Can't Move");

            return true;
        }
    }
}
