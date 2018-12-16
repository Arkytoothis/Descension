using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Descension.Characters;
#pragma warning disable 0414
namespace Descension.Core
{
    public class CameraRaycaster : MonoBehaviour
    {
        [SerializeField] Texture2D moveCursor = null;
        [SerializeField] Texture2D interactCursor = null;
        [SerializeField] Texture2D transitionCursor = null;
        [SerializeField] Texture2D meleeCursor = null;
        [SerializeField] Texture2D rangedCursor = null;
        [SerializeField] Texture2D powerCursor = null;
        [SerializeField] Texture2D spellCursor = null;
        [SerializeField] Texture2D guiCursor = null;
        [SerializeField] Texture2D errorCursor = null;

        [SerializeField] Vector2 hotspot = Vector2.zero;

        public delegate bool OnMouseOverTerrain(RaycastHit hit);
        public event OnMouseOverTerrain onMouseOverWalkable = null;

        public delegate bool OnMouseOverEnemy(GameObject go);
        public event OnMouseOverEnemy onMouseOverEnemy = null;

        public delegate bool OnMouseOverInteractable(GameObject go);
        public event OnMouseOverInteractable onMouseOverInteractable = null;

        const int WALKABLE_LAYER_NUMBER = 8;
        const int OBSTACLE_LAYER_NUMBER = 10;

        void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject() == true)
            {
                Cursor.SetCursor(guiCursor, hotspot, CursorMode.Auto);
                return;
            }
            else
            {
                PerformRaycasts();
            }
        }

        void PerformRaycasts()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (RaycastForEnemy(ray) == true) { return; }
            if (RaycastForInteraction(ray) == true) { return; }
            if (RaycastForObstacle(ray) == true) { return; }
            if (RaycastForWalkability(ray) == true) { return; }
        }

        bool RaycastForEnemy(Ray ray)
        {
            //RaycastHit hit;

            //if (Physics.Raycast(ray, out hit, 1000))
            //{
            //    NpcController npc = hit.collider.gameObject.GetComponent<NpcController>();

            //    if (npc != null && npc.CheckIsAlive() == true)
            //    {
            //        Cursor.SetCursor(meleeCursor, hotspot, CursorMode.Auto);

            //        if(onMouseOverEnemy != null)
            //            onMouseOverEnemy(npc.gameObject);

            //        return true;
            //    }
            //}

            return false;
        }

        bool RaycastForInteraction(Ray ray)
        {
            //RaycastHit hit;

            //if (Physics.Raycast(ray, out hit, 1000))
            //{
            //    Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();

            //    if (interactable != null)
            //    {
            //        if(interactable is Transition == true)
            //            Cursor.SetCursor(transitionCursor, hotspot, CursorMode.Auto);
            //        else
            //            Cursor.SetCursor(interactCursor, hotspot, CursorMode.Auto);

            //        if(onMouseOverInteractable != null)
            //            onMouseOverInteractable(interactable.gameObject);

            //        return true;
            //    }
            //}

            return false;
        }

        bool RaycastForObstacle(Ray ray)
        {
            RaycastHit hit;
            LayerMask obstacleLayer = 1 << OBSTACLE_LAYER_NUMBER;
            bool obstacle = Physics.Raycast(ray, out hit, 1000, obstacleLayer);

            if (obstacle == true)
            {
                Cursor.SetCursor(errorCursor, hotspot, CursorMode.Auto);
                return true;
            }
            else
            {
                return false;
            }
        }

        bool RaycastForWalkability(Ray ray)
        {
            RaycastHit hit;
            LayerMask walkableLayer = 1 << WALKABLE_LAYER_NUMBER;
            bool walkability = Physics.Raycast(ray, out hit, 1000, walkableLayer);

            if (walkability == true)
            {
                if (hit.collider.gameObject.layer == WALKABLE_LAYER_NUMBER)
                {
                    Cursor.SetCursor(moveCursor, hotspot, CursorMode.Auto);

                    if(onMouseOverWalkable != null)
                        onMouseOverWalkable(hit);

                    return true;
                }
            }
            else
            {
                Cursor.SetCursor(errorCursor, hotspot, CursorMode.Auto);
            }

            return false;
        }
    }
}
#pragma warning restore 0649