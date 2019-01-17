/********************************************************************
	created:	2016/11/20
	file base:	OutlinePostEffect
	file ext:	cs
	author:		Alessandro Maione
	version:	1.8.0
	purpose:	Outline post effect
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;

namespace Cromos
{
    /// <summary>
    /// Outline post effect
    /// </summary>
    public class OutlinePostEffect : MonoBehaviour
    {
        /// <summary>
        /// list of outline post effect shader passes
        /// </summary>
        //         private enum OutlineShaderPasses : int
        //         {
        //             HorizontalAccurateSolid = 0,
        //             VerticalAndBlendAccurateSolid,
        //             HorizontalAccurateGlow,
        //             VerticalAndBlendAccurateGlow,
        //             FastSolid,
        //             FastGlow,
        //             Flip
        //         }

        /// <summary>
        /// layer name for outline effect
        /// </summary>
        private const string OutlineLayerName = "Outline";

        /// <summary>
        /// outline post effect component uses singleton pattern. This property contains the only instance of this component
        /// </summary>
        public static OutlinePostEffect Instance
        {
            get;
            private set;
        }

        [Header( "Debug info" )]
        /// <summary>
        /// if true, the outline post effect is supported
        /// </summary>
        public bool IsSupported = true;
        /// <summary>
        /// true if device supports HDR textures
        /// </summary>
        public bool SupportHDRTextures = true;
        /// <summary>
        /// true if device supports DirectX capabilities
        /// </summary>
        public bool SupportDX11 = false;

        #region SILHOUETTES
        private readonly RenderTexture[] silhouettesRT = new RenderTexture[OutlineModesConst.NumberOfOutlineModes];
        //         public Shader SilhouettesShader
        //         {
        //             get
        //             {
        //                 if ( silhouettesShader == null )
        //                     InitShadersAndMaterials();
        // 
        //                 return silhouettesShader;
        //             }
        //         }
        public Material SilhouettesMaterial
        {
            get
            {
                if ( silhouettesMaterial == null )
                    InitSilhouetteShaderAndMaterial();

                return silhouettesMaterial;
            }
        }
        private Shader silhouettesShader;
        private Material silhouettesMaterial;
        #endregion

        #region OUTLINE
        //         public Shader PostOutlineShader
        //         {
        //             get
        //             {
        //                 if ( postOutlineShader == null )
        //                     InitShadersAndMaterials();
        // 
        //                 return postOutlineShader;
        //             }
        //         }
        //         public Material OutlineMat
        //         {
        //             get
        //             {
        //                 if ( outlineMaterial == null )
        //                     InitShadersAndMaterials();
        // 
        //                 return outlineMaterial;
        //             }
        //         }
        private Shader[] outlineShaders = new Shader[OutlineModesConst.NumberOfOutlineModes];
        private Material[] outlineMaterials = new Material[OutlineModesConst.NumberOfOutlineModes];
        #endregion

        private Camera myCamera = null;
        private bool materialsInitialized = false;

        /// <summary>
        /// adds outline post effect on a game object with camera component
        /// </summary>
        /// <param name="camera">an existing camera</param>
        /// <returns>a new outline post effect component if none was created before, the previous instantiated else</returns>
        public static OutlinePostEffect Add( Camera camera = null )
        {
            OutlinePostEffect result = null;

            if ( Instance )
                return Instance;

            if ( camera == null )
                camera = Camera.main;

            if ( !camera )
            {
                Debug.LogError( "no main camera set in scene", Instance );
                return result;
            }

            Instance = camera.gameObject.AddComponent<OutlinePostEffect>();
            Instance.myCamera = camera;

            return Instance;
        }

        /// <summary>
        /// removes the outline post effect instance (if any)
        /// </summary>
        public static void Remove()
        {
            if ( Instance )
            {
                Destroy( Instance );
                Instance = null;
            }
        }

        void Start()
        {
            //attachedCamera = GetComponent<Camera>();
            /*GameObject cameraGo = new GameObject( "Outline Camera" )
            {
                hideFlags = HideFlags.HideInHierarchy
            };
            OutlineCamera = cameraGo.AddComponent<Camera>();
            OutlineCamera.enabled = false;*/

            IsSupported = CheckSupport();

            //UpdateCommandBuffers();
        }

        //HIGH: nella scena Outline Modes, anche se cancello tutti i cubi, il numero di Render Texture rimane eccessivo (dovrebbe essere 3 senza alcun effetto di post)


        /// <summary>
        /// all the outline magics happens here. Just don't touch this method please
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        void OnRenderImage( RenderTexture source, RenderTexture destination )
        {
            if ( ( !IsSupported ) || ( OutlineTarget.All.Count == 0 ) )
            {
                Graphics.Blit( source, destination );
                return;
            }

            //             try
            //             {
            if ( !materialsInitialized )
                InitSilhouetteShaderAndMaterial();

            //attachedCamera.cullingMask = originalCullinkgMask;

            // DEBUG: uncomment to see silhouette texture from command buffer
            //RenderTexture silhouettesTex = Shader.GetGlobalTexture( CommandBuffers.GetCommandBufferItem( OutlineModes.FastSolid ).RenderTextureID ) as RenderTexture;
            //#if UNITY_EDITOR
            if ( debugView )
            {
                Graphics.Blit( GetSilhouettesRT( OutlineModes.FastSolid ), destination );
                return;
            }
            //#endif

            if ( OutlineTarget.All != null )
            {
                if ( OutlineTarget.All.Count > 0 )
                {
                    int numberOfSteps = OutlineTarget.All.Count;
                    RenderTexture src = source;
                    RenderTexture dst = destination;
                    RenderTexture tmpRT = null;
                    bool multiBlit = numberOfSteps > 1;
                    if ( multiBlit )
                        tmpRT = RenderTexture.GetTemporary( source.width, source.height, RenderTextureSettings.Depth, RenderTextureSettings.Format, RenderTextureReadWrite.Default, 1 );

                    float deltaX = (float)OutlineTarget.MaxThickness / (float)myCamera.pixelWidth;
                    float deltaY = (float)OutlineTarget.MaxThickness / (float)myCamera.pixelHeight;

                    int counter = 0;
                    foreach ( KeyValuePair<OutlineTargetListKey, HashSet<OutlineTarget>> pair in OutlineTarget.All )
                    {
                        OutlineModes mode = pair.Key.Mode;
                        int thickness = pair.Key.Thickness;
                        HashSet<OutlineTarget> targets = pair.Value;

                        if ( multiBlit )
                            UpdateMultiBlitSrcDest( numberOfSteps, counter, source, destination, ref src, ref dst, tmpRT );

                        Vector2 minUV = Vector2.positiveInfinity;
                        Vector2 maxUV = Vector2.negativeInfinity;

                        foreach ( OutlineTarget ot in targets )
                        {
                            minUV.x = Mathf.Min( minUV.x, ot.MinUV.x );
                            minUV.y = Mathf.Min( minUV.y, ot.MinUV.y );

                            maxUV.x = Mathf.Max( maxUV.x, ot.MaxUV.x );
                            maxUV.y = Mathf.Max( maxUV.y, ot.MaxUV.y );
                        }
                        minUV.x -= deltaX;
                        minUV.y -= deltaY;
                        maxUV.x += deltaX;
                        maxUV.y += deltaY;

#if UNITY_EDITOR
                        UVs[(int)mode].min = minUV;
                        UVs[(int)mode].max = maxUV;
#endif

                        ApplyOutline( src, dst, mode, thickness, minUV, maxUV );
                        counter++;
                    }

                    if ( tmpRT )
                        RenderTexture.ReleaseTemporary( tmpRT );

                    return;
                }
            }
            //             }
            //             catch ( System.Exception ex )
            //             {
            //                 Debug.LogException( ex, this );
            //             }

            Graphics.Blit( source, destination );
        }

        public RenderTexture GetSilhouettesRT( OutlineModes mode )
        {
            int idx = (int)mode;

            if ( silhouettesRT[idx] )
            {
                if ( ( silhouettesRT[idx].width != myCamera.pixelWidth ) || ( silhouettesRT[idx].height != myCamera.pixelHeight ) )
                {
                    ReleaseSilhouettesRT( mode );
                    silhouettesRT[idx] = null;
                }
            }

            if ( !silhouettesRT[idx] )
            {
                RenderTexture rt = new RenderTexture( myCamera.pixelWidth, myCamera.pixelHeight, RenderTextureSettings.Depth, RenderTextureSettings.Format, RenderTextureReadWrite.Default );
                rt.useMipMap = false;
                rt.antiAliasing = 1;
                rt.Create();
                silhouettesRT[idx] = rt;
            }

            return silhouettesRT[idx];
        }

        public void UpdateResources()
        {
            OutlineTargetList list = OutlineTarget.All;
            for ( int i = 0; i < OutlineModesConst.NumberOfOutlineModes; i++ )
            {
                if ( list.ModeCounter[i] == 0 )
                {
                    ReleaseSilhouettesRT( i );
                    ReleaseOutlineShaderAndMaterial( i );
                }
                else
                {
                    CreateOutlineShaderAndMaterial( i );
                }
            }
        }

        public void ReleaseSilhouettesRT( OutlineModes mode )
        {
            ReleaseSilhouettesRT( (int)mode );
        }

        private void ReleaseSilhouettesRT( int mode )
        {
            if ( silhouettesRT[mode] )
            {
                silhouettesRT[mode].Release();
                Destroy( silhouettesRT[mode] );
                silhouettesRT[mode] = null;
            }
        }

        private void CreateOutlineShaderAndMaterial( OutlineModes mode )
        {
            CreateOutlineShaderAndMaterial( (int)mode );
        }

        private void CreateOutlineShaderAndMaterial( int mode )
        {
            if ( outlineShaders[mode] == null )
            {
                switch ( mode )
                {
                    case (int)OutlineModes.FastSolid:
                        outlineShaders[mode] = Shader.Find( "Outliner/Outline Fast Solid" );
                        break;
                    case (int)OutlineModes.FastGlow:
                        outlineShaders[mode] = Shader.Find( "Outliner/Outline Fast Glow" );
                        break;
                    case (int)OutlineModes.AccurateSolid:
                        outlineShaders[mode] = Shader.Find( "Outliner/Outline Accurate Solid" );
                        break;
                    case (int)OutlineModes.AccurateGlow:
                        outlineShaders[mode] = Shader.Find( "Outliner/Outline Accurate Glow" );
                        break;
                }

                Debug.Log( "shader " + outlineShaders[mode], this );
            }

            if ( outlineMaterials[mode] == null )
                outlineMaterials[mode] = new Material( outlineShaders[mode] );

            Debug.Log( "material " + outlineMaterials[mode], this );
        }

        private void ReleaseOutlineShaderAndMaterial( OutlineModes mode )
        {
            ReleaseOutlineShaderAndMaterial( (int)mode );
        }

        private void ReleaseOutlineShaderAndMaterial( int mode )
        {
            //             if ( outlineShaders[mode] )
            //             {
            //                 Destroy( outlineShaders[mode] );
            //                 outlineShaders[mode] = null;
            //             }

            if ( outlineMaterials[mode] )
            {
                Destroy( outlineMaterials[mode] );
                outlineMaterials[mode] = null;
            }
        }

        private static void UpdateMultiBlitSrcDest( int numberOfSteps, int counter, RenderTexture source, RenderTexture destination, ref RenderTexture src, ref RenderTexture dst, RenderTexture tmpRT )
        {
            if ( counter == 0 )
            {
                src = source;
                dst = tmpRT;
            }
            else
            {
                if ( counter == ( numberOfSteps - 1 ) )
                {
                    src = dst;
                    dst = destination;
                }
                else
                {
                    if ( counter % 2 == 0 )
                    {
                        src = dst;
                        dst = tmpRT;
                    }
                    else
                    {
                        src = dst;
                        dst = source;
                    }
                }
            }
        }

        private void ApplyOutline( RenderTexture source, RenderTexture destination, OutlineModes mode, int thickness, Vector2 minUV, Vector2 maxUV )
        {
            // RenderTexture silhouettesTex = Shader.GetGlobalTexture( silhouetteRTID ) as RenderTexture;
            // RenderTexture silhouettesTex = Shader.GetGlobalTexture( silhouetteRTID ) as RenderTexture;
            // Shader.GetGlobalTexture( CommandBuffers.GetCommandBufferItem( /*OutlineModes.FastSolid*/mode ).RenderTextureID ) as RenderTexture;
            RenderTexture silhouettesTex = GetSilhouettesRT( mode );

            if ( !silhouettesTex )
                return;

            int modeInt = (int)mode;

            outlineMaterials[modeInt].SetInt( "_Thickness", thickness );
            outlineMaterials[modeInt].SetVector( "_MinUV", minUV );
            outlineMaterials[modeInt].SetVector( "_MaxUV", maxUV );

            switch ( mode )
            {
                case OutlineModes.FastSolid:
                    // [_Maintexture]: source image
                    // [tempRT1]: silhouettes
                    outlineMaterials[modeInt].SetTexture( "_SecondTex", silhouettesTex );
                    Graphics.Blit( source, destination, outlineMaterials[modeInt], 0 /*(int)OutlineShaderPasses.FastSolid*/ );
                    break;

                case OutlineModes.FastGlow:
                    // [_Maintexture]: source image
                    // [tempRT1]: silhouettes
                    outlineMaterials[modeInt].SetTexture( "_SecondTex", silhouettesTex );
                    Graphics.Blit( source, destination, outlineMaterials[modeInt], 0/*(int)OutlineShaderPasses.FastGlow*/ );
                    break;

                case OutlineModes.AccurateSolid:
                    RenderTexture tmpAS = RenderTexture.GetTemporary( source.width, source.height, RenderTextureSettings.Depth, RenderTextureSettings.Format, RenderTextureReadWrite.Default, 1 );

                    // PASS 0 of OutlineMat shader
                    // [SOURCE] tempRT1: white silhouettes (_Maintexture)
                    // [DESTINATION] tempRT2: horizontal blur
                    Graphics.Blit( silhouettesTex, tmpAS, outlineMaterials[modeInt], 0/*(int)OutlineShaderPasses.HorizontalAccurateSolid*/ );
                    //Graphics.Blit( tmpAS, destination );

                    // PASS 1 of OutlineMat shader
                    // [SOURCE] source: original complete image (_MainTexture)
                    // [OutlineMat._SecondTexture] tempRT2: horizontal blurred image from first pass (_SecondTexture)
                    // [DESTINATION] destination
                    outlineMaterials[modeInt].SetTexture( "_SecondTex", tmpAS );
                    Graphics.Blit( source, destination, outlineMaterials[modeInt], 1/*(int)OutlineShaderPasses.VerticalAndBlendAccurateSolid*/ );

                    RenderTexture.ReleaseTemporary( tmpAS );
                    break;

                case OutlineModes.AccurateGlow:
                    RenderTexture tmpAG = RenderTexture.GetTemporary( source.width, source.height, RenderTextureSettings.Depth, RenderTextureSettings.Format, RenderTextureReadWrite.Default, 1 );

                    // PASS 0 of OutlineMat shader
                    // [SOURCE] tempRT1: white silhouettes (_Maintexture)
                    // [DESTINATION] tempRT2: horizontal blur
                    Graphics.Blit( silhouettesTex, tmpAG, outlineMaterials[modeInt], 0/*(int)OutlineShaderPasses.HorizontalAccurateGlow*/ );
                    //Graphics.Blit( tmpAG, destination );
                    //return;

                    // PASS 1 of OutlineMat shader
                    // [SOURCE] source: original complete image (_MainTexture)
                    // [OutlineMat._SecondTexture] tempRT2: horizontal blurred image from first pass (_SecondTexture)
                    // [DESTINATION] destination
                    outlineMaterials[modeInt].SetTexture( "_SecondTex", tmpAG );
                    Graphics.Blit( source, destination, outlineMaterials[modeInt], 1/*(int)OutlineShaderPasses.VerticalAndBlendAccurateGlow*/ );
                    //HIGH HIGH HIGH TODO: Tiled GPU perf. warning: RenderTexture color surface (1269x605) was not cleared/discarded. See TiledGPUPerformanceWarning.ColorSurface label in Profiler for info
                    // UnityEngine.Graphics:Blit( Texture, RenderTexture, Material, Int32 )
                    // Cromos.OutlinePostEffect:ApplyOutline( RenderTexture, RenderTexture, OutlineModes, Int32, Vector2, Vector2 )( at Assets / Cromos / Highlighter / Outliner / OutlinePostEffect.cs:477 )
                    // Cromos.OutlinePostEffect:OnRenderImage( RenderTexture, RenderTexture )( at Assets / Cromos / Highlighter / Outliner / OutlinePostEffect.cs:251 ) 

                    RenderTexture.ReleaseTemporary( tmpAG );
                    break;

                default:
                    Debug.LogError( "unknown outline mode", Instance );
                    //Debug.LogError( "unknown outline mode" );
                    break;
            }
        }

        private void InitSilhouetteShaderAndMaterial()
        {
            if ( !silhouettesShader )
                silhouettesShader = Shader.Find( "Outliner/Draw Silhouettes" );

            if ( !silhouettesMaterial )
                if ( silhouettesShader )
                    silhouettesMaterial = new Material( silhouettesShader );

            //             if ( !outlineMaterials )
            //                 if ( outlineShaders )
            //                     outlineMaterials = new Material( outlineShaders );
            // 
            //             if (!outlineShaders)
            //                 outlineShaders = Shader.Find("Outliner/Outline Fast Solid");

            materialsInitialized = true;
        }

        private bool CheckSupport()
        {
            return CheckSupport( false );
        }

        private bool CheckSupport( bool needDepth )
        {
            IsSupported = true;
            SupportHDRTextures = SystemInfo.SupportsRenderTextureFormat( RenderTextureSettings.Format );
            SupportDX11 = SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders;
            GraphicsDeviceType gdt = SystemInfo.graphicsDeviceType;
            //             flipOnBlit = (
            // #pragma warning disable CS0618 // Il tipo o il membro è obsoleto
            //                     ( gdt == GraphicsDeviceType.Direct3D9 ) ||
            // #pragma warning restore CS0618 // Il tipo o il membro è obsoleto
            //                     ( gdt == GraphicsDeviceType.Direct3D11 ) ||
            //                     ( gdt == GraphicsDeviceType.Direct3D12 ) ||
            // #if !UNITY_5_5_OR_NEWER
            //                     ( gdt == GraphicsDeviceType.Xbox360 ) ||
            // #endif
            //                     ( gdt == GraphicsDeviceType.XboxOneD3D12 ) ||
            //                     ( gdt == GraphicsDeviceType.XboxOne ) );
            // 
            //             if ( Application.platform == RuntimePlatform.WebGLPlayer )
            //                 flipOnBlit = false;
            // 
            //             if ( XRDevice.model == "Vive. MV" ) flipOnBlit = false;
            //             

            #region DEBUG
#if UNITY_EDITOR    
            StringBuilder sb = new StringBuilder();
            sb.AppendLine( "SUPPORTED RENDER TEXTURE FORMATS" );
            foreach ( RenderTextureFormat format in Enum.GetValues( typeof( RenderTextureFormat ) ) )
            {
                sb.AppendLine( format.ToString() + " supported: " + SystemInfo.SupportsRenderTextureFormat( format ) );
            }
            Debug.Log( sb.ToString(), this );
#endif
            #endregion

#if UNITY_5_5_OR_NEWER
            if ( !SystemInfo.supportsImageEffects )
#else
            if ( !SystemInfo.supportsImageEffects || !SystemInfo.supportsRenderTextures )
#endif
            {
                NotSupported();
                return false;
            }

            if ( needDepth && !SystemInfo.SupportsRenderTextureFormat( RenderTextureFormat.Depth ) )
            {
                NotSupported();
                return false;
            }

            if ( needDepth )
                GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;

            return true;
        }

        private void NotSupported()
        {
            enabled = false;
            IsSupported = false;
            throw new System.Exception( "image effects not supported" );
        }

        #region DEBUG

        bool debugView = false;

