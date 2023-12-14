using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectBase;
using UnityEngine;
using QFramework;
using UnityEngine.Playables;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB.Game
{
	public partial class Task_FemaleClerk : ViewController
	{
		public PlayableDirector _director;
		private GamePanel _gamePanel;
		public CancellationToken _token;
		public GameState State = GameState.Stopped;
		public ReportPanelData _reportPanelData = new ReportPanelData();

		private void Awake()
		{
			_token = this.GetCancellationTokenOnDestroy();
			_director = GetComponent<PlayableDirector>();
		}

		public async void StartTask()
		{
			gameObject.SetActive(true);
			_gamePanel = UIKit.GetPanel<GamePanel>();
			await UniTask.WaitForSeconds(1, cancellationToken: _token);
			await ExtensionFunction._topPanel.OpenEye();
			_director.Play();
			State = GameState.Running;
			//实验报告_浸泡
			ReportPanelData.Instance.RecordStartTime(ReportName.病例一_浸泡);
		}

		/// <summary>
		/// 防止执行方法时，忘记控制Timeline或者出现token取消
		/// </summary>
		/// <param name="func"></param>
		void StartAsync(Func<UniTask> func)
		{
			UniTask.Void(async () =>
			{
				if (_token.IsCancellationRequested) return;
				State = GameState.Stopped;
				_director.Pause();
				await func();
				State = GameState.Running;
				_director.Play();
			});
		}

		internal void PauseGame()
		{
			_director.Pause();
		}

		internal void ResumeGame()
		{
			if (State == GameState.Running)
				_director.Resume();
		}

		internal void StopGame()
		{
			_director.Stop();
		}

		/// <summary>
		/// 浸泡答题
		/// </summary>
		public void SoakQuestion()
		{
			StartAsync(async () =>
			{
				float getedScore = await _gamePanel.WaitQuestion(ExtensionFunction.soakQuestion);
				//实验报告_浸泡
				ReportPanelData.Instance.RecordGetedScore(ReportName.病例一_浸泡, getedScore);
			});
		}

		public void BowlClick()
		{
			StartAsync(async () =>
			{
				await _gamePanel.WaitClock(-360, "浸泡时间为30~60分钟");
				//实验报告_浸泡
				ReportPanelData.Instance.RecordEndTime(ReportName.病例一_浸泡);
				//实验报告_先煎
				await _gamePanel.WaitTip("提示", "您已完成浸泡！点击下方按钮开始先煎", "开始先煎");
				ReportPanelData.Instance.RecordStartTime(ReportName.病例一_先煎);
				await Bowl.gameObject.HightlightClickAsync(_token);
			});
		}

		public void ElixationQuestion()
		{
			StartAsync(async () =>
			{
				float getedScore = await _gamePanel.WaitQuestion(ExtensionFunction.previewCokeQuestion);
				//实验报告_先煎
				ReportPanelData.Instance.RecordGetedScore(ReportName.病例一_先煎, getedScore);

				await Casserole.HightlightClickAsync(_token);
				LeftFire.gameObject.SetActive(true);
				LeftFire.Play();
				await _gamePanel.WaitClock(-180, "煎煮时间为30分钟左右");
				LeftFire.Stop();
				LeftFire.gameObject.SetActive(false);
				//实验报告_先煎
				ReportPanelData.Instance.RecordEndTime(ReportName.病例一_先煎);
				//实验报告_一煎
				await _gamePanel.WaitTip("提示", "您已完成先煎！点击下方按钮开始一煎", "开始一煎");
				ReportPanelData.Instance.RecordStartTime(ReportName.病例一_一煎);
				await Casserole.HightlightClickAsync(_token);
			});
		}

		public void FirstCoolDown()
		{
			StartAsync(async () => { await _gamePanel.WaitClock(-360, "将附子药液放凉"); });
		}

		public void OtherMedicant()
		{
			StartAsync(async () => { await OpenMedicineBag.HightlightClickAsync(_token); });
		}

		public void FirstCokeClick()
		{
			StartAsync(async () => { await Casserole.HightlightClickAsync(_token); });
		}

		public void FirstCokeQuestion()
		{
			StartAsync(async () =>
			{
				float getedScore = await _gamePanel.WaitQuestion(ExtensionFunction.firstCokeQuestion);
				//实验报告_一煎
				ReportPanelData.Instance.RecordGetedScore(ReportName.病例一_一煎, getedScore);

				LeftFire.gameObject.SetActive(true);
				LeftFire.Play();
				await _gamePanel.WaitClock(-180, "煎煮30分钟");
				LeftFire.Stop();
				LeftFire.gameObject.SetActive(false);
			});
		}

		/// <summary>
		/// 二煎
		/// </summary>
		public void SecondCokeKettleClick()
		{
			StartAsync(async () =>
			{
				//实验报告_一煎
				ReportPanelData.Instance.RecordEndTime(ReportName.病例一_一煎);
				//实验报告_二煎
				await _gamePanel.WaitTip("提示", "您已完成一煎！点击下方按钮开始二煎", "开始二煎");
				ReportPanelData.Instance.RecordStartTime(ReportName.病例一_二煎);
				await Kettle.HightlightClickAsync(_token);
			});
		}

		public void SecondCokeWaterQuestion()
		{
			StartAsync(async () =>
			{
				float getedScore = await _gamePanel.WaitQuestion(ExtensionFunction.secondCokeQuestion);
				//实验报告_二煎
				ReportPanelData.Instance.RecordGetedScore(ReportName.病例一_二煎, getedScore);
			});
		}

		public void SecondCokeQuestion()
		{
			StartAsync(async () =>
			{
				LeftFire.gameObject.SetActive(true);
				LeftFire.Play();
				await _gamePanel.WaitClock(-120, "第二次煎煮，20分钟");
				LeftFire.Stop();
				LeftFire.gameObject.SetActive(false);
				//实验报告_二煎
				ReportPanelData.Instance.RecordEndTime(ReportName.病例一_二煎);
				await _gamePanel.WaitTip("煎煮完成", "您成功制作四逆汤，接下来将返回诊所喂药。", "返回病房");
				//实验报告_二诊
				ReportPanelData.Instance.RecordStartTime(ReportName.病例二_二诊);
			});
		}

		public void FeedingMedicine()
		{
			StartAsync(async () =>
			{
				await _gamePanel.WaitProgressBar("患者服药中", 10);
				await _gamePanel.WaitTip("二诊",
					"症状：病见起色，语声清晰，水肿渐退，腰痛复作，血压160-100mmHg（毫米汞柱），余症同前。", "结束二诊");
				//实验报告_二诊
				ReportPanelData.Instance.RecordGetedScore(ReportName.病例二_二诊);
				ReportPanelData.Instance.RecordEndTime(ReportName.病例二_二诊);
				//实验报告_三诊
				ReportPanelData.Instance.RecordStartTime(ReportName.病例二_三诊);
			});
		}

		public void SecondDiagnosis()
		{
			StartAsync(async () =>
			{
				float getedScore = await _gamePanel.WaitQuestion(ExtensionFunction.secondVisitQuestion);
				//实验报告_三诊
				ReportPanelData.Instance.RecordGetedScore(ReportName.病例二_三诊, getedScore);

				drugStorage.Play();
				double totalTime = drugStorage.duration;
				await UniTask.WaitUntil(() => totalTime - drugStorage.time < 0.1);
			});
		}

		public void ThirdDiagnosis()
		{
			UniTask.Void(async () =>
			{
				if (_token.IsCancellationRequested) return;
				State = GameState.Stopped;
				_director.Pause();

				await _gamePanel.WaitProgressBar("患者服药中", 10);
				await _gamePanel.WaitTip("三诊",
				"症状：肿已退尽，余症悉消，血压130-90mmHg（毫米汞柱），尿常规，蛋白 (+)。" +
				"\n\n医嘱：服桂附地黄丸以固疗效。", "结束三诊");
				//实验报告_三诊
				ReportPanelData.Instance.RecordEndTime(ReportName.病例二_三诊);
				await _gamePanel.WaitTip("结束提示", "您已完成治疗！点击下方按钮返回主页", "返回主页");
				await GameRoot.Instance.EndCase();
			});

		}
	}
}