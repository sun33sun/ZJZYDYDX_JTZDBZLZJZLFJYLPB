using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace ProjectBase
{
	public class WebDataGenerator
	{
		#region 使用JS从Token中获取的参数
		string token = null;
		string sequenceCode = null;
		string appId = null;
		string expId = null;
		string userAccount = null;
		string host = null;
		string un = null;
		string code = null;

		/// <summary>
		/// 负责获取在程序加载完成之前润尼尔传入进来的平台参数数据
		/// </summary>
		/// <returns></returns>
		[DllImport("__Internal")]
		private static extern string StringReturnValueFunction();

		private void ParseUrlMsg()
		{
			string urlMsg = null;

			try
			{
				urlMsg = StringReturnValueFunction();
				if (!string.IsNullOrEmpty(urlMsg))
				{
					Debug.Log($"urlMsg : {urlMsg}");
				}
			}
			catch (System.Exception e)
			{
				urlMsg = "[catch]" + e.Message;
			}
			if (urlMsg == null || urlMsg.Length < 1)
				return;

			string[] msgArray = urlMsg.Split('&');
			string[] tokenArray = new string[2];

			if (msgArray.Length == 7)
			{
				token = ParseValueFromSingeMsg(msgArray[0]);
				if (token.Contains("_"))
				{
					tokenArray = token.Split('_');
					if (tokenArray.Length == 2)
					{
						appId = tokenArray[0];
						expId = tokenArray[1];
					}
				}

				sequenceCode = ParseValueFromSingeMsg(msgArray[1]);
				userAccount = ParseValueFromSingeMsg(msgArray[3]);
				host = ParseValueFromSingeMsg(msgArray[4]);
				un = ParseValueFromSingeMsg(msgArray[5]);
				code = ParseValueFromSingeMsg(msgArray[6]);
			}
			for (int i = 0; i < msgArray.Length; i++)
			{
				Debug.Log($"msgArray[{i}] : {msgArray[i]}");
			}

			for (int i = 0; i < tokenArray.Length; i++)
			{
				Debug.Log($"tokenArray[{i}] : {tokenArray[i]}");
			}
		}

		/// <summary>
		/// 从单条信息中解析出值
		/// </summary>
		private string ParseValueFromSingeMsg(string singleMsg)
		{
			if (singleMsg.Contains("="))
			{
				string[] singleMsgArray = singleMsg.Split('=');
				if (singleMsgArray.Length == 2)
				{
					return singleMsgArray[1];
				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}
		}
		#endregion

		public byte[] GeneratePostbytes(SendData sendData)
		{
			if (appId == null)
				ParseUrlMsg();

			NetworkExperimentData experimentData = new NetworkExperimentData(appId, expId);

			//填充reportData
			Dictionary<string,TextDataBase> reportData = new Dictionary<string, TextDataBase>();
			foreach (var pair in sendData.headData)
				reportData.Add(pair.Key, new TextDataBase(pair.Value));
			
			experimentData.reportData.Add(reportData);

			for (int i = 0; i < sendData.moduleDatas.Count; i++)
			{
				ModuleData moduleData = sendData.moduleDatas[i];

				//填充expScoreDetails
				ExpScoreDetail scoreDetail = new ExpScoreDetail()
				{
					moduleFlag = moduleData.reportName,
					questionNumber = i,
					questionStem = "无",
					score = moduleData.score,
					trueOrFalse = "True",
					startTime = moduleData.startTime.ToString("yyyy-MM-dd HH:mm:ss"),
					expectTime = 0,
					maxScore = moduleData.maxScore,
					repeatCount = 1,
					evaluation = "无",
					scoringModel = "无",
					remarks = "无",
					endTime = moduleData.endTime.ToString("yyyy-MM-dd HH:mm:ss"),
				};
				experimentData.expScoreDetails.Add(scoreDetail);

				//暂时不需要填充expScriptContent
				//experimentData.expScriptContent.Add("无", "无");
			}
			string json = JsonConvert.SerializeObject(experimentData);


			return Encoding.UTF8.GetBytes(json); ;
		}
	}

	#region  实验数据结构类
	[System.Serializable]
	/// <summary>
	/// 传输到网络的实验数据类
	/// </summary>
	public class NetworkExperimentData
	{
		public string appId;

		public string expId;

		public NetworkExperimentData(string appId, string expId)
		{
			this.appId = appId;
			this.expId = expId;
		}

		public List<Dictionary<string, TextDataBase>> reportData = new List<Dictionary<string, TextDataBase>>();

		public List<ExpScoreDetail> expScoreDetails = new List<ExpScoreDetail>();

		public Dictionary<string, string> expScriptContent = new Dictionary<string, string>();
	}


	[System.Serializable]
	public class TextDataBase
	{
		public TextDataBase(string text)
		{
			this.text = text;
		}
		public string text;
	}

	[System.Serializable]
	public class ExpScoreDetail
	{
		public string moduleFlag;
		public int questionNumber;
		public string questionStem;
		public int score;
		public string trueOrFalse;
		public string startTime;
		public int expectTime;
		public int maxScore;
		public int repeatCount;
		public string evaluation;
		public string scoringModel;
		public string remarks;
		public string endTime;
	}

	[System.Serializable]
	public class ExperimentUniqueIdentificationData
	{
		public string appId;
		public string expId;
		public string dataType;
	}
	#endregion
}

/// <summary>
/// 每个ModuleData对应实验报告中的一个报告
/// </summary>
public class ModuleData
{
	public string reportName;
	public DateTime startTime;
	public DateTime endTime;
	public int score;
	public int maxScore;
}

/// <summary>
/// 使用SendData生成NetworkExperimentData，然后将NetworkExperimentData转成byte[]进行传输
/// </summary>
public class SendData
{
	public Dictionary<string, string> headData = new Dictionary<string, string>();
	public List<ModuleData> moduleDatas = new List<ModuleData>();
}