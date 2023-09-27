using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:2608c848-be74-4390-97cc-9461d2370bfe
	public partial class PreviewPanel
	{
		public const string Name = "PreviewPanel";
		
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
