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
		#region Constantes
		/// <summary>
		/// <para>Key que contiene la ultima carpeta contenedora.</para>
		/// </summary>
		public const string carpetaContenedoraKey = "ImageToTextCCK";
		#endregion

		#region Variables Privadas
		/// <summary>
		/// <para>Direccion de la carpeta contenedora.</para>
		/// </summary>
		private string carpetaContenedora;
		/// <summary>
		/// <para>Contiene la preview de la imagen.</para>
		/// </summary>
		private Rect preImagen;
		#endregion

		#region Menu
		/// <summary>
		/// <para>Menu de <see cref="ImageToText"/>.</para>
		/// </summary>
		[MenuItem("MoonAntonio/Image2Text")]
		public static void Window()
		{
			// Crear window
			GetWindow(typeof(ImageToText), true, "Image2Text");
		}
		#endregion

		#region Eventos
		/// <summary>
		/// <para>Al habilitarse <see cref="ImageToText"/>.</para>
		/// </summary>
		public void OnEnable()
		{
			// Obtener valores
			minSize = new Vector2(300, 93);
			maxSize = new Vector2(4000, 93);
			carpetaContenedora = EditorPrefs.GetString(carpetaContenedoraKey);
			preImagen = new Rect(0, 2, 84, 84);
		}
		#endregion

	}
}
