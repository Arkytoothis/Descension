using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Descension
{
    [CreateAssetMenu(menuName = "Descension/Button Data")]
    public class ButtonData : ScriptableObject
    {
        [Header("Image Data")]
        public Sprite background;
        public Sprite icon;
        public ColorBlock buttonColors;
        public string text;

        [Header("Sound Data")]
        public string clickSound;
    }
}
