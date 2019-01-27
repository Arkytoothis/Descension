using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    [ExecuteInEditMode()]
    public class GuiElement : MonoBehaviour
    {

        protected virtual void OnSkinGui()
        {
        }

        public virtual void Awake()
        {
            OnSkinGui();
        }

        public virtual void Update()
        {
            if (Application.isEditor == true)
            {
                OnSkinGui();
            }
        }
    }
}