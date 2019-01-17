using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        public float radius = 1f;
        public bool hasInteracted = false;
        public bool locked = false;

        [SerializeField] Transform interactionTransform = null;
        protected GameObject other;

        public Transform InteractionTransform { get { return interactionTransform; } }

        public abstract bool Interact(GameObject other);
        //public abstract void MouseEnter();
        //public abstract void MouseExit();
    }
}