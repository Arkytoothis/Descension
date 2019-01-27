using UnityEngine;
using System.Collections;

namespace Descension
{
    public class MusicManager : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.instance.PlayMusic("Along the Journey", 3);
            }

        }
    }
}