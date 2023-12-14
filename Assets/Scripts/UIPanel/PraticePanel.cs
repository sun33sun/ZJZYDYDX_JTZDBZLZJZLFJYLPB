using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public class PraticePanelData : UIPanelData
	{
	}
	public partial class PraticePanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as PraticePanelData ?? new PraticePanelData();
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
