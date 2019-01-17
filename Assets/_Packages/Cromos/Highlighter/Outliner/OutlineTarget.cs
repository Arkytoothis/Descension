/********************************************************************
	created:	2016/11/20
	file base:	OutlineTarget
	file ext:	cs
	author:		Alessandro Maione
	version:	1.8.0
	purpose:	A game object with this component will been "seen" from the outline camera. It contains the color and the thickness of an element that will be outlined target
*********************************************************************/
using UnityEngine;

namespace Cromos
{
    /// <summary>
    /// A game object with this component will been "seen" from the outline camera.
    /// It contains the color and the thickness of an element that will be outlined
    /// </summary>
    public class OutlineTarget : MonoBehaviour
    {
        public const int MinThickness = 0;
        public const int MaxThickness = 20;

        /// <summary>
        /// list of all instantiated outline targets
        /// </summary>
        public static OutlineTargetList All
        {
            get
            {
                return all;
            }
        }
        private static OutlineTargetList all = new OutlineTargetList();
        private static int[] clearFrame = null;

        public struct TransformMesh
        {
            public Renderer renderer;
            public Mesh mesh;
            public bool visible;
        }


        [Header( "Options" )]
        public OutlineModes Mode = OutlineModes.FastSolid;
        /// <summary>
        /// thickness of outline around the object
        /// </summary>
        [Tooltip( "thickness of outline around the object" )]
        [Range( 0, MaxThickness )]
        public int Thickness = 2;
        /// <summary>
        /// color of outline around object
        /// </summary>
        [Tooltip( "color of outline around object" )]
        public Color32 OutlineColor = Color.red;
        public bool AffectChildren = true;
        [Tooltip( "if true, the particle systems will not outlined" )]
        [HideInInspector]
        /// <summary>
        /// if true, the particle systems will not outlined
        /// </summary>
        public bool ExcludeParticleSystems = true;

        public static int ActiveCounter
        {
            get;
            private set;
        }
        public bool NeedsUpdate
        {
            get
            {
                return ( ( Mode != OldMode ) ||
                         ( Thickness != OldThickness ) ||
                         /*( !ColorHash.Color32Equals( OutlineColor, OldOutlineColor ) ) ||*/
                         /*( AffectChildren != OldAffectChildren ) ||*/
                         ( ExcludeParticleSystems != OldExcludeParticleSystems ) ||
                         forceUpdate );
            }
        }
        private bool forceUpdate = true;

        public int OldThickness
        {
            get;
            private set;
        }
        public OutlineModes OldMode
        {
            get;
            private set;
        }
        public Color32 OldOutlineColor
        {
            get;
            private set;
        }
        public bool OldAffectChildren
        {
            get;
            private set;
        }
        public bool OldExcludeParticleSystems
        {
            get;
            private set;
        }

        [HideInInspector]
        public Vector2 MinUV = Vector2.zero;
        [HideInInspector]
        public Vector2 MaxUV = Vector2.zero;
        [HideInInspector]
        public Vector2 UVSize = Vector2.zero;

        public TransformMesh[] Targets = null;

        #region LOD
        private LODGroup lodGroup = null;
        private LOD[] lods = null;
        #endregion



        void Start()
        {
            //myLayerOnStart = gameObject.layer;
            //outlineLayer = LayerMask.NameToLayer( "Outline" );
            lodGroup = GetComponent<LODGroup>();
            if ( lodGroup )
            {
                lods = lodGroup.GetLODs();
                for ( int l = 0; l < lods.Length; l++ )
                {
                    LOD lod = lods[l];
                    for ( int r = 0; r < lod.renderers.Length; r++ )
                    {
                        if ( lod.renderers[r] )
                        {
                            OutlineTargetLODRenderer visEvent = lod.renderers[r].gameObject.AddComponent<OutlineTargetLODRenderer>();
                            visEvent.OnVisibilityChanged.AddListener( OnLODRendererVisibilityChanged );
                            OnLODRendererVisibilityChanged( visEvent, lod.renderers[r].isVisible );
                        }
                    }
                }
            }
        }

