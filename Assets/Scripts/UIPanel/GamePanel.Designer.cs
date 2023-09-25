using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:26e6e84b-46fe-4640-95ed-c037abdc17ab
	public partial class GamePanel
	{
		public const string Name = "GamePanel";
		
		[SerializeField]
		public RectTransform objQuestion;
		[SerializeField]
		public ProjectBase.Exam.QuestionParent svQuestion;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmQuestion;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmitQuestion;
		[SerializeField]
		public UnityEngine.RectTransform objDoubleConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnDoubleConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnDoubleCancel;
		[SerializeField]
		public UnityEngine.RectTransform objClock;
		[SerializeField]
		public UnityEngine.RectTransform objArraw;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpClockTime;
		[SerializeField]
		public UnityEngine.RectTransform objTip;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpHead;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTip;
		[SerializeField]
		public UnityEngine.UI.Button btnTip;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpBtnTip;
		[SerializeField]
		public UnityEngine.RectTransform RightDrugGroup;
		[SerializeField]
		public ProjectBase.HorizontalSegmentation RightDrugParent;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		
		private GamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			objQuestion = null;
			svQuestion = null;
			btnConfirmQuestion = null;
			btnSubmitQuestion = null;
			objDoubleConfirm = null;
			btnDoubleConfirm = null;
			btnDoubleCancel = null;
			objClock = null;
			objArraw = null;
			tmpClockTime = null;
			objTip = null;
			tmpHead = null;
			tmpTip = null;
			btnTip = null;
			tmpBtnTip = null;
			RightDrugGroup = null;
			RightDrugParent = null;
			btnSubmit = null;
			
			mData = null;
		}
		
		public GamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		GamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new GamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
