using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using ProjectBase;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
    [Serializable]
    public class ProfileContent
    {
        public string content;
        public List<Sprite> sprites;
    }

    public class ProfilePanelData : UIPanelData
    {
    }

    public partial class ProfilePanel : UIPanel
    {
        [Space(10)] [SerializeField] Image imgPrefab;
        [SerializeField] List<ProfileContent> Contents;

        private List<Image> imgs = new List<Image>();

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as ProfilePanelData ?? new ProfilePanelData();

            Image imgGoal = togGoal.GetComponent<Image>();
            togGoal.AddAwaitAction(isOn =>
            {
                togGoal.animator.SetBool("isOn", isOn);
                if (isOn)
                {
                    LoadContent(0);
                }
            });

            Image imgRequirement = togRequirement.GetComponent<Image>();
            togRequirement.AddAwaitAction( isOn =>
            {
                togRequirement.animator.SetBool("isOn", isOn);
                if (isOn)
                {
                    LoadContent(1);
                }
            });

            Image imgPrinciple = togPrinciple.GetComponent<Image>();
            togPrinciple.onValueChanged.AddListener(isOn =>
            {
                togPrinciple.animator.SetBool("isOn", isOn);
                if (isOn)
                {
                    LoadContent(2);
                }
            });
        }

        void LoadContent(int nowIndex)
        {
            //Çå³ý
            for (int i = 0; i < imgs.Count; i++)
                Destroy(imgs[i].gameObject);
            imgs.Clear();
            tmpContent.text = "";
            //¼ÓÔØ
            foreach (var VARIABLE in Contents[nowIndex].sprites)
            {
                Image img = Instantiate(imgPrefab);
                img.sprite = VARIABLE;
                img.transform.SetParent(vlgImage, false);
                imgs.Add(img);
            }
            tmpContent.text = Contents[nowIndex].content;
            LayoutRebuilder.ForceRebuildLayoutImmediate(vlgImage);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
            UIKit.GetPanel<BottomPanel>().SwitchGroup(1);
            togGoal.isOn = true;
            togGoal.animator.SetBool("isOn", true);
            LoadContent(0);
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }
    }
}