using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DG.Tweening;
using ProjectBase;
using ProjectBase.EnumExtension;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.Serialization;
using ZJZYDYDX_JTZDBZLZJZLFJYLPB.Game;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public enum Disease
	{
		[Description("轻度")] Mild,
		[Description("中度")] Moderate,
		[Description("重度")] Severe,
	}

	[System.Flags]
	public enum Symptom
	{
		[Description("未选择")] None = 0,
		[Description("催吐")] Emetic = 1,
		[Description("洗胃")] Gastrolavage = 2,
		[Description("导泄")] Catharsis = 4,
		[Description("静脉注射")] Injection = 8,
		[Description("血液灌流")] Perfusion = 16
	}

	public class PreviewPanelData : UIPanelData
	{
		public bool isSubmit = false;
		public bool isRight = false;
		public Disease rightDisease;
		public float getedScore;
	}

	public partial class PreviewPanel : UIPanel
	{
		[SerializeField] private List<Sprite> _sprites;
		
		Disease _selectedDisease = Disease.Mild;
		[Header("患者症状")][SerializeField] List<Toggle> _leftToggles;

		
		Symptom _introductionSymptom = Symptom.None;
		[Header("救治方法说明")][SerializeField] List<Toggle> _topToggles;
		[SerializeField] List<string> _topContent;


		[Header("我的治疗方案")] public Symptom _selectedSymptom = Symptom.None;
		[SerializeField] List<Toggle> _bottomToggles;
		[SerializeField] List<Symptom> _rightSymptoms;
		//List<Symptom> _rightSymptoms = new List<Symptom>()
		//{
		//	Symptom.Emetic | Symptom.Gastrolavage | Symptom.Catharsis,
		//	Symptom.Emetic | Symptom.Gastrolavage | Symptom.Catharsis | Symptom.Injection |Symptom.Perfusion,
		//	Symptom.Emetic | Symptom.Gastrolavage | Symptom.Catharsis | Symptom.Injection |Symptom.Perfusion,
		//};

		protected override void OnInit(IUIData uiData = null)
		{
			mData = Main.Instance.previewPanelData;
			ReportPanelData.Instance.RecordStartTime(ReportName.实验预习);

			GameRoot.Instance.StartPatient(mData.rightDisease);
			InitTopToggle();
			btnConfirm.AddAwaitAction(ExtensionFunction._topPanel.DoubleConfirmBackMain);
			InitAnim();
			if (mData.isSubmit)
			{
				LoadSubmitedPanel();
				return;
			}

			InitLeftToggle();
			InitBottomToggle();
			btnSubmit.AddAwaitAction(Submit);
			btnTip.AddAwaitAction(ExtensionFunction._topPanel.DoubleConfirmBackMain);
		}

		async UniTask Submit()
		{
			if (IsRightResult())
			{
				mData.isRight = true;
				mData.getedScore = 5;
				await objTip.ShowAsync();
			}
			else
			{
				btnSubmit.gameObject.SetActive(false);
				btnConfirm.gameObject.SetActive(true);
				ShowAnalysis();
				SetBottomAndLeftToggleInteractable(false);
			}
			mData.isSubmit = true;

			//实验报告_实验预习

			ReportPanelData.Instance.RecordGetedScore(ReportName.实验预习,mData.getedScore);
			ReportPanelData.Instance.RecordEndTime(ReportName.实验预习);
		}

		void LoadSubmitedPanel()
		{
			if (!mData.isRight)
				ShowAnalysis();
			btnSubmit.gameObject.SetActive(false);
			btnConfirm.gameObject.SetActive(true);
			SetBottomAndLeftToggleInteractable(false);
		}

		void SetBottomAndLeftToggleInteractable(bool interactable)
		{
			foreach (var item in _bottomToggles)
				item.interactable = interactable;
			foreach (var item in _leftToggles)
			{
				item.interactable = interactable;
			}

		}

		bool IsRightResult()
		{
			Symptom rightSymptom = _rightSymptoms[mData.rightDisease.GetHashCode()];
			bool isRightSymptom = rightSymptom.Compare(_selectedSymptom);

			bool isRightDisease = _selectedDisease == mData.rightDisease;

			return isRightSymptom && isRightDisease;
		}

		void ShowAnalysis()
		{
			IEnumerable<string> symptoms = _rightSymptoms[mData.rightDisease.GetHashCode()].Split().Descriptions();
			StringBuilder sb = new StringBuilder();
			foreach (var symptom in symptoms)
				sb.Append("【").Append(symptom).Append("】、");
			sb.Remove(sb.Length - 1, 1);

			tmpAnalysis.text = $"回答错误：患者应为{mData.rightDisease.Description()} ; 治疗方案应为 {sb}";
		}

		void InitAnim()
		{
			RectTransform rectHeart = imgHeart.transform as RectTransform;
			rectHeart.DOAnchorPosX(-700, 4).SetLoops(-1, LoopType.Restart);
			imgHeart.sprite = _sprites[(int)mData.rightDisease + 4];
		}

		void InitLeftToggle()
		{


			for (var i = 0; i < _leftToggles.Count; i++)
			{
				int index = i;
				Image img = _leftToggles[i].GetComponent<Image>();
				Disease disease = (Disease)index;
				_leftToggles[i].AddAwaitAction(isOn =>
				{
					if (isOn)
					{
						_selectedDisease = disease;
						img.sprite = _sprites[1];
					}
					else
					{
						img.sprite = _sprites[0];
					}
				});
			}
		}

		void InitTopToggle()
		{
			for (var i = 0; i < _topToggles.Count; i++)
			{
				Image img = _topToggles[i].GetComponent<Image>();
				Symptom symptom = (Symptom)ExtensionFunction.Pow2(i);
				int index = i;
				_topToggles[i].AddAwaitAction(isOn =>
				{
					if (isOn)
					{
						tmpDescribe.text = _topContent[index];
						_introductionSymptom = symptom;
						img.sprite = _sprites[3];
					}
					else
					{
						img.sprite = _sprites[2];
					}
				});
			}
		}

		void InitBottomToggle()
		{
			for (var i = 0; i < _bottomToggles.Count; i++)
			{
				Image img = _bottomToggles[i].GetComponent<Image>();
				Symptom value = (Symptom)ExtensionFunction.Pow2(i);
				_bottomToggles[i].AddAwaitAction(isOn =>
				{
					if (isOn)
					{
						_selectedSymptom = _selectedSymptom | value;
						img.sprite = _sprites[3];
					}
					else
					{
						_selectedSymptom = _selectedSymptom & (~value);
						img.sprite = _sprites[2];
					}

				});
			}
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
		}

		protected override void OnHide()
		{
			GameRoot.Instance.EndPatient();
		}

		protected override void OnClose()
		{
		}
	}
}