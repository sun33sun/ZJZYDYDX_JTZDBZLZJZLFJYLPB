using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectBase
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class NPCController : MonoBehaviour
    {
        [SerializeField] protected NavMeshAgent _agent;
        [SerializeField] protected Animator _animator;
        
        public async UniTask WalkTo(Transform target)
        {
            _agent.SetDestination(target.position);
            _animator.Play("走路");
            await UniTask.WaitUntil(() => _agent.remainingDistance <= _agent.stoppingDistance);
            _animator.Play("站立");
        }

        public virtual async UniTask PlayAnimAsync(string animName)
        {
            if(this.GetCancellationTokenOnDestroy().IsCancellationRequested)
                return;
            if (_animator.HasState(0, Animator.StringToHash(animName)))
            {
                _animator.Play(animName);
                _animator.Play(animName);
                await UniTask.Yield();
                await UniTask.WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
            }
            else
            {
                await UniTask.Yield();
                print($"没有动画<color=green>{animName}</color>");
            }
        }

        public virtual void PlayAnim(string animName)
        {
            if (_animator.HasState(0, Animator.StringToHash(animName)))
            {
                _animator.Play(animName);
            }
            else
            {
                print($"没有动画<color=green>{animName}</color>");
            }
        }

        public async UniTask SitDown(Transform target)
        {
            // transform.position = target.position;
            // transform.forward = target.forward;
            _animator.Play("坐下");
            await _animator.GetAsyncAnimatorMoveTrigger().FirstOrDefaultAsync(this.GetCancellationTokenOnDestroy());
        }
        
    }
}