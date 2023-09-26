/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using System.Collections.Generic;
using System.Linq;
using ProjectBase;
using ProjectBase.DataClass;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using ZJZYDYDX_JTZDBZLZJZLFJYLPB.Game;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
    public partial class ObjFirstVisit : UIElement
    {
        [SerializeField] private List<Toggle> _toggles;
        [SerializeField] private List<Sprite> _sprites;
        [SerializeField] private IntStringArrayDictionary _diagnosticRecords;
        public ObjSelectCase.Case NowCase { get; set; }
        List<bool> isClicks = new List<bool>();

        private void Awake()
        {
            for (int i = 0; i < _toggles.Count; i++)
            {
                isClicks.Add(false);
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
                    isClicks[index] = true;
                    if(isClicks.All(x => x))
                        btnSubmit.gameObject.SetActive(true);
                });
            }

            btnSubmit.AddAwaitAction(async () => await this.HideAsync());
        }

        protected override void OnBeforeDestroy()
        {
            if(GameRoot.Instance != null)
                GameRoot.Instance.EndPharmacy();
        }

        protected override void OnShow()
        {
            tmpRecordContent.text = _diagnosticRecords[(int)NowCase][0];
            GameRoot.Instance.StartPharmacy(NowCase);
            base.OnShow();
        }

        protected override void OnHide()
        {
            GameRoot.Instance.EndPharmacy();
            base.OnHide();
        }
    }
}