        void OnDestroy()
        {
            //ResetLayer();
            All.RemoveFromList( this );
        }

        void OnEnable()
        {
            InitClearFrame();
            ActiveCounter++;
            OldMode = OutlineModesConst.NotSet;
            OldThickness = -1;
            OldOutlineColor = Color.clear;
            DoUpdate();
        }

        void OnDisable()
        {
            ActiveCounter--;
            //ResetLayer();
            All.RemoveFromList( this );
        }


        private Vector3 boundsMin = Vector3.zero;
        private Vector3 boundsMax = Vector3.zero;
        void OnRenderObject()
        {
            if ( Targets.Length == 0 )
                return;

            RenderTexture old = RenderTexture.active;
            RenderTexture.active = OutlinePostEffect.Instance.GetSilhouettesRT( Mode );
            int modeInt = (int)Mode;
            if ( Time.frameCount != clearFrame[modeInt] )
            {
                GL.Clear( false, true, Color.clear );
                clearFrame[modeInt] = Time.frameCount;
            }

            Color c = OutlineInfo.Encode( OutlineColor, Thickness );
            OutlinePostEffect.Instance.SilhouettesMaterial.SetColor( "_Color", c );
            OutlinePostEffect.Instance.SilhouettesMaterial.SetPass( 0 );

            for ( int i = 0; i < Targets.Length; i++ )
            {
                if ( Targets[i].visible )
                {
                    Bounds bounds = Targets[i].renderer.bounds;
                    if ( i == 0 )
                    {
                        boundsMin = bounds.min;
                        boundsMax = bounds.max;
                    }
                    else
                    {
                        boundsMin = Vector3.Min( boundsMin, bounds.min );
                        boundsMax = Vector3.Max( boundsMax, bounds.max );
                    }

                    //Matrix4x4 matrix = Targets[i].renderer.transform.localToWorldMatrix;
                    //matrix.SetTRS( transform.position, transform.rotation, transform.localScale * 2.0f );
                    Graphics.DrawMeshNow( Targets[i].mesh, Targets[i].renderer.transform.localToWorldMatrix/*matrix*/ );
                }
            }

            Bounds totalBounds = new Bounds();
            totalBounds.SetMinMax( boundsMin, boundsMax );
            Vector3 center = totalBounds.center;
            Vector3 ext = totalBounds.extents;
            Vector3 c0 = center + new Vector3( -ext.x, -ext.y, -ext.z );
            Vector3 c1 = center + new Vector3( -ext.x, -ext.y, ext.z );
            Vector3 c2 = center + new Vector3( -ext.x, ext.y, -ext.z );
            Vector3 c3 = center + new Vector3( -ext.x, ext.y, ext.z );
            Vector3 c4 = center + new Vector3( ext.x, -ext.y, -ext.z );
            Vector3 c5 = center + new Vector3( ext.x, -ext.y, ext.z );
            Vector3 c6 = center + new Vector3( ext.x, ext.y, -ext.z );
            Vector3 c7 = center + new Vector3( ext.x, ext.y, ext.z );

            //boundsMin.z = boundsMax.z = 0.5f * ( boundsMin.z + boundsMax.z );
            Vector3 tmp = Camera.main.WorldToViewportPoint( c0 );
            tmp = Vector3.Min( tmp, Camera.main.WorldToViewportPoint( c1 ) );
            tmp = Vector3.Min( tmp, Camera.main.WorldToViewportPoint( c2 ) );
            tmp = Vector3.Min( tmp, Camera.main.WorldToViewportPoint( c3 ) );
            tmp = Vector3.Min( tmp, Camera.main.WorldToViewportPoint( c4 ) );
            tmp = Vector3.Min( tmp, Camera.main.WorldToViewportPoint( c5 ) );
            tmp = Vector3.Min( tmp, Camera.main.WorldToViewportPoint( c6 ) );
            tmp = Vector3.Min( tmp, Camera.main.WorldToViewportPoint( c7 ) );
            MinUV = tmp;

            tmp = Camera.main.WorldToViewportPoint( c0 );
            tmp = Vector3.Max( tmp, Camera.main.WorldToViewportPoint( c1 ) );
            tmp = Vector3.Max( tmp, Camera.main.WorldToViewportPoint( c2 ) );
            tmp = Vector3.Max( tmp, Camera.main.WorldToViewportPoint( c3 ) );
            tmp = Vector3.Max( tmp, Camera.main.WorldToViewportPoint( c4 ) );
            tmp = Vector3.Max( tmp, Camera.main.WorldToViewportPoint( c5 ) );
            tmp = Vector3.Max( tmp, Camera.main.WorldToViewportPoint( c6 ) );
            tmp = Vector3.Max( tmp, Camera.main.WorldToViewportPoint( c7 ) );
            MaxUV = tmp;

            UVSize = MaxUV - MinUV;

            RenderTexture.active = old;
        }

