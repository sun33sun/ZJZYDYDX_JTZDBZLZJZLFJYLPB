using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
    public class GamePanelData : UIPanelData
    {
    }

    public partial class GamePanel : UIPanel
    {
        private CancellationToken _token;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as GamePanelData ?? new GamePanelData();
            _token = this.GetCancellationTokenOnDestroy();

            btnSubmitQuestion.AddAwaitAction(async () => await objDoubleConfirm.ShowAsync());
            btnDoubleCancel.AddAwaitAction(async () => await objDoubleConfirm.HideAsync());
            btnDoubleConfirm.AddAwaitAction(async () =>
            {
                await objDoubleConfirm.HideAsync();
                svQuestion.Submit();
                btnSubmitQuestion.gameObject.SetActive(false);
            });
            btnConfirmQuestion.AddAwaitAction(async () =>
            {
                await objQuestion.HideAsync();
                btnSubmitQuestion.gameObject.SetActive(true);
            });
            btnTip.AddAwaitAction(async () => await objTip.HideAsync());
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
            ExtensionFunction._bottomPanel.imgBk.enabled = false;
        }

        protected override void OnHide()
        {
            ExtensionFunction._bottomPanel.imgBk.enabled = true;
        }

        protected override void OnClose()
        {
        }

        public bool DrugStorageAnimation
        {
            set { riDrugStorage.gameObject.SetActive(value); }
        }

        public async UniTask WaitQuestion(string questionPath)
        {
            ExtensionFunction._topPanel.imgMask.gameObject.SetActive(true);
            svQuestion.LoadQuestion(questionPath, ExtensionFunction.questionPrefab, ExtensionFunction.optionPrefab);
            await objQuestion.ShowAsync();
            ExtensionFunction._topPanel.imgMask.gameObject.SetActive(false);
            await UniTask.WaitUntil(() => !objQuestion.gameObject.activeInHierarchy, cancellationToken: _token);

            await UniTask.WaitForSeconds(0.2f, cancellationToken: _token);
        }

        public async UniTask WaitClock(float endValue, string clockTip)
        {
            tmpClockTime.text = clockTip;
            ExtensionFunction._topPanel.imgMask.gameObject.SetActive(true);
            await objClock.ShowAsync();
            await UniTask.WaitForSeconds(0.2f, cancellationToken: _token);

            float nowValue = 0;
            float halfEndValue = endValue / 2;
            float addValue;
            while (nowValue > endValue)
            {
                addValue = Time.deltaTime * halfEndValue;
                objArraw.Rotate(Vector3.forward, addValue);
                nowValue += addValue;
                await UniTask.Yield();
            }

            objArraw.rotation = Quaternion.Euler(0, 0, endValue);

            await UniTask.WaitForSeconds(0.2f, cancellationToken: _token);
            await objClock.HideAsync();
            objArraw.rotation = Quaternion.identity;

            await UniTask.WaitForSeconds(0.2f, cancellationToken: _token);
            ExtensionFunction._topPanel.imgMask.gameObject.SetActive(false);
        }

        public async UniTask WaitTip(string strHead, string strContent, string strBtn)
        {
            tmpHead.text = strHead;
            tmpTip.text = strContent;
            tmpBtnTip.text = strBtn;
            ExtensionFunction._topPanel.imgMask.gameObject.SetActive(true);
            await objTip.ShowAsync();
            ExtensionFunction._topPanel.imgMask.gameObject.SetActive(false);
            await UniTask.WaitUntil(() => !objTip.gameObject.activeInHierarchy, cancellationToken: _token);

            await UniTask.WaitForSeconds(0.2f, cancellationToken: _token);
        }
    }
}