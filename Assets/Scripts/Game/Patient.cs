using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB.Game
{
	public partial class Patient : ViewController
	{
		private Transform _cameraTrans;
		
		public void StartPatient(Camera camera,Disease disease)
		{
			_cameraTrans = camera.transform;
			_cameraTrans.rotation = PatientCameraPosition.transform.rotation;
			_cameraTrans.position = PatientCameraPosition.transform.position;
			gameObject.SetActive(true);
			camera.gameObject.SetActive(true);
			Model.SetInteger("Diease", (int)disease + 1);
			// Model.Play(disease.ToString());
		}


		private void OnDisable()
		{
			if (_cameraTrans != null)
				_cameraTrans.gameObject.SetActive(false);
		}
	}
}