        private void LateUpdate()
        {
            DoUpdate();
        }

        /// <summary>
        /// adds outline target to a game object
        /// </summary>
        /// <param name="go">game object to which new outline target will be added</param>
        /// <param name="hidden">if true, the component will not be visible in editor</param>
        /// <returns>new outline target component</returns>
        public static OutlineTarget Add( GameObject go, bool hidden = false )
        {
            OutlineTarget target = null;

            if ( go == null )
                return target;

            target = go.GetComponent<OutlineTarget>();

            if ( !target )
            {
                target = go.AddComponent<OutlineTarget>();
                if ( hidden )
                    target.hideFlags = HideFlags.HideInInspector;
            }

            target.enabled = true;

            return target;
        }

        /// <summary>
        /// removes an outline target component from an object
        /// </summary>
        /// <param name="go">game object from which the outline target will be removed</param>
        /// <param name="destroy">if true, the component will be destroyed, else it will be just disabled</param>
        public static void Remove( GameObject go, bool destroy = false )
        {
            if ( go != null )
            {
                OutlineTarget target = go.GetComponent<OutlineTarget>();

                if ( target )
                {
                    if ( destroy )
                        UnityEngine.Object.Destroy( target );
                    else
                        target.enabled = false;
                }
            }
        }

        private void DoUpdate()
        {
            UpdateAffectChildren();
            All.UpdateInList( this );
            OldMode = Mode;
            OldThickness = Thickness;
            OldOutlineColor = OutlineColor;
            OldExcludeParticleSystems = ExcludeParticleSystems;
            forceUpdate = false;

            //All.PrintContent();
        }

        private void InitClearFrame()
        {
            if ( clearFrame == null )
            {
                clearFrame = new int[OutlineModesConst.NumberOfOutlineModes];
                for ( int i = 0; i < clearFrame.Length; i++ )
                    clearFrame[i] = -1;
            }
        }

        private void UpdateAffectChildren()
        {
            if ( OldAffectChildren == AffectChildren )
                return;

            if ( AffectChildren )
            {
                MeshFilter[] mfs = GetComponentsInChildren<MeshFilter>();
                if ( mfs != null )
                {
                    Targets = new TransformMesh[mfs.Length];
                    for ( int i = 0; i < mfs.Length; i++ )
                        Targets[i] = new TransformMesh()
                        {
                            mesh = mfs[i].sharedMesh,
                            renderer = mfs[i].GetComponent<Renderer>(),
                            visible = true
                        };
                }
                else
                    Targets = new TransformMesh[0];
            }
            else
            {
                MeshFilter mfs = GetComponentInChildren<MeshFilter>();
                if ( mfs != null )
                {
                    Targets = new TransformMesh[1];
                    Targets[0] = new TransformMesh()
                    {
                        mesh = mfs.sharedMesh,
                        renderer = mfs.GetComponent<Renderer>(),
                        visible = true
                    };
                }
                else
                    Targets = new TransformMesh[0];
            }

            OldAffectChildren = AffectChildren;
        }

        private void OnLODRendererVisibilityChanged( OutlineTargetLODRenderer lODRenderer, bool visibility )
        {
            GameObject lodGO = lODRenderer.gameObject;

            for ( int i = 0; i < Targets.Length; i++ )
            {
                if ( Targets[i].renderer.gameObject == lodGO )
                {
                    Targets[i].visible = visibility;
                    break;
                }
            }

            //forceUpdate = true;
            DoUpdate();
        }

