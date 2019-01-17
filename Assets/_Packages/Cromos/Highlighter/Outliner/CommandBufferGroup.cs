/********************************************************************
	created:	2018/02/02
	file base:	CommandBufferGroup
	file ext:	cs
	author:		Alessandro Maione
	version:	1.2.0
	
	purpose:	manages a group of command buffer specialized for each outline mode
*********************************************************************/
using System.Collections.Generic;
using UnityEngine;

namespace Cromos
{
    /// <summary>
    /// manages a group of command buffer specialized for each outline mode
    /// </summary>
    public class CommandBufferGroup
    {
        private Camera cam;
        private Shader drawSilhouettesShader;
        private CommandBufferGroupItem[] buffers = null;


        public CommandBufferGroup( int size, Camera cam, Shader drawSilhouettesShader )
        {
            buffers = new CommandBufferGroupItem[size];
            this.cam = cam;
            this.drawSilhouettesShader = drawSilhouettesShader;
        }

        public CommandBufferGroupItem GetCommandBufferItem( OutlineModes mode )
        {
            int idx = (int)mode;

            if ( buffers[idx] == null )
                buffers[idx] = new CommandBufferGroupItem( mode, cam );

            return buffers[idx];
        }

        public void BeginUpdate()
        {
            for ( int i = 0; i < buffers.Length; i++ )
                if ( buffers[i] != null )
                    buffers[i].ClearOnUpdate = true;
        }

        //         public void BeginUpdate()
        //         { }

        public void UpdateMode( OutlineModes mode, HashSet<OutlineTarget> targets )
        {
            CommandBufferGroupItem commandBuffer = GetCommandBufferItem( mode );
            commandBuffer.BeginUpdate();
            foreach ( OutlineTarget ot in targets )
            {
                Renderer[] otRenderers = null;
                if ( ot.AffectChildren )
                    otRenderers = ot.gameObject.GetComponentsInChildren<Renderer>();
                else
                    otRenderers = ot.GetComponents<Renderer>();

                if ( ot.ExcludeParticleSystems )
                    otRenderers = PurgeParticleSystemRenderers( otRenderers );
                commandBuffer.AddRenderingSteps( otRenderers, ot.OutlineColor, ot.Thickness, drawSilhouettesShader );
            }
            commandBuffer.EndUpdate();
        }

        private Renderer[] PurgeParticleSystemRenderers( Renderer[] otRenderers )
        {
            List<Renderer> result = new List<Renderer>( otRenderers.Length );
            for ( int i = 0; i < otRenderers.Length; i++ )
                if ( !( otRenderers[i] is ParticleSystemRenderer ) )
                    result.Add( otRenderers[i] );

            return result.ToArray();
        }

        public void EndUpdate()
        { }

        //         public void EndUpdate()
        //         {
        //             for ( int i = 0; i < Items.Length; i++ )
        //                 if ( !Items[i].Used )
        //                     Items[i].Clear();
        //         }

        public void Clear( OutlineModes mode )
        {
            int idx = (int)mode;

            if ( buffers[idx] != null )
                buffers[idx].Clear();
        }
    }

}
