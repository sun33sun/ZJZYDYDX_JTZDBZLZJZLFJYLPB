using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using HighlightPlus;
using QFramework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ZJZYDYDX_JTZDBZLZJZLFJYLPB;

namespace ProjectBase
{
    public static class ExtensionFunction
    {
        public static float HideTime = 0.5f;
        public static float ShowTime = 0.5f;
        public static string UI => "Resources/UIPrefab/";
        public static UIPanel NowPanel = null;

        #region UI扩展

        public static async UniTask UnrecordOpenPanelAsync<T>(string prefabName, IUIData uiData = null)
            where T : UIPanel
        {
            await UIKit.OpenPanelAsync<T>(UILevel.CanvasPanel, uiData, prefabName: UI + prefabName).ToUniTask();
            UIKit.GetPanel<T>().GetComponent<Canvas>().worldCamera = UIRoot.Instance.UICamera;
        }

        /// <summary>
        /// 执行异步函数过程中会屏蔽所有UI交互,Button动画会与异步事件同时执行
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="invoke"></param>
        public static void AddAwaitAction(this Button btn, Func<UniTask> invoke)
        {
            CancellationToken token = btn.GetCancellationTokenOnDestroy();
            UnityAction asyncAction = async () =>
            {
                if (token.IsCancellationRequested) return;
                UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(true);
                await invoke();
                UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(false);
            };
            btn.onClick.AddListener(asyncAction);
        }

        /// <summary>
        /// 执行异步函数过程中会屏蔽所有UI交互,Toggle动画会与异步事件同时执行
        /// </summary>
        /// <param name="tog"></param>
        /// <param name="invoke"></param>
        public static void AddAwaitAction(this Toggle tog, Func<bool, UniTask> invoke)
        {
            CancellationToken token = tog.GetCancellationTokenOnDestroy();
            UnityAction<bool> asyncAction = null;
            //有group的情况下，会同时触发两个toggle，因此屏蔽由isOn的Toggle管理。
            if (tog.group != null && !tog.group.allowSwitchOff)
            {
                asyncAction = async isOn =>
                {
                    if (token.IsCancellationRequested) return;
                    if (isOn)
                        UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(true);
                    await invoke(isOn);
                    if (isOn)
                        UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(false);
                };
            }
            else
            {
                asyncAction = async isOn =>
                {
                    if (token.IsCancellationRequested) return;
                    UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(true);
                    await invoke(isOn);
                    UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(false);
                };
            }

            tog.onValueChanged.AddListener(asyncAction);
        }

        /// <summary>
        /// 执行异步函数过程中会屏蔽所有UI交互,Toggle动画执行完成后才会执行异步事件
        /// </summary>
        /// <param name="tog"></param>
        /// <param name="invoke"></param>
        public static void AddAwaitAction(this Toggle tog, Action<bool> invoke)
        {
            CancellationToken token = tog.GetCancellationTokenOnDestroy();
            UnityAction<bool> asyncAction = null;
            //有group的情况下，会同时触发两个toggle，因此屏蔽由isOn的Toggle管理。
            if (tog.group != null && !tog.group.allowSwitchOff)
            {
                asyncAction = async isOn =>
                {
                    if (token.IsCancellationRequested) return;
                    if (isOn)
                        UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(true);
                    await tog.animator.GetAsyncAnimatorMoveTrigger().FirstAsync(tog.GetCancellationTokenOnDestroy());
                    invoke(isOn);
                    if (isOn)
                        UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(false);
                };
            }
            else
            {
                asyncAction = async isOn =>
                {
                    if (token.IsCancellationRequested) return;
                    UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(true);
                    await tog.animator.GetAsyncAnimatorMoveTrigger().FirstAsync(tog.GetCancellationTokenOnDestroy());
                    invoke(isOn);
                    UIKit.GetPanel<TopPanel>().imgMask.gameObject.SetActive(false);
                };
            }

            tog.onValueChanged.AddListener(asyncAction);
        }

