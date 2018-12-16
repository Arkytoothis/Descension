/// <summary>
/// AutoSave.cs, editor helper script. Saves the scene ans modified assets when you hit Play in the Unity Editor, so if Unity should 
/// crash, or similar, you don't lose any scene or assets you haddn't saved before hitting Play.
/// </summary>
/// 
/// <remarks>
/// Created (date - name): 10th March 2015 - Gavin Thornton
/// Modified (date - name): 8th Jan 2017 - Gavin Thornton - #if UNITY_EDITOR moved to fix cloud build
/// Modified (date - name): 30th March 2017 - Gavin Thornton - line "EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());" changed to multi scene version "EditorSceneManager.SaveOpenScenes();"
/// </remarks>

#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]

#if false                           // Change this to "#if true" if you want to disble the script temporarily (without having to delete it)
public class OnUnityLoad
{
    static OnUnityLoad()
    {
        EditorApplication.playmodeStateChanged = () =>
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
                Debug.Log("*** WARNING:Auto-Saving DISABLED ***" + EditorApplication.currentScene);
        };
    }
}
#else

public class OnUnityLoad
{
    static OnUnityLoad()
    {
        EditorApplication.playModeStateChanged += Save;
    }

    static void Save(PlayModeStateChange state)
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
        {
            // Unity 5+
            //Debug.Log("Auto-saving scene and any asset edits" + EditorSceneManager.GetActiveScene());
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
        }
    }
}

#endif
#endif  // #if UNITY_EDITOR