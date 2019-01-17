/********************************************************************
	created:	2018/07/19
	file base:	HighlightTypes
	file ext:	cs
	author:		Alessandro Maione
	version:	1.0.0
	
	purpose:	Highlight types. Colors will animate only target color (with alpha). Outline will not animate target colors but it will use colors contained in Color gradient to create an outline colored effect around the target. ColorAndOutline will apply both Colors and Outline effects to the target
*********************************************************************/

namespace Cromos
{
    /// <summary>
    /// Highlight types.
    /// Colors will animate only target color (with alpha)
    /// Outline will not animate target colors but it will use colors contained in Color gradient to create an outline colored effect around the target
    /// ColorAndOutline will apply both Colors and Outline effects to the target
    /// </summary>
    [System.Flags]
    public enum HighlightTypes
    {
        Color = 1,
        Outline = 2,
        ColorsAndOutline = 3
    }
}
