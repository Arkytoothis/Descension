// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/********************************************************************
created:	2016/09/30
file base:	Outliner/Post Outline
file ext:	shader
author:		Alessandro Maione
version:	1.7.1
purpose:	draws outline around non-black silhouettes rendered through DrawSilhouettes shader
*********************************************************************/
Shader"Outliner/Outline Fast Glow"
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
		
	// PASS 5 - Fast Glow
	// _MainTex: source image
	// _SecondTex: image of object seen by outline camera as white silhouettes
	half4 frag(v2f i) : SV_Target
	{
		float2 uv = i.uv.xy;
		half4 mainColor = tex2D(_MainTex, uv);
		half4 silhouetteColor = tex2D(_SecondTex, uv);
		half4 result = mainColor;
		float useMainColor = step(checkUVBounds(_MinUV, _MaxUV, uv), 0);  //UV outside uv margins?
		useMainColor += any(silhouetteColor);
		useMainColor += step(_Thickness, 0);

		if (useMainColor > 0)
			return mainColor;
		else
		{
			half lerpVal = 0;

			UNITY_UNROLL
			for (half k = 1; k <= MAX_THICKNESS; k++)
			{
				if (k > _Thickness)
					break;

				lerpVal = saturate(1 - ((k+1) / _Thickness));
				//lerpVal *= lerpVal;
				silhouetteColor = getNeighborsSumHV( _SecondTex, _SecondTex_TexelSize, uv, k, _Thickness );
				silhouetteColor /= silhouetteColor.a;
				if (silhouetteColor.a > 0)
				{
					result = lerp(mainColor, silhouetteColor, lerpVal);
					break;
				}
				else
				{
					silhouetteColor = getNeighborsSumOblique(_SecondTex, _SecondTex_TexelSize, uv, k, _Thickness );
					silhouetteColor /= silhouetteColor.a;
					if (silhouetteColor.a > 0)
					{
						result = lerp(mainColor, silhouetteColor, lerpVal);
						break;
					}
				}
			}
		}

		return result;
	}

	// PASS 6 - Flip
	half4 fragFlip(v2f i) : SV_Target
	{
		//#if UNITY_UV_STARTS_AT_TOP
		return tex2D(_MainTex, float2(i.uv.x, 1 - i.uv.y));
	//#else
	//			return tex2D(_MainTex, i.uv);
	//#endif
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


		// 5 - Fast Glow pass
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
		// end pass 5 - Fast Glow

	}

}
//end shader
