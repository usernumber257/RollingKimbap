using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Interactor))]
[RequireComponent(typeof(Holder))]
public class Server: MonoBehaviour
{
    Interactor interactor;

    public Holder serveTarget;
    public InteractableObject holdTarget;

    Holder myholder;

    public UnityAction OnServe;

    public void Awake()
    {
        interactor = GetComponent<Interactor>();
        myholder = GetComponent<Holder>();

        interactor.OnTryInteract += TryHold;
        interactor.OnTryInteract += TryServe;
    }

    void TryHold(InteractableObject obj)
    {
        if (!obj.canBeHolded)
            return;

        holdTarget = obj;
        obj.OnInteract += Hold;
    }

    void Hold(bool isInteracted)
    {
        if (!isInteracted)
            return;

        if (myholder.holdingObj != null)
            return;

        myholder.Hold(holdTarget.gameObject);

        Collider2D holdTargetCol = holdTarget.GetComponent<Collider2D>(); //들고있을 땐 다른 사물과 Interact 안 되게
        if (holdTargetCol != null)
            holdTargetCol.enabled = false;
    }

    void TryServe(InteractableObject obj)
    {
        if (obj.canBeHolded)
            return;

        serveTarget = obj.GetComponent<Holder>();
        obj.OnInteract += Serve;
    }

    void Serve(bool isInteracted)
    {
        if (!isInteracted)
            return;

        if (serveTarget == null || holdTarget == null || myholder.holdingObj == null)
            return;

        serveTarget.Hold(myholder.holdingObj);
        myholder.holdingObj = null;
        OnServe?.Invoke();

        Collider2D holdTargetCol = holdTarget.GetComponent<Collider2D>(); //다른 사물 위에 놓여졌을 땐 다시 들 수 있도록 콜라이더 켜주기
        if (holdTargetCol != null)
            holdTargetCol.enabled = true;
    }
}
