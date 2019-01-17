/********************************************************************
	created:	2018/02/02
	file base:	CommandBufferGroupItem
	file ext:	cs
	author:		Alessandro Maione
	version:	1.1.3
	
	purpose:	abstraction for specialized command buffer that draws silhouettes of outline objects of a given outline mode
*********************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Cromos
{
    /// <summary>
    /// abstraction for specialized command buffer that draws silhouettes of outline objects of a given outline mode
    /// </summary>
    public class CommandBufferGroupItem
    {
        private static int silhouetteColorID = Shader.PropertyToID( "_Color" );

        public CommandBuffer CommandBuffer
        {
            get;
            private set;
        }
        public List<Renderer> Renderers
        {
            get;
            private set;
        }
        public List<Material> Materials
        {
            get;
            private set;
        }
        public int RenderTextureID
        {
            get;
            private set;
        }
        public bool ClearOnUpdate
        {
            get;
            set;
        }
        public bool Used
        {
            get
            {
                if ( Renderers != null )
                    return ( Renderers.Count > 0 );

                return false;
            }
        }

        private int materialCounter = 0;
        private Camera attachedCamera = null;
        private OutlineModes mode;
        private int tmpRenderTextureID = 0;
        private bool renderTextureCreated = false;


        public CommandBufferGroupItem( OutlineModes outlineMode, Camera cam )
        {
            mode = outlineMode;
            attachedCamera = cam;
            tmpRenderTextureID = Shader.PropertyToID( "_SilhouetteTempRT" + mode.ToString() );
            RenderTextureID = Shader.PropertyToID( "_SilhouetteRT" + mode.ToString() );
            ClearOnUpdate = true;
        }

        public void Clear()
        {
            if ( Renderers != null )
            {
                Renderers.Clear();
                Renderers = null;
            }

            if ( Materials != null )
            {
                for ( int i = 0; i < Materials.Count; i++ )
                    Object.Destroy( Materials[i] );
                Materials.Clear();
                Materials = null;
            }

            if ( renderTextureCreated )
            {
                CommandBuffer.ReleaseTemporaryRT( tmpRenderTextureID );
                renderTextureCreated = false;
            }
            else
            {
                if ( CommandBuffer != null )
                {
                    attachedCamera.RemoveCommandBuffer( CameraEvent.BeforeImageEffects, CommandBuffer );
                    CommandBuffer.Dispose();
                    CommandBuffer = null;
                }
            }

            //TODO: resettare anche la RenderTexture? 
        }

        public void BeginUpdate()
        {
            if ( !ClearOnUpdate )
                return;

            ClearOnUpdate = false;

            if ( CommandBuffer == null )
            {
                CommandBuffer = new CommandBuffer();
                CommandBuffer.name = "Draw Silhouettes " + mode.ToString();
                attachedCamera.AddCommandBuffer( CameraEvent.BeforeImageEffects, CommandBuffer );
            }
            else
                CommandBuffer.Clear();

            CommandBuffer.GetTemporaryRT( tmpRenderTextureID, -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1, false );
            renderTextureCreated = true;
            CommandBuffer.SetRenderTarget( tmpRenderTextureID );

            //commandBuffer.ClearRenderTarget( true, true, Color.green );
            CommandBuffer.ClearRenderTarget( true, true, Color.clear );

            if ( Renderers == null )
                Renderers = new List<Renderer>();
            Renderers.Clear();

            if ( Materials == null )
                Materials = new List<Material>();


            //             for ( int i = 0; i < Materials.Count; i++ )
            //                 Object.Destroy( Materials[i] );
            // 
            // 
            //             Materials.Clear();

            materialCounter = 0;
        }

        public void AddRenderingSteps( Renderer[] renderers, Color outlineColor, int thickness, Shader drawSilhouettesShader )
        {
            if ( ( renderers == null ) || ( renderers.Length == 0 ) )
            {
                Debug.LogError( "target to be rendered not set" );
                return;
            }

            Material currentMat = null;
            if ( materialCounter >= Materials.Count )
            {
                currentMat = new Material( drawSilhouettesShader );
                Materials.Add( currentMat );
            }
            else
                currentMat = Materials[materialCounter];

            materialCounter++;

            currentMat.SetColor( silhouetteColorID, OutlineInfo.Encode( outlineColor, thickness ) );

            int renderersStart = Renderers.Count;
            Renderers.AddRange( renderers );

            Renderer r = null;
            for ( int i = renderersStart; i < Renderers.Count; i++ )
            {
                r = Renderers[i];
                if ( r.isVisible )
                {
                    if ( r is SkinnedMeshRenderer )
                    {
                        SkinnedMeshRenderer skinned = (SkinnedMeshRenderer)r;
                        if ( skinned.sharedMesh )
                        {
                            for ( int c = 0; c < skinned.sharedMesh.subMeshCount; c++ )
                                CommandBuffer.DrawRenderer( r, currentMat, c, 0 );
                        }
                    }
                    else
                        CommandBuffer.DrawRenderer( r, currentMat, 0, 0 );
                }
            }
            //TODO: probabilmente Renderers può essere eliminato e può essere usato solo l'array argomento del metodo renderers
        }

        public void EndUpdate()
        {
            for ( int i = materialCounter; i < Materials.Count; i++ )
            {
                Object.Destroy( Materials[i] );
                Materials[i] = null;
            }

            CommandBuffer.SetGlobalTexture( RenderTextureID, tmpRenderTextureID );
        }

    }

}