        #region DEBUG
        //#if UNITY_EDITOR
        public override string ToString()
        {
            return name + " - " + base.ToString();
        }

        private void OnDrawGizmos()
        {
            Bounds bounds = new Bounds();
            bounds.SetMinMax( boundsMin, boundsMax );
            Gizmos.DrawWireCube( bounds.center, bounds.size );
        }
        //#endif
        #endregion

        #region UNUSED

        /// <summary>
        /// this property should be used ONLY by OutlineTargetList
        /// </summary>
        //         public int MyHash
        //         {
        //             get;
        //             set;
        //         }

        //private int myLayerOnStart = 0;
        //private int outlineLayer = -1;

        /*
        /// <summary>
        /// sets the game object layer to 'Outline'. The layer of the object is temporary changed to apply outline post effect
        /// </summary>
        public void SetOutlineLayer()
        {
            SetOutlineLayer( AffectChildren );
        }

        /// <summary>
        /// sets the game object layer to 'Outline'. The layer of the object is temporary changed to apply outline post effect
        /// </summary>
        /// <param name="recursive">if true the layer is changed to game object in hierarchy too</param>
        public void SetOutlineLayer( bool recursive )
        {
            SetOutlineLayer( gameObject, recursive );
        }

        /// <summary>
        /// sets the game object layer to 'Outline'. The layer of the object is temporary changed to apply outline post effect
        /// </summary>
        /// <param name="go">target game object</param>
        /// <param name="recursive">if true the layer is changed to game object in hierarchy too</param>
        public void SetOutlineLayer( GameObject go, bool recursive )
        {
            if ( outlineLayer < 0 )
                Debug.LogError( @"in order to use outline effect add a new layer called 'Outline' in editor 'Tags and Layers'" );
            else
                go.layer = outlineLayer;

            if ( recursive )
                foreach ( Transform child in go.transform )
                    SetOutlineLayer( child.gameObject, recursive );
            //SetOutlineLayerLODGroup();
        }

        /// <summary>
        /// resets the game object layer to the original one
        /// </summary>
        public void ResetLayer()
        {
            ResetLayer( AffectChildren );
        }

        /// <summary>
        /// resets the game object layer to the original one
        /// </summary>
        /// <param name="recursive">if true the layer is changed to game object in hierarchy too</param>
        public void ResetLayer( bool recursive )
        {
            ResetLayer( gameObject, recursive );
        }

        /// <summary>
        /// resets the game object layer to the original one
        /// </summary>
        /// <param name="go">target game object</param>
        /// <param name="recursive">if true the layer is changed to game object in hierarchy too</param>
        public void ResetLayer( GameObject go, bool recursive )
        {
            go.layer = myLayerOnStart;

            if ( recursive )
                foreach ( Transform child in go.transform )
                    ResetLayer( child.gameObject, recursive );
        }*/


        //         private void SetOutlineLayerLODGroup()
        //         {
        //             if ( lodGroup )
        //             {
        //                 if ( lods == null )
        //                 {
        //                     lods = lodGroup.GetLODs();
        //                 }
        //                 for ( int l = 0; l < lods.Length; l++ )
        //                 {
        //                     LOD lod = lods[l];
        //                     Renderer rend = null;
        //                     for ( int r = 0; r < lod.renderers.Length; r++ )
        //                     {
        //                         rend = lod.renderers[r];
        //                         //                         if ( !rend.isVisible )
        //                         //                             ResetLayer( rend.gameObject, false );
        //                     }
        //                 }
        //             }
        //         }


        #endregion

    }

}



//TODO: da  Chizhong Jin: How do I avoid the outline over the character. Provare a modificare ZTest in silhouette shader ?? Se si facesse DrawRenderer probabilmente funzionerebbe (??), ma con DrawMesh??

//HIGH: per ottenere la sovrapposizione di vari outline, si potrebbe usare il blend mode alpha|1-alpha
