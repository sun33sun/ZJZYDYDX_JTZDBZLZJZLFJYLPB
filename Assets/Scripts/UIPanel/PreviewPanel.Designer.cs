using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:87d02f23-9362-4932-afef-076d7456ab3b
	public partial class PreviewPanel
	{
		public const string Name = "PreviewPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgHeart;
		[SerializeField]
		public UnityEngine.UI.Image imgPatient;
		[SerializeField]
		public UnityEngine.UI.Image imgInstruction;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpDescribe;
		[SerializeField]
		public UnityEngine.UI.Toggle togConfirm;
		[SerializeField]
		public UnityEngine.UI.Toggle togSubmit;
		
		private PreviewPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgHeart = null;
			imgPatient = null;
			imgInstruction = null;
			tmpDescribe = null;
			togConfirm = null;
			togSubmit = null;
			
			mData = null;
		}
		
		public PreviewPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		PreviewPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new PreviewPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
