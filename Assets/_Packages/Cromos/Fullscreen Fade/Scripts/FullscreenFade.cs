/********************************************************************
    created:	2015/10/19
    file base:	FullscreenFade
    file ext:	cs
    author:		Alessandro Maione
    version:    1.3.5
    
    purpose:	Dissolve sphere used for fadein/out of the camera
*********************************************************************/
using UnityEngine;

namespace Cromos
{

    /// <summary>
    /// Dissolve sphere used for fadein/out of the camera
    /// </summary>
    public class FullscreenFade : ColorTransition
    {
        /// <summary>
        /// default effect duration
        /// </summary>
        public const float DefaultDuration = 3;


        [Header( "Fullscreen Fade" )]
        /// <summary>
        /// if true, the normals of the mesh will be inverted before fade starts
        /// </summary>
        [Tooltip( "if true, the normals of the mesh will be inverted before fade starts" )]
        public bool FlipNormals = false;
        /// <summary>
        /// if true, the dissolve sphere object will not be destroyed during level change.
        /// Very useful when playing a fade while loading a new level of the game
        /// </summary>
        [Tooltip( "if true, the dissolve sphere object will not be destroyed during level change. Very useful when playing a fade while loading a new level of the game" )]
        public bool DontDestroyOnLevelChange = true;
        /// <summary>
        /// if true the dissolve sphere will be linked to main camera. If no main camera is present, it will follow the first camera present in scene
        /// </summary>
        [Tooltip( "if true the dissolve sphere will be linked to main camera. If no main camera is present, it will follow the first camera present in scene" )]
        public bool FollowCamera = false;
        public float SphereSize = -1;

        private Camera mainCamera = null;
        private Canvas[] allCanvas = null;
        private RenderMode[] renderModes = null;


        protected override void Start()
        {
            base.Start();
            try
            {
                FullscreenFade[] tobedestroyed = FindObjectsOfType<FullscreenFade>();
                foreach ( FullscreenFade sphere in tobedestroyed )
                    if ( sphere != this )
                        Destroy( sphere.gameObject );

                //            transform.parent = Camera.main.transform;
                if ( !mainCamera )
                    mainCamera = Camera.main;

                if ( ( SphereSize < 0 ) || ( SphereSize < mainCamera.nearClipPlane ) )
                    SphereSize = ComputeSphereSize( mainCamera );

                ChangeSize( SphereSize );

                if ( mainCamera )
                    transform.position = mainCamera.transform.position;

                Material mat = (Material)Resources.Load( "DissolveSphere", typeof( Material ) );
                if ( mat )
                    GetComponent<Renderer>().material = mat;

                if ( FlipNormals )
                    gameObject.AddComponent<FlipMesh>();

                if ( DontDestroyOnLevelChange )
                    gameObject.AddComponent<DontDestroyOnLevelLoaded>();
            }
            catch ( System.Exception ex )
            {
                Debug.LogException( ex );
            }
        }

        private float ComputeSphereSize( Camera mainCamera )
        {
            float alpha = mainCamera.fieldOfView * 0.5f;
            float beta = 90.0f - alpha;
            float angleRatio = alpha / beta;
            float otherCathetus = mainCamera.nearClipPlane * angleRatio;
            float screenRatio = Mathf.Max( mainCamera.pixelWidth, mainCamera.pixelHeight ) / Mathf.Min( mainCamera.pixelWidth, mainCamera.pixelHeight );

            return ( 2.0f * screenRatio * Vector2.Distance( Vector2.zero, new Vector2( mainCamera.nearClipPlane, otherCathetus ) ) ) + 0.1f;
        }

        protected override void Update()
        {
            base.Update();

            if ( FollowCamera )
            {
                if ( Camera.allCamerasCount > 0 )
                {
                    if ( Camera.main )
                        transform.position = Camera.main.transform.position;
                    else
                    {
                        transform.position = Camera.allCameras[0].transform.position;
                    }
                }
            }

            if ( AnimationComplete )
            {
                try
                {
                    Resources.UnloadUnusedAssets();
                }
                catch ( System.Exception ex )
                {
                    Debug.LogWarning( "an error occurred while trying to unload unused resources" );
                    Debug.LogException( ex );
                }
            }
        }

        void OnDestroy()
        {
            Resources.UnloadUnusedAssets();
        }

