// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/********************************************************************
created:	2016/09/30
file base:	Outliner/Post Outline
file ext:	shader
author:		Alessandro Maione
version:	1.7.1
purpose:	draws outline around non-black silhouettes rendered through DrawSilhouettes shader
*********************************************************************/
Shader"Outliner/Outline Accurate Glow"
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
		//#if SHADER_API_D3D11 || SHADER_API_D3D11_9X || SHADER_API_XBOXONE || SHADER_API_D3D9
			if (_MainTex_TexelSize.y < 0)
				o.uv.y = 1.0 - o.uv.y;
		//#endif
		#endif

		return o;
	}
		
	// PASS 2 - Horizontal Accurate Glow
	// _MainTex:  image of object seen by outline camera as white silhouettes
	// _SecondTexture: unused
	half4 fragHorizontal(v2f i) : SV_Target
	{
		half4 result = half4(0,0,0,0);
		float2 uv = i.uv.xy;
		half4 silhouetteColor = tex2D(_MainTex, uv);

		//return silhouetteColor;

		float outside = step(checkUVBounds(_MinUV, _MaxUV, uv), 0);  //UV outside uv margins?

		if (outside > 0)
			return silhouetteColor;

		float useSilhouetteColor = (silhouetteColor.a > 0) ? 1 : 0;
		useSilhouetteColor += step(_Thickness, 0);

		if (useSilhouetteColor > 0)
		{
			result = silhouetteColor;
			result.a = 1;
			return result;
		}

		half inverseThickness = 1.0 / _Thickness;
		float2 deltax = float2(0,0);
		half4 increment;
		half incrementFactor = 0;

		//for every iteration we need to do horizontally
		//int k = 1;
		//[unroll(MAX_THICKNESS)] while ( k <= _Thickness)
		UNITY_UNROLL
		for (half k = 1; k <= MAX_THICKNESS; k++)
		{
			if (k > _Thickness)
				break;

			silhouetteColor = getNeighborsSumHorizontal(_MainTex, /*_MainTex_TexelSize,*/ uv, _MainTex_TexelSize.x, k, _Thickness);
			if (silhouetteColor.a > 0)
			{
				silhouetteColor /= silhouetteColor.a;
				incrementFactor = saturate((_Thickness - k - 1.0)*inverseThickness);
				//incrementFactor *= incrementFactor; // GLOW_FACTOR;

				deltax = float2(k * _MainTex_TexelSize.x, 0);
				increment = incrementFactor * silhouetteColor;

				result += any(tex2D(_MainTex, uv - deltax)) ? increment : half4(0, 0, 0, 0);
				result += any(tex2D(_MainTex, uv + deltax)) ? increment : half4(0, 0, 0, 0);
				break;
			}
		}
		result.a = min(result.a, 0.99);
		return saturate(result);
	}

	// PASS 3 - Vertical And Blend Accurate Glow
	// _MainTex: source: original complete image
	// _SecondTexture: horizontal blurred image from horizontal pass
	half4 fragVerticalAndBlend(v2f i) : SV_Target
	{
		float2 uv = i.uv.xy;
		half4 mainColor = tex2D(_MainTex, uv);
		half4 silhouetteColor = tex2D(_SecondTex, uv);
		float useMainColor = step(checkUVBounds(_MinUV, _MaxUV, uv), 0);  //UV outside uv margins?
		useMainColor += step(1, silhouetteColor.a);
		useMainColor += step(_Thickness, 0);

		/*if ((_Thickness <= 0) || (silhouetteColor.a >= 1) || (outside))
			return mainColor;*/
		if (useMainColor > 0)
			return mainColor;

		half4 result = silhouetteColor;
		float2 deltay;
		half lerpVal = 0;
		half4 up;
		half4 down;
		half inverseThickness = 1 / _Thickness;
		half incrementFactor = 0;
	
		//[unroll(MAX_THICKNESS)] while ( k <= _Thickness)
		UNITY_UNROLL
			for (half k = 1; k <= MAX_THICKNESS; k++)
			{
				if (k > _Thickness)
					break;
				
				incrementFactor = 1.0 - (k * inverseThickness);

				deltay = float2(0, (float)k * _SecondTex_TexelSize.y);
				up = tex2D(_SecondTex, uv + deltay);
				down = tex2D(_SecondTex, uv - deltay);
				
				result.rgb = max( max(result.rgb, up.rgb), down.rgb);
				lerpVal += (incrementFactor * (up.a + down.a));
			}
		
		lerpVal *= lerpVal;
		result.rgb = lerp(mainColor.rgb, result.rgb, saturate(lerpVal));
		
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

		// 2 - Horizontal Accurate Glow pass
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment fragHorizontal
			ENDCG
		}
		// end pass 2 - Horizontal Accurate Glow

		// 3 - Vertical And Blend Accurate Glow pass
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment fragVerticalAndBlend
			ENDCG
		}
		// end pass 3 - Vertical And Blend Accurate Glow pass

	}

}
//end shader
