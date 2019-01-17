/********************************************************************
	created:	2016/12/29
	file base:	ClickToFullscreenFade
	file ext:	cs
	author:		Alessandro Maione
	version:	1.0.0
	
	purpose:	sample component to test Dissolve Sphere capabilities
*********************************************************************/
using Cromos;
using UnityEngine;

/// <summary>
/// sample component to test Dissolve Sphere capabilities
/// </summary>
public class ClickToFullscreenFade : MonoBehaviour
{
    public Color DissolveColor = Color.black;

    public void Fade()
    {
        FullscreenFade.BetweenLevels( DissolveColor );
    }

}
