using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB.Game
{
    public partial class Pharmacy : ViewController
    {
        private CancellationTokenSource _ctsEnable;
        private Transform _cameraTrans;

        public async void StartPharmacy(Case caseType, Camera camera)
        {
            if (this.GetCancellationTokenOnDestroy().IsCancellationRequested)
                return;
            gameObject.SetActive(true);
            _cameraTrans = camera.transform;
            _cameraTrans.gameObject.SetActive(true);
            _cameraTrans.rotation = PharmacyCameraPosition.transform.rotation;
            _cameraTrans.position = PharmacyCameraPosition.transform.position;
            Companion.transform.rotation = CompanionSource.transform.rotation;
            Companion.transform.position = CompanionSource.transform.position;
            switch (caseType)
            {
                case Case.MaleStudent:
                    MaleStudent.gameObject.SetActive(true);
                    await UniTask.Yield();
                    try
                    {
                        await Companion.WalkTo(CompanionTarget.transform, _ctsEnable.Token);
                    }
                    catch (OperationCanceledException e)
                    {
                        print(e);
                    }

                    break;
                case Case.FemaleClerk:
                    FemaleClerk.gameObject.SetActive(true);
                    FemaleClerk.transform.rotation = FemaleClerkSource.transform.rotation;
                    FemaleClerk.transform.position = FemaleClerkSource.transform.position;
                    FemaleClerk.PlayAnim("Idle");
                    await UniTask.Yield();
                    try
                    {
                        Companion.WalkTo(CompanionTarget.transform, _ctsEnable.Token).Forget();
                        await FemaleClerk.WalkTo(FemaleClerkTarget.transform, _ctsEnable.Token);
                        await FemaleClerk.SitDown(FemaleClerkTarget.transform, _ctsEnable.Token);
                    }
                    catch (OperationCanceledException e)
                    {
                        print(e);
                    }

                    break;
            }
        }

        private void OnDisable()
        {
            _ctsEnable.Cancel();
            FemaleClerk.gameObject.SetActive(false);
            MaleStudent.gameObject.SetActive(false);
            _cameraTrans.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _ctsEnable = null;
            _ctsEnable = new CancellationTokenSource();
        }
    }
}