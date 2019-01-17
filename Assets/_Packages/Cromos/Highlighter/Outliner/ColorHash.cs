/********************************************************************
	created:	2018/01/09
	file base:	ColorHash
	file ext:	cs
	author:		Alessandro Maione
	version:	1.2.0
	
	purpose:	fast encoding and decoding of in color struct for cromos
*********************************************************************/
using UnityEngine;

namespace Cromos
{
    public struct OutlineInfo
    {
        private static readonly float alphaEncode = 255.0f / OutlineTarget.MaxThickness;
        public OutlineModes Mode;
        public byte OutlineThickness;
        public Color32 OutlineColor;

        public OutlineInfo( OutlineModes mode, int thickness, Color color )
        {
            Mode = mode;
            OutlineThickness = (byte)thickness;
            OutlineColor = color;
        }

        public static Color32 Encode( Color32 color, int thickness )
        {
            Color32 res = color;
            res.a = (byte)( alphaEncode * thickness ); // (byte)( mode + (byte)thickness );

            return res;
        }

        public static OutlineInfo Decode( Color32 hash )
        {
            OutlineModes mode = GetMode( hash.a );
            byte outlineThickness = (byte)( hash.a - (byte)mode );
            hash.a = 255; // (byte)mode;

            return new OutlineInfo()
            {
                Mode = mode,
                OutlineThickness = outlineThickness,
                OutlineColor = hash
            };
        }

        public static OutlineModes GetMode( int a )
        {
            if ( ( a & (int)OutlineModes.AccurateGlow ) == (int)OutlineModes.AccurateGlow )
                return OutlineModes.AccurateGlow;
            else
            {
                if ( ( a & (int)OutlineModes.AccurateSolid ) == (int)OutlineModes.AccurateSolid )
                    return OutlineModes.AccurateSolid;
                else
                {
                    if ( ( a & (int)OutlineModes.FastGlow ) == (int)OutlineModes.FastGlow )
                        return OutlineModes.FastGlow;
                    else
                        return OutlineModes.FastSolid;
                }
            }
        }

        public static OutlineModes GetMode( byte a )
        {
            if ( ( a & (byte)OutlineModes.AccurateGlow ) == (byte)OutlineModes.AccurateGlow )
                return OutlineModes.AccurateGlow;
            else
            {
                if ( ( a & (byte)OutlineModes.AccurateSolid ) == (byte)OutlineModes.AccurateSolid )
                    return OutlineModes.AccurateSolid;
                else
                {
                    if ( ( a & (byte)OutlineModes.FastGlow ) == (byte)OutlineModes.FastGlow )
                        return OutlineModes.FastGlow;
                    else
                        return OutlineModes.FastSolid;
                }
            }
        }

    }

    /// <summary>
    /// fast encoding and decoding of in color struct for cromos
    /// </summary>
    public static class ColorHash
    {
        private const byte UseFastGlow = 128;
        private const byte UseAccurateGlow = 128;

        public static bool Color32Equals( Color32 c1, Color32 c2 )
        {
            return ( c1.r == c2.r &&
                c1.g == c2.g &&
                c1.b == c2.b &&
                c1.a == c2.a );
        }

#if false
        #region COMMAND BUFFER KEY
        public struct CommandBufferKey
        {
            public OutlineModes Mode;
            public Color OutlineColor;
        }

        public static Color32 EncodeToCommandBufferKey( Color32 color, OutlineModes mode )
        {
            Color32 res = color;
            res.a = (byte)mode;

            return res;
        }

        public static CommandBufferKey DecodeCommandBufferKey( Color32 key )
        {
            OutlineModes mode = GetMode( key.a );
            key.a = 255;
            return new CommandBufferKey()
            {
                Mode = mode,
                OutlineColor = key
            };
        }
        #endregion
#endif
    }

}

