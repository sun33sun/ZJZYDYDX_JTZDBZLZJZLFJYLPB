using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ProjectBase
{
    /// <summary>
    /// 点击btnPre，水平方向分段向右移动，显示左侧孩子
    /// 点击btnNext按钮，水平方向分段向左移动，显示右侧孩子
    /// </summary>
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class HorizontalSegmentation : MonoBehaviour
    {
        [Header("需要外部设置的参数")]
        public Button btnPre;
        public Button btnNext;
        public int showItemCount = 1;
        public float duration = 0.1f;
        
        private RectTransform _rect;

        float maxX;
        float minX;
        float moveSegment;
        Vector2 startPos;

        private void Awake()
        {
            Caculate();
        }

        public void Caculate()
        {
            btnPre.onClick.RemoveAllListeners();
            btnNext.onClick.RemoveAllListeners();
            
            _rect = transform as RectTransform;
            moveSegment = (_rect.GetChild(0).transform as RectTransform).sizeDelta.x +
                          GetComponent<HorizontalLayoutGroup>().spacing;

            startPos = _rect.anchoredPosition;
            float distance = (_rect.childCount - showItemCount) * moveSegment / 2;
            //偶
            if (_rect.childCount % 2 == 0)
            {
                //设置起始位置
                startPos.x = -_rect.childCount * moveSegment / 2;
                _rect.anchoredPosition = startPos;
                
                maxX = startPos.x + distance;
                minX = startPos.x - distance;
            }
            else //奇
            {
                //奇数情况下，起始位置要向右移动半个moveSegment，使（奇数除2向下取整的）中间居中显示
                startPos.x = -(_rect.childCount * moveSegment + moveSegment) / 2;
                _rect.anchoredPosition = startPos;
                
                maxX = startPos.x + distance + moveSegment;
                minX = startPos.x - distance;
            }

            //右移变大，看到左孩子
            btnPre.onClick.AddListener(() =>
            {
                float nowX = _rect.anchoredPosition.x;
                if (nowX < maxX)
                {
                    _rect.DOAnchorPosX(nowX + moveSegment, duration);
                }
            });
            //左移变小，看到右孩子
            btnNext.onClick.AddListener(() =>
            {
                float nowX = _rect.anchoredPosition.x;
                if (nowX > minX)
                {
                    _rect.DOAnchorPosX(nowX - moveSegment, duration);
                }
            });
        }
    }
}