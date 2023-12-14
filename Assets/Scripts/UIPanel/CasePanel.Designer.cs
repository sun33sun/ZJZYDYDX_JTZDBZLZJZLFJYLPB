using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:ef2bef0b-9f87-462a-befc-7525040f3e5b
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
