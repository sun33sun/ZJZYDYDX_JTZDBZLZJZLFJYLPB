using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:a116cc09-bde5-4274-9cc4-1b45eae23e60
	public partial class BottomPanel
	{
		public const string Name = "BottomPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgBk;
		[SerializeField]
		public UnityEngine.UI.Button btnHelp;
		[SerializeField]
		public UnityEngine.UI.Button btnScreen;
		[SerializeField]
		public UnityEngine.UI.Button btnMain;
		
		private BottomPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgBk = null;
			btnHelp = null;
			btnScreen = null;
			btnMain = null;
			
			mData = null;
		}
		
		public BottomPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		BottomPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new BottomPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
