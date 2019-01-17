/********************************************************************
	created:	2016/12/29
	file base:	HighlighterController
	file ext:	cs
	author:		Alessandro Maione
	version:	1.0.1
	
	purpose:	sample component to test Highlighter capabilities. It will intercept common mouse events and calls highlight and de-hilight from Highlighter component
*********************************************************************/

using Cromos;
using UnityEngine;

/// <summary>
/// sample component to test Highlighter capabilities.
/// It will intercept common mouse events and calls highlight and de-hilight from Highlighter component
/// </summary>
[RequireComponent( typeof( Highlighter ) )]
public class HighlighterController : MonoBehaviour
{
    public Highlighter MyHighlighter;

    void Awake()
    {
        if ( !MyHighlighter ) MyHighlighter = GetComponent<Highlighter>();
    }

    void OnMouseEnter()
    {
        MyHighlighter.Highlight();
    }

    void OnMouseExit()
    {
        MyHighlighter.DeHighlight();
    }

    void OnMouseUp()
    {
    }

}
