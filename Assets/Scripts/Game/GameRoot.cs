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

        private ObjSelectCase.Case _nowCase;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
        }

        public async void StartCase(ObjSelectCase.Case nowCase)
        {
            _nowCase = nowCase;
            await ExtensionFunction._topPanel.CloseEye();
            await ExtensionFunction.ClosePanelAsync();
            await ExtensionFunction.OpenPanelAsync<GamePanel>(GamePanel.Name);
            await drugStorage.StartDrugStorage(renderCamera);
            await ExtensionFunction._topPanel.CloseEye();
            UIKit.GetPanel<GamePanel>().DrugStorageAnimation = false;
            switch (_nowCase)
            {
                case ObjSelectCase.Case.MaleStudent:
                    task_MaleStudent.StartTask();
                    PauseGame = task_MaleStudent.PauseGame;
                    ResumeGame = task_MaleStudent.ResumeGame;
                    break;
                case ObjSelectCase.Case.FemaleClerk:
                    task_FemaleClerk.StartTask();
                    PauseGame = task_FemaleClerk.PauseGame;
                    ResumeGame = task_FemaleClerk.ResumeGame;
                    break;
            }
        }

        public async UniTask EndCase()
        {
            PauseGame = null;
            ResumeGame = null;
            await ExtensionFunction._topPanel.CloseEye();
            switch (_nowCase)
            {
                case ObjSelectCase.Case.MaleStudent:
                    task_MaleStudent.StopGame();
                    task_MaleStudent.gameObject.SetActive(false);
                    break;
                case ObjSelectCase.Case.FemaleClerk:
                    task_FemaleClerk.StopGame();
                    task_FemaleClerk.gameObject.SetActive(false);
                    break;
            }
            await ExtensionFunction.ClosePanelAsync();
            await ExtensionFunction.OpenPanelAsync<MainPanel>(MainPanel.Name);
            await ExtensionFunction._topPanel.OpenEye();
        }

        public void StartPharmacy(ObjSelectCase.Case caseType)
        {
            pharmacy.StartPharmacy(caseType, renderCamera);
        }

        public void EndPharmacy()
        {
            pharmacy.gameObject.SetActive(false);
        }

        public void StartPatient(Disease disease)
        {
            patient.StartPatient(renderCamera, disease);
        }

        public void EndPatient()
        {
            patient.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }
    }
}