/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/
using System.Collections.Generic;
using ProjectBase;
using UnityEngine;
using QFramework;
using System.Linq;
using System.Text;
using System.IO;
using System;
using Cysharp.Threading.Tasks;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	[Serializable]
	public class DrugInfo
	{
		public DrugName drugName;
		public Sprite sprite;
		public int minWeight;
		public int maxWeight;
	}

	[Serializable]
	public class DrugInfos
	{
		public int Count => drugInfos.Count;
		public DrugInfo this[int index]
		{
			get
			{
				return drugInfos[index];
			}
		}
		public List<DrugInfo> drugInfos = new List<DrugInfo>();
	}

	public partial class SelectDrug : UIElement
	{
		public List<DrugItem> _drugItems;
		public List<SelectedDrugItem> _selectedDrugItems;

		Case mNowCase;

		public Case NowCase
		{
			get
			{
				return mNowCase;
			}
			set
			{
				mNowCase = value;
				imgInputWeight.drugInfos = drugInfoss[(int)value];
				imgRightDrug.drugInfos = drugInfoss[(int)value];
			}
		}

		public List<DrugInfos> drugInfoss;

		DateTime startTime;
		public float GetedScore
		{
			get
			{
				float getedScore = 0;
				if (IsRight)
					getedScore += 2;
				if(imgInputWeight.showRightResult)
					getedScore += 3;
				return getedScore;
			}
		}
		bool IsRight;

		private void Awake()
		{
			switch (mNowCase)
			{
				case Case.MaleStudent:
					ReportPanelData.Instance.RecordStartTime(ReportName.病例一_一诊);
					break;
				case Case.FemaleClerk:
					ReportPanelData.Instance.RecordStartTime(ReportName.病例二_一诊);
					break;
			}

			for (var i = 0; i < _drugItems.Count; i++)
			{
				DrugItem drugItem = _drugItems[i];
				SelectedDrugItem selectedDrugItem = _selectedDrugItems[i];

				drugItem.tog.onValueChanged.AddListener(isOn =>
				{
					selectedDrugItem.gameObject.SetActive(isOn);
				});

				selectedDrugItem.btn.onClick.AddListener(() =>
				{
					drugItem.tog.isOn = false;
					selectedDrugItem.gameObject.SetActive(false);
					drugItem.tog.isOn = false;
				});
			}

			btnSubmitDrug.AddAwaitAction(SubmitDrug);

			imgRightDrug.btnConfirmRightDrug.AddAwaitAction(async () =>
			{
				await imgRightDrug.HideAsync();
				await imgInputWeight.ShowAsync();
			});
		}

		async UniTask SubmitDrug()
		{
			//如果正确
			if (SelectedDrugIsRight())
			{
				await imgInputWeight.ShowAsync();
			}
			else
			{
				//如果错误
				await imgRightDrug.ShowAsync();
			}

			switch (mNowCase)
			{
				case Case.MaleStudent:
					ReportPanelData.Instance.RecordGetedScore(ReportName.病例一_一诊, this.GetedScore);
					ReportPanelData.Instance.RecordEndTime(ReportName.病例一_一诊);
					break;
				case Case.FemaleClerk:
					ReportPanelData.Instance.RecordGetedScore(ReportName.病例二_一诊, this.GetedScore);
					ReportPanelData.Instance.RecordEndTime(ReportName.病例二_一诊);
					break;
			}
		}

		bool SelectedDrugIsRight()
		{
			bool isRight = false;
			List<DrugInfo> list = drugInfoss[(int)NowCase].drugInfos;
			//正确的是否都已选中
			isRight = list.Select(pair => _selectedDrugItems[(int)pair.drugName]).All(item => item.gameObject.activeInHierarchy);
			//选中的个数是否等于正确的个数
			IEnumerable<SelectedDrugItem> selected = _selectedDrugItems.Where(s => s.gameObject.activeInHierarchy);
			isRight = selected.Count() == list.Count();
			return isRight;
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}