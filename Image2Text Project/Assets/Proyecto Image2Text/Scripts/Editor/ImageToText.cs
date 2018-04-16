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
using System.Text;
using System;
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
		/// <para>Nombre del archivo.</para>
		/// </summary>
		private string nombreArchivo;
		/// <summary>
		/// <para>Nombre del archivo original.</para>
		/// </summary>
		private string nombreArchivoOri;
		/// <summary>
		/// <para>Direccion de la carpeta contenedora.</para>
		/// </summary>
		private string carpetaContenedora;
		/// <summary>
		/// <para>Contiene la dimension de la preview de la imagen.</para>
		/// </summary>
		private Rect preImagenRect;
		/// <summary>
		/// <para>Data de la imagen.</para>
		/// </summary>
		private byte[] data;
		/// <summary>
		/// <para>Imagen.</para>
		/// </summary>
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
							Convertir();
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

		#region Metodos
		/// <summary>
		/// <para>Convierte una imagen en bytes.</para>
		/// </summary>
		private void Convertir()
		{
			byte[] bytes = data;

			var outputFileName = carpetaContenedora + "/" + nombreArchivo + ".cs";
			FileStream fileStream = null;

			if (!File.Exists(outputFileName))
			{
				fileStream = File.Create(outputFileName);
			}
			else
			{
				fileStream = File.Open(outputFileName, FileMode.Truncate);
			}
			StringBuilder outData = new StringBuilder();
			var writer = new StreamWriter(fileStream);

			outData.Append("using UnityEngine;\nusing UnityEditor;\n\n");
			outData.Append("namespace MoonAntonio.ResData\n{\n");
			outData.Append("\tpublic static class " + nombreArchivo + "\n\t{\n");

			outData.AppendLine("\t\tprivate static Texture2D texture;\n");
			outData.AppendLine("\t\tpublic static Texture2D Get()");
			outData.AppendLine("\t\t{");
			outData.AppendLine("\t\t\tif( texture == null )");
			outData.AppendLine("\t\t\t{");
			outData.AppendLine("\t\t\t\ttexture = new Texture2D( 2, 2 );");
			outData.AppendLine("\t\t\t\ttexture.LoadImage( data );");
			outData.AppendLine("\t\t\t\ttexture.Apply();");
			outData.AppendLine("\t\t\t}");
			outData.AppendLine("\t\t\treturn texture;");
			outData.AppendLine("\t\t}");
			outData.AppendLine("\n\t\tprivate static byte[] data = \n\t\t{\t\t\t");
			for (int i = 0; i < bytes.Length; i++)
			{
				if (i % 20 == 0)
				{
					EditorUtility.DisplayProgressBar("Convirtiendo...", "...", (float)i / bytes.Length);
					if (i > 0)
					{
						outData.Append(writer.NewLine);
					}
					outData.Append("\t\t\t");
				}
				byte b = bytes[i];
				outData.Append("0x" + BitConverter.ToString(bytes, i, 1));
				if (i + 1 < bytes.Length)
				{
					outData.Append(", ");
				}
			}
			outData.Append("\n\t\t};\n\t}\n}");
			writer.Write(outData.ToString());
			writer.Close();
			EditorUtility.ClearProgressBar();
			AssetDatabase.Refresh(ImportAssetOptions.Default);
		}
		#endregion
	}
}
