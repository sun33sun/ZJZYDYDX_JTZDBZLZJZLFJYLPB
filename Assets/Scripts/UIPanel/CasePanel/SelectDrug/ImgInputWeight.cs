/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Linq;
using ProjectBase;
using Cysharp.Threading.Tasks;
using TMPro;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public enum DrugName
	{
		熟附子,
		干姜,
		炙甘草,
		党参,
		人参,
		麦门冬,
		五味子,
		茯苓,
		桑寄生,
		杜仲,
		益母草
	}
	public partial class ImgInputWeight : UIElement
	{
		[SerializeField] List<InputDrugWeight> inputs;

		public bool showRightResult { get;private set; }

		public DrugInfos drugInfos;

		DateTime startTime;

		private void Awake()
		{
			btnSubmit.AddAwaitAction(Submit);
		}

		private void OnEnable()
		{
			for (int i = 0; i < drugInfos.Count; i++)
				inputs[(int)drugInfos[i].drugName].gameObject.SetActive(true);
			Recalculate();

			startTime = DateTime.Now;
		}

		async void Recalculate()
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(RightDrugGroup.Content);
			await UniTask.Yield();
			RightDrugGroup.Caculate();
		}

		async UniTask Submit()
		{
			if (showRightResult)
			{
				await transform.HideAsync();
				return;
			}
			if (CheckIsRight())
			{
				await transform.HideAsync();
			}
			else
			{
				showRightResult = true;
				for (int i = 0; i < drugInfos.Count; i++)
				{
					DrugInfo drugInfo = drugInfos[i];

					InputDrugWeight inputDrugWeight = inputs[(int)drugInfos[i].drugName];
					InputField input = inputDrugWeight.input;
					input.interactable = false;
					input.textComponent.color = Color.red;

					//设置tmpRightWeight
					inputDrugWeight.tmpRightWeight.text = $"{drugInfo.minWeight}-{drugInfo.maxWeight}";
					inputDrugWeight.tmpRightWeight.gameObject.SetActive(true);
				}
			}
		}

		private void OnDisable()
		{
			foreach (var input in inputs)
				input.gameObject.SetActive(false);
		}

		bool CheckIsRight()
		{
			for (int i = 0; i < drugInfos.Count; i++)
			{
				DrugInfo info = drugInfos[i];
				int value = inputs[(int)info.drugName].input.text.ParseToInt();
				if (value < info.minWeight || value > info.maxWeight)
					return false;
			}

			return true;
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}