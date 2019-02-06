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
        }

        public bool OnMouseOverWalkable(RaycastHit hit)
        {
            transform.position = hit.point;
            //WorldCursorTooltip.instance.UpdateText(hit.collider.gameObject.name, "", "");

            return true;
        }

        public bool OnMouseOverObstacle(RaycastHit hit)
        {
            transform.position = hit.point;
            //WorldCursorTooltip.instance.UpdateText(hit.collider.gameObject.name, "", "");

            return true;
        }

        public void Enable()
        {
            gameObject.SetActive(true);
            //WorldCursorTooltip.instance.gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            //WorldCursorTooltip.instance.gameObject.SetActive(false);
        }
    }
}
