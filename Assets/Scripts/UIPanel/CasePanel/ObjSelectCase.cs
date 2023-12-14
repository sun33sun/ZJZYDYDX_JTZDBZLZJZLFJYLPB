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
	public partial class ObjSelectCase : UIElement
    {
        public Case NowCase { get; private set; }

        private void Awake()
        {
            btnCase1.AddAwaitAction(async () =>
            {
                NowCase = Case.MaleStudent;
                await this.HideAsync();
            });
            btnCase2.AddAwaitAction(async () =>
            {
                NowCase = Case.FemaleClerk;
                await this.HideAsync();
            });
        }

        protected override void OnBeforeDestroy()
        {
        }
    }
}