        public static async UniTask ShowPanelAsync(this UIPanel panel)
        {
            panel.Show();
            await panel.GetComponent<CanvasGroup>().DOFade(1, ShowTime).AsyncWaitForCompletion();
        }

        public static async UniTask OpenPanelAsync<T>(string panelName) where T : UIPanel
        {
            if (NowPanel == null && panelName.IsNullOrEmpty())
                return;
            await UIKit.OpenPanelAsync<T>(UILevel.CanvasPanel, prefabName: UI + panelName).ToUniTask();
            NowPanel = UIKit.GetPanel<T>();
            Canvas canvas = NowPanel.GetComponent<Canvas>();
            canvas.worldCamera = UIRoot.Instance.Camera;
            await NowPanel.GetComponent<CanvasGroup>().DOFade(1, ShowTime).AsyncWaitForCompletion();
        }

        public static async UniTask ClosePanelAsync()
        {
            if (NowPanel == null)
                return;
            await NowPanel.GetComponent<CanvasGroup>().DOFade(0, ShowTime).AsyncWaitForCompletion();
            // await t.transform.DOLocalMoveY(1080, HideTime).AsyncWaitForCompletion();
            UIKit.ClosePanel(NowPanel);
            NowPanel = null;
        }
        
        public static async UniTask ShowAsync(this Component target)
        {
            target.gameObject.SetActive(true);
            await target.transform.DOLocalMoveY(0, ShowTime).AsyncWaitForCompletion();
        }

        public static async UniTask HideAsync(this Component target)
        {
            await target.transform.DOLocalMoveY(1080, ShowTime).AsyncWaitForCompletion();
            target.gameObject.SetActive(false);
        }

        public static async UniTask ShowAsync(this UIElement target)
        {
            target.Show();
            await target.transform.DOLocalMoveY(0, ShowTime).AsyncWaitForCompletion();
        }

        public static async UniTask HideAsync(this UIElement target)
        {
            await target.transform.DOLocalMoveY(1080, ShowTime).AsyncWaitForCompletion();
            target.Hide();
        }
        
        public static void HideSync(this Component target)
        {
            target.transform.position = new Vector3(0, 1080, 0);
            target.gameObject.SetActive(false);
        }

        public static void ShowSync(this Component target)
        {
            target.transform.position = Vector3.zero;
            target.gameObject.SetActive(true);
        }
        #endregion


        #region 3D物体扩展

        public static async UniTask HightlightClickAsync(this GameObject obj, CancellationToken cancellationToken)
        {
            HighlightEffect highlightEffect = obj.GetComponent<HighlightEffect>();
            if (highlightEffect == null)
                highlightEffect = obj.AddComponent<HighlightEffect>();
            highlightEffect.highlighted = true;
            highlightEffect.outlineColor = Color.red;

            await obj.GetAsyncPointerClickTrigger().FirstOrDefaultAsync(cancellationToken);
            highlightEffect.highlighted = false;
        }

        public static async UniTask HightlightClickAsync(this List<GameObject> objs,
            CancellationToken cancellationToken, Action<HighlightEffect> callBack = null)
        {
            List<HighlightEffect> objsHighlight = new List<HighlightEffect>();
            int count = objs.Count;
            for (int i = 0; i < objs.Count; i++)
            {
                GameObject obj = objs[i];
                HighlightEffect highlightEffect = obj.GetComponent<HighlightEffect>();
                if (highlightEffect == null)
                    highlightEffect = obj.AddComponent<HighlightEffect>();
                highlightEffect.highlighted = true;
                highlightEffect.outlineColor = Color.red;
                objsHighlight.Add(highlightEffect);
                obj.GetAsyncPointerClickTrigger().FirstOrDefaultAsync(d =>
                {
                    highlightEffect.highlighted = false;
                    count--;
                    callBack?.Invoke(highlightEffect);
                    return true;
                }, cancellationToken).Forget();
            }

            await UniTask.WaitUntil(() => count == 0, cancellationToken: cancellationToken);
        }

        #endregion
    }
}