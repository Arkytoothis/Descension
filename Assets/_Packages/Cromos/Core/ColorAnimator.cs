/********************************************************************
	created:	2016/12/13
	file base:	coloranimator
	file ext:	cs
	author:		Alessandro Maione
	version:	1.6.3
	
	purpose:	a color animator (property animator of colors)
*********************************************************************/
using System.Collections.Generic;
using UnityEngine;

namespace Cromos
{
    /// <summary>
    /// a color animator (property animator of colors)
    /// </summary>
    public abstract class ColorAnimator : PropertyAnimator<Color>
    {
        [Header( "Colors" )]
        /// <summary>
        /// Colors and alpha values used during color transition
        /// </summary>
        [Tooltip( "Colors and alpha values used during color transition" )]
        public UnityEngine.Gradient Colors;

        [Header( "Properties" )]
        /// <summary>
        /// should use shared materials instead of instanced materials?
        /// Note: if shared materials are used, color animations will affect all objects using that material
        /// </summary>
        [Tooltip( "should use shared materials instead of instanced materials? Note: if shared materials are used, color animations will affect all objects using that material" )]
        public bool UseSharedMaterials = false;
        protected bool useSharedMaterials = false;
        /// <summary>
        /// Allows to choose what to animate in color transition: Color, Alpha, or Color and Alpha
        /// </summary>
        [Tooltip( "Transition will affect only alpha, color or both" )]
        public TransitionTargets Target = TransitionTargets.ColorAndAlpha;
        /// <summary>
        /// color mixing during the animations. The colors defined in Colors gradient will be mixed with starting colors 
        /// </summary>
        [Tooltip( "color mixing during the animations" )]
        public ColorModes MixMode = ColorModes.Replace;
        /// <summary>
        /// Name of the property to animate in material. Custom requires MaterialPropertyName to be set
        /// </summary>
        [Tooltip( "Name of the property to animate in material. Custom requires MaterialPropertyName to be set" )]
        public NamedMaterialPropertyNames MaterialProperty = NamedMaterialPropertyNames._Color;
        protected NamedMaterialPropertyNames currentMaterialProperty = NamedMaterialPropertyNames._Color;
        /// <summary>
        /// If MaterialProperty is set to Custom, the property set here will be used
        /// </summary>
        [Tooltip( "If MaterialProperty is set to Custom, the property set here will be used" )]
        public string MaterialPropertyName = "";
        protected string currentMaterialPropertyName = "";
        [Tooltip( "if true, particle system materials will not animated" )]
        /// <summary>
        /// if true, particle system materials will not animated
        /// </summary>
        public bool ExcludeParticleSystems = true;
        private bool excludeParticleSystems = true;
        //TODO: estendere anche a Trail e Line renderer



        /// <summary>
        /// allows to get or set the first color in Colors gradient (alpha will be ignored)
        /// </summary>
        public Color StartColor
        {
            get
            {
                return StartValue;
            }
            set
            {
                SetValue( Reversed ? int.MaxValue : 0, value );
            }
        }
        /// <summary>
        /// allows to get or set the last color in Colors gradient (alpha will be ignored)
        /// </summary>
        public Color EndColor
        {
            get
            {
                return EndValue;
            }
            set
            {
                SetValue( Reversed ? 0 : int.MaxValue, value );
            }
        }
        /// <summary>
        /// the current color used for animation from Colors gradient (alpha is ignored)
        /// </summary>
        public Color CurrentColor
        {
            get
            {
                return CurrentValue;
            }
        }
        /// <summary>
        /// allows to get or set the first alpha value in Colors gradient (color will be ignored)
        /// </summary>
        public float StartAlpha
        {
            get
            {
                return StartValue.a;
            }
            set
            {
                SetValue( Reversed ? int.MaxValue : 0, value );
            }
        }
        /// <summary>
        /// allows to get or set the last alpha value in Colors gradient (color will be ignored)
        /// </summary>
        public float EndAlpha
        {
            get
            {
                return EndValue.a;
            }
            set
            {
                SetValue( Reversed ? 0 : int.MaxValue, value );
            }
        }
        /// <summary>
        /// the current alphas used for animation from Colors gradient (color is ignored)
        /// </summary>
        public float CurrentAlpha
        {
            get
            {
                return CurrentValue.a;
            }
        }

        public Dictionary<Renderer, Color[]> RenderersTable
        {
            get;
            private set;
        }
        public Dictionary<UnityEngine.UI.Graphic, Color> GraphicsTable
        {
            get;
            private set;
        }

#if UNITY_EDITOR
        public Color CurrentColorEditor = Color.black;
#endif

