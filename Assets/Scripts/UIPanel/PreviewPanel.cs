using System;
using System.Collections.Generic;
using DG.Tweening;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using ZJZYDYDX_JTZDBZLZJZLFJYLPB.Game;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
    public enum Disease
    {
        Mild,
        Moderate,
        Severe
    }

    [System.Flags]
    public enum Symptom
    {
        None = 0,
        Emetic = 1,
        Gastrolavage = 2,
        Catharsis = 4,
        Injection = 8,
        Perfusion = 16
    }

    public class PreviewPanelData : UIPanelData
    {
    }

    public partial class PreviewPanel : UIPanel
    {
        [SerializeField] private List<Sprite> _sprites;

        Disease _nowDisease = Disease.Mild;
        [SerializeField] List<Toggle> _leftToggles;

        Symptom _introductionSymptom = Symptom.Emetic;
        [SerializeField] List<Toggle> _topToggles;

        public Symptom _selectedSymptom = Symptom.Emetic;
        [SerializeField] List<Toggle> _bottomToggles;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as PreviewPanelData ?? new PreviewPanelData();

            GameRoot.Instance.StartPatient(_nowDisease);
            InitLeft();
            InitIntroduction();
            InitSelected();
        }

        void InitLeft()
        {
            RectTransform rectHeart = imgHeart.transform as RectTransform;
            rectHeart.DOAnchorPosX(-700, 4).SetLoops(-1,LoopType.Restart);
            
            for (var i = 0; i < _leftToggles.Count; i++)
            {
                int index = i;
                Image img = _leftToggles[i].GetComponent<Image>();
                Disease disease = (Disease)index;
                _leftToggles[i].AddAwaitAction(isOn =>
                {
                    if (isOn)
                    {
                        _nowDisease = disease;
                        imgHeart.sprite = _sprites[index + 4];
                        rectHeart.DORestart();
                        GameRoot.Instance.StartPatient(_nowDisease);
                        img.sprite = _sprites[1];
                    }
                    else
                    {
                        img.sprite = _sprites[0];
                    }
                });
            }
        }

        void InitIntroduction()
        {
            for (var i = 0; i < _topToggles.Count; i++)
            {
                Image img = _topToggles[i].GetComponent<Image>();
                Symptom symptom = (Symptom)ExtensionFunction.Pow2(i);
                _topToggles[i].AddAwaitAction(isOn =>
                {
                    if (isOn)
                    {
                        _introductionSymptom = symptom;
                        img.sprite = _sprites[3];
                    }
                    else
                    {
                        img.sprite = _sprites[2];
                    }
                });
            }
        }

        void InitSelected()
        {
            for (var i = 0; i < _bottomToggles.Count; i++)
            {
                Image img = _bottomToggles[i].GetComponent<Image>();
                int value = ExtensionFunction.Pow2(i);
                _bottomToggles[i].AddAwaitAction(isOn =>
                {
                    int nowValue = (int)_selectedSymptom;
                    if (isOn)
                    {
                        nowValue += value;
                        img.sprite = _sprites[3];
                    }
                    else
                    {
                        nowValue -= value;
                        img.sprite = _sprites[2];
                    }
                    _selectedSymptom = (Symptom)nowValue;                    
                });
            }
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
            GameRoot.Instance.EndPatient();
        }

        protected override void OnClose()
        {
        }
    }
}