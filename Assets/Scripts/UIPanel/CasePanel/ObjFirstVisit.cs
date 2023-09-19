/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using ProjectBase;
using ProjectBase.DataClass;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
    public partial class ObjFirstVisit : UIElement
    {
        [SerializeField] private List<Toggle> _toggles;
        [SerializeField] private List<Sprite> _sprites;
        [SerializeField] private IntStringArrayDictionary _diagnosticRecords;
        public ObjSelectCase.Case NowCase { get; set; }

        private void Awake()
        {
            for (int i = 0; i < _toggles.Count; i++)
            {
                int index = i;
                Image image = _toggles[i].GetComponent<Image>();
                _toggles[i].AddAwaitAction(isOn =>
                {
                    if (isOn)
                    {
                        image.sprite = _sprites[0];
                        tmpRecordContent.text = _diagnosticRecords[(int)NowCase][index];
                    }
                    else
                    {
                        image.sprite = _sprites[1];
                    }
                });
            }

            btnStartPractice.AddAwaitAction(async () => await this.HideAsync());
        }

        protected override void OnBeforeDestroy()
        {
        }

        protected override void OnShow()
        {
            tmpRecordContent.text = _diagnosticRecords[(int)NowCase][0];
            base.OnShow();
        }
    }
}