        protected PropertyAnimatorEvent OnMaterialPropertyChanged = new PropertyAnimatorEvent();



        protected virtual void Awake()
        {
            animateChildren = AnimateChildren;
            RenderersTable = new Dictionary<Renderer, Color[]>();
            GraphicsTable = new Dictionary<UnityEngine.UI.Graphic, Color>();
            useSharedMaterials = UseSharedMaterials;
        }


        protected override void OnDisable()
        {
            ResetColors();
            base.OnDisable();
        }

        protected override void Start()
        {
            Duration = Mathf.Max( 0.01f, Duration );
            if ( Colors == null ) Colors = new UnityEngine.Gradient();
            if ( Colors.colorKeys.Length <= 1 )
            {
                Debug.LogError( "not enough colors specified for Colors field" );
                return;
            }

            OnMaterialPropertyChanged.AddListener( DetectRenderers );

            base.Start();
        }

        /// <summary>
        /// starts the color transition (animation) using the properties set for the ColorAnimator object
        /// </summary>
        public override void StartTransition()
        {
            // setting up material
            base.StartTransition();
        }

        /// <summary>
        /// starts the color transition (animation) using the properties set for the ColorAnimator object
        /// </summary>
        /// <param name="initColor">if true, the transition will start using the start color defined in the Colors gradient</param>
        public void StartTransition( bool initColor )
        {
            if ( initColor )
            {
                Color initialColor = Reversed ? EndColor : StartColor;
                AssignColor( initialColor, Target, MixMode );
            }
            StartTransition();
        }

        /// <summary>
        /// Starts the color transition (animation) using the properties set for the ColorAnimator object.
        /// Before starting the transition, the start and end colors and alpha values are set by resetting the Colors gradient. This is a more expensive variant of StartTransition. Use other versions where possible
        /// </summary>
        /// <param name="startColor">start color of the transition</param>
        /// <param name="endColor">end color of the transition</param>
        /// <param name="setAlphas">if true, the start alpha and end alpha values will be reset using alpha values of start color and end color</param>
        public void StartTransition( Color startColor, Color endColor, bool setAlphas = true )
        {
            ResetGradient();
            StartColor = startColor;
            EndColor = endColor;

            if ( setAlphas )
            {
                StartAlpha = startColor.a;
                EndAlpha = endColor.a;
            }
        }

        /// <summary>
        /// assigns a color mixing it with the start color of each graphics or renderer component
        /// </summary>
        /// <param name="newColor">color to assign</param>
        /// <param name="target">Color, Alpha or Color and Alpha</param>
        /// <param name="mixMode">mix mode</param>
        public virtual void AssignColor( Color newColor, TransitionTargets target, ColorModes mixMode )
        {
            if ( GraphicsTable != null )
                foreach ( KeyValuePair<UnityEngine.UI.Graphic, Color> pair in GraphicsTable )
                {
                    UnityEngine.UI.Graphic g = pair.Key;
                    Color startColor = pair.Value;
                    g.color = ApplyColorMode( startColor, newColor, target, mixMode );
                }

            if ( RenderersTable != null )
            {
                Material[] mats = null;
                foreach ( KeyValuePair<Renderer, Color[]> pair in RenderersTable )
                {
                    Renderer r = pair.Key;
                    Color[] startColors = pair.Value;

                    mats = UseSharedMaterials ? r.sharedMaterials : r.materials;
                    for ( int i = 0; i < mats.Length; i++ )
                        if ( mats[i].HasProperty( MaterialPropertyName ) )
                            mats[i].SetColor( MaterialPropertyName, ApplyColorMode( startColors[i], newColor, target, mixMode ) );
                }
            }
        }


        /// <summary>
        /// assigns a color mixing it with the current color of each graphics or renderer component
        /// </summary>
        /// <param name="newColor">color to assign</param>
        /// <param name="target">Color, Alpha or Color and Alpha</param>
        /// <param name="mixMode">mix mode</param>
        public virtual void AssignColorFromCurrent( Color newColor, TransitionTargets target, ColorModes mixMode )
        {
            AssignColorFromCurrentGraphics( newColor, target, mixMode );
            AssignColorFromCurrentRenderers( newColor, target, mixMode );
        }

