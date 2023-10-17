using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using QFramework;
using UnityEngine;
using ProjectBase;
using ProjectBase.EnumExtension;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public class Main : PersistentMonoSingleton<Main>
	{
		public PreviewPanelData previewPanelData = null;
		public PreviewPanelData PreviewPanelData
		{
			get
			{
				if (previewPanelData == null)
				{
					previewPanelData = new PreviewPanelData();
					previewPanelData.isSubmit = false;
					//previewPanelData.rightDisease = EnumHelper.Random<Disease>();
					previewPanelData.rightDisease = Disease.Severe;
				}
				return previewPanelData;
			}
		}


		async UniTask Start()
		{
			await ExtensionFunction.UnrecordOpenPanelAsync<TopPanel>(TopPanel.Name).ToUniTask(this);
			await ExtensionFunction.UnrecordOpenPanelAsync<BottomPanel>(BottomPanel.Name).ToUniTask(this);
			ExtensionFunction._topPanel = UIKit.GetPanel<TopPanel>();
			ExtensionFunction._bottomPanel = UIKit.GetPanel<BottomPanel>();
		}
	}
}