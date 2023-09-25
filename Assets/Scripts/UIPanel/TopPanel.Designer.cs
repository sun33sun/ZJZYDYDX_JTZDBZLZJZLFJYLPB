using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:3b4e4cef-d97c-41d8-8c12-2b56023bc025
	public partial class TopPanel
	{
		public const string Name = "TopPanel";
		
		[SerializeField]
		public RectTransform StartGroup;
		[SerializeField]
		public UnityEngine.UI.Button btnStart;
		[SerializeField]
		public UnityEngine.RectTransform imgBackMain;
		[SerializeField]
		public UnityEngine.UI.Button btnDoubleConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnDoubleCancel;
		[SerializeField]
		public UnityEngine.RectTransform imgHelp;
		[SerializeField]
		public UnityEngine.UI.Button btnCloseHelp;
		[SerializeField]
		public UnityEngine.UI.Image imgMask;
		[SerializeField]
		public UnityEngine.UI.Image imgBlank;
		
		private TopPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			StartGroup = null;
			btnStart = null;
			imgBackMain = null;
			btnDoubleConfirm = null;
			btnDoubleCancel = null;
			imgHelp = null;
			btnCloseHelp = null;
			imgMask = null;
			imgBlank = null;
			
			mData = null;
		}
		
		public TopPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		TopPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new TopPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
