using System.Collections;
using System.Collections.Generic;
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
            ChangeDrugUIName();
        }

        static void SelectDrugData()
        {
            GameObject parent = UnityEditor.Selection.gameObjects[0];
            SelectDrug selectDrug = parent.GetComponent<SelectDrug>();
            for (int i = 0; i < selectDrug.drugDic.Count; i++)
            {
                List<string> list = selectDrug.drugDic[i];
                for (int j = 0; j < list.Count; j++)
                {
                    list[j] = $"{i}行{j}列药";
                }
            }
        }

        static void InitSeletDrugUI()
        {
            GameObject parent = UnityEditor.Selection.gameObjects[0];
            List<string> names = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    names.Add($"{i}行{j}列药");
                }
            }

            int index = -1;
            Func<string> GetName = () =>
            {
                index++;
                return names[index];
            };
            foreach (var VARIABLE in parent.GetComponentsInChildren<SelectedDrugItem>())
            {
                VARIABLE.tmp.text = GetName();
            }
        }

        static void ChangeDrugUIName()
        {
            GameObject parent = UnityEditor.Selection.gameObjects[0];
            foreach (var VARIABLE in parent.GetComponentsInChildren<DrugItem>())
            {
                Debug.Log(VARIABLE.transform.name);
                VARIABLE.transform.name = VARIABLE.transform.name.Replace("Selected","");
            }
        }
    }
}

