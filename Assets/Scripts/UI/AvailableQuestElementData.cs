using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Descension
{
    [CreateAssetMenu(menuName = "Descension/Available Quest Element Data")]
    public class AvailableQuestElementData : ScriptableObject
    {
        [Header("Image Data")]
        public Sprite background;
        public ColorBlock buttonColors;

        [Header("Sound Data")]
        public string clickSound;
    }
}
