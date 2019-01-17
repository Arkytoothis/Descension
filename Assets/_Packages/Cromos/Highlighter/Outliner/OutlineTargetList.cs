/********************************************************************
	created:	2018/01/30
	file base:	OutlineTargetList
	file ext:	cs
	author:		Alessandro Maione
	version:	1.1.0
	
	purpose:	an optimized list of outline  target objects
*********************************************************************/
using System.Collections.Generic;
using UnityEngine;

namespace Cromos
{
    /// <summary>
    /// an optimized list of outline  target objects
    /// </summary>
    public class OutlineTargetList : Dictionary<OutlineTargetListKey, HashSet<OutlineTarget>>
    {
        public OutlineTargetListEvent OnAdd = new OutlineTargetListEvent();
        public OutlineTargetListEvent OnRemove = new OutlineTargetListEvent();
        public OutlineTargetListEvent OnChange = new OutlineTargetListEvent();

        public int[] ModeCounter
        {
            get
            {
                return modeCounter;
            }
        }
        private readonly int[] modeCounter = new int[OutlineModesConst.NumberOfOutlineModes];
        private Dictionary<int, int> thicknessCounter = new Dictionary<int, int>();


        public bool AddToList( OutlineTarget target )
        {
            bool result = false;
            OutlineTargetListKey key = new OutlineTargetListKey( target );
            if ( !ContainsKey( key ) )
                this[key] = new HashSet<OutlineTarget>();

            result = this[key].Add( target );

            if ( result )
            {
                modeCounter[(int)key.Mode]++;
                if ( !thicknessCounter.ContainsKey( key.Thickness ) )
                    thicknessCounter.Add( key.Thickness, 1 );
                else
                    thicknessCounter[key.Thickness]++;
                OutlinePostEffect.Add().enabled = true;
            }

            if ( OnAdd != null )
                OnAdd.Invoke( target );

            return result;
        }

        public bool RemoveFromList( OutlineTarget target, bool oldMode = false, bool oldThickness = false )
        {
            bool result = false;
            OutlineTargetListKey key = new OutlineTargetListKey( target, oldMode, oldThickness );
            HashSet<OutlineTarget> targetSet = null;

            if ( ContainsKey( key ) )
                targetSet = this[key];


            if ( targetSet != null )
            {
                if ( targetSet.Contains( target ) )
                {
                    result = targetSet.Remove( target );

                    if ( targetSet.Count == 0 )
                        Remove( key );
                }
            }

            if ( result )
            {
                int modeInt = (int)key.Mode;
                modeCounter[modeInt]--;

                if ( thicknessCounter.ContainsKey( key.Thickness ) )
                {
                    thicknessCounter[key.Thickness]--;
                    if ( thicknessCounter[key.Thickness] == 0 )
                        thicknessCounter.Remove( key.Thickness );
                }

                if ( OnRemove != null )
                    OnRemove.Invoke( target );

                if ( OutlineTarget.ActiveCounter == 0 )
                    if ( OutlinePostEffect.Instance )
                        OutlinePostEffect.Instance.enabled = false;
            }

            return result;
        }

        public OutlineTarget UpdateInList( OutlineTarget target )
        {
            //             OutlineTargetListKey oldKey = new OutlineTargetListKey( target.OldMode, target.OldThickness );
            //             OutlineTargetListKey newKey = new OutlineTargetListKey( target.Mode, target.Thickness );

            if ( target.NeedsUpdate )
            {
                bool needsGC = RemoveFromList( target, true, true );
                AddToList( target );

                //if ( needsGC )
                OutlinePostEffect.Instance.UpdateResources();

                if ( OnChange != null )
                    OnChange.Invoke( target );
            }

            return target;
        }

        public int CountByMode( OutlineModes mode )
        {
            return modeCounter[(int)mode];
        }

        public int CountByThickness( int thickness )
        {
            if ( thicknessCounter.ContainsKey( thickness ) )
                return thicknessCounter[thickness];

            return 0;
        }

        #region DEBUG
        public void PrintContent()
        {
            foreach ( KeyValuePair<OutlineTargetListKey, HashSet<OutlineTarget>> pair in this )
            {
                Debug.Log( "MODE: " + pair.Key.Mode + " / THICKNESS: " + pair.Key.Thickness );
                foreach ( OutlineTarget ot in pair.Value )
                {
                    Debug.Log( "-- OUTLINE TARGET: " + ot.name );
                }
            }
        }
        #endregion

        #region UNUSED

        /*
        public OutlineTargetList()
        {
            ActiveModes = 0;
            ActiveThickness = 0;
//             foreach ( OutlineModes mode in Enum.GetValues( typeof( OutlineModes ) ) )
//                 this[mode] = new Dictionary<int, HashSet<OutlineTarget>>();
        }*/

        /*
                public static int EncodeOutlineTargetHash( OutlineTarget target )
                {
                    int thicknessRange = ( OutlineTarget.MaxThickness - OutlineTarget.MinThickness );
                    return ( (int)target.Mode * thicknessRange ) + target.OutlineThickness;
                }*/

        /*
                public static void DecodeOutlineTargetHash( int hash, out OutlineModes mode, out int thickness )
                {
                    //SBAGLIATA
                    int thicknessRange = ( OutlineTarget.MaxThickness - OutlineTarget.MinThickness );
                    mode = (OutlineModes)( hash / ( thicknessRange + 1 ) );
                    thickness = hash - ( (int)mode * thicknessRange );
                }*/

        #endregion

    }

}
