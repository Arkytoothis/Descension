/********************************************************************
created:	2018/09/14
file base:	Outliner/DrawSilhouettes
file ext:	shader
author:		Alessandro Maione
version:	1.0.0
purpose:	commons constants and functions used for outline
*********************************************************************/

#define MAX_THICKNESS  20
#define GLOW_FACTOR 0.275
#define COS45 ((half) 0.70710678118654752440084436210485)
#define SIN45 ((half) 0.70710678118654752440084436210485)
//	#define cos22_5 ((half) 0.99997651217454865478849954406816)
//	#define cos67_5 ((half) 0.00685383828411102723918684180104)
//	#define sin22_5 ((half) 0.00685383828411102723918684180104)
//	#define sin67_5 ((half) 0.99997651217454865478849954406816)



//1.0/255.0 
#define MIN_DELTA			((half) ((half)MAX_THICKNESS / 255.0))
#define THICKNESS_SCALER	(1.0 / (half)MAX_THICKNESS)

//(half)MAX_THICKNESS)
//MIN_DELTA 


inline half4 getNeighborsSumHV(sampler2D tex, float2 texelSize, float2 uv, half distance, half thickness)
{
	half normThickness = thickness * THICKNESS_SCALER;
	float distTexSizeX = distance * texelSize.x;
	float distTexSizeY = distance * texelSize.y;

	//N
	half4 n = tex2D(tex, uv + float2(0, distTexSizeY));
	n *= step(abs(n.a - normThickness), MIN_DELTA);
	n.a = (n.a > 0);

	//S
	half4 s = tex2D(tex, uv + float2(0, -distTexSizeY));
	s *= step(abs(s.a - normThickness), MIN_DELTA);
	s.a = (s.a > 0);

	//W
	half4 w = tex2D(tex, uv + float2(-distTexSizeX, 0));
	w *= step(abs(w.a - normThickness), MIN_DELTA);
	w.a = (w.a > 0);

	//E
	half4 e = tex2D(tex, uv + float2(distTexSizeX, 0));
	e *= step(abs(e.a - normThickness), MIN_DELTA);
	e.a = (e.a > 0);

	return n + s + w + e;
}

inline half4 getNeighborsSumOblique(sampler2D tex, float2 texelSize, float2 uv, half distance, half thickness)
{
	half normThickness = thickness * THICKNESS_SCALER;
	float distCos45TexSizeX = distance * COS45 * texelSize.x;
	float distSin45TexSizeY = distance * SIN45 * texelSize.y;

	//NW
	half4 nw = tex2D(tex, uv + float2(-distCos45TexSizeX, distSin45TexSizeY));
	nw *= step(abs(nw.a - normThickness), MIN_DELTA);
	nw.a = (nw.a > 0);

	//NE
	half4 ne = tex2D(tex, uv + float2(distCos45TexSizeX, distSin45TexSizeY));
	ne *= step(abs(ne.a - normThickness), MIN_DELTA);
	ne.a = (ne.a > 0);

	//SW
	half4 sw = tex2D(tex, uv + float2(-distCos45TexSizeX, -distSin45TexSizeY));
	sw *= step(abs(sw.a - normThickness), MIN_DELTA);
	sw.a = (sw.a > 0);

	//SE
	half4 se = tex2D(tex, uv + float2(distCos45TexSizeX, -distSin45TexSizeY));
	se *= step(abs(se.a - normThickness), MIN_DELTA);
	se.a = (se.a > 0);

	return nw + ne + sw + se;
	/*
	//NW
	tex2D(_SecondTex, uv + float2(-distance * cos45 * TX_x, distance * sin45 * TX_y)) +
	//NE
	tex2D(_SecondTex, uv + float2(distance * cos45 * TX_x, distance * sin45 * TX_y)) +
	//SW
	tex2D(_SecondTex, uv + float2(-distance * cos45 * TX_x, -distance * sin45 * TX_y)) +
	//SE
	tex2D(_SecondTex, uv + float2(distance * cos45 * TX_x, -distance * sin45 * TX_y)) +
	//NNW
	tex2D(_SecondTex, uv + float2(-distance * cos67_5 * TX_x, distance * sin67_5 * TX_y)) +
	//NNE
	tex2D(_SecondTex, uv + float2(distance * cos67_5 * TX_x, distance * sin67_5 * TX_y)) +
	//ENE
	tex2D(_SecondTex, uv + float2(distance * cos22_5 * TX_x, distance * sin22_5 * TX_y)) +
	//ESE
	tex2D(_SecondTex, uv + float2(distance * cos22_5 * TX_x, -distance * sin22_5 * TX_y)) +
	//SSE
	tex2D(_SecondTex, uv + float2(distance * cos67_5 * TX_x, -distance * sin67_5 * TX_y)) +
	//SSW
	tex2D(_SecondTex, uv + float2(-distance * cos67_5 * TX_x, -distance * sin67_5 * TX_y)) +
	//WSW
	tex2D(_SecondTex, uv + float2(-distance * cos22_5 * TX_x, -distance * sin22_5 * TX_y)) +
	//WSW
	tex2D(_SecondTex, uv + float2(-distance * cos22_5 * TX_x, distance * sin22_5 * TX_y));
	*/
}

inline half4 getNeighborsSumHorizontal(sampler2D tex, /*float2 texelSize,*/ float2 uv, half deltaX, half distance, half thickness)
{
	half normThickness = thickness * THICKNESS_SCALER; /// 255.0;
	float distanceDeltaX = distance * deltaX;

	//W
	half4 w = tex2D(tex, uv + float2(-distanceDeltaX, 0));
	w *= step(abs(w.a - normThickness), MIN_DELTA);
	w.a = (w.a > 0);

	//E
	half4 e = tex2D(tex, uv + float2(distanceDeltaX, 0));
	e *= step(abs(e.a - normThickness), MIN_DELTA);
	e.a = (e.a > 0);

	return w + e;
}

inline half4 getNeighborsSumVertical(sampler2D tex, /*float2 texelSize,*/ float2 uv, half deltaY, half distance, half thickness)
{
	half normThickness = thickness * THICKNESS_SCALER;
	float distanceDeltaY = distance * deltaY;

	//N
	half4 n = tex2D(tex, uv + float2(0, distanceDeltaY));
	n *= step(abs(n.a - normThickness), MIN_DELTA);
	n.a = (n.a > 0);

	//S
	half4 s = tex2D(tex, uv + float2(0, -distanceDeltaY));
	s *= step(abs(s.a - normThickness), MIN_DELTA);
	s.a = (s.a > 0);

	return n + s;
}

inline float checkUVBounds(float2 minUV, float2 maxUV, float2 val)
{
	return step(4, step(minUV.x, val.x) + step(minUV.y, val.y) + step(val.x, maxUV.x) + step(val.y, maxUV.y));
	//return ((val.x >= minUV.x) && (val.y >= minUV.y) && (val.x <= maxUV.x) && (val.y <= maxUV.y) );
}



// // PASS 6 - Flip
// half4 fragFlip(v2f i) : SV_Target
// {
// 	//#if UNITY_UV_STARTS_AT_TOP
// 	return tex2D(_MainTex, float2(i.uv.x, 1 - i.uv.y));
// //#else
// //			return tex2D(_MainTex, i.uv);
// //#endif
// }