#if UNITY_EDITOR
        public Rect[] UVs = new Rect[OutlineModesConst.NumberOfOutlineModes];

        void Update()
        {
            if ( Input.GetKeyUp( KeyCode.F12 ) )
                debugView = !debugView;
        }
#endif

        #endregion

        #region UNUSED

        /// <summary>
        /// gets and sets the outline color to be used during post effect passes.
        /// It varies depending from the number and color of outline targets
        /// </summary>
        //         public Color32 OultineColor
        //         {
        //             get
        //             {
        //                 if ( outlineMat )
        //                     return outlineMat.GetColor( "_Color" );
        // 
        //                 return Color.clear;
        // 
        //             }
        //             set
        //             {
        //                 if ( outlineMat )
        //                     outlineMat.SetColor( "_Color", value );
        //             }
        //         }
        /// <summary>
        /// gets and sets the outline thickness to be used during post effect passes.
        /// It varies depending from the number and thickness of outline targets
        /// </summary>
        //         public int OutlineThickness
        //         {
        //             get
        //             {
        //                 if ( outlineMat )
        //                     return Mathf.RoundToInt( outlineMat.GetFloat( "_Thickness" ) );
        // 
        //                 return 0;
        //             }
        //             set
        //             {
        //                 if ( outlineMat )
        //                     outlineMat.SetFloat( "_Thickness", value );
        //             }
        //         }
        //         public OutlineModes OutlineMode
        //         {
        //             get;
        //             private set;
        //         }
        //         

        //private Camera attachedCamera;
        //private int originalCullinkgMask = 0;
        //private Camera OutlineCamera;
        //private RenderTexture tempRT1 = null;
        //private RenderTexture tempRT2 = null;
        /*        private bool flipOnBlit = false;*/


        //private int outlineLayer = 0;

        //#region COMMAND BUFFER
        //private CommandBufferGroup CommandBuffers;
        //private bool needsCommandBufferUpdate = false;
        //#endregion

        /*void Awake()
{
    //             outlineLayer = LayerMask.NameToLayer( OutlineLayerName );
    //             if ( outlineLayer < 0 )
    //             {
    //                 Debug.LogError( "in order to use outline features a layer named " + OutlineLayerName + " is needed. Please add it through Tag and Layer manager in Unity Editor" );
    //                 return;
    //             }

    //            InitShadersAndMaterials();

    OutlineTarget.All.OnAdd.AddListener( OnOutlineTargetListChanged );
    OutlineTarget.All.OnRemove.AddListener( OnOutlineTargetListChanged );
    //OutlineTarget.All.OnChange.AddListener( OnOutlineTargetListChanged );
}*/

        //         void Ondestroy()
        //         {
        //             ResetRenderTextures();
        //         }
        // 
        //         void OnDisable()
        //         {
        //             ResetRenderTextures();
        //         }

        //         private void OnPreRender()
        //         {
        // 
        //             originalCullinkgMask = attachedCamera.cullingMask;
        // 
        //             if ( needsCommandBufferUpdate )
        //                 UpdateCommandBuffers();
        //         }

        //         private void OnPostRender()
        //         {
        //             //attachedCamera.cullingMask = 1 << outlineLayer;
        // 
        //             if ( OutlineTarget.All != null )
        //             {
        //                 int numberOfSteps = OutlineTarget.All.Count;
        // 
        //                 foreach ( KeyValuePair<OutlineTargetListKey, HashSet<OutlineTarget>> pair in OutlineTarget.All )
        //                 {
        //                     /*                    OutlineModes mode = pair.Key.Mode;*/
        //                     /*                    int thickness = pair.Key.Thickness;*/
        //                     HashSet<OutlineTarget> targets = pair.Value;
        // 
        //                     foreach ( OutlineTarget ot in targets )
        //                     {
        //                         if ( ot != null )
        //                             if ( ot.enabled )
        //                                 ot.SetOutlineLayer();
        //                     }
        //                 }
        //             }
        //         }

        /// <summary>
        /// outline for single key
        /// </summary>
        /// <param name="source">source render texture</param>
        /// <param name="destination">destination render texture</param>
        /*
                private void OutlineSingleKey( RenderTexture source, RenderTexture destination )
                {
                    Dictionary<Color32, List<OutlineTarget>>.Enumerator enumerator = OutlineTarget.All.GetEnumerator();
                    enumerator.MoveNext();
                    KeyValuePair<Color32, List<OutlineTarget>> pair = enumerator.Current;
                    Color32 key = pair.Key;
                    List<OutlineTarget> targets = pair.Value;

                    ApplyHashResults( ColorHash.DecodeToOutlineInfo( key ) );
                    UpdateRenderTextures( source );

                    if ( targets != null )
                        foreach ( OutlineTarget ot in targets )
                            if ( ot.enabled )
                                ot.SetOutlineLayer();

                    //             if ( OutlineCamera.targetTexture != tempRT1 )
                    //                 OutlineCamera.targetTexture = tempRT1;
                    // 
                    //             OutlineCamera.Render();
                    ApplyOutline( source, destination );

                    if ( targets != null )
                        foreach ( OutlineTarget ot in targets )
                            ot.ResetLayer();
                }*/

        /// <summary>
        /// outline for multiple keys.
        /// Note multiple keys couses multiple post effect passes and can reduce performance. Use a single key outline anytime it's possible.
        /// </summary>
        /// <param name="source">source render texture</param>
        /// <param name="destination">destination render texture</param>
        /*
                private void OutlineMultipleKeys( RenderTexture source, RenderTexture destination )
                {
                    int counter = 0;
                    Color32 key;
                    foreach ( KeyValuePair<Color32, List<OutlineTarget>> pair in OutlineTarget.All )
                    {
                        key = pair.Key;
                        List<OutlineTarget> targets = pair.Value;
                        ApplyHashResults( ColorHash.DecodeToOutlineInfo( key ) );

                        UpdateRenderTextures( source );

                        if ( targets != null )
                            foreach ( OutlineTarget ot in targets )
                                if ( ot.enabled )
                                    ot.SetOutlineLayer();

                        //bool needsFlip = ( flipOnBlit ) && ( ( counter % 2 ) != 0 );

                        //                 if ( OutlineCamera.targetTexture != tempRT1 )
                        //                     OutlineCamera.targetTexture = tempRT1;
                        // 
                        //                 OutlineCamera.Render();
                        ApplyOutline( source, destination );

                        //                outlineMat.SetInt( "_Flip", needsFlip ? 1 : 0 );

                        if ( targets != null )
                            foreach ( OutlineTarget ot in targets )
                                ot.ResetLayer();

                        //                 if ( counter == OutlineTarget.All.Count - 1 )
                        //                 {
                        //                     if ( ( OutlineTarget.All.Count % 2 ) != 0 )
                        //                         Graphics.Blit( source, destination, outlineMat, (int)OutlineShaderPasses.Flip );
                        //                     else
                        //                         Graphics.Blit( source, destination );
                        //                 }

                        counter++;
                    }
                }*/

        //         private void UpdateRenderTextures( RenderTexture toBeCopied, OutlineModes outlineMode )
        //         {
        //             //             if ( !tempRT1 )
        //             //                 tempRT1 = InitRenderTexture( toBeCopied );
        //             //             else
        //             //             {
        //             //                 if ( ( tempRT1.width != toBeCopied.width ) || ( tempRT1.height != toBeCopied.height ) || ( tempRT1.depth != toBeCopied.depth ) || ( tempRT1.format != toBeCopied.format ) )
        //             //                 {
        //             //                     ResetRenderTexture( tempRT1 );
        //             //                     tempRT1 = InitRenderTexture( toBeCopied );
        //             //                 }
        //             //             }
        // 
        //             //             if ( outlineMode == OutlineModes.FastSolid || outlineMode == OutlineModes.FastGlow )
        //             //             {
        //             //                 ResetRenderTexture( tempRT2 );
        //             //                 tempRT2 = null;
        //             //             }
        //             //             else
        //             //             {
        //             //                 if ( !tempRT2 )
        //             //                     tempRT2 = InitRenderTexture( toBeCopied );
        //             //                 else
        //             //                 {
        //             //                     if ( ( tempRT2.width != toBeCopied.width ) || ( tempRT2.height != toBeCopied.height ) || ( tempRT2.depth != toBeCopied.depth ) || ( tempRT2.format != toBeCopied.format ) )
        //             //                     {
        //             //                         ResetRenderTexture( tempRT2 );
        //             //                         tempRT2 = InitRenderTexture( toBeCopied );
        //             //                     }
        //             //                 }
        //             //             }
        //         }

        //         private RenderTexture InitRenderTexture( RenderTexture toBeCopied )
        //         {
        //             RenderTexture result = new RenderTexture( toBeCopied );
        //             result.Create();
        // 
        //             return result;
        //         }

        //         private void ResetRenderTextures()
        //         {
        //             //ResetRenderTexture( tempRT1 );
        //             //tempRT1 = null;
        // //             ResetRenderTexture( tempRT2 );
        // //             tempRT2 = null;
        //         }

        //         private void ResetRenderTexture( RenderTexture toReset )
        //         {
        //             if ( toReset )
        //             {
        //                 toReset.Release();
        //                 toReset = null;
        //             }
        // 
        //         }

        //         private void ApplyHashResults( ColorHash.OutlineInfo colorHashResult )
        //         {
        //             OutlineMode = colorHashResult.Mode;
        //             OutlineThickness = colorHashResult.OutlineThickness;
        //             OultineColor = colorHashResult.OutlineColor;
        //         }

        //         private void UpdateCommandBuffers()
        //         {
        //             if ( !materialsInitialized )
        //                 InitShadersAndMaterials();
        // 
        //             if ( OutlineTarget.All != null )
        //             {
        //                 //                int numberOfSteps = OutlineTarget.All.Count;
        //                 if ( CommandBuffers == null )
        //                     CommandBuffers = new CommandBufferGroup( OutlineModesConst.NumberOfOutlineModes, attachedCamera, silhouettesShader );
        // 
        //                 CommandBuffers.BeginUpdate();
        //                 foreach ( KeyValuePair<OutlineTargetListKey, HashSet<OutlineTarget>> pair in OutlineTarget.All )
        //                 {
        //                     OutlineModes mode = pair.Key.Mode;
        //                     /*                    int thickness = pair.Key.Thickness;*/
        //                     HashSet<OutlineTarget> targets = pair.Value;
        // 
        //                     if ( targets.Count > 0 )
        //                         CommandBuffers.UpdateMode( mode, targets );
        //                     else
        //                         CommandBuffers.Clear( mode );
        //                 }
        // 
        //                 CommandBuffers.EndUpdate();
        // 
        //                 //commandBuffer.ReleaseTemporaryRT( silhouetteTempRTID );
        //                 //commandBuffer.SetRenderTarget( BuiltinRenderTextureType.CameraTarget );
        // 
        //                 needsCommandBufferUpdate = false;
        //             }
        //         }

        //         private void OnOutlineTargetListChanged( OutlineTarget changed )
        //         {
        //             needsCommandBufferUpdate = true;
        //         }

        #endregion
    }

}


//HIGH TODO: sostituire i nomi delle proprietà dei materiali con gli Shader.ID corrispondenti
//TODO: creare una versione dell'outline che possa essere aggiunta al postprocessing stack https://github.com/Unity-Technologies/PostProcessing/wiki/(v2)-Writing-custom-effects 
//TODO: aggiungere la possibilità di effettuare un blend ulteriore sugli oggetti sottoposti ad outline. Il mainColor di post outline deve essere sottoposto a lerp con il colore di outline (da valutare)
//TODO: correggere problema di sovrapposizione degli outline quando viene selezionato AccurateGlow

