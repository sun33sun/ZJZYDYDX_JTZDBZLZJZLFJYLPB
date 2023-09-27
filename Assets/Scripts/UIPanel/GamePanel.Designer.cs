using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:6788a33f-a90d-4a01-ba7b-ee88907cf0bd
	public partial class GamePanel
	{
		public const string Name = "GamePanel";
		
		[SerializeField]
		public UnityEngine.UI.RawImage riDrugStorage;
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
		
		private GamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			riDrugStorage = null;
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
