using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class PlayerSpawner : MonoBehaviour
    {
        public void SpawnPlayer()
        {
            Debug.Log("Spawning Player at " + transform.position.ToString());

            
        }
    }
}