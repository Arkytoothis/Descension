using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public abstract class TownScreen : MonoBehaviour
    {
        [SerializeField] TownGuiManager uiManager = null;
        [SerializeField] CameraRaycaster raycaster = null;

        protected RectTransform rect;
        protected bool isOpen;
        public float startX;

        public virtual void Initialize()
        {
            uiManager = GetComponentInParent<TownGuiManager>();
            raycaster = Camera.main.gameObject.GetComponent<CameraRaycaster>();

            rect = GetComponent<RectTransform>();
            startX = rect.localPosition.x;
        }

        public virtual void Open()
        {
            isOpen = true;
            rect.localPosition = new Vector3(0, rect.localPosition.y, rect.localPosition.z);
        }

        public virtual void Close()
        {
            isOpen = false;
            rect.localPosition = new Vector3(startX, rect.localPosition.y, rect.localPosition.z);
        }

        public virtual void Toggle()
        {
            if (isOpen == true)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (isOpen == true)
                {
                    uiManager.CloseScreens();
                    Close();
                }
            }
        }
    }
}