/********************************************************************
	created:	2018/01/09
	file base:	OutlineModes
	file ext:	cs
	author:		Alessandro Maione
	version:	1.1.1
	
	purpose:	Outline modes for Cromos outline system
*********************************************************************/
namespace Cromos
{
    /// <summary>
    /// Outline modes for Cromos outline system
    /// </summary>
    public enum OutlineModes : int
    {
        FastSolid = 0,/*= 32,*/
        FastGlow, /*= 64,*/
        AccurateSolid, /*= 128,*/
        AccurateGlow/*= 192*/
    }

    public static class OutlineModesConst
    {
        public const int NumberOfOutlineModes = 4;
        public const OutlineModes NotSet = (OutlineModes)( -1 );
    }

}
