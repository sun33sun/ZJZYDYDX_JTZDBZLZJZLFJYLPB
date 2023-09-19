using System.Collections.Generic;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public class MainPanelData : UIPanelData
	{
	}
	public partial class MainPanel : UIPanel
	{
		[SerializeField] private List<Sprite> _sprites;
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as MainPanelData ?? new MainPanelData();
			btnProfile.AddAwaitAction(async () =>
			{
				await ExtensionFunction.ClosePanelAsync();
				await ExtensionFunction.OpenPanelAsync<ProfilePanel>(ProfilePanel.Name);
			});
			btnPreview.AddAwaitAction(async () =>
			{
				await ExtensionFunction.ClosePanelAsync();
				await ExtensionFunction.OpenPanelAsync<PreviewPanel>(PreviewPanel.Name);
			});
			btnCase.AddAwaitAction(async () =>
			{
				await ExtensionFunction.ClosePanelAsync();
				await ExtensionFunction.OpenPanelAsync<CasePanel>(CasePanel.Name);
			});
			btnExamine.AddAwaitAction(async () =>
			{
				await ExtensionFunction.ClosePanelAsync();
				await ExtensionFunction.OpenPanelAsync<ExaminePanel>(ExaminePanel.Name);
			});
			btnReport.AddAwaitAction(async () =>
			{
				await ExtensionFunction.ClosePanelAsync();
				await ExtensionFunction.OpenPanelAsync<ReportPanel>(ReportPanel.Name);
			});
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
			UIKit.GetPanel<BottomPanel>().SwitchGroup(0);
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
