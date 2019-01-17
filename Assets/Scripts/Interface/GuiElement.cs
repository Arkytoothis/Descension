using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Gui
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

        //public virtual void Update()
        //{
        //    if (Application.isEditor == true)
        //    {
        //        OnSkinGui();
        //    }
        //}
    }
}