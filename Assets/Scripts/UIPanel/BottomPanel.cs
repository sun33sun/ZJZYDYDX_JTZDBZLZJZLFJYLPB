using System.Collections.Generic;
using DG.Tweening;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
    public class BottomPanelData : UIPanelData
    {
    }

    public partial class BottomPanel : UIPanel
    {
        [SerializeField] private List<RectTransform> groups;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as BottomPanelData ?? new BottomPanelData();

            TopPanel topPanel = UIKit.GetPanel<TopPanel>();
            btnMain.AddAwaitAction(async () =>
            {
                if (ExtensionFunction.NowPanel is MainPanel)
                    return;
                await topPanel.imgBackMain.ShowAsync();
            });
            
            btnHelp.AddAwaitAction(async () => await topPanel.imgHelp.ShowAsync());

            btnScreen.OnPointerClickEvent(d => { Screen.fullScreen = !Screen.fullScreen; });
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

        public void SwitchGroup(int nowIndex)
        {
            for (int i = 0; i < groups.Count; i++)
            {
                groups[i].gameObject.SetActive(false);
            }

            groups[nowIndex].gameObject.SetActive(true);
        }
    }
}