        protected virtual void AssignColorFromCurrentRenderers( Color newColor, TransitionTargets target, ColorModes mixMode )
        {
            if ( RenderersTable != null )
            {
                Material[] mats = null;
                foreach ( KeyValuePair<Renderer, Color[]> pair in RenderersTable )
                {
                    Renderer r = pair.Key;

                    mats = UseSharedMaterials ? r.sharedMaterials : r.materials;

                    for ( int i = 0; i < mats.Length; i++ )
                        if ( mats[i].HasProperty( MaterialPropertyName ) )
                            mats[i].SetColor( MaterialPropertyName, ApplyColorMode( mats[i].GetColor( MaterialPropertyName ), newColor, target, mixMode ) );
                }
            }
        }

        protected virtual void AssignColorFromCurrentGraphics( Color newColor, TransitionTargets target, ColorModes mixMode )
        {
            if ( GraphicsTable != null )
                foreach ( KeyValuePair<UnityEngine.UI.Graphic, Color> pair in GraphicsTable )
                {
                    UnityEngine.UI.Graphic g = pair.Key;
                    g.color = ApplyColorMode( g.color, newColor, target, mixMode );
                }
        }

        /// <summary>
        /// resets the colors of the game object 
        /// </summary>
        /// <param name="target">target to be reset</param>
        public void ResetColors( TransitionTargets target = TransitionTargets.ColorAndAlpha )
        {
            if ( GraphicsTable != null )
                foreach ( KeyValuePair<UnityEngine.UI.Graphic, Color> pair in GraphicsTable )
                {
                    UnityEngine.UI.Graphic g = pair.Key;
                    Color startColor = pair.Value;
                    g.color = startColor;
                }

            if ( RenderersTable != null )
            {
                Material[] mats = null;
                foreach ( KeyValuePair<Renderer, Color[]> pair in RenderersTable )
                {
                    Renderer r = pair.Key;
                    Color[] startColors = pair.Value;

                    mats = UseSharedMaterials ? r.sharedMaterials : r.materials;
                    for ( int i = 0; i < mats.Length; i++ )
                        if ( mats[i].HasProperty( MaterialPropertyName ) )
                            mats[i].SetColor( MaterialPropertyName, startColors[i] );
                }
            }
        }

        protected Color ApplyColorMode( Color startColor, Color newColor, TransitionTargets target, ColorModes mixMode )
        {
            Color32 s32;
            Color32 n32;

            switch ( mixMode )
            {
                case ColorModes.Add:
                    newColor += startColor;
                    newColor.r = Mathf.Clamp01( newColor.r );
                    newColor.g = Mathf.Clamp01( newColor.g );
                    newColor.b = Mathf.Clamp01( newColor.b );
                    newColor.a = Mathf.Clamp01( newColor.a );
                    break;
                case ColorModes.AddHDR:
                    newColor += startColor;
                    newColor.a = Mathf.Clamp01( newColor.a );
                    break;
                case ColorModes.Multiply:
                    newColor.r *= startColor.r;
                    newColor.g *= startColor.g;
                    newColor.b *= startColor.b;
                    newColor.a *= startColor.a;
                    newColor.a = Mathf.Clamp01( newColor.a );
                    break;
                case ColorModes.Subtract:
                    newColor = startColor - newColor;
                    newColor.r = Mathf.Clamp01( newColor.r );
                    newColor.g = Mathf.Clamp01( newColor.g );
                    newColor.b = Mathf.Clamp01( newColor.b );
                    newColor.a = Mathf.Clamp01( newColor.a );
                    break;
                case ColorModes.Invert:
                    ApplyColorMode( startColor, newColor, target, ColorModes.Replace );
                    newColor = new Color( 1, 1, 1, 1 ) - newColor;
                    newColor.a = Mathf.Clamp01( newColor.a );
                    break;
                case ColorModes.And:
                    s32 = startColor;
                    n32 = newColor;
                    n32.r &= s32.r;
                    n32.g &= s32.g;
                    n32.b &= s32.b;
                    n32.a &= s32.a;
                    newColor = n32;
                    newColor.a = Mathf.Clamp01( newColor.a );
                    break;
                case ColorModes.Or:
                    s32 = startColor;
                    n32 = newColor;
                    n32.r |= s32.r;
                    n32.g |= s32.g;
                    n32.b |= s32.b;
                    n32.a |= s32.a;
                    newColor = n32;
                    newColor.a = Mathf.Clamp01( newColor.a );
                    break;
                case ColorModes.Xor:
                    s32 = startColor;
                    n32 = newColor;
                    n32.r ^= s32.r;
                    n32.g ^= s32.g;
                    n32.b ^= s32.b;
                    n32.a ^= s32.a;
                    newColor = n32;
                    newColor.a = Mathf.Clamp01( newColor.a );
                    break;
                case ColorModes.IfDarker:
                    newColor.r = Mathf.Min( startColor.r, newColor.r );
                    newColor.g = Mathf.Min( startColor.g, newColor.g );
                    newColor.b = Mathf.Min( startColor.b, newColor.b );
                    newColor.a = Mathf.Min( startColor.a, newColor.a );
                    break;
                case ColorModes.IfLighter:
                    newColor.r = Mathf.Max( startColor.r, newColor.r );
                    newColor.g = Mathf.Max( startColor.g, newColor.g );
                    newColor.b = Mathf.Max( startColor.b, newColor.b );
                    newColor.a = Mathf.Max( startColor.a, newColor.a );
                    break;
                case ColorModes.Replace:
                default:
                    ;
                    break;
            }

            switch ( target )
            {
                case TransitionTargets.Alpha:
                    newColor.r = startColor.r;
                    newColor.g = startColor.g;
                    newColor.b = startColor.b;
                    break;
                case TransitionTargets.Color:
                    newColor.a = startColor.a;
                    break;
                case TransitionTargets.ColorAndAlpha:
                    ;
                    break;
            }

            //        NoticeMessage( "start: " + startColor + " - newColor: " + newColor );

            return newColor;
        }

