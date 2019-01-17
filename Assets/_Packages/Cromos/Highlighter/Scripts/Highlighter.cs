/********************************************************************
	created:	2016/09/30
	file base:	Highlighter
	file ext:	cs
	author:		Alessandro Maione
	version:	1.9.0
	purpose:	Highlighter based on Color Transition component
*********************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cromos
{
    /// <summary>
    /// Highlighter based on Color Transition component
    /// </summary>
    public class Highlighter : ColorAnimator
    {
        public enum OverlapTransitionModes
        {
            Transition = 0,         // the highlight is executed through a transition from the current color to the target one
            NoTransition           // the highlight is executed just executing the animation ignoring the current color
                                   /*ImmediateSingleColor   // the highlights are executed immediately by replacing the color with target*/
        }

        /// <summary>
        /// a reference to a Color Transition component. If null, a new Color Transition component is added to the game object
        /// </summary>
        [Tooltip( "a reference to a Color Transition component. If null, a new Color Transition component is added to the game object" )]
        public ColorTransition MyColorTransition;

        [Header( "Highlight Mode" )]
        /// <summary>
        /// highlight type.
        /// Colors will animate only target color (with alpha)
        /// Outline will not animate target colors but it will use colors contained in Color gradient to create an outline colored effect around the target
        /// ColorAndOutline will apply both Colors and Outline effects to the target
        /// </summary>
        [Tooltip( "Highlight type. 'Colors' will animate only target color (with alpha). 'Outline' will not animate target colors but it will use colors contained in Color gradient to create an outline colored effect around the target. 'ColorAndOutline' will apply both Colors and Outline effects to the target" )]
        public HighlightTypes Mode = HighlightTypes.Color;

        /*
                //[Header( "Properties" )]
                /// <summary>
                /// overlap mode for highlight operations. If set to 'Transition' the highlight colors will be gradually mixed with the original ones, while 'No transition' will overlap the highlight colors immediately 
                /// </summary>
                [Tooltip( "overlap mode for highlight operations. If set to 'Transition' the highlight colors will be gradually mixed with the original ones, while 'No transition' will overlap the highlight colors immediately " )]
                public OverlapTransitionModes OverlapMode = OverlapTransitionModes.NoTransition;*/

        [Header( "Outline" )]
        public OutlineModes OutlineMode = OutlineModes.FastGlow;
        /// <summary>
        /// Outline thickness over time.
        /// The animation curve is long 1 second. Its duration is not relevant since its length is normalized respect to the highlight duration
        /// The y-axis values will be clamped between min and max outline thickness range [0,20]
        /// </summary>
        [Tooltip( "Outline thickness over time. The animation curve is long 1 second. Its duration is not relevant since its length is normalized respect to the highlight duration. The y-axis values will be clamped between min and max outline thickness range [0,20]" )]
        public AnimationCurve OutlineThickness = AnimationCurve.Linear( 0, 4, 1, 4 );

        [Header( "Events" )]
        /// <summary>
        /// event to be set in editor or by script. The event callbacks will be called when an highlight starts
        /// </summary>
        [Tooltip( "event to be set in editor or by script. The event callbacks will be called when an highlight starts" )]
        public PropertyAnimatorEvent OnHighlight;
        /// <summary>
        /// event to be set in editor or by script. The event callbacks will be called when a de-highlight starts
        /// </summary>
        [Tooltip( "event to be set in editor or by script. The event callbacks will be called when a de-highlight starts" )]
        public PropertyAnimatorEvent OnDeHighlight;

        private OutlineTarget myOutlineTarget;
        public bool Highlighting
        {
            get;
            private set;
        }
        private bool highlightColors = false;
        private bool highlightOutline = false;


        protected override void Awake()
        {
            base.Awake();
            //if ( !MyColorTransition ) MyColorTransition = GetComponent<ColorTransition>();
            //            MyColorTransition.StartTransitionOnStart = false;
            AfterTransition = AfterTransitionActions.DoNothing;

            Highlighting = false;
        }

        protected override void OnEnable()
        {
            if ( StartTransitionMode == PropertyStartupModes.OnEnable )
                Highlight();
        }

        protected override void Start()
        {
            base.Start();

            if ( StartTransitionMode == PropertyStartupModes.OnStart )
                Highlight();
        }

        //         protected override void Start()
        //         {
        //             bool toBeStarted = StartTransitionOnStart;
        //             StartTransitionOnStart = false;
        //             base.Start();
        // 
        //             StartTransitionOnStart = toBeStarted;
        //             if ( StartTransitionOnStart )
        //                 Highlight();
        //         }

        private void LateUpdate()
        {
            CheckForPropertyChanges();

            UpdateHighlightModes();

            if ( AnimationComplete )
            {
                if ( highlightColors )
                {
                    if ( ( LoopMode == TransitionLoopModes.PlayOnce ) && ( WrapMode == WrapModes.Clamp ) && ( !Reversed ) )
                    {
                        startTime = 0;
                        ManageColorHighlight();
                    }
                }
            }
            else
            {
                if ( InProgress )
                {
                    if ( highlightColors ) ManageColorHighlight();
                    if ( highlightOutline ) ManageOutline();
                }
            }
        }

        //HIGH TODO: da controllare!
        public override void StopTransition( bool executeAfterTransitionOperations = true )
        {
            if ( InProgress )
                DeHighlight();
            base.StopTransition( executeAfterTransitionOperations );
        }

        private void UpdateHighlightModes()
        {
            bool oldHighlightColors = highlightColors;
            highlightColors = ( ( Mode & HighlightTypes.Color ) != 0 );
            if ( ( oldHighlightColors == false ) && ( highlightColors == true ) )
                MyColorTransition = GetComponent<ColorTransition>();

            highlightOutline = ( ( Mode & HighlightTypes.Outline ) != 0 );
            if ( !highlightOutline )
                if ( myOutlineTarget )
                    OutlineTarget.Remove( gameObject );
        }

        //HIGH TODO: testare che i colori vengano correttamente ripristinati dopo che l'highlight finisca
        private void ManageColorHighlight()
        {
            if ( InProgress )
            {
                if ( !Highlighting )
                {
                    if ( ElapsedTimeNormalized <= 0 )
                    {
                        StopTransition();
                        return;
                    }
                }
            }

            if ( ( RenderersTable.Count == 0 ) && ( GraphicsTable.Count == 0 ) )
                DetectRenderers();

            AssignColorFromCurrent( CurrentColor, Target, MixMode );
        }

        private void ManageOutline()
        {
            if ( myOutlineTarget )
            {
                if ( myOutlineTarget.enabled )
                {
                    myOutlineTarget.Mode = OutlineMode;
                    myOutlineTarget.OutlineColor = CurrentColor;
                    myOutlineTarget.Thickness = (int)Mathf.Clamp( OutlineThickness.Evaluate( ElapsedTimeNormalized ), OutlineTarget.MinThickness, OutlineTarget.MaxThickness );
                    myOutlineTarget.AffectChildren = AnimateChildren;
                }
            }
        }

        //HIGH TODO: aggiungere Highlight statico (uguale a DoTransition di Color Transition)

        /// <summary>
        /// starts an Highlight
        /// </summary>
        public void Highlight()
        {
            Highlighting = true;
            restartOnComplete = true;

            UpdateHighlightModes();

            if ( highlightColors )
                HighlightColorTransition();

            if ( highlightOutline )
                HighlightOutline();
        }

        /// <summary>
        /// starts an Highlight effect with a custom highlight mode
        /// </summary>
        /// <param name="highlightType">highlight mode</param>
        public void Highlight( HighlightTypes highlightType )
        {
            Mode = highlightType;
            UpdateHighlightModes();
            Highlight();
        }

        /// <summary>
        /// starts a De-highlight (if the object is highlighted)
        /// </summary>
        public void DeHighlight()
        {
            Highlighting = false;
            restartOnComplete = false;
            InProgress = false;

            if ( ( Mode & HighlightTypes.Color ) != 0 )
                DeHighlightColorTransition();

            if ( ( Mode & HighlightTypes.Outline ) != 0 )
                DeHighlightOutline();
        }

        #region STATIC HIGHLIGHT AND DEHIGHLIGHT
        public static Highlighter Highlight( GameObject go, HighlightParameters highlightParameters )
        {
            if ( !go )
                throw new System.Exception( "null game object passed to Highlighter.Highlight" );

            Highlighter res = go.GetComponent<Highlighter>();
            if ( !res )
                res = go.AddComponent<Highlighter>();

            if ( res.Colors == null ) res.Colors = new Gradient();
            res.Colors.colorKeys = highlightParameters.ColorsAndAlphas.colorKeys;
            res.Colors.alphaKeys = highlightParameters.ColorsAndAlphas.alphaKeys;

            res.UseSharedMaterials = highlightParameters.UseSharedMaterials;
            res.Mode = highlightParameters.Mode;
            res.Target = highlightParameters.Target;
            res.MixMode = highlightParameters.MixMode;
            res.MaterialProperty = highlightParameters.MaterialProperty;
            res.MaterialPropertyName = highlightParameters.MaterialPropertyName;
            res.OutlineMode = highlightParameters.OutlineMode;
            res.OutlineThickness = highlightParameters.OutlineThickness;
            res.Duration = highlightParameters.Duration;
            res.Delay = highlightParameters.Delay;
            res.LoopMode = highlightParameters.LoopMode;
            res.WrapMode = highlightParameters.WrapMode;
            res.AnimateChildren = highlightParameters.AnimateChildren;
            res.ExcludeParticleSystems = highlightParameters.ExcludeParticleSystem;
            res.StartTransitionMode = highlightParameters.StartTransitionMode;
            res.AfterTransition = highlightParameters.AfterTransition;
            res.OnHighlight = highlightParameters.OnHighlight;
            res.OnDeHighlight = highlightParameters.OnDeHighlight;

            res.Highlight();

            return res;
        }

        public static Highlighter DeHighlight( GameObject go )
        {
            if ( !go )
                throw new System.Exception( "null game object passed to Highlighter.DeHighlight" );

            Highlighter res = go.GetComponent<Highlighter>();
            if ( res )
                res.DeHighlight();

            return res;
        }

        #endregion

        protected override void OnUseSharedMaterialsChanged()
        {
            base.OnUseSharedMaterialsChanged();
            MyColorTransition.UseSharedMaterials = UseSharedMaterials;
        }

        protected override void OnExcludeParticleSystemChanged()
        {
            base.OnExcludeParticleSystemChanged();
            if ( myOutlineTarget )
                myOutlineTarget.ExcludeParticleSystems = ExcludeParticleSystems;
        }

        #region COLOR TRANSITIONS

        protected override void AssignColorFromCurrentRenderers( Color newColor, TransitionTargets target, ColorModes mixMode )
        {
            if ( RenderersTable != null )
            {
                Material[] mats = null;
                foreach ( KeyValuePair<Renderer, Color[]> pair in RenderersTable )
                {
                    Renderer r = pair.Key;
                    Color[] startColors = pair.Value;

                    mats = UseSharedMaterials ? r.sharedMaterials : r.materials;

                    for ( int i = 0; i < mats.Length; i++ )
                    {
                        if ( mats[i].HasProperty( MaterialPropertyName ) )
                        {
                            if ( ( MyColorTransition ) && ( MyColorTransition.InProgress ) )
                                mats[i].SetColor( MaterialPropertyName, ApplyColorMode( mats[i].GetColor( MaterialPropertyName ), newColor, target, mixMode ) );
                            else
                                mats[i].SetColor( MaterialPropertyName, ApplyColorMode( startColors[i], newColor, target, mixMode ) );
                        }
                    }
                }
            }
        }

        protected override void AssignColorFromCurrentGraphics( Color newColor, TransitionTargets target, ColorModes mixMode )
        {
            if ( GraphicsTable != null )
                foreach ( KeyValuePair<Graphic, Color> pair in GraphicsTable )
                {
                    Graphic g = pair.Key;
                    if ( MyColorTransition.InProgress )
                        g.color = ApplyColorMode( g.color, newColor, target, mixMode );
                    else
                        g.color = ApplyColorMode( GraphicsTable[g], newColor, target, mixMode );
                }
        }

        private void HighlightColorTransition()
        {
            SetReversed( false );
            float elapsed = ElapsedTimeNormalized;
            if ( ( elapsed <= 0 ) || ( elapsed >= 1 ) )
                StartTransition( highlightColors );
        }

        private void DeHighlightColorTransition()
        {
            SetReversed( true );
            if ( AnimationComplete )
                StartTransition( highlightColors );
        }

        #endregion

        #region OUTLINE
        public void SetOutlineThickness( Keyframe[] thicknessKeys )
        {
            if ( thicknessKeys == null )
            {
                Debug.LogError( "no thickness keys" );
                return;
            }
            else
            {
                if ( thicknessKeys.Length < 2 )
                {
                    Debug.LogWarning( "outline thickness should contain at least 2 key frames" );
                    return;
                }
            }

            OutlineThickness.keys = thicknessKeys;
        }

        private void HighlightOutline()
        {
            myOutlineTarget = OutlineTarget.Add( gameObject );
            ClampOutlineThicknessCurve();
            StartTransition( highlightColors );
        }

        private void DeHighlightOutline()
        {
            OutlineTarget.Remove( gameObject );
            StopTransition();
        }

        private void ClampOutlineThicknessCurve()
        {
            Keyframe[] keys = OutlineThickness.keys;

            for ( int i = 0; i < keys.Length; i++ )
                keys[i].value = Mathf.Clamp( keys[i].value, OutlineTarget.MinThickness, OutlineTarget.MaxThickness );

            OutlineThickness.keys = keys;
        }

        #endregion

        #region UNUSED
        /*#region OVERLAP
         

                #region OVERLAP
        private float overlapDuration
        {
            get
            {
                return Duration * 0.1f;
            }
        }
        private float overlapElapsedTime
        {
            get
            {
                return Mathf.Max( 0, Time.time - overlapStartTime );
            }
        }
        private float overlapElapsedTimeNormalized
        {
            get
            {
                return Mathf.Clamp01( overlapElapsedTime / overlapDuration );
            }
        }

        private Gradient overlapGradient;

        private OverlapTransitionModes overlapTransitionMode;
        private float overlapStartTime = 0;
        private float overlapMixSpeed = 0.1f;
        private float overlapMixVal = 0;
        private TransitionTargets overlapTarget;
        private WrapModes overlapWrap;
        private bool overlapReversed = false;
        private ColorModes overlapMixMode;

        private Color overlapCurrentColor
        {
            get
            {
                float toEvaluate = overlapElapsedTimeNormalized;
                if ( overlapReversed )
                    toEvaluate = 1.0f - overlapElapsedTimeNormalized;

                //Debug.Log( overlapElapsedTimeNormalized );
                if ( this.overlapGradient == null ) this.overlapGradient = new Gradient();
                return overlapGradient.Evaluate( toEvaluate );
            }
        }
        #endregion

        /// <summary>
        /// overlaps a new coloring layer for a certain time using a full customizable set of options (from current color to overlapColor parameter)
        /// </summary>
        /// <param name="overlapColor">color to overlap</param>
        /// <param name="overlapTime">duration of overlap</param>
        /// <param name="target">color, alpha or Color and Alpha</param>
        /// <param name="mixMode">color mix mode</param>
        /// <param name="wrap">wrap policy respect to overlap</param>
        /// <param name="transitionMode">if set to 'Transition' the overlapping color will be gradually mixed with the original one, while 'No transition' will overlap the highlight colors immediately </param>
        public void Overlap( Color overlapColor, float overlapTime, TransitionTargets target = TransitionTargets.ColorAndAlpha, ColorModes mixMode = ColorModes.Replace, WrapModes wrap = WrapModes.Clamp, OverlapTransitionModes transitionMode = OverlapTransitionModes.Transition )
        {
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey( overlapCurrentColor, 0 ), new GradientColorKey( overlapColor, 1 ) },
                new GradientAlphaKey[] { new GradientAlphaKey( overlapCurrentColor.a, 0 ), new GradientAlphaKey( overlapColor.a, 1 ) }
                );
            Overlap( gradient, overlapTime, target, mixMode, wrap, transitionMode );
        }

        /// <summary>
        /// overlaps a new coloring layer for a certain time using a full customizable set of options (using a gradient)
        /// </summary>
        /// <param name="overlapColors">gradient of overlap colors</param>
        /// <param name="overlapTime">duration of overlap</param>
        /// <param name="target">color, alpha or Color and Alpha</param>
        /// <param name="mixMode">color mix mode</param>
        /// <param name="wrap">wrap policy respect to overlap</param>
        /// <param name="transitionMode">if set to 'Transition' the overlapping color will be gradually mixed with the original one, while 'No transition' will overlap the highlight colors immediately </param>
        public void Overlap( Gradient overlapColors, float overlapTime, TransitionTargets target = TransitionTargets.ColorAndAlpha, ColorModes mixMode = ColorModes.Replace, WrapModes wrap = WrapModes.Clamp, OverlapTransitionModes transitionMode = OverlapTransitionModes.Transition )
        {
            if ( this.overlapGradient == null ) this.overlapGradient = new Gradient();
            this.overlapGradient.SetKeys( overlapColors.colorKeys, overlapColors.alphaKeys );
            //            overlapDuration = overlapTime;
            overlapStartTime = Time.time;
            overlapTarget = target;
            overlapWrap = wrap;
            overlapMixSpeed = 0.05f / Duration;
            overlapMixMode = mixMode;
            this.overlapTransitionMode = transitionMode;
        }

        /// <summary>
        /// stops color overlap (if any)
        /// </summary>
        public void StopOverlap()
        {
            if ( overlapMixSpeed > 0 )
                overlapMixSpeed = -overlapMixSpeed;
            overlapStartTime = Time.time;
            overlapWrap = WrapModes.Restore;
        }

        private void ManageColorOverlap()
        {

            bool canApply = true;
            if ( overlapElapsedTime >= overlapDuration )
            {
                switch ( overlapWrap )
                {
                    //                     case WrapModes.Repeat:
                    //                         overlapStartTime = Time.time;
                    //                         break;
                    //                     case WrapModes.PingPong:
                    //                         overlapStartTime = Time.time;
                    //                         overlapReversed = !overlapReversed;
                    //                         break;
                    case WrapModes.Clamp:
                        ;
                        break;
                    case WrapModes.Restore:
                    default:
                        ;
                        break;
                }
            }

            if ( !canApply )
                return;

            ApplyOverlap( CurrentColor, overlapCurrentColor, overlapMixVal, overlapMixMode, overlapTransitionMode );
            overlapMixVal += overlapMixSpeed;
            overlapMixVal = Mathf.Clamp01( overlapMixVal );
        }

        private void ApplyOverlap( Color first, Color second, float lerpVal, ColorModes mixMode, OverlapTransitionModes transitionMode )
        {

            Color toBeAssigned = ( transitionMode == OverlapTransitionModes.Transition ) ? Color.Lerp( first, second, Mathf.Clamp01( lerpVal ) ) : second;

            AssignColorFromCurrent( toBeAssigned, overlapTarget, mixMode );
        }

        #endregion*/
        #endregion

    }

}

