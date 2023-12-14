using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public class ExecuteEditorScript : Editor
	{
		[MenuItem("GameObject/执行脚本", false, 49)]
		public static void Execute()
		{
		}

		static void FindInput()
		{
			Transform parent = Selection.gameObjects[0].transform;

			InputDrugWeight[] drugItems = parent.GetComponentsInChildren<InputDrugWeight>(true);
			foreach (var drugItem in drugItems)
			{
				drugItem.tmpRightWeight = drugItem.transform.Find("tmpRightWeight").GetComponent<TextMeshProUGUI>();
				Debug.Log(drugItem.tmpRightWeight.gameObject.GetInstanceID());
			}
		}
	}
}

