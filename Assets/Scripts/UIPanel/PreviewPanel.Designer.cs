using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:d8b2111e-22ba-4b98-ad34-f55e7139e41a
	public partial class PreviewPanel
	{
		public const string Name = "PreviewPanel";
		
		[SerializeField]
		public UnityEngine.UI.Toggle togMild;
		[SerializeField]
		public UnityEngine.UI.Toggle togModerate;
		[SerializeField]
		public UnityEngine.UI.Toggle togSevere;
		[SerializeField]
		public UnityEngine.UI.Image imgPatient;
		[SerializeField]
		public UnityEngine.UI.Image imgInstruction;
		[SerializeField]
		public UnityEngine.UI.Toggle togEmetic;
		[SerializeField]
		public UnityEngine.UI.Toggle togGastrolavage;
		[SerializeField]
		public UnityEngine.UI.Toggle togCatharsis;
		[SerializeField]
		public UnityEngine.UI.Toggle togInjection;
		[SerializeField]
		public UnityEngine.UI.Toggle togPerfusion;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpDescribe;
		[SerializeField]
		public UnityEngine.UI.Toggle togEmeticSelected;
		[SerializeField]
		public UnityEngine.UI.Toggle togGastrolavageSelected;
		[SerializeField]
		public UnityEngine.UI.Toggle togCatharsisSelected;
		[SerializeField]
		public UnityEngine.UI.Toggle togInjectionSelected;
		[SerializeField]
		public UnityEngine.UI.Toggle togPerfusionSelected;
		[SerializeField]
		public UnityEngine.UI.Toggle togSubmit;
		[SerializeField]
		public UnityEngine.UI.Toggle togConfirm;
		
		private PreviewPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			togMild = null;
			togModerate = null;
			togSevere = null;
			imgPatient = null;
			imgInstruction = null;
			togEmetic = null;
			togGastrolavage = null;
			togCatharsis = null;
			togInjection = null;
			togPerfusion = null;
			tmpDescribe = null;
			togEmeticSelected = null;
			togGastrolavageSelected = null;
			togCatharsisSelected = null;
			togInjectionSelected = null;
			togPerfusionSelected = null;
			togSubmit = null;
			togConfirm = null;
			
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