        protected virtual void OnUseSharedMaterialsChanged()
        {
            useSharedMaterials = UseSharedMaterials;
        }

        #region OPERATIONS ON Colors

        protected override Color EvaluateValue( float toEvaluate )
        {
            if ( Colors == null ) Colors = new UnityEngine.Gradient();
            return Colors.Evaluate( toEvaluate );
        }

        protected override void SetValue( int idx, Color valueToBeSet )
        {
            if ( Colors == null ) Colors = new UnityEngine.Gradient();
            UnityEngine.GradientColorKey[] colors = Colors.colorKeys;

            idx = Mathf.Clamp( idx, 0, colors.Length - 1 );
            colors[idx].color = valueToBeSet;

            Colors.colorKeys = colors;
        }

        protected void SetValue( int idx, float alpha )
        {
            if ( Colors == null ) Colors = new UnityEngine.Gradient();
            UnityEngine.GradientAlphaKey[] alphas = Colors.alphaKeys;

            idx = Mathf.Clamp( idx, 0, alphas.Length - 1 );
            alphas[idx].alpha = alpha;

            Colors.alphaKeys = alphas;
        }

        /// <summary>
        /// UNUSED
        /// </summary>
        /// <param name="t">UNUSED</param>
        /// <param name="valueToBeSet">UNUSED</param>
        protected override void SetValue( float t, Color valueToBeSet )
        {
            throw new System.NotImplementedException();
        }

        protected override void ResetValues()
        {
            ResetColors();
        }

        public void ResetGradient()
        {
            ResetGradientColors( (UnityEngine.GradientColorKey[])null );
            ResetGradientAlphas( (UnityEngine.GradientAlphaKey[])null );
        }

        /// <summary>
        /// resets the colors of the Colors gradient with a default white-white color preset if colors parameter is null,
        /// else the color array passed as argument is used to populate it
        /// </summary>
        /// <param name="colors">a color array. The colors are distributed along the gradient timeline at fixed steps</param>
        public void ResetGradientColors( Color[] colors = null )
        {
            if ( colors != null )
            {
                if ( colors.Length > 0 )
                {
                    UnityEngine.GradientColorKey[] gradient = new UnityEngine.GradientColorKey[colors.Length];
                    for ( int i = 0; i < colors.Length; i++ )
                    {
                        float time = ( i == colors.Length - 1 ) ? 1 : ( (float)i / (float)( colors.Length - 1 ) );
                        gradient[i] = new UnityEngine.GradientColorKey( colors[i], time );
                    }
                    ResetGradientColors( gradient );
                }
            }
        }

        /// <summary>
        /// resets the colors of the Colors gradient with a default white-white color preset if colors parameter is null,
        /// else the GradientColorKey array passed as argument is used to populate it
        /// </summary>
        /// <param name="colors">an array of GradientColorKeys to use for reset of the Colors gradient. If null is passed a preset gradient is used</param>
        public void ResetGradientColors( UnityEngine.GradientColorKey[] colors = null )
        {
            if ( colors == null )
            {
                colors = new UnityEngine.GradientColorKey[]
                {
                new UnityEngine.GradientColorKey(Color.white, 0),
                new UnityEngine.GradientColorKey(Color.white, 1)
                };
            }

            if ( Colors == null ) Colors = new Gradient();
            Colors.colorKeys = colors;
        }

