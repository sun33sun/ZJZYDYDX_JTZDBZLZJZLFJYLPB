using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:ccb4d905-8081-4d39-8c19-312376ed9cc5
	public partial class ExaminePanel
	{
		public const string Name = "ExaminePanel";
		
		[SerializeField]
		public ProjectBase.Exam.QuestionParent svTitle;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.RectTransform imgDoubleConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnDoubleConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnDoubleCancel;
		
		private ExaminePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			svTitle = null;
			btnConfirm = null;
			btnSubmit = null;
			imgDoubleConfirm = null;
			btnDoubleConfirm = null;
			btnDoubleCancel = null;
			
			mData = null;
		}
		
		public ExaminePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		ExaminePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new ExaminePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
