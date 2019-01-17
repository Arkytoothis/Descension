/********************************************************************
created:	2015/10/19
file base:	FullscreenFade
file ext:	shader
author:		Alessandro Maione
version:    1.3.2

purpose:	Dissolve sphere used for fadein/out of the camera
*********************************************************************/

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/FullscreenFade"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
	}
		SubShader
	{
		Tags { "LightMode"="Always" "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" }
		Blend SrcAlpha OneMinusSrcAlpha
		Cull back Lighting Off ZWrite Off
		//LOD 2000

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			// vertex shader
			// this time instead of using "appdata" struct, just spell inputs manually,
			// and instead of returning v2f struct, also just return a single output
			// float4 clip position
			float4 vert(float4 vertex : POSITION) : SV_POSITION
			{
				return UnityObjectToClipPos(vertex);
			}

			// color from the material
			fixed4 _Color;

			// pixel shader, no inputs needed
			fixed4 frag() : SV_Target
			{
				return _Color; // just return it
			}
			ENDCG
		}
	}
}
