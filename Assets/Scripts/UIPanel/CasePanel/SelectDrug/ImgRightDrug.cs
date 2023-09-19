/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
    public partial class ImgRightDrug : UIElement
    {
        private void Awake()
        {
            btnConfirmDrug.AddAwaitAction(async () => await this.HideAsync());
            
            // btnPre.AddAwaitAction(() =>
            // {
            //     
            // });
        }

        protected override void OnBeforeDestroy()
        {
        }
    }
}