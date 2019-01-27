using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Core
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public float pitch = 1.2f;
        public float zoomSpeed = 4F;
        public float minZoom = 1f;
        public float maxZoom = 150f;
        public float yawSpeed = 200f;

        private float currentZoom = 10f;
        private float currentYaw = 0f;

        void Update()
        {
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * (zoomSpeed + currentZoom);
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            currentYaw += Input.GetAxis("Horizontal") * yawSpeed * Time.unscaledDeltaTime;

            //if (target != null)
            //{
            //    transform.position = target.position - offset * currentZoom;
            //    transform.LookAt(target.position + Vector3.up * pitch);
            //    transform.RotateAround(target.position, Vector3.up, currentYaw);
            //}
        }

        void LateUpdate()
        {
            if (target != null)
            {
                transform.position = target.position - offset * currentZoom;
                transform.LookAt(target.position + Vector3.up * pitch);
                transform.RotateAround(target.position, Vector3.up, currentYaw);
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
            //transform.SetParent(target, false);
        }


    }
}