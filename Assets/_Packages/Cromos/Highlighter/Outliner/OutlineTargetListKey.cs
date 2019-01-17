/********************************************************************
	created:	2018/02/02
	file base:	OutlineTargetListKey
	file ext:	cs
	author:		Alessandro Maione
	version:	1.0.1
	
	purpose:	key used for fast indexing outlinetarget objects in outline target list
*********************************************************************/
using System.Collections.Generic;

namespace Cromos
{
    /// <summary>
    /// key used for fast indexing outlinetarget objects in outline target list
    /// </summary>
    public struct OutlineTargetListKey : IEqualityComparer<OutlineTargetListKey>
    {
        public OutlineModes Mode;
        public int Thickness;


        public OutlineTargetListKey( OutlineTarget target, bool useOldMode = false, bool useOldThickness = false )
        {
            Mode = useOldMode ? target.OldMode : target.Mode;
            Thickness = useOldThickness ? target.OldThickness : target.Thickness;
        }

        public OutlineTargetListKey( OutlineModes mode, int thickness )
        {
            Mode = mode;
            Thickness = thickness;
        }


        public bool Equals( OutlineTargetListKey x, OutlineTargetListKey y )
        {
            return ( x.Mode == y.Mode ) && ( x.Thickness == y.Thickness );
        }

        public int GetHashCode( OutlineTargetListKey obj )
        {
            return GetHashCode();
        }
    }

}
