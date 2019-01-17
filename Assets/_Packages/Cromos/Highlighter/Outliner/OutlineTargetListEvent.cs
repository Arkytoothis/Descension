/********************************************************************
	created:	2018/01/30
	file base:	OutlineTargetListEvent
	file ext:	cs
	author:		Alessandro Maione
	version:	1.0.0
	
	purpose:	event used by OutlineTargetList class to notify list changes
*********************************************************************/
using UnityEngine.Events;

namespace Cromos
{
    /// <summary>
    /// event used by OutlineTargetList class to notify list changes
    /// </summary>
    public class OutlineTargetListEvent : UnityEvent<OutlineTarget>
    { }
}