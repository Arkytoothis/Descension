/********************************************************************
	created:	2016/12/15
	file base:	ColorTransitionEditor
	file ext:	cs
	author:		Alessandro Maione
	version:	1.2.8
	
	purpose:	custom editor for ColorTransition component
*********************************************************************/
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Cromos
{
    /// <summary>
    /// custom editor for ColorTransition component
    /// </summary>
    [CustomEditor( typeof( ColorTransition ) )]
    [CanEditMultipleObjects]
    public class ColorTransitionEditor : Editor
    {
        private List<SerializedProperty> allProperties = new List<SerializedProperty>();
        private bool viewDebug = false;

        void OnEnable()
        {
            allProperties.Clear();
            AddProperty( "Colors" );
            AddProperty( "UseSharedMaterials" );
            AddProperty( "Target" );
            AddProperty( "MixMode" );
            AddProperty( "MaterialProperty" );
            AddProperty( "MaterialPropertyName" );
            AddProperty( "Duration" );
            AddProperty( "Delay" );
            AddProperty( "LoopMode" );
            AddProperty( "WrapMode" ); //wrap mode deve essere visibile solo se loop mode è "Play Once"
            //TODO: to be added
            //AddProperty( "Reversed" );
            AddProperty( "AnimateChildren" );
            AddProperty( "ExcludeParticleSystems" );
            AddProperty( "StartTransitionMode" );
            AddProperty( "AfterTransition" );
            AddProperty( "InProgress" );
            AddProperty( "AnimationComplete" );
            AddProperty( "CurrentColorEditor" );
            AddProperty( "OnStart" );
            AddProperty( "OnEnd" );
        }

        private bool AddProperty( string propertyName )
        {
            SerializedProperty prop = serializedObject.FindProperty( propertyName );
            if ( prop != null )
                allProperties.Add( prop );
            else
                Debug.LogWarning( "property " + propertyName + " not found in " + target.GetType() );

            return ( prop != null );
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            foreach ( SerializedProperty prop in allProperties )
                EditorGUILayout.PropertyField( prop );

            viewDebug = EditorGUILayout.Foldout( viewDebug, "Debug info" );
            if ( viewDebug )
            {
                ColorAnimator colorAnim = (ColorAnimator)target;
                if ( colorAnim ) EditorGUILayout.ColorField( "current color", colorAnim.CurrentColor );
                EditorGUILayout.FloatField( "elapsed time", colorAnim.ElapsedTime );
                EditorGUILayout.Slider( "elapsed normalized time", colorAnim.ElapsedTimeNormalized, 0, 1 );
                EditorGUILayout.Toggle( "reversed", colorAnim.Reversed );
            }

            serializedObject.ApplyModifiedProperties();
        }

    }

}
