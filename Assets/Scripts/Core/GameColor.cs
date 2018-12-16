using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Core
{
    [System.Serializable]
    public class GameColor
    {
        public float Red;
        public float Green;
        public float Blue;
        public float Alpha;

        public GameColor()
        {
            Red = 1.0f;
            Green = 1.0f;
            Blue = 1.0f;
            Alpha = 1.0f;
        }

        public GameColor(GameColor color)
        {
            Red = color.Red;
            Green = color.Green;
            Blue = color.Blue;
            Alpha = color.Alpha;
        }

        public GameColor(Color color)
        {
            Red = color.r;
            Green = color.g;
            Blue = color.b;
            Alpha = color.a;
        }

        public Color GetColor()
        {
            return new Color(Red, Green, Blue, Alpha);
        }
    }
}