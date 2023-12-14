using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public class ReportData
	{
		public DateTime startTime;
		public DateTime endTime;
		public float getedScore;
		public float maxScore;
		public bool haveData;
	}
	public class ReportItem : MonoBehaviour
	{
		public TextMeshProUGUI tmpReport;
		public TextMeshProUGUI tmpStartTime;
		public TextMeshProUGUI tmpEndTime;
		public TextMeshProUGUI tmpTotalTime;
		public TextMeshProUGUI tmpGetedScore;
		public TextMeshProUGUI tmpTotalScore;

		public void LoadData(ReportData data)
		{
			tmpStartTime.text = "开始：" + data.startTime.ToString("yyyy-MM-dd HH:mm:ss");
			tmpEndTime.text = "结束：" + data.endTime.ToString("yyyy-MM-dd HH:mm:ss");
			TimeSpan span = data.endTime - data.startTime;
			tmpTotalTime.text = $"用时：{span.Minutes}min{span.Seconds}s";
			tmpGetedScore.text = $"得分：{data.getedScore}";
		}
	}
}

