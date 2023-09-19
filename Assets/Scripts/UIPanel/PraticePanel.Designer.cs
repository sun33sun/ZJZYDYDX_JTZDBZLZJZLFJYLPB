using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:584f9374-ad6e-4712-acef-12772264250f
	public partial class PraticePanel
	{
		public const string Name = "PraticePanel";
		
		[SerializeField]
		public UnityEngine.UI.Button imgStepTip;
		[SerializeField]
		public ImgTitle imgTitle;
		[SerializeField]
		public ImgWait imgWait;
		[SerializeField]
		public ImgWait imgTip;
		
		private PraticePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgStepTip = null;
			imgTitle = null;
			imgWait = null;
			imgTip = null;
			
			mData = null;
		}
		
		public PraticePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		PraticePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new PraticePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
