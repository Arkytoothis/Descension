/********************************************************************
	created:	2016/03/09
	file base:	FlipMesh
	file ext:	cs
	author:		Alessandro Maione
	version:	1.0.2
	purpose:	util class to flip the normals of a mesh
*********************************************************************/
using UnityEngine;

namespace Cromos
{

    /// <summary>
    /// util component to flip mesh normals
    /// </summary>
    public class FlipMesh : MonoBehaviour
    {
        public bool FlipOnStart = true;

        void Start()
        {
            if ( FlipOnStart )
            {
                MeshFilter filter = GetComponent( typeof( MeshFilter ) ) as MeshFilter;
                Flip( filter );
            }
        }

        /// <summary>
        /// flips the normals of the mesh
        /// </summary>
        /// <param name="filter">mesh to flip</param>
        public static void Flip( MeshFilter filter )
        {
            if ( filter != null )
            {
                Mesh mesh = filter.mesh;

                Vector3[] normals = mesh.normals;
                for ( int i = 0; i < normals.Length; i++ )
                    normals[i] = -normals[i];
                mesh.normals = normals;

                for ( int m = 0; m < mesh.subMeshCount; m++ )
                {
                    int[] triangles = mesh.GetTriangles( m );
                    for ( int i = 0; i < triangles.Length; i += 3 )
                    {
                        int temp = triangles[i + 0];
                        triangles[i + 0] = triangles[i + 1];
                        triangles[i + 1] = temp;
                    }
                    mesh.SetTriangles( triangles, m );
                }
            }
        }

    }

}
