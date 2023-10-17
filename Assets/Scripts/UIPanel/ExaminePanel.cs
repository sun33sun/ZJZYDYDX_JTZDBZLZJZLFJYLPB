using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

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
            btnSubmit.AddAwaitAction(async () => await imgDoubleConfirm.ShowAsync());
            btnConfirm.AddAwaitAction(ExtensionFunction._topPanel.DoubleConfirmBackMain);

            btnDoubleCancel.AddAwaitAction(async () => await imgDoubleConfirm.HideAsync());

            
            btnDoubleConfirm.AddAwaitAction(async () =>
            {
                svTitle.Submit();
                print($"得分情况：{svTitle.GetedTotalScore}");
                btnSubmit.gameObject.SetActive(false);
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
        }

        protected override void OnClose()
        {
        }
    }
}