using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace ProjectBase
{
	public class WebDataSender : MonoBehaviour
	{
		#region ����
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
		/// ʵ�������ύ�ӿ�
		/// �ӿ��������ýӿ���Ҫ�����ύʵ�����������Ľ�����ݣ�������������
		///	ʵ�鲽�衢ʵ�鱨���е����ֲ��֡�ʵ������Ľű������ڻط�ʵ��ȣ�
		/// </summary>
		private const string url_post = "http://api.vr-mooc.com/openapi/data_upload";

		Coroutine sending = null;

		/// <summary>
		/// ʹ��ͬ�����������첽������������
		/// </summary>
		/// <param name="postbytes"></param>
		public void StartSend(byte[] postbytes, Action<DownloadHandler> callBack)
		{
			if (sending != null) return;

			sending = StartCoroutine(SendAsync(postbytes, callBack));
		}

		/// <summary>
		/// �첽��������
		/// </summary>
		/// <param name="postObj"></param>
		/// <returns></returns>
		public IEnumerator SendAsync(byte[] postBytes, Action<DownloadHandler> callBack)
		{
			if (sending != null) yield break;

			//���������� ��̨
			UnityWebRequest request = UnityWebRequest.Post(url_post, UnityWebRequest.kHttpVerbPOST);


			//Ϊ��������ֵ
			request.uploadHandler = new UploadHandlerRaw(postBytes);
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", "application/json");
			yield return request.SendWebRequest();

			//��鷢�����ķ���ֵ
			if (!request.isNetworkError && !request.isHttpError)
			{
				Debug.Log($"������������ɹ���\n����ֵΪ��{request.downloadHandler.text}");
			}
			else
			{
				Debug.LogError($"������������ʧ�ܣ�ȷ�Ϲ�բ�ӿ� -{request.error}");
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

