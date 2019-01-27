using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Descension
{
    [System.Serializable]
    public class CharacterPortrait : MonoBehaviour
    {
        [SerializeField] Transform pivot = null;
        [SerializeField] Light spotLight = null;
        [SerializeField] Light pointLightLeft = null;
        [SerializeField] Light pointLightRight = null;

        [SerializeField] Camera closeCamera = null;
        [SerializeField] Camera farCamera = null;
        [SerializeField] RenderTexture rtClose = null;
        [SerializeField] RenderTexture rtFar = null;

        public Transform Pivot { get { return pivot; } }
        public Camera CloseCamera { get { return closeCamera; } }
        public RenderTexture RtClose { get { return rtClose; } }
        public Camera FarCamera { get { return farCamera; } }
        public RenderTexture RtFar { get { return rtFar; } }

        [SerializeField] int index = -1;

        public void Setup(int index)
        {
            pivot.name = "Pivot";
            spotLight.name = "Spotlight";
            pointLightLeft.name = "Point Light Left";
            pointLightRight.name = "Point Light Right";

            this.index = index;
            rtClose = new RenderTexture(128, 128, 32);
            closeCamera.targetTexture = rtClose;

            if (FarCamera != null)
            {
                rtFar = new RenderTexture(250, 375, 32);
                farCamera.targetTexture = rtFar;
                DisableFarCamera();
            }
        }

        public void EnableFarCamera()
        {
            farCamera.enabled = true;
        }

        public void DisableFarCamera()
        {
            farCamera.enabled = false;
        }
    }
}