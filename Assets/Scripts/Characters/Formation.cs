using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension.Core
{
    [System.Serializable]
    public class Formation
    {
        [SerializeField]
        private List<Vector2> positions;
        public List<Vector2> Positions { get { return positions; } }

        public Formation()
        {
            positions = new List<Vector2>();
        }

        public void AddPosition(Vector2 position)
        {
            positions.Add(position);
        }
    }
}