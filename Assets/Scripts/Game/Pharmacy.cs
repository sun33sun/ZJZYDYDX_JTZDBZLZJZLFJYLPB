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

        public async void StartPharmacy(ObjSelectCase.Case caseType)
        {
            if (this.GetCancellationTokenOnDestroy().IsCancellationRequested)
                return;
            gameObject.SetActive(true);
            switch (caseType)
            {
                case ObjSelectCase.Case.MaleStudent:
                    MaleStudent.gameObject.SetActive(true);
                    Companion.transform.position = CompanionSource.transform.position;
                    Companion.transform.rotation = CompanionSource.transform.rotation;
                    try
                    {
                        await Companion.WalkTo(CompanionTarget.transform, _ctsEnable.Token);
                    }
                    catch (OperationCanceledException e)
                    {
                        print(e);
                    }
                    break;
                case ObjSelectCase.Case.FemaleClerk:
                    FemaleClerk.gameObject.SetActive(true);
                    FemaleClerk.transform.position = FemaleClerkSource.transform.position;
                    FemaleClerk.transform.rotation = FemaleClerkSource.transform.rotation;
                    Companion.transform.rotation = CompanionSource.transform.rotation;
                    Companion.transform.position = CompanionSource.transform.position;
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
        }

        private void OnEnable()
        {
            _ctsEnable = null;
            _ctsEnable = new CancellationTokenSource();
        }
    }
}