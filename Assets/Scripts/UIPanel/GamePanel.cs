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
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private List<InputDrugWeight> _inputDrugWeights = new List<InputDrugWeight>();
        [SerializeField] private InputDrugWeight _inputDrugWeightPrefab;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as GamePanelData ?? new GamePanelData();
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
            _cts.Dispose();
            _cts = new CancellationTokenSource();
            ExtensionFunction._bottomPanel.imgBk.enabled = false;
        }

        protected override void OnHide()
        {
            _cts.Cancel();
            ExtensionFunction._bottomPanel.imgBk.enabled = true;
        }

        protected override void OnClose()
        {
        }

        protected override void OnBeforeDestroy()
        {
            _cts.Cancel();
            base.OnBeforeDestroy();
        }

        public async UniTask WaitQuestion(string questionPath)
        {
            ExtensionFunction._topPanel.imgMask.gameObject.SetActive(true);
            svQuestion.LoadQuestion(questionPath, ExtensionFunction.questionPrefab, ExtensionFunction.optionPrefab);
            await objQuestion.ShowAsync();
            ExtensionFunction._topPanel.imgMask.gameObject.SetActive(false);
            await UniTask.WaitUntil(() => !objQuestion.gameObject.activeInHierarchy, cancellationToken: _cts.Token);
        }

        public async UniTask WaitClock(float endValue, string clockTip)
        {
            tmpClockTime.text = clockTip;
            ExtensionFunction._topPanel.imgMask.gameObject.SetActive(true);
            await objClock.ShowAsync();
            await UniTask.WaitForSeconds(1);

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

            await UniTask.WaitForSeconds(1);
            await objClock.HideAsync();
            objArraw.rotation = Quaternion.identity;
            ExtensionFunction._topPanel.imgMask.gameObject.SetActive(false);
        }

        public async UniTask WaitTip(string strHead,string strContent, string strBtn)
        {
            tmpHead.text = strHead;
            tmpTip.text = strContent;
            tmpBtnTip.text = strBtn;
            ExtensionFunction._topPanel.imgMask.gameObject.SetActive(true);
            await objTip.ShowAsync();
            ExtensionFunction._topPanel.imgMask.gameObject.SetActive(false);
            await btnTip.OnClickAsync(_cts.Token); 
        }
    }
}