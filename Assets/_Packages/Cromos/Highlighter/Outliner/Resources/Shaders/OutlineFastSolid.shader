// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/********************************************************************
created:	2016/09/30
file base:	Outliner/Post Outline
file ext:	shader
author:		Alessandro Maione
version:	1.7.0
purpose:	draws outline around non-black silhouettes rendered through DrawSilhouettes shader
*********************************************************************/
Shader"Outliner/Outline Fast Solid"
{
	Properties
	{
		_MainTex("Main render texture",2D) = "white"{}
		_SecondTex("Second render texture",2D) = "white"{}
		_Thickness("Outline thickness", Range(0.0, 50.0)) = 10.0
		_MinUV("_MinUV", Vector) = (0,0,0,0)
		_MaxUV("_MaxUV", Vector) = (1,1,1,1)
		//_Ratio("Outline ratio", Range(0.0, 1)) = 1
	}

	CGINCLUDE
	//#pragma target 3.0
	#include "UnityCG.cginc"
	#include "./CGIncludes/Outline.cginc"
	
	sampler2D _MainTex;
	float2 _MainTex_TexelSize;
	sampler2D _SecondTex;
	float2 _SecondTex_TexelSize;
	half _Thickness;
	float4 _MinUV;
	float4 _MaxUV;

	struct v2f
	{
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

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

	// PASS 4 - Fast Solid
	// _MainTex: source image
	// _SecondTex: image of object seen by outline camera as white silhouettes
	half4 frag(v2f i) : SV_Target
	{
		half4 result;
		float2 uv = i.uv.xy;
		half4 mainColor = tex2D(_MainTex, uv);
		float4 silhouetteColor = tex2D(_SecondTex, uv);

		float useMainColor = step( checkUVBounds(_MinUV, _MaxUV, uv), 0 );  //UV outside uv margins?
		useMainColor += any(silhouetteColor);
		useMainColor += step(_Thickness, 0);

		/*if (useMainColor)
			return 1;
		else
			return 0;*/

		if ( useMainColor > 0 )
		{
			result = mainColor;
		}
		else
		{
			//return half4(1,1,0,1);
			silhouetteColor = getNeighborsSumOblique(_SecondTex, _SecondTex_TexelSize, uv, _Thickness, _Thickness );
			//silhouetteColor /= silhouetteColor.a;
			if (silhouetteColor.a > 0)
			{
				silhouetteColor /= silhouetteColor.a;
				result.rgb = silhouetteColor.rgb;
				result.a = 1;
			}
			else
			{
				silhouetteColor = getNeighborsSumHV( _SecondTex, _SecondTex_TexelSize, uv, _Thickness, _Thickness );
				//silhouetteColor /= silhouetteColor.a;
				if (silhouetteColor.a > 0)
				{
					silhouetteColor /= silhouetteColor.a;
					result.rgb = silhouetteColor.rgb;
					result.a = 1;
				}
				else
					result = mainColor;
			}
		}

		return result;
	}




	ENDCG


	SubShader
	{
		//NEVER ANABLE IT AGAIN! IT COUSES DIRTY RENDER TEXTURES
		//Blend SrcAlpha OneMinusSrcAlpha
		/*Blend Off
		ZTest Always
		Cull Off
		ZWrite Off
		Fog{ Mode Off }*/
		ZTest Always Cull Off ZWrite Off

		// 4 - Fast Solid pass
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
		// end pass 4 - Fast Solid

	}

}
//end shader
