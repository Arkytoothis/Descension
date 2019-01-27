using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Descension
{
    [CreateAssetMenu(menuName = "Descension/Pc List Element Data")]
    public class PcListElementData : ScriptableObject
    {
        [Header("Image Data")]
        public Sprite background;
        public Sprite icon;
        public ColorBlock buttonColors;

        [Header("Sound Data")]
        public string clickSound;
    }
}
