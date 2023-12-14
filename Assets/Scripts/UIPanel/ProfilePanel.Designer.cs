using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:edf79584-b629-4593-a6ed-b60f3b4e2144
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
		public UnityEngine.UI.Image objGoal;
		[SerializeField]
		public UnityEngine.UI.Image objRequirement;
		[SerializeField]
		public UnityEngine.UI.Image objPrinciple;
		
		private ProfilePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			togGoal = null;
			togRequirement = null;
			togPrinciple = null;
			objGoal = null;
			objRequirement = null;
			objPrinciple = null;
			
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
