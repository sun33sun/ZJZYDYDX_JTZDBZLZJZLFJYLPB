using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AssetScanner : EditorWindow
{
	private Object selectedFolder;
	private IEnumerable<Material> materialsInFolder;

	[MenuItem("Tools/Scan Selected Material Folder")]
	static void Init()
	{
		AssetScanner window = (AssetScanner)EditorWindow.GetWindow(typeof(AssetScanner));
		window.Show();
	}

	void OnGUI()
	{
		selectedFolder = EditorGUILayout.ObjectField("Select Folder", selectedFolder, typeof(DefaultAsset), false);

		if (GUILayout.Button("Scan Selected Folder"))
		{
			if (selectedFolder != null)
			{
				string folderPath = AssetDatabase.GetAssetPath(selectedFolder);

				if (!string.IsNullOrEmpty(folderPath))
				{
					Debug.Log("Scanning folder: " + folderPath);

					// 获取文件夹下的所有资产
					string[] assetPaths = AssetDatabase.FindAssets("", new string[] { folderPath });

					// 过滤出Material资产
					materialsInFolder = assetPaths
						.Select(path => AssetDatabase.GUIDToAssetPath(path))
						.Where(fullPath => Path.GetExtension(fullPath).Equals(".mat"))
						.Select(fullPath => AssetDatabase.LoadAssetAtPath<Object>(fullPath))
						.Select(asset => asset as Material);
				}
			}
		}

		if (materialsInFolder != null && materialsInFolder.Count() > 0)
		{
			string destinationColor = "969696";
			string destinationColor1 = "808080";
			EditorGUILayout.LabelField("Materials in Folder:");
			foreach (Material material in materialsInFolder)
			{
				if (material.mainTexture != null)
				{
					string nowColor = ColorUtility.ToHtmlStringRGB(material.color);
					if (nowColor == destinationColor || nowColor == destinationColor1)
					{
						material.color = Color.white;
						EditorUtility.SetDirty(material);
					}
					Debug.Log(nowColor);
				}
			}
			materialsInFolder = null;
		}
	}
}
