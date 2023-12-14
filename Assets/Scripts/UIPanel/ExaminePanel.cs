using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
    public class ExaminePanelData : UIPanelData
    {
    }

    public partial class ExaminePanel : UIPanel
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as ExaminePanelData ?? new ExaminePanelData();

			ReportPanelData.Instance.RecordStartTime(ReportName.知识考核);

			btnSubmit.AddAwaitAction(async () => await imgDoubleConfirm.ShowAsync());
            btnConfirm.AddAwaitAction(ExtensionFunction._topPanel.DoubleConfirmBackMain);

            btnDoubleCancel.AddAwaitAction(async () => await imgDoubleConfirm.HideAsync());

            
            btnDoubleConfirm.AddAwaitAction(async () =>
            {
                svTitle.Submit();
                print($"得分情况：{svTitle.GetedTotalScore}");
                btnSubmit.gameObject.SetActive(false);
                btnConfirm.gameObject.SetActive(true);
                await imgDoubleConfirm.HideAsync();
            });
            svTitle.LoadQuestion(ExtensionFunction.questionJson, ExtensionFunction.questionPrefab, ExtensionFunction.optionPrefab);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
		}

		protected override void OnHide()
        {
			ReportPanelData.Instance.RecordGetedScore(ReportName.知识考核, svTitle.GetedTotalScore);
			ReportPanelData.Instance.RecordEndTime(ReportName.知识考核);
		}

        protected override void OnClose()
        {
        }
    }
}