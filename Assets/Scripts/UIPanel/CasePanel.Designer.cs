using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:465b28a4-1c6c-45bb-b993-acfdc6d12455
	public partial class CasePanel
	{
		public const string Name = "CasePanel";
		
		[SerializeField]
		public ObjSelectCase objSelectCase;
		[SerializeField]
		public ObjFirstVisit objFirstVisit;
		[SerializeField]
		public SelectDrug selectDrug;
		
		private CasePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			objSelectCase = null;
			objFirstVisit = null;
			selectDrug = null;
			
			mData = null;
		}
		
		public CasePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		CasePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new CasePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
