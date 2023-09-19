using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public class CasePanelData : UIPanelData
	{
	}
	public partial class CasePanel : UIPanel
	{
		private CancellationToken _tokenDestroy;
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as CasePanelData ?? new CasePanelData();
			_tokenDestroy = this.GetCancellationTokenOnDestroy();
		}

		async UniTaskVoid Process()
		{
			await UniTask.WhenAny(objSelectCase.btnCase1.OnClickAsync(_tokenDestroy),
				objSelectCase.btnCase2.OnClickAsync(_tokenDestroy));
			await objFirstVisit.ShowAsync();
			await objFirstVisit.btnStartPractice.OnClickAsync(_tokenDestroy);
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
			Process().Forget();
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
