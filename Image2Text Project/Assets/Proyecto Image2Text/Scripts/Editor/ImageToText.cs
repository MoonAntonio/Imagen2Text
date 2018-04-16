//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// ImageToText.cs (16/04/2018)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Clase convertidora de una imagen en texto.					\\
// Fecha Mod:		16/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEditor;
#endregion

namespace MoonAntonio
{
	/// <summary>
	/// <para>Clase convertidora de una imagen en texto.</para>
	/// </summary>
	public class ImageToText : EditorWindow
	{
		#region Menu
		/// <summary>
		/// <para>Menu de <see cref="ImageToText"/>.</para>
		/// </summary>
		[MenuItem("MoonAntonio/Image2Text")]
		public static void Window()
		{
			GetWindow(typeof(ImageToText), true, "Image2Text");
		}
		#endregion

	}
}
