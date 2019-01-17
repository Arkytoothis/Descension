using UnityEngine;

namespace Cromos
{
    public class ChangeProperties : MonoBehaviour
    {
        public TextMesh label;

        protected ColorAnimator ca = null;

        private bool outlineTicknessToggle = false;


        protected virtual void Start()
        {
            UpdateLabel();
        }

        public virtual void ChangeTarget()
        {
            TransitionTargets target = ca.Target;
            int targetInt = (int)target;
            targetInt++;
            targetInt = targetInt % 3;
            ca.Target = (TransitionTargets)targetInt;

            UpdateLabel();
        }

        public virtual void ChangeMixMode()
        {
            ColorModes mix = ca.MixMode;
            int mixInt = (int)mix;
            mixInt++;
            mixInt = mixInt % 11;
            ca.MixMode = (ColorModes)mixInt;

            UpdateLabel();
        }

        public virtual void ChangeMaterialProperty()
        {
            NamedMaterialPropertyNames prop = ca.MaterialProperty;
            int propInt = (int)prop;
            propInt++;
            propInt = propInt % 5;
            ca.MaterialProperty = (NamedMaterialPropertyNames)propInt;

            UpdateLabel();
        }

        public virtual void AnimateChildrenToggle()
        {
            ca.AnimateChildren = !ca.AnimateChildren;

            UpdateLabel();
        }

        public virtual void ChangeHighlightMode()
        {
            Highlighter h = ca as Highlighter;
            if ( !h )
                return;

            HighlightTypes mode = h.Mode;
            int modeInt = (int)mode;
            modeInt++;
            modeInt = modeInt % 3;
            h.Mode = (HighlightTypes)( modeInt + 1 );
            if ( h.Mode == HighlightTypes.ColorsAndOutline || h.Mode == HighlightTypes.Outline )
                h.OutlineMode = OutlineModes.AccurateGlow;


            h.Highlight();

            UpdateLabel();
        }

        public virtual void AnimateOutlineThickToggle()
        {
            Highlighter h = ca as Highlighter;
            if ( !h )
                return;

            if ( outlineTicknessToggle )
                h.OutlineThickness = new AnimationCurve( new Keyframe[] { new Keyframe( 0, 10 ), new Keyframe( 1, 10 ) } );
            else
                h.OutlineThickness = new AnimationCurve( new Keyframe[] { new Keyframe( 0, 5 ), new Keyframe( 0.5f, 10 ), new Keyframe( 1, 5 ) } );
            outlineTicknessToggle = !outlineTicknessToggle;

            UpdateLabel();
        }

        public virtual void UpdateLabel()
        {
            if ( !label )
                return;

            string children = "";
            if ( !ca.AnimateChildren )
                children = "NOT ";

            string outline = "";
            if ( !outlineTicknessToggle )
                outline = "NOT ";

            Highlighter h = ca as Highlighter;

            label.text = ca.Target.ToString() + System.Environment.NewLine +
                ca.MixMode.ToString() + System.Environment.NewLine +
                ca.MaterialProperty + System.Environment.NewLine +
                children + "ANIMATING CHILDREN" + System.Environment.NewLine;

            if ( h )
            {
                label.text = h.Mode.ToString() + System.Environment.NewLine + label.text;
                label.text += ( outline + "ANIMATING OUTLINE THICKNESS" );
            }
        }
    }

}
