using Cysharp.Threading.Tasks;
using DG.Tweening;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public class TopPanelData : UIPanelData
	{
	}
	public partial class TopPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as TopPanelData ?? new TopPanelData();
			btnStart.AddAwaitAction(async () =>
			{
				await StartGroup.HideAsync();
				await ExtensionFunction.OpenPanelAsync<MainPanel>(MainPanel.Name);
			});
			btnCloseHelp.AddAwaitAction(async ()=>await imgHelp.HideAsync());
			btnDoubleCancel.AddAwaitAction(async ()=>await imgBackMain.HideAsync());
			btnDoubleConfirm.AddAwaitAction(async ()=>
			{
				await imgBackMain.HideAsync();
				await ExtensionFunction.ClosePanelAsync();
				await ExtensionFunction.OpenPanelAsync<MainPanel>(MainPanel.Name);
			});
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
