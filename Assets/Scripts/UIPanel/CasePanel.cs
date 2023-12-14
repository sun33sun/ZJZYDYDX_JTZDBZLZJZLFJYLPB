using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using ZJZYDYDX_JTZDBZLZJZLFJYLPB.Game;
using System.Collections.Generic;
using System.Linq;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public enum Case
	{
		MaleStudent,
		FemaleClerk
	}


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
			//ÉèÖÃNowCase
			objFirstVisit.NowCase = objSelectCase.NowCase;
			selectDrug.NowCase = objSelectCase.NowCase;

			await objFirstVisit.ShowAsync();
			await objFirstVisit.btnSubmit.OnClickAsync(_tokenDestroy);
			await selectDrug.ShowAsync();
			GameObject objInputWeight = selectDrug.imgInputWeight.gameObject;
			await UniTask.WaitUntil(() => objInputWeight.activeInHierarchy, cancellationToken: _tokenDestroy);
			await UniTask.WaitUntil(() => !objInputWeight.activeInHierarchy, cancellationToken: _tokenDestroy);

			DrugInfos datas = selectDrug.drugInfoss[(int)selectDrug.NowCase];

			GameRoot.Instance.StartCase(objSelectCase.NowCase, datas);
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