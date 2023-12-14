/****************************************************************************
 * 2023.10 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using ProjectBase;
using Cysharp.Threading.Tasks;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public partial class ImgRightDrug : UIElement
	{
		public List<RightDrugItem> rightDrugItems;
		[HideInInspector] public CaseToIntInts caseToIntInts;

		public DrugInfos drugInfos;

		private void Awake()
		{
			btnConfirmRightDrug.AddAwaitAction(async()=>await transform.HideAsync());
		}

		private void OnEnable()
		{
			for (int i = 0; i < drugInfos.Count; i++)
			{
				rightDrugItems[(int)drugInfos[i].drugName].gameObject.SetActive(true);
			}
			Recalculate();
		}

		async void Recalculate()
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(RightDrugGroup.Content);
			await UniTask.Yield();
			RightDrugGroup.Caculate();
		}


		protected override void OnBeforeDestroy()
		{
		}
	}
}