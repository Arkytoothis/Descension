using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public enum BuildingType { Barracks, Chapel, Graveyard, Market, Sage, Smith, Tavern, Town_Hall, Town_Camp, Number, None };

    public class Building : Interactable
    {
        [SerializeField] BuildingType buildingType = BuildingType.None;
        [SerializeField] GameObject uiObject = null;
        [SerializeField] TownGuiManager uiManager = null;

        public void SetUiObject(GameObject uiObject)
        {
            this.uiObject = uiObject;
        }

        public override bool Interact(GameObject other)
        {
            Debug.Log("Interacting with " + buildingType.ToString().Replace('_', ' '));
            return true;
        }

        private void OnMouseOver()
        {
            if (uiObject.activeSelf == false)
            {
                OpenUi();
            }
        }

        private void OnMouseExit()
        {
            CloseUi();
        }

        public void OpenUi()
        {
            if (uiManager.IsScreenOpen == false)
            {
                if (uiObject != null)
                {
                    uiObject.SetActive(true);
                }
            }
        }

        public void CloseUi()
        {
            if (uiManager.IsScreenOpen == false)
            {
                if (uiObject != null)
                {
                    uiObject.SetActive(false);
                }
            }
        }

        public void Clear()
        {
            if (uiObject != null)
            {
                uiObject.SetActive(false);
            }
        }
    }
}