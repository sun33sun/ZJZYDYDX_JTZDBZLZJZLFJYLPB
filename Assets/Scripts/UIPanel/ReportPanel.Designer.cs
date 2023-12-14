using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:02ce230c-1bee-4b1a-969f-3bf0fd7029bf
	public partial class ReportPanel
	{
		public const string Name = "ReportPanel";
		
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTotalScore;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTotalTime;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpAnswer;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.RectTransform imgDoubleConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnDoubleConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnDoubleCancel;
		
		private ReportPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			tmpTotalScore = null;
			tmpTotalTime = null;
			tmpAnswer = null;
			btnSubmit = null;
			imgDoubleConfirm = null;
			btnDoubleConfirm = null;
			btnDoubleCancel = null;
			
			mData = null;
		}
		
		public ReportPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		ReportPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new ReportPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
