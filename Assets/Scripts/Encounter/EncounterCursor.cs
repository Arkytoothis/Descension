using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class EncounterCursor : MonoBehaviour
    {
        [SerializeField] CameraRaycaster raycaster = null;

        private void Awake()
        {
            raycaster = Camera.main.gameObject.GetComponent<CameraRaycaster>();
        }

        private void Start()
        {
            raycaster.onMouseOverWalkable += OnMouseOverWalkable;
            raycaster.onMouseOverObstacle += OnMouseOverObstacle;
        }

        public bool OnMouseOverWalkable(RaycastHit hit)
        {
            EncounterCell cell = hit.transform.parent.GetComponent<EncounterCell>();

            if (cell != null)
            {
                if (cell.PcIndex != -1)
                {
                    WorldCursorTooltip.instance.UpdateText("X: " + cell.X, "Y: " + cell.Y, PcManager.instance.PartyData.Pcs[cell.PcIndex].Name.ShortName);
                }

                if (cell.NpcIndex != -1)
                {
                    WorldCursorTooltip.instance.UpdateText("X: " + cell.X, "Y: " + cell.Y, EncounterManager.instance.Npcs[cell.NpcIndex].Name.ShortName);
                }
            }

            transform.position = new Vector3(hit.collider.transform.position.x - 0.5f, 0, hit.collider.transform.position.z + 0.5f);

            return false;
        }

        public bool OnMouseOverObstacle(RaycastHit hit)
        {
            transform.position = new Vector3(0, 0, -2000);
            return false;
        }
    }
}