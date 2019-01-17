/********************************************************************
	created:	2015/06/24
	file base:	DontDestroyOnLevelLoaded
	file ext:	cs
	author:		Alessandro Maione
	version:	1.0.2
	
	purpose:	prevents the destroy of the currennt game object when loading a new level
*********************************************************************/
using UnityEngine;

namespace Cromos
{

    /// <summary>
    /// prevents the destroy of the currennt game object when loading a new level
    /// </summary>
    public class DontDestroyOnLevelLoaded : MonoBehaviour
    {
        protected void Awake()
        {
            DontDestroyOnLoad( gameObject );
        }

    }

}
