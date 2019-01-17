using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cromos
{
    public class ChangeProperertiesCT : ChangeProperties
    {
        void Awake()
        {
            ca = GetComponent<ColorTransition>();
        }
    }

}
