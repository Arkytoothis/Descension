// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/********************************************************************
created:	2016/09/30
file base:	Outliner/DrawSilhouettes
file ext:	shader
author:		Alessandro Maione
version:	1.1.1
purpose:	It just draws the object as white, and has the "Outline" tag
*********************************************************************/
Shader "Outliner/Draw Silhouettes"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "" {}
		_Color("Silhouette Color", Color) = (1, 1, 1, 1)
	}

		CGINCLUDE
		#pragma target 3.0
		#include "UnityCG.cginc"

		struct v2f
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
		};


		sampler2D _MainTex;
		float2 _MainTex_TexelSize;
		fixed4 _Color;

		v2f vert(appdata_img v)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;
			
			/*#ifdef UNITY_HALF_TEXEL_OFFSET
			v.texcoord.y += _MainTex_TexelSize.y;
			#endif*/
			#if UNITY_UV_STARTS_AT_TOP
				if (_MainTex_TexelSize.y < 0)
					o.uv.y = 1.0 - o.uv.y;
			#endif
			
			return o;
		}

		half4 frag(v2f i) : SV_Target
		{
			return any(tex2D(_MainTex, i.uv)) ? _Color : half4(0,0,0,0);
		//		half3 col = tex2D(_MainTex, i.uv);
		//		half val = col.r + col.g + col.b;
		//		return (val > 0)? 1 : 0;
		}

			ENDCG

			Subshader
		{
			Pass
			{
				  ZTest Always Cull Off ZWrite Off

				  CGPROGRAM
				  #pragma vertex vert
				  #pragma fragment frag
				  ENDCG
			}
		}

}
