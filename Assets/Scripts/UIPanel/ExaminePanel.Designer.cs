using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:a932889a-541c-45b7-97e3-b5b2fdb5f54b
	public partial class ExaminePanel
	{
		public const string Name = "ExaminePanel";
		
		[SerializeField]
		public RectTransform objConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		[SerializeField]
		public UnityEngine.RectTransform imgDoubleConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnDoubleConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnDoubleCancel;
		
		private ExaminePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			objConfirm = null;
			btnConfirm = null;
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
