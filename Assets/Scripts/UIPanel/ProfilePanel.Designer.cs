using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:e7c33d23-ff96-41b0-90b4-1cf514ae1047
	public partial class ProfilePanel
	{
		public const string Name = "ProfilePanel";
		
		[SerializeField]
		public UnityEngine.UI.Toggle togGoal;
		[SerializeField]
		public UnityEngine.UI.Toggle togRequirement;
		[SerializeField]
		public UnityEngine.UI.Toggle togPrinciple;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpContent;
		[SerializeField]
		public RectTransform vlgImage;
		
		private ProfilePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			togGoal = null;
			togRequirement = null;
			togPrinciple = null;
			tmpContent = null;
			vlgImage = null;
			
			mData = null;
		}
		
		public ProfilePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		ProfilePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new ProfilePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