        /// <summary>
        /// resets the alphas of the Colors gradient with a default 1-1 alpha preset if alphas parameter is null,
        /// else the alpha array passed as argument is used to populate it
        /// </summary>
        /// <param name="alphas">an array of alpha values to use for reset of the Colors gradient. If null is passed a preset gradient is used. The alpha values are distributed along the gradient timeline at fixed steps</param>
        public void ResetGradientAlphas( float[] alphas = null )
        {
            if ( alphas != null )
            {
                if ( alphas.Length > 0 )
                {
                    UnityEngine.GradientAlphaKey[] gradient = new UnityEngine.GradientAlphaKey[alphas.Length];
                    for ( int i = 0; i < alphas.Length; i++ )
                    {
                        float time = ( i == alphas.Length - 1 ) ? 1 : ( (float)i / (float)( alphas.Length - 1 ) );
                        gradient[i] = new UnityEngine.GradientAlphaKey( alphas[i], time );
                        ResetGradientAlphas( gradient );
                    }
                }
            }
        }

        /// <summary>
        /// resets the alphas of the Colors gradient with a default 1-1 alpha preset if alphas parameter is null,
        /// else the alpha array passed as argument is used to populate it
        /// </summary>
        /// <param name="alphas">an array of GradientAlphaKeys to use for reset of the Colors gradient. If null is passed a preset gradient is used</param>
        public void ResetGradientAlphas( UnityEngine.GradientAlphaKey[] alphas = null )
        {
            if ( alphas == null )
            {
                alphas = new UnityEngine.GradientAlphaKey[]
                {
                new UnityEngine.GradientAlphaKey(1, 0),
                new UnityEngine.GradientAlphaKey(1, 1)
                };
            }

            if ( Colors == null ) Colors = new UnityEngine.Gradient();
            Colors.alphaKeys = alphas;
        }

        protected override void OnAnimateChildrenChanged()
        {
            base.OnAnimateChildrenChanged();
            ResetColors( Target );
            DetectRenderers();
        }

        protected virtual void OnExcludeParticleSystemChanged()
        {
            excludeParticleSystems = ExcludeParticleSystems;
            ResetColors( Target );
            DetectRenderers();
        }

        protected void DetectRenderers()
        {
            if ( MaterialPropertyName == "" )
                MaterialPropertyName = MaterialProperty.ToString();

            Renderer[] renderers = null;
            UnityEngine.UI.Graphic[] graphics = null;

            RenderersTable.Clear();
            GraphicsTable.Clear();

            if ( animateChildren )
            {
                renderers = GetComponentsInChildren<Renderer>();
                graphics = GetComponentsInChildren<UnityEngine.UI.Graphic>();
            }
            else
            {
                renderers = GetComponents<Renderer>();
                graphics = GetComponents<UnityEngine.UI.Graphic>();
            }

            Material[] mats = null;
            if ( renderers != null )
            {
                foreach ( Renderer r in renderers )
                {
                    bool canAdd = !( ExcludeParticleSystems && ( r is ParticleSystemRenderer ) );
                    if ( canAdd )
                    {
                        mats = UseSharedMaterials ? r.sharedMaterials : r.materials;
                        Color[] startColors = new Color[mats.Length];

                        for ( int i = 0; i < mats.Length; i++ )
                            if ( mats[i].HasProperty( MaterialPropertyName ) )
                                startColors[i] = mats[i].GetColor( MaterialPropertyName );
                        RenderersTable.Add( r, startColors );
                    }
                }
            }

            if ( graphics != null )
            {
                foreach ( UnityEngine.UI.Graphic g in graphics )
                {
                    GraphicsTable.Add( g, g.color );
                }
            }

        }

        #endregion

        protected virtual void CheckForPropertyChanges()
        {
            if ( UseSharedMaterials != useSharedMaterials )
                OnUseSharedMaterialsChanged();

            if ( AnimateChildren != animateChildren )
                OnAnimateChildrenChanged();

            if ( excludeParticleSystems != ExcludeParticleSystems )
                OnExcludeParticleSystemChanged();

            if ( MaterialProperty != currentMaterialProperty )
            {
                ResetColors();

                currentMaterialProperty = MaterialProperty;
                MaterialPropertyName = "";

                if ( OnMaterialPropertyChanged != null )
                {
                    OnMaterialPropertyChanged.Invoke();
                }
            }
            else
            {
                if ( MaterialPropertyName != currentMaterialPropertyName )
                {
                    ResetColors();

                    currentMaterialPropertyName = MaterialPropertyName;

                    if ( OnMaterialPropertyChanged != null )
                    {
                        OnMaterialPropertyChanged.Invoke();
                    }
                }
            }
        }
    }

}
