using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class Selector : MonoBehaviour
    {
        [SerializeField] Color color = Color.magenta;
        [SerializeField] MeshRenderer meshRenderer = null;

        public void Setup(Color color)
        {
            this.color = color;
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.materials[0].color = color;
        }

        public void Show()
        {
            meshRenderer.enabled = true;
        }

        public void Hide()
        {
            meshRenderer.enabled = false;
        }
    }
}