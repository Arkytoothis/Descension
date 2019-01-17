/********************************************************************
	created:	2018/11/29
	file base:	ColorAnimatorEnums
	file ext:	cs
	author:		Alessandro Maione
	version:	1.0.0
	
	purpose:	Transition Targets and Color Modes used by Color Animator
*********************************************************************/
namespace Cromos
{
    /// <summary>
    /// 
    /// </summary>
    public enum TransitionTargets
    {
        Color = 0,
        Alpha,
        ColorAndAlpha
    }

    public enum ColorModes
    {
        Replace = 0,
        Multiply,
        Add,
        AddHDR,
        Subtract,
        Invert,
        And,
        Or,
        Xor,
        IfDarker,
        IfLighter
    }

}
