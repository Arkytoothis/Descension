using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Descension.Equipment;
using Descension.Core;
using UnityEngine.Animations;

namespace Descension.Characters
{
    public class CharacterRenderer : MonoBehaviour
    {
        [SerializeField] Transform pivot = null;
        public Transform Pivot { get { return pivot; } }

        [SerializeField] List<Transform> mounts = new List<Transform>();
        public List<Transform> Mounts { get { return mounts; } }

        [SerializeField] List<SkinnedMeshRenderer> meshes = new List<SkinnedMeshRenderer>();
    }
}