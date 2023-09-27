using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using ZJZYDYDX_JTZDBZLZJZLFJYLPB.Game;

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
			btnCloseHelp.AddAwaitAction(async () =>
			{
				GameRoot.Instance.ResumeGame?.Invoke();
				await imgHelp.HideAsync();
			});
			btnDoubleCancel.AddAwaitAction(async () =>
			{
				await imgBackMain.HideAsync();
				GameRoot.Instance.ResumeGame?.Invoke();
			});
			btnDoubleConfirm.AddAwaitAction(async ()=>
			{
				if (GameRoot.Instance.PauseGame != null)
				{
					imgBackMain.HideSync();
					await GameRoot.Instance.EndCase();
				}
				else
				{
					await imgBackMain.HideAsync();
					await ExtensionFunction.ClosePanelAsync();
					await ExtensionFunction.OpenPanelAsync<MainPanel>(MainPanel.Name);
				}
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

		public async UniTask OpenEye()
		{
			imgBlank.gameObject.SetActive(true);
			Material mat = imgBlank.material;
			Vector4 vector = new Vector4(0.6f, 0, 1, 1);
			while (vector.y < 1)
			{
				vector.y += Time.deltaTime;
				mat.SetVector("_Param", vector);
				await UniTask.Yield();
			}
			vector.y = 1;
			mat.SetVector("_Param", vector);
			imgBlank.gameObject.SetActive(false);
		}
		
		public async UniTask CloseEye()
		{
			imgBlank.gameObject.SetActive(true);
			Material mat = imgBlank.material;
			Vector4 vector = new Vector4(0.6f, 1, 1, 1);
			while (vector.y > 0)
			{
				vector.y -= Time.deltaTime;
				mat.SetVector("_Param", vector);
				await UniTask.Yield();
			}
			vector.y = 0;
			mat.SetVector("_Param", vector);
		}
	}
}
