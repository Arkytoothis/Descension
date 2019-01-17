/********************************************************************
	created:	2018/09/14
	file base:	RenderTextureSettings
	file ext:	cs
	author:		Alessandro Maione
	version:	1.0.0
	
	purpose:	per-platform specific settings for render texture used for outline
*********************************************************************/
using UnityEngine;
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX || UNITY_IOS || UNITY_IPHONE || UNITY_TVOS
using UnityEngine.Rendering;
#endif

/// <summary>
/// per-platform specific settings for render texture used for outline
/// </summary>
public static class RenderTextureSettings
{
    public static int Depth =
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX || UNITY_IOS || UNITY_IPHONE || UNITY_TVOS
        SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal ? 24 : 0;
#else
        0;
#endif

    public static RenderTextureFormat Format = RenderTextureFormat.ARGB32;

}
