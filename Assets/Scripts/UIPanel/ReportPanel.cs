using UnityEngine;
using QFramework;
using ProjectBase;
using System.Collections.Generic;
using ProjectBase.EnumExtension;
using System;
using Cysharp.Threading.Tasks;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public enum ReportName
	{
		����һ_һ��, ����һ_����, ����һ_�ȼ�, ����һ_һ��, ����һ_����, ����һ_����, ������_һ��, ������_����, ������_����, ʵ����, ʵ��Ԥϰ, ֪ʶ����
	}
	public class ReportPanelData : UIPanelData
	{
		public static ReportPanelData Instance = new ReportPanelData();

		public DateTime startTime;

		Dictionary<string, ReportData> reportDataDic = new Dictionary<string, ReportData>();
		Dictionary<ReportName, float> maxScoreDic = new Dictionary<ReportName, float>()
		{
			{ReportName.����һ_һ��,10 },{ReportName.����һ_����,6},{ReportName.����һ_�ȼ�,6},
			{ReportName.����һ_һ��,6},{ReportName.����һ_����,6},{ReportName.����һ_����,5},
			{ReportName.������_һ��,10},{ReportName.������_����,5},{ReportName.������_����,6},
			{ReportName.ʵ����,5},{ReportName.ʵ��Ԥϰ,10},{ReportName.֪ʶ����,25}
		};

		public ReportPanelData()
		{
			startTime = DateTime.Now;

			foreach (var item in EnumHelper.All<ReportName>())
			{
				reportDataDic.Add(item.ToString(), new ReportData()
				{
					maxScore = maxScoreDic[item]
				});
			}
		}

		public ReportData this[string key]
		{
			get => reportDataDic[key];
			set
			{
				value.haveData = true;
				reportDataDic[key] = value;
			}
		}

		public void RecordStartTime(ReportName reportName)
		{
			string key = reportName.ToString();
			ReportData reportData = reportDataDic[key];
			reportData.startTime = DateTime.Now;
			reportData.getedScore = 5;
			reportDataDic[key] = reportData;
		}

		public void RecordGetedScore(ReportName reportName, float getedScore = 0)
		{
			string key = reportName.ToString();
			ReportData reportData = reportDataDic[key];
			reportData.getedScore = getedScore;
			reportDataDic[key] = reportData;
		}

		public void RecordEndTime(ReportName reportName)
		{
			string key = reportName.ToString();
			ReportData reportData = reportDataDic[key];
			reportData.endTime = DateTime.Now;
			reportData.haveData = true;
			reportDataDic[key] = reportData;
		}
	}

	public partial class ReportPanel : UIPanel
	{
		[SerializeField] List<ReportItem> reportItems;
		protected override void OnInit(IUIData uiData = null)
		{
			mData = ReportPanelData.Instance;
			LoadData();

			btnSubmit.AddAwaitAction(async () => await imgDoubleConfirm.ShowAsync());

			btnDoubleConfirm.AddAwaitAction(Submit);
			btnDoubleCancel.AddAwaitAction(async () => await imgDoubleConfirm.HideAsync());
		}

		[ContextMenu("Change ReportItem Name")]
		void ChangeReportItemName()
		{
			List<int> list = new List<int>
			{
				10,6,6,
				6,6,5,
				10,5,6,
				5,10,25
			};
			int totalScore = 0;
			for (int i = 0; i < reportItems.Count; i++)
			{
				ReportItem item = reportItems[i];
				item.name = ((ReportName)i).ToString();
				item.tmpReport.text = item.name;
				item.tmpTotalScore.text = $"ʵ���ܷ֣�{list[i]}";
				totalScore += list[i];
			}
			Debug.Log(totalScore);
		}

		void LoadData()
		{
			//bool interactable = true;
			float totalScore = 0;
			foreach (var item in reportItems)
			{
				ReportData data = mData[item.tmpReport.text];
				if (data.haveData)
				{
					item.LoadData(data);
					totalScore += data.getedScore;
				}
				//else
				//{
				//	interactable = false;
				//}
			}

			//btnSubmit.interactable = interactable;

			tmpTotalScore.text = $"�ܵ÷֣�{totalScore}";
		}

		void UpdateTotalTime()
		{
			TimeSpan span = DateTime.Now - mData.startTime;
			tmpTotalTime.text = $"����ʱ��{span.Minutes}min{span.Seconds}s";
		}

		async UniTask Submit()
		{
			//SendData sendData = new SendData();
			//sendData.headData.Add("ʵ������", "�ɻ���Ƥ���ֻ���׼װ���������ʵ��");
			//foreach (var item in reportItems)
			//{
			//	ReportData reportData = ReportPanelData.Instance[item.name];

			//	ModuleData moduleData = new ModuleData()
			//	{
			//		reportName = item.name,
			//		startTime = reportData.startTime,
			//		endTime = reportData.endTime,
			//		score = (int)reportData.getedScore,
			//		maxScore = (int)reportData.maxScore
			//	};

			//	sendData.moduleDatas.Add(moduleData);
			//}

			//await WebKit.StartSendAsync(sendData, back => print(back.text)).ToUniTask();

			await imgDoubleConfirm.HideAsync();
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			MonoMgr.GetInstance().AddUpdateListener(UpdateTotalTime);
		}

		protected override void OnHide()
		{
			MonoMgr.GetInstance().RemoveUpdateListener(UpdateTotalTime);
		}

		protected override void OnClose()
		{
		}

	}
}
