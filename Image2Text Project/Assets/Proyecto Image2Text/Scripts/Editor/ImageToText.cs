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
using System.IO;
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
		private string nombreArchivo;
		private string nombreArchivoOri;
		/// <summary>
		/// <para>Direccion de la carpeta contenedora.</para>
		/// </summary>
		private string carpetaContenedora;
		/// <summary>
		/// <para>Contiene la dimension de la preview de la imagen.</para>
		/// </summary>
		private Rect preImagenRect;
		private byte[] data;
		private Texture2D preImagen;
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
			preImagenRect = new Rect(0, 2, 84, 84);
		}
		#endregion

		#region GUI
		/// <summary>
		/// <para>GUI</para>
		/// </summary>
		public void OnGUI()
		{
			EditorGUILayout.BeginHorizontal();
			{
				var controlsRect = EditorGUILayout.BeginVertical(GUILayout.Width(Screen.width - 98));
				{
					EditorGUILayout.Space();
					EditorGUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Nombre", GUILayout.Width(80));
						nombreArchivo = EditorGUILayout.TextField(nombreArchivo);
					}
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Carpeta", GUILayout.Width(80));
						EditorGUILayout.TextField(carpetaContenedora);
						if (GUILayout.Button("Select"))
						{
							carpetaContenedora = EditorUtility.OpenFolderPanel("Seleccionar carpeta de salida", carpetaContenedora, "");
							EditorPrefs.SetString(carpetaContenedoraKey, carpetaContenedora);
						}
					}
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Archivo", GUILayout.Width(80));
						EditorGUILayout.TextField(nombreArchivoOri);
						if (GUILayout.Button("Select"))
						{
							nombreArchivoOri = EditorUtility.OpenFilePanelWithFilters("Seleccionar imagen", "Assets/",
								new string[] { "Image files", "png,jpg,jpeg,bmp,dds,gif,psd,tga,tiff" });
							if (File.Exists(nombreArchivoOri))
							{
								data = File.ReadAllBytes(nombreArchivoOri);

								if (preImagen == null)
								{
									preImagen = new Texture2D(2, 2);
								}
								preImagen.LoadImage(data);
							}
						}
					}
					EditorGUILayout.EndHorizontal();

					if (GUILayout.Button("Convertir"))
					{
						if (string.IsNullOrEmpty(nombreArchivo))
						{
							EditorUtility.DisplayDialog("Error", "Falta el nombre del archivo!", "OK");
						}
						else if (string.IsNullOrEmpty(nombreArchivoOri))
						{
							EditorUtility.DisplayDialog("Error", "Falta la imagen!", "OK");
						}
						else
						{
							// Guardar Textura
						}
					}
				}
				EditorGUILayout.EndVertical();

				preImagenRect.x = controlsRect.xMax + 4;
				GUI.DrawTexture(preImagenRect, preImagen);
			}
			EditorGUILayout.EndHorizontal();
		}
		#endregion

	}
}
