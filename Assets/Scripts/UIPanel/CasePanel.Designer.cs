using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	// Generate Id:aadbc722-8356-4479-a857-f44db03ceb99
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
