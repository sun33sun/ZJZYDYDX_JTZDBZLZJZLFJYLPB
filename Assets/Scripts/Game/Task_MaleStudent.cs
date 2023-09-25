using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectBase;
using UnityEngine;
using QFramework;
using UnityEngine.Playables;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB.Game
{
    public partial class Task_MaleStudent : ViewController
    {
        public PlayableDirector _director;
        private GamePanel _gamePanel;
        public CancellationToken _token;
        public GameState State = GameState.Stopped;

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
                //浸泡
                await _gamePanel.WaitQuestion(ExtensionFunction.soakQuestion);
            });
        }

        public void BowlClick()
        {
            StartAsync(async () =>
            {
                await _gamePanel.WaitClock(-360, "浸泡时间为30~60分钟");
                //先煎
                await _gamePanel.WaitTip("提示", "您已完成浸泡！点击下方按钮开始先煎", "开始先煎");
                await Bowl.gameObject.HightlightClickAsync(_token);
            });
        }

        public void ElixationQuestion()
        {
            StartAsync(async () =>
            {
                await _gamePanel.WaitQuestion(ExtensionFunction.previewCokeQuestion);
                await Casserole.HightlightClickAsync(_token);
                LeftFire.gameObject.SetActive(true);
                LeftFire.Play();
                await _gamePanel.WaitClock(-180, "煎煮时间为30分钟左右");
                LeftFire.Stop();
                LeftFire.gameObject.SetActive(false);
                //一煎
                await _gamePanel.WaitTip("提示", "您已完成先煎！点击下方按钮开始一煎", "开始一煎");
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
                await _gamePanel.WaitQuestion(ExtensionFunction.firstCokeQuestion);
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
                await _gamePanel.WaitTip("提示", "您已完成一煎！点击下方按钮开始二煎", "开始二煎");
                await Kettle.HightlightClickAsync(_token);
            });
        }

        public void SecondCokeQuestion()
        {
            UniTask.Void(async () =>
            {
                if (_token.IsCancellationRequested) return;
                State = GameState.Stopped;
                _director.Pause();
                await _gamePanel.WaitQuestion(ExtensionFunction.secondCokeQuestion);
                LeftFire.gameObject.SetActive(true);
                LeftFire.Play();
                await _gamePanel.WaitClock(-120, "第二次煎煮，20分钟");
                LeftFire.Stop();
                LeftFire.gameObject.SetActive(false);
                await _gamePanel.WaitTip("二诊", 
                    "症状：手足已温，神清汗收，阳复脉出，血压100/70毫米汞柱。觉气短口微渴。投生脉散三剂而愈。" + 
                    "\n\n医嘱：温疟伤阴，吐泻竭液，阴亡阳脱，故首先回阳救脱，投四逆加人参汤；阳问脉复，继用生脉散，益气养阴以善后。", 
                    "结束二诊");
                await _gamePanel.WaitTip("结束提示", "您已完成治疗！点击下方按钮返回主页", "返回主页");
                await GameRoot.Instance.EndMaleStudent();
            });
        }
    }
}