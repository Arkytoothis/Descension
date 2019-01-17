/********************************************************************
	created:	2016/12/15
	file base:	HighlighterEditor
	file ext:	cs
	author:		Alessandro Maione
	version:	1.2.10
	
	purpose:	custom editor for Highlighter component
*********************************************************************/
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Cromos
{
    /// <summary>
    /// custom editor for Highlighter component
    /// </summary>
    [CustomEditor( typeof( Highlighter ) )]
    [CanEditMultipleObjects]
    public class HighlighterEditor : Editor
    {
        private List<SerializedProperty> allProperties = new List<SerializedProperty>();
        private bool viewDebug = false;
        private SerializedProperty mixModeProperty;
        private SerializedProperty outlineThicknessProperty;
        private SerializedProperty outlineModeProperty;


        void OnEnable()
        {
            allProperties.Clear();
            AddProperty( "Colors" );
            AddProperty( "UseSharedMaterials" );
            AddProperty( "Mode" );
            AddProperty( "Target" );
            mixModeProperty = AddProperty( "MixMode" );
            //TODO: to be deleted
            //AddProperty( "OverlapMode" );
            AddProperty( "MaterialProperty" );
            AddProperty( "MaterialPropertyName" );
            outlineModeProperty = AddProperty( "OutlineMode" );
            outlineThicknessProperty = AddProperty( "OutlineThickness" );
            AddProperty( "Duration" );
            AddProperty( "Delay" );
            AddProperty( "LoopMode" );
            AddProperty( "WrapMode" ); //wrap mode deve essere visibile solo se loop mode è "Play Once"
            //TODO: to be added
            //AddProperty( "Reversed" );
            AddProperty( "AnimateChildren" );
            //AddProperty( "ExcludeParticleSystems" );
            AddProperty( "StartTransitionMode" );
            AddProperty( "AfterTransition" );
            AddProperty( "InProgress" );
            AddProperty( "AnimationComplete" );
            //AddProperty( "CurrentColorEditor" );
            AddProperty( "OnHighlight" );
            AddProperty( "OnDeHighlight" );
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            Highlighter highlighter = (Highlighter)target;

            foreach ( SerializedProperty prop in allProperties )
            {
                bool showProperty = true;
                if ( prop == mixModeProperty && highlighter.Mode == HighlightTypes.Outline )
                    showProperty = false;
                else
                {
                    if ( ( prop == outlineThicknessProperty ) || ( prop == outlineModeProperty ) )
                    {
                        if ( highlighter.Mode == HighlightTypes.Color )
                            showProperty = false;
                    }

                    if ( showProperty )
                        EditorGUILayout.PropertyField( prop );
                }
            }

            viewDebug = EditorGUILayout.Foldout( viewDebug, "Debug info" );
            if ( viewDebug )
            {
                if ( highlighter != null ) EditorGUILayout.ColorField( "current color", highlighter.CurrentColor );
                EditorGUILayout.FloatField( "elapsed time", highlighter.ElapsedTime );
                EditorGUILayout.Slider( "elapsed normalized time", highlighter.ElapsedTimeNormalized, 0, 1 );
                EditorGUILayout.Toggle( "reversed", highlighter.Reversed );
            }

            serializedObject.ApplyModifiedProperties();
        }

        protected SerializedProperty AddProperty( string propertyName )
        {
            SerializedProperty prop = serializedObject.FindProperty( propertyName );
            if ( prop != null )
                allProperties.Add( prop );
            else
                Debug.LogWarning( "property " + propertyName + " not found in " + target.GetType() );

            return prop;
        }

    }

}
