using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Descension
{
    public class TownGuiManager : MonoBehaviour
    {
        [SerializeField] Camera mainCamera;
        [SerializeField] CameraRaycaster raycaster;

        [SerializeField] List<GameObject> buildingPanels = new List<GameObject>();
        [SerializeField] List<TownScreen> screens = new List<TownScreen>();

        [SerializeField] GameObject buildingPanelPrefab = null;
        [SerializeField] Transform buildingPanelsParent = null;

        [SerializeField] bool isScreenOpen = false;
        public bool IsScreenOpen { get { return isScreenOpen; } } 

        public void Initialize(List<GameObject> buildingObjects)
        {
            mainCamera = Camera.main;
            raycaster = Camera.main.gameObject.GetComponent<CameraRaycaster>();
            raycaster.SetMode(CameraRaycaster.Mode.Town);

            for (int i = 0; i < buildingObjects.Count; i++)
            {
                GameObject panelObject = Instantiate(buildingPanelPrefab, buildingPanelsParent);
                panelObject.SetActive(false);
                Vector3 screenPosition = mainCamera.WorldToScreenPoint(buildingObjects[i].transform.position);
                screenPosition.y += 60f;
                panelObject.transform.position = screenPosition;

                BuildingPanel panelScript = panelObject.GetComponent<BuildingPanel>();
                int index = i;
                panelScript.Initialize(((BuildingType)i).ToString().Replace('_', ' '), "Level 1", delegate { OpenScreen(index); });

                buildingPanels.Add(panelObject);

                buildingObjects[i].GetComponent<Building>().SetUiObject(panelObject);
            }

            if (screens == null)
            {
                return;
            }

            for (int i = 0; i < screens.Count; i++)
            {
                screens[i].Initialize();
                screens[i].Close();
            }
        }

        public void OpenScreen(int index)
        {
            isScreenOpen = true;
            for (int i = 0; i < screens.Count; i++)
            {
                if (i == index)
                {
                    screens[i].Toggle();
                }
                else
                {
                    screens[i].Close();
                }
            }

            CloseBuildingPanels();
        }

        public void CloseScreens()
        {
            isScreenOpen = false;

            for (int i = 0; i < screens.Count; i++)
                screens[i].Close();
        }

        public void CloseBuildingPanels()
        {
            for (int i = 0; i < buildingPanels.Count; i++)
                buildingPanels[i].SetActive(false);
        }
    }
}