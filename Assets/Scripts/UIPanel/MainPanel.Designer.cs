using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:0e185e24-1201-4abc-bea1-d10db90d6e0a
	public partial class MainPanel
	{
		public const string Name = "MainPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnProfile;
		[SerializeField]
		public UnityEngine.UI.Button btnPreview;
		[SerializeField]
		public UnityEngine.UI.Button btnCase;
		[SerializeField]
		public UnityEngine.UI.Button btnExamine;
		[SerializeField]
		public UnityEngine.UI.Button btnReport;
		
		private MainPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnProfile = null;
			btnPreview = null;
			btnCase = null;
			btnExamine = null;
			btnReport = null;
			
			mData = null;
		}
		
		public MainPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		MainPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new MainPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
