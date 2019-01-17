using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Descension.Gui
{
#if (UNITY_EDITOR)
    public class GuiInstance : Editor
    {
        static GameObject clickedObject;

        [MenuItem("GameObject/Reclamation/Button", priority = 0)]
        public static void AddButton()
        {
            Create("Button");
        }

        [MenuItem("GameObject/Reclamation/Pc Button", priority = 0)]
        public static void AddPcButton()
        {
            Create("Pc Button");
        }

        [MenuItem("GameObject/Reclamation/Action Button", priority = 0)]
        public static void AddActionButton()
        {
            Create("Action Button");
        }

        [MenuItem("GameObject/Reclamation/List Panel", priority = 0)]
        public static void AddListPanel()
        {
            Create("List Panel");
        }

        [MenuItem("GameObject/Reclamation/Panel", priority = 0)]
        public static void AddPanel()
        {
            Create("Panel");
        }

        [MenuItem("GameObject/Reclamation/Attribute Element", priority = 0)]
        public static void AddAttributeElementPanel()
        {
            Create("Attribute Element");
        }

        private static GameObject Create(string objectName)
        {
            GameObject instance = Instantiate(Resources.Load<GameObject>("Gui/" + objectName));
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