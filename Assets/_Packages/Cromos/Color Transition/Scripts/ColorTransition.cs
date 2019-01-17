/********************************************************************
	created:	2016/04/22
	file base:	ColorTransition
	file ext:	cs
	author:		Alessandro Maione
	version:	1.6.7
	purpose:	color transition manager
*********************************************************************/
using UnityEngine;


namespace Cromos
{
    /// <summary>
    /// color transition manager
    /// </summary>
    public class ColorTransition : ColorAnimator
    {

        protected override void Update()
        {
            CheckForPropertyChanges();

            if ( ( !InProgress ) )
                return;

            if ( ( RenderersTable.Count == 0 ) && ( GraphicsTable.Count == 0 ) )
                DetectRenderers();

            if ( !RenderersHaveProperty( MaterialPropertyName ) )
                return;

            if ( !ManagePropertyAnimation() )
                ManageColorTransition();
        }

        #region STATIC DO TRANSITIONs

        /// <summary>
        /// Adds a Color Transition component on a game object that's populated with the parameters passed to the method. Then the animation starts automatically. 
        /// </summary>
        /// <param name="go">game object where to add the Color Transition component</param>
        /// <param name="colors">colors of the animation to create</param>
        /// <param name="affectChildren">if true, animation will be propagated to all the hierarchy of the go game object</param>
        /// <param name="delay">time to wait before starting the animation</param>
        /// <returns>the Color Transition component created</returns>
        public static ColorTransition DoTransition( GameObject go, GradientColorKey[] colors, bool affectChildren = true, float delay = 0 )
        {
            return DoTransition<ColorTransition>( go, colors, affectChildren, delay );
        }

        /// <summary>
        /// Adds a Color Transition base-type component on a game object that's populated with the parameters passed to the method. Then the animation starts automatically. 
        /// </summary>
        /// <typeparam name="T">type of component to create. The type should be ColorTransition or a subtype</typeparam>
        /// <param name="go">game object where to add the Color Transition component</param>
        /// <param name="colors">colors of the animation to create</param>
        /// <param name="affectChildren">if true, animation will be propagated to all the hierarchy of the go game object</param>
        /// <param name="delay">time to wait before starting the animation</param>
        /// <returns>the Color Transition base-type component created</returns>
        public static T DoTransition<T>( GameObject go, GradientColorKey[] colors, bool affectChildren = true, float delay = 0 ) where T : ColorTransition
        {
            return DoTransition<T>( go, colors, null, affectChildren, delay ) as T;
        }

        /// <summary>
        /// Adds a Color Transition component on a game object that's populated with the parameters passed to the method. Then the animation starts automatically. 
        /// </summary>
        /// <param name="go">game object where to add the Color Transition component</param>
        /// <param name="colors">colors of the animation to create</param>
        /// <param name="alphas">alphas of the animation to create</param>
        /// <param name="affectChildren">if true, animation will be propagated to all the hierarchy of the go game object</param>
        /// <param name="delay">time to wait before starting the animation</param>
        /// <returns>the Color Transition component created</returns>
        public static ColorTransition DoTransition( GameObject go, GradientColorKey[] colors, GradientAlphaKey[] alphas, bool affectChildren = true, float delay = 0 )
        {
            return DoTransition<ColorTransition>( go, colors, alphas, affectChildren, delay );
        }

        /// <summary>
        /// Adds a Color Transition component on a game object that's populated with the parameters passed to the method. Then the animation starts automatically. 
        /// </summary>
        /// <param name="go">game object where to add the Color Transition component</param>
        /// <param name="colors">colors of the animation to create</param>
        /// <param name="alphas">alphas of the animation to create</param>
        /// <param name="affectChildren">if true, animation will be propagated to all the hierarchy of the go game object</param>
        /// <param name="delay">time to wait before starting the animation</param>
        /// <returns>the Color Transition component created</returns>
        public static T DoTransition<T>( GameObject go, GradientColorKey[] colors, GradientAlphaKey[] alphas, bool affectChildren = true, float delay = 0 ) where T : ColorTransition
        {
            if ( !go )
            {
                go = GameObject.CreatePrimitive( PrimitiveType.Sphere );
                go.name = typeof( T ).Name;
            }

            ColorTransition res = go.GetComponent<ColorTransition>();
            if ( !res ) res = go.AddComponent<T>() as T;

            if ( res.Colors == null ) res.Colors = new Gradient();
            if ( colors != null ) res.Colors.colorKeys = colors;
            if ( alphas != null ) res.Colors.alphaKeys = alphas;

            res.StartTransition();

            return res as T;
        }

        #endregion

        #region COLOR MANAGEMENT

        private void ManageColorTransition()
        {
            if ( Time.time - startTime >= Delay )
                AssignColor( CurrentColor, Target, MixMode );
        }

        private bool RenderersHaveProperty( string propertyName )
        {
            bool res = true;
            bool anyRenderers = true;
            bool anyGraphics = true;

            //checking if any renderers is present
            if ( RenderersTable == null )
                anyRenderers = false;
            else
            {
                if ( RenderersTable.Count == 0 )
                    anyRenderers = false;
            }

            //checking if any graphics is present
            if ( GraphicsTable == null )
                anyGraphics = false;
            else
            {
                if ( GraphicsTable.Count == 0 )
                    anyGraphics = false;
            }

            // if no renderers and no graphics is present, the result will be always false
            if ( ( !anyRenderers ) && ( !anyGraphics ) )
                res = false;

            //if any graphics or renderers
            if ( res )
            {
                if ( RenderersTable != null )
                {
                    Material mat = null;
                    foreach ( Renderer r in RenderersTable.Keys )
                    {
                        mat = UseSharedMaterials ? r.sharedMaterial : r.material;
                        res = mat.HasProperty( MaterialPropertyName );
                        if ( res )
                            break;
                    }
                }
            }

            return res;
        }

        #endregion

    }

}

//LOW: gestire anche eventi arbitrari, temporizzati. Evento il cui parametro è il tempo normalizzato in cui deve essere innescato

