using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public class ReportPanelData : UIPanelData
	{
	}
	public partial class ReportPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as ReportPanelData ?? new ReportPanelData();
			// please add init code here
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}