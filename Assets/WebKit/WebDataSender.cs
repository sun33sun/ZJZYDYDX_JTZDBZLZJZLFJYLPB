using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace ProjectBase
{
	public class WebDataSender : MonoBehaviour
	{
		#region 单例
		private static WebDataSender instance = null;
		public static WebDataSender Instance
		{
			get
			{
				if(instance == null)
				{
					instance = new GameObject().AddComponent<WebDataSender>();
					DontDestroyOnLoad(instance.gameObject);
				}
				return instance;
			}
		}

		private void OnDestroy()
		{
			if (instance == this)
			{
				instance = null;
			}
		}
		#endregion

		/// <summary>
		/// 实验数据提交接口
		/// 接口描述：该接口主要用于提交实验中所产生的结果数据，包括但不限于
		///	实验步骤、实验报告中的文字部分、实验产生的脚本（用于回放实验等）
		/// </summary>
		private const string url_post = "http://api.vr-mooc.com/openapi/data_upload";

		Coroutine sending = null;

		/// <summary>
		/// 使用同步方法调用异步方法发送数据
		/// </summary>
		/// <param name="postbytes"></param>
		public void StartSend(byte[] postbytes, Action<DownloadHandler> callBack)
		{
			if (sending != null) return;

			sending = StartCoroutine(SendAsync(postbytes, callBack));
		}

		/// <summary>
		/// 异步发送数据
		/// </summary>
		/// <param name="postObj"></param>
		/// <returns></returns>
		public IEnumerator SendAsync(byte[] postBytes, Action<DownloadHandler> callBack)
		{
			if (sending != null) yield break;

			//发送数据至 后台
			UnityWebRequest request = UnityWebRequest.Post(url_post, UnityWebRequest.kHttpVerbPOST);


			//为发送器赋值
			request.uploadHandler = new UploadHandlerRaw(postBytes);
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", "application/json");
			yield return request.SendWebRequest();

			//检查发送器的返回值
			if (!request.isNetworkError && !request.isHttpError)
			{
				Debug.Log($"发起网络请求成功！\n返回值为：{request.downloadHandler.text}");
			}
			else
			{
				Debug.LogError($"发起网络请求失败：确认过闸接口 -{request.error}");
			}
			callBack?.Invoke(request.downloadHandler);
			sending = null;
		}

		public void StopSend()
		{
			if (sending != null)
			{
				StopCoroutine(sending);
				sending = null;
			}
		}
	}

}

