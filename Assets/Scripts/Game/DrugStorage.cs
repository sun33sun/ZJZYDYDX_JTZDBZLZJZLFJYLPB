using System;
using Cysharp.Threading.Tasks;
using ProjectBase;
using UnityEngine;
using QFramework;
using UnityEngine.Playables;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB.Game
{
	public partial class DrugStorage : ViewController
	{
		private Transform _cameraTrans;
		public PlayableDirector _director;
		private bool isEnd = false;

		public void EndSignal()
		{
			isEnd = true;
		}
		
		public async UniTask StartDrugStorage(Camera camera)
		{
			isEnd = false;
			gameObject.SetActive(true);
			camera.gameObject.SetActive(true);
			_cameraTrans = camera.transform;
			_cameraTrans.rotation = DrugStorageCameraPosition.transform.rotation;
			_cameraTrans.position = DrugStorageCameraPosition.transform.position;
			_director.Play();
			await ExtensionFunction._topPanel.OpenEye();
			await UniTask.WaitUntil(() => isEnd);
		}
		
		private void OnDisable()
		{
			_director.Stop();
			_cameraTrans.gameObject.SetActive(false);
		}
	}
}
