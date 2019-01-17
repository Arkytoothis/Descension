/********************************************************************
	created:	2018/03/20
	file base:	RendererVisibilityEvent
	file ext:	cs
	author:		Alessandro Maione
	version:	1.1.0
	
	purpose:	manages the visibility change of renderers of LOD group managed by OutlineTarget
*********************************************************************/
using UnityEngine.Events;

namespace Cromos
{

    /// <summary>
    /// manages the visibility change of renderers of LOD group managed by OutlineTarget
    /// </summary>
    public class RendererVisibilityEvent : UnityEvent<OutlineTargetLODRenderer, bool>
    { }

}

