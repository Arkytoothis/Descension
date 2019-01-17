/********************************************************************
	created:	2018/03/20
	file base:	OutlineTargetLODRenderer
	file ext:	cs
	author:		Alessandro Maione
	version:	1.1.0
	
	purpose:	detects if a Renderer changes visibility  when outlining LOD objects, and communicates the change to OutlineTarget component
*********************************************************************/
using UnityEngine;

namespace Cromos
{
    /// <summary>
    /// detects if a Renderer changes visibility  when outlining LOD objects, and communicates the change to OutlineTarget component
    /// </summary>
    public class OutlineTargetLODRenderer : MonoBehaviour
    {
        public RendererVisibilityEvent OnVisibilityChanged = new RendererVisibilityEvent();

        public Mesh MyMesh
        {
            get;
            private set;
        }


        private void Awake()
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            if ( !meshFilter )
                throw new System.Exception( "mesh filter not found" );

            MyMesh = meshFilter.sharedMesh;
        }

        void OnBecameVisible()
        {
            if ( !MyMesh )
                return;

            if ( OnVisibilityChanged != null )
                OnVisibilityChanged.Invoke( this, true );
        }

        void OnBecameInvisible()
        {
            if ( !MyMesh )
                return;

            if ( OnVisibilityChanged != null )
                OnVisibilityChanged.Invoke( this, false );
        }

    }

}
