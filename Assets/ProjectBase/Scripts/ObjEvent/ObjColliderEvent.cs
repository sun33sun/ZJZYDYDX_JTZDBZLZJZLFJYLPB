using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ObjColliderEvent : MonoBehaviour
{
    MeshRenderer mr;
    bool isCollision = false;
    public Action<Collider> OnColliderEnterEvent;
    BoxCollider box;
    private string targetTag;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        box = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals(targetTag))
            return;
        OnColliderEnterEvent?.Invoke(other);
        OnColliderEnterEvent = null;
        isCollision = true;
        mr.enabled = false;
        box.enabled = false;
    }

    public async UniTask AreaHighlight(string targetTag = "Teacher")
    {
        mr.enabled = true;
        box.enabled = true;
        this.targetTag = targetTag;
        isCollision = false;
        await UniTask.WaitUntil(CheckCollision, cancellationToken: this.GetCancellationTokenOnDestroy());
    }

    bool CheckCollision()
    {
        return isCollision;
    }
}