using ProjectBase;
using System;
using System.Collections;
using UnityEngine.Networking;

public static class WebKit
{
	private static WebDataGenerator dataGenerator = new WebDataGenerator();

	public static void StartSend(SendData sendData, Action<DownloadHandler> callBack)
	{
		byte[] postbytes = dataGenerator.GeneratePostbytes(sendData);
		WebDataSender.Instance.StartSend(postbytes, callBack);
	}

	public static IEnumerator StartSendAsync(SendData sendData, Action<DownloadHandler> callBack)
	{
		byte[] postbytes = dataGenerator.GeneratePostbytes(sendData);
		yield return WebDataSender.Instance.SendAsync(postbytes, callBack);
	}

	public static void StopSend()
	{
		WebDataSender.Instance.StopSend();
	}
}
