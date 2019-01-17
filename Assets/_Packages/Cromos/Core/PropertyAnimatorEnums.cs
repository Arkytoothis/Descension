/********************************************************************
	created:	2016/04/22
	file base:	NamedMaterialPropertyNames
	file ext:	cs
	author:		Alessandro Maione
	version:	1.3.1
	purpose:	enumeration of common material color properties
*********************************************************************/

namespace Cromos
{
    /// <summary>
    /// behaviour after animation is complete
    /// </summary>
    public enum WrapModes
    {
        Restore,
        Clamp
    }

    public enum TransitionLoopModes
    {
        PlayOnce,
        Repeat,
        PingPong
    }

    public enum PropertyStartupModes
    {
        OnEnable = 0,
        OnStart,
        None
    }

    public enum AfterTransitionActions
    {
        DoNothing = 0,
        DeactivateGameObject,
        DestroyGameObject,
        DisableComponent,
        DestroyComponent
    }
}


