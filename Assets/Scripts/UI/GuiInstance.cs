using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Descension
{
    #if (UNITY_EDITOR) 
    public class GuiInstance : Editor
    {
        static GameObject clickedObject;

        [MenuItem("GameObject/Descension/Generic Button", priority = 0)]
        public static void AddButton()
        {
            Create("Generic Button");
        }

        private static GameObject Create(string objectName)
        {
            GameObject instance = Instantiate(Resources.Load<GameObject>("UI/" + objectName));
            instance.name = objectName;
            clickedObject = UnityEditor.Selection.activeObject as GameObject;

            if (clickedObject != null)
            {
                instance.transform.SetParent(clickedObject.transform, false);
            }

            return instance;
        }
    }
    #endif
}