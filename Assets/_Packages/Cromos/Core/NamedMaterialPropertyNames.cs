/********************************************************************
	created:	2016/04/22
	file base:	NamedMaterialPropertyNames
	file ext:	cs
	author:		Alessandro Maione
	version:	1.0.4
	purpose:	enumeration of common material color properties
*********************************************************************/
namespace Cromos
{
    public enum NamedMaterialPropertyNames
    {
        /// <summary>
        /// The main color of a material. Used by default and not required for Color methods to work in iTween.
        /// </summary>
        _Color,
        /// <summary>
        /// The specular color of a material (used in specular/glossy/vertexlit shaders).
        /// </summary>
        _SpecColor,
        /// <summary>
        /// The emissive color of a material (used in vertexlit shaders).
        /// </summary>
        _EmissionColor,
        /// <summary>
        /// The reflection color of the material (used in reflective shaders).
        /// </summary>
        _ReflectColor,
        //  [11/13/2013 alexandros]
        /// <summary>
        /// tint color used mainly in particle shaders
        /// </summary>
        _TintColor,

        Custom/*,

    NumberOfNamedMaterialPropertyNames*/

    }
}
