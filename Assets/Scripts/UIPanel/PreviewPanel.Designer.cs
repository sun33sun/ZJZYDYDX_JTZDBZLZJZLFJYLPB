using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:1afd91e3-32aa-46bd-958b-e614a13df3c8
	public partial class PreviewPanel
	{
		public const string Name = "PreviewPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgHeart;
		[SerializeField]
		public UnityEngine.UI.Image imgInstruction;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpDescribe;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpAnalysis;
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
		
		private PreviewPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgHeart = null;
			imgInstruction = null;
			tmpDescribe = null;
			btnConfirm = null;
			btnSubmit = null;
			tmpAnalysis = null;
			objTip = null;
			tmpHead = null;
			tmpTip = null;
			btnTip = null;
			tmpBtnTip = null;
			
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
