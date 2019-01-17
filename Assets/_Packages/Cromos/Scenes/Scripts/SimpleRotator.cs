using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    public Vector3 Rotation;

    void Update()
    {
        transform.Rotate( Rotation * Time.deltaTime );
    }

}
