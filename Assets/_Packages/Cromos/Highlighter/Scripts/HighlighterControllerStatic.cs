/********************************************************************
	created:	2018/06/19
	file base:	HighlighterControllerStatic
	file ext:	cs
	author:		Alessandro Maione
	version:	1.0.0
	
	purpose:	sample component to test static methods Highlight and dehighlight of Highlighter component over multiple objects.
*********************************************************************/
using UnityEngine;


namespace Cromos
{
    /// <summary>
    /// sample component to test static methods Highlight and dehighlight of Highlighter component over multiple objects.
    /// </summary>
    public class HighlighterControllerStatic : MonoBehaviour
    {
        public GameObject[] Targets = new GameObject[0];
        [Header( "Highlight Parameters" )]
        public HighlightParameters Parameters = new HighlightParameters();

        private bool toggle = false;

        private void Start()
        {
            AlternateHighlight();
        }

        public void AlternateHighlight()
        {
            toggle = !toggle;

            int halfLength = Targets.Length / 2;

            for ( int i = 0; i < Targets.Length; i++ )
            {
                if ( toggle )
                {
                    if ( i < halfLength )
                        Highlighter.DeHighlight( Targets[i] );
                    else
                        Highlighter.Highlight( Targets[i], Parameters );
                }
                else
                {
                    if ( i >= halfLength )
                        Highlighter.DeHighlight( Targets[i] );
                    else
                        Highlighter.Highlight( Targets[i], Parameters );
                }
            }
        }

    }
}

//HIGH TODO: finire l'implementazione per questo e per ColorTransition

