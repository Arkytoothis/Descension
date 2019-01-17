/********************************************************************
	created:	2018/11/29
	file base:	HighlightParameters
	file ext:	cs
	author:		Alessandro Maione
	version:	1.0.0
	
	purpose:	Highlight parameters
*********************************************************************/
using UnityEngine;

namespace Cromos
{
    /// <summary>
    /// Highlight parameters
    /// </summary>
    [System.Serializable]
    public class HighlightParameters
    {
        public Gradient ColorsAndAlphas = new Gradient();
        public bool UseSharedMaterials = false;
        public HighlightTypes Mode = HighlightTypes.ColorsAndOutline;
        public TransitionTargets Target = TransitionTargets.ColorAndAlpha;
        public ColorModes MixMode = ColorModes.Replace;
        public NamedMaterialPropertyNames MaterialProperty = NamedMaterialPropertyNames._Color;
        public string MaterialPropertyName = "";
        public OutlineModes OutlineMode = OutlineModes.FastSolid;
        public AnimationCurve OutlineThickness = AnimationCurve.Linear( 0, 4, 1, 4 );
        public float Duration = 3;
        public float Delay = 0;
        public TransitionLoopModes LoopMode = TransitionLoopModes.PingPong;
        public WrapModes WrapMode = WrapModes.Clamp;
        public bool AnimateChildren = true;
        public bool ExcludeParticleSystem = true;
        public PropertyStartupModes StartTransitionMode = PropertyStartupModes.OnStart;
        public AfterTransitionActions AfterTransition = AfterTransitionActions.DoNothing;
        public PropertyAnimatorEvent OnHighlight = null;
        public PropertyAnimatorEvent OnDeHighlight = null;
    }

}
