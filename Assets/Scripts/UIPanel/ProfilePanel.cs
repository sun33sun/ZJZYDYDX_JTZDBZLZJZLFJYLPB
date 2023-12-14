using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using ProjectBase;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	[Serializable]
	public class ProfileContent
	{
		public string content;
		public List<Sprite> sprites;
	}

	public class ProfilePanelData : UIPanelData
	{
	}

	public partial class ProfilePanel : UIPanel
	{
		[Space(10)][SerializeField] Image imgPrefab;
		[SerializeField] List<ProfileContent> Contents;

		private List<Image> imgs = new List<Image>();

		DateTime startTime;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as ProfilePanelData ?? new ProfilePanelData();

			Image imgGoal = togGoal.GetComponent<Image>();
			togGoal.AddAwaitAction(isOn =>
			{
				togGoal.animator.SetBool("isOn", isOn);
				objGoal.gameObject.SetActive(isOn);
			});

			Image imgRequirement = togRequirement.GetComponent<Image>();
			togRequirement.AddAwaitAction(isOn =>
			{
				togRequirement.animator.SetBool("isOn", isOn);
				objRequirement.gameObject.SetActive(isOn);
			});

			Image imgPrinciple = togPrinciple.GetComponent<Image>();
			togPrinciple.onValueChanged.AddListener(isOn =>
			{
				togPrinciple.animator.SetBool("isOn", isOn);
				objPrinciple.gameObject.SetActive(isOn);
			});
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			UIKit.GetPanel<BottomPanel>().SwitchGroup(1);
			togGoal.isOn = true;
			togGoal.animator.SetBool("isOn", true);

			ReportPanelData.Instance.RecordStartTime(ReportName.实验简介);
		}

		protected override void OnHide()
		{
			ReportPanelData.Instance.RecordGetedScore(ReportName.实验简介);
			ReportPanelData.Instance.RecordEndTime(ReportName.实验简介);
		}

		protected override void OnClose()
		{
		}
	}
}