        /// <summary>
        /// instantiates a Dissolve Sphere persistent over level change.
        /// A sphere object is created and linked to the main camera position and the color transition is played on it
        /// </summary>
        /// <param name="fadeColor">fade color</param>
        /// <param name="size">size of fade sphere (-1 means that size will be calculated automatically)</param>
        /// <param name="duration">duration of fade (both in and out)</param>
        /// <param name="fadeSounds">if true, audio source on main camera gameobject will be faded in duration/2 seconds</param>
        /// <param name="hideUI">if true, hides each Canvas component in scene during the animation</param>
        /// <returns>the new Dissolve Sphere component created</returns>
        public static FullscreenFade BetweenLevels( Color fadeColor, float size = -1, float duration = DefaultDuration, bool hideUI = true/*, bool fadeSounds = true*/ )
        {
            FullscreenFade dissolve = FullscreenFade.DoFade( null,
                new GradientColorKey[] { new GradientColorKey( fadeColor, 0 ), new GradientColorKey( fadeColor, 1 ) },
                new GradientAlphaKey[] { new GradientAlphaKey( 0, 0 ), new GradientAlphaKey( 1, 0.2f ), new GradientAlphaKey( 1, 0.8f ), new GradientAlphaKey( 0, 1 ) },
                size,
                duration,
                true );

            if ( hideUI )
                dissolve.ChangeRenderModeUI();

            dissolve.FlipNormals = true;
            dissolve.FollowCamera = true;
            if ( dissolve.OnEnd == null )
                dissolve.OnEnd = new PropertyAnimatorEvent();
            dissolve.OnEnd.AddListener( dissolve.ResetAndDestroy );

            if ( !dissolve.gameObject.GetComponent<DontDestroyOnLevelLoaded>() )
                dissolve.gameObject.AddComponent<DontDestroyOnLevelLoaded>();

            InitRenderer( dissolve.gameObject.GetComponent<Renderer>() );

            //             if ( fadeSounds )
            //             {
            //                 AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
            //                 foreach ( AudioSource audioSource in audioSources )
            //                 {
            //                     iTween.AudioTo( audioSource.gameObject, 0, 1, dissolve.Duration * 0.5f );
            //                 }
            //             }

            return dissolve;
        }

        private void ResetAndDestroy()
        {
            ResetRenderModeUI();
            Destroy( gameObject );
        }

        #region DO FADE

        /// <summary>
        /// Adds a Dissolve Sphere component on a game object and executes a fade-in/fade-out animation using the parameters passed to the method. 
        /// To execute the fade properly, a custom material is used for game object
        /// </summary>
        /// <param name="go">game object where to add the Dissolve Spere component. If null is passed, a new primitive object (sphere) is created and the component is added to it</param>
        /// <param name="colors">colors of the animation to create</param>
        /// <param name="size">local scale to be set for the game obect</param>
        /// <param name="duration">duration of the effect</param>
        /// <param name="affectChildren">if true, animation will be propagated to all the hierarchy of the go game object</param>
        /// <param name="delay">time to wait before starting the animation</param>
        /// <returns>the new Dissolve Sphere component created and added to the game object</returns>
        public static FullscreenFade DoFade( GameObject go, GradientColorKey[] colors, float size = 2, float duration = -1, bool affectChildren = true, float delay = 0 )
        {
            return DoTransition<FullscreenFade>( go, colors, affectChildren, delay );
        }

        /// <summary>
        /// Adds a DissolveSphere base-type component on a game object and executes a fade-in/fade-out animation using the parameters passed to the method. 
        /// To execute the fade properly, a custom material is used for game object
        /// </summary>
        /// <typeparam name="T">DissolveSphere base-type for the component to create</typeparam>
        /// <param name="go">game object where to add the Dissolve Spere component. If null is passed, a new primitive object (sphere) is created and the component is added to it</param>
        /// <param name="colors">colors of the animation to create</param>
        /// <param name="size">local scale to be set for the game obect</param>
        /// <param name="duration">duration of the effect</param>
        /// <param name="affectChildren">if true, animation will be propagated to all the hierarchy of the go game object</param>
        /// <param name="delay">time to wait before starting the animation</param>
        /// <returns>the new Dissolve Sphere-based component created and added to the game object</returns>
        public static T DoFade<T>( GameObject go, GradientColorKey[] colors, float size = 2, float duration = -1, bool affectChildren = true, float delay = 0 ) where T : FullscreenFade
        {
            T res = DoTransition<T>( go, colors, affectChildren, delay );
            res.ChangeSize( size );
            InitRenderer( res.gameObject.GetComponent<Renderer>() );
            res.Duration = duration;
            return res;
        }

        /// <summary>
        /// Adds a Dissolve Sphere component on a game object and executes a fade-in/fade-out animation using the parameters passed to the method. 
        /// To execute the fade properly, a custom material is used for game object
        /// </summary>
        /// <param name="go">game object where to add the Dissolve Spere component. If null is passed, a new primitive object (sphere) is created and the component is added to it</param>
        /// <param name="colors">colors of the animation to create</param>
        /// <param name="alphas">alpha values of the animation to create</param>
        /// <param name="size">local scale to be set for the game obect</param>
        /// <param name="duration">duration of the effect</param>
        /// <param name="affectChildren">if true, animation will be propagated to all the hierarchy of the go game object</param>
        /// <param name="delay">time to wait before starting the animation</param>
        /// <returns>the new Dissolve Sphere component created and added to the game object</returns>
        public static FullscreenFade DoFade( GameObject go, GradientColorKey[] colors, GradientAlphaKey[] alphas, float size = -1, float duration = -1, bool affectChildren = true, float delay = 0 )
        {
            return DoFade<FullscreenFade>( go, colors, alphas, size, duration, affectChildren, delay );
        }

        /// <summary>
        /// Adds a DissolveSphere base-type component on a game object and executes a fade-in/fade-out animation using the parameters passed to the method. 
        /// To execute the fade properly, a custom material is used for game object
        /// </summary>
        /// <typeparam name="T">DissolveSphere base-type for the component to create</typeparam>
        /// <param name="go">game object where to add the Dissolve Spere component. If null is passed, a new primitive object (sphere) is created and the component is added to it</param>
        /// <param name="colors">colors of the animation to create</param>
        /// <param name="alphas">alpha values of the animation to create</param>
        /// <param name="size">local scale to be set for the game obect</param>
        /// <param name="duration">duration of the effect</param>
        /// <param name="affectChildren">if true, animation will be propagated to all the hierarchy of the go game object</param>
        /// <param name="delay">time to wait before starting the animation</param>
        /// <returns>the new Dissolve Sphere-based component created and added to the game object</returns>
        public static T DoFade<T>( GameObject go, GradientColorKey[] colors, GradientAlphaKey[] alphas, float size = -1, float duration = -1, bool affectChildren = true, float delay = 0 ) where T : FullscreenFade
        {
            T res = DoTransition<T>( go, colors, alphas, affectChildren, delay );
            res.ChangeSize( size );
            InitRenderer( res.gameObject.GetComponent<Renderer>() );

            if ( duration > 0 ) res.Duration = duration;

            return res;
        }

        #endregion

        private void ChangeRenderModeUI()
        {
            allCanvas = FindObjectsOfType<Canvas>();
            if ( allCanvas.Length == 0 )
                return;

            renderModes = new RenderMode[allCanvas.Length];
            for ( int i = 0; i < allCanvas.Length; i++ )
            {
                renderModes[i] = allCanvas[i].renderMode;
                if ( allCanvas[i].renderMode == RenderMode.ScreenSpaceOverlay )
                {
                    allCanvas[i].renderMode = RenderMode.ScreenSpaceCamera;
                    allCanvas[i].worldCamera = Camera.main;
                }
            }
        }

        private void ResetRenderModeUI()
        {
            for ( int i = 0; i < allCanvas.Length; i++ )
                allCanvas[i].renderMode = renderModes[i];

            allCanvas = null;
            renderModes = null;
        }

        private static void InitRenderer( Renderer renderer )
        {
            if ( renderer )
            {
                renderer.receiveShadows = false;
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

#if UNITY_5_3_OR_NEWER
                renderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
                renderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
                renderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
#else
                renderer.useLightProbes = false;
#endif
                renderer.material = Resources.Load( "FullscreenFade" ) as Material;
            }
        }

        private void ChangeSize( float size )
        {
            if ( size > 0 )
                SphereSize = size;
            transform.localScale = Vector3.one * Mathf.Max( 0.0001f, size );
        }

    }

}

//HIGH: VR splashscreen

//TODO: scinedere il componente: deve esserci un componente generico che effettua un fade su qualsiasi mesh, e un altro che fa fade in fullscreen da chiamare FullscreenFade
//TODO: prendere shader da Bosch
