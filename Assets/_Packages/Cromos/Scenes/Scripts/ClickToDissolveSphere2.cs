/********************************************************************
	created:	2016/12/29
	file base:	ClickToDissolveSphere2
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
public class ClickToDissolveSphere2 : MonoBehaviour
{

    public Gradient MyGradientColors;

    void OnMouseUp()
    {
        FullscreenFade dissolve = FullscreenFade.DoFade( null, MyGradientColors.colorKeys, MyGradientColors.alphaKeys );
        dissolve.gameObject.AddComponent<FlipMesh>();
    }

}
