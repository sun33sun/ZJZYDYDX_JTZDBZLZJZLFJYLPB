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
    public class NPCController : MonoBehaviour
    {
        [SerializeField] internal NavMeshAgent _agent;
        [SerializeField] internal Animator _animator;

        public async UniTask WalkTo(Transform target, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;
            _agent.enabled = true;
            _agent.isStopped = false;
            _agent.SetDestination(target.position);
            _animator.Play("Walk");
            await UniTask.WaitUntil(() => Mathf.Abs(_agent.stoppingDistance - _agent.remainingDistance) < 0.05f,
                cancellationToken: token);
            _animator.Play("Idle");
            _agent.isStopped = true;
            transform.rotation = target.rotation;
            transform.position = target.position;
        }

        public virtual async UniTask PlayAnimAsync(string animName, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;
            if (_animator.HasState(0, Animator.StringToHash(animName)))
            {
                _animator.Play(animName);
                await UniTask.Yield();
                await _animator.GetAsyncAnimatorMoveTrigger().FirstOrDefaultAsync(token);
            }
            else
            {
                await UniTask.Yield();
                print($"没有动画<color=red>{animName}</color>");
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
                print($"没有动画<color=red>{animName}</color>");
            }
        }

        public async UniTask SitDown(Transform target, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;
            _agent.enabled = false;
            await UniTask.Yield(token);
            transform.rotation = target.rotation;
            transform.position = target.position;
            _animator.Play("SitDown");
            await _animator.GetAsyncAnimatorMoveTrigger().FirstOrDefaultAsync(token);
        }
    }
}