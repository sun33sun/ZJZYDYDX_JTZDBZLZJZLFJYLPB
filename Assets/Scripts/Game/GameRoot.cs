using System;
using Cysharp.Threading.Tasks;
using ProjectBase;
using UnityEngine;
using QFramework;
using UnityEngine.Playables;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB.Game
{
    public enum GameState
    {
        Running,
        Stopped
    }

    public partial class GameRoot : ViewController
    {
        //单例
        private static GameRoot instance;
        public static GameRoot Instance => instance;

        public Action PauseGame = null;
        public Action ResumeGame = null;
        public Func<UniTask> EndGame = null;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
        }

        public async void StartCokeMaleStudent()
        {
            await ExtensionFunction._topPanel.CloseEye();
            await ExtensionFunction.ClosePanelAsync();
            await ExtensionFunction.OpenPanelAsync<GamePanel>(GamePanel.Name);
            task_MaleStudent.StartTask();
            PauseGame = task_MaleStudent.PauseGame;
            ResumeGame = task_MaleStudent.ResumeGame;
            EndGame = EndCokeMaleStudent;
        }

        public async UniTask EndCokeMaleStudent()
        {
            PauseGame = null;
            ResumeGame = null;
            EndGame = null;
            await ExtensionFunction._topPanel.CloseEye();
            task_MaleStudent.StopGame();
            task_MaleStudent.gameObject.SetActive(false);
            await ExtensionFunction.ClosePanelAsync();
            await ExtensionFunction.OpenPanelAsync<MainPanel>(MainPanel.Name);
            await ExtensionFunction._topPanel.OpenEye();
        }

        public async void StartCokeFemaleClerk()
        {
            await ExtensionFunction._topPanel.CloseEye();
            await ExtensionFunction.ClosePanelAsync();
            await ExtensionFunction.OpenPanelAsync<GamePanel>(GamePanel.Name);
            task_FemaleClerk.StartTask();
            PauseGame = task_FemaleClerk.PauseGame;
            ResumeGame = task_FemaleClerk.ResumeGame;
            EndGame = EndCokeFemaleClerk;
        }

        public async UniTask EndCokeFemaleClerk()
        {
            PauseGame = null;
            ResumeGame = null;
            EndGame = null;
            await ExtensionFunction._topPanel.CloseEye();
            task_FemaleClerk.StopGame();
            task_FemaleClerk.gameObject.SetActive(false);
            await ExtensionFunction.ClosePanelAsync();
            await ExtensionFunction.OpenPanelAsync<MainPanel>(MainPanel.Name);
            await ExtensionFunction._topPanel.OpenEye();
        }

        public void StartPharmacy(ObjSelectCase.Case caseType)
        {
            Pharmacy.StartPharmacy(caseType);
        }

        public void EndPharmacy()
        {
            Pharmacy.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }
    }
}