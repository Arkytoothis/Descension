/********************************************************************
	created:	2016/12/29
	file base:	ClickMeCube
	file ext:	cs
	author:		Alessandro Maione
	version:	1.0.0
	
	purpose:	sample component to test Color Transition capabilities
*********************************************************************/
using Cromos;
using UnityEngine;

/// <summary>
/// sample component to test Color Transition capabilities
/// </summary>
public class ClickMeCube : MonoBehaviour
{

    private ColorTransition colorTransition;

    void OnMouseEnter()
    {
        colorTransition = ColorTransition.DoTransition( gameObject, new GradientColorKey[] { new GradientColorKey( Color.yellow, 0 ), new GradientColorKey( Color.white, 1 ) } );
        colorTransition.Duration = 0.5f;
        colorTransition.LoopMode = TransitionLoopModes.PlayOnce;
    }

    void OnMouseExit()
    {
        colorTransition = ColorTransition.DoTransition( gameObject, new GradientColorKey[] { new GradientColorKey( Color.white, 0 ), new GradientColorKey( Color.yellow, 1 ) } );
        colorTransition.Duration = 0.5f;
        colorTransition.LoopMode = TransitionLoopModes.PlayOnce;
    }

    void OnMouseUp()
    {
        colorTransition = ColorTransition.DoTransition( gameObject, new GradientColorKey[] { new GradientColorKey( Color.blue, 0 ), new GradientColorKey( Color.black, 0.5f ), new GradientColorKey( Color.green, 1 ) } );
        colorTransition.Duration = 1;
        colorTransition.LoopMode = TransitionLoopModes.PlayOnce;
    }

}
