using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// holder 와 상호작용하여 holder 가 들고 있는 오브젝트를 다른 holder 에게 건넴
/// </summary>
[RequireComponent(typeof(Interactor))]
[RequireComponent(typeof(Holder))]
public class Server : MonoBehaviour
{
    Interactor interactor;

    public InteractableObject serveTarget;
    public InteractableObject holdTarget;

    Holder myholder;

    public UnityAction OnServe;

    Holder curHolder;
    Server curServer;

    public void Awake()
    {
        interactor = GetComponent<Interactor>();
        myholder = GetComponent<Holder>();

        interactor.OnTryInteract += DoHoldOrServe;
    }

    void DoHoldOrServe(InteractableObject obj)
    {
        curHolder = obj.GetComponent<Holder>();

        if (curHolder == null)
        {
            holdTarget = null;
            serveTarget = null;
            return;
        }

        if (curHolder.alreadyHold && myholder.holdingObj == null)
        {
            holdTarget = obj;
            serveTarget = obj;
            TryHold();
        }
        else if (!curHolder.alreadyHold && myholder.holdingObj != null)
        {
            serveTarget = obj;
            holdTarget = obj;
            TryServe();
        }
    }

    void TryHold()
    {
        if (myholder.holdingObj != null)
            holdTarget.OnInteract -= Hold; //이전 타겟과의 이벤트 해지

        curHolder.alreadyHold = false;
        holdTarget.OnInteract += Hold;
    }

    void Hold(bool isInteracted)
    {
        if (!isInteracted || holdTarget == null)
        {
            //이전 타겟과의 이벤트 해지를 위함
            if (holdTarget != null)
                holdTarget.OnInteract -= Hold; 

            return;
        }

        Holder targatHolder = holdTarget.GetComponent<Holder>();
        
        if (targatHolder == null)
        {
            holdTarget.OnInteract -= Hold;

            return;
        }

        //다른 holder 로 부터 건네받음
        myholder.Hold(holdTarget.GetComponent<Holder>().Give());
        myholder.alreadyHold = true;

        holdTarget.OnInteract -= Hold;

        StartCoroutine(ColliderEnableTime());

        /*
        Collider2D holdTargetCol = holdTarget.GetComponent<Collider2D>(); //들고있을 땐 다른 사물과 Interact 안 되게
        if (holdTargetCol != null)
            holdTargetCol.enabled = false;
        */
    }

    void TryServe()
    {
        if (serveTarget == null)
            return;

        if (myholder.holdingObj == null)
            return;

        serveTarget.OnInteract += Serve;
    }

    void Serve(bool isInteracted)
    {
        if (!isInteracted || serveTarget == null)
        {
            //이전 타겟과의 이벤트 해지를 위함
            if (serveTarget != null)
                serveTarget.OnInteract -= Serve;

            return;
        }

        serveTarget.GetComponent<Holder>().Hold(myholder.Give());
        OnServe?.Invoke();

        serveTarget.OnInteract -= Serve;

        StartCoroutine(ColliderEnableTime());

        /*
        Collider2D holdTargetCol = holdTarget.GetComponent<Collider2D>(); //다른 사물 위에 놓여졌을 땐 다시 들 수 있도록 콜라이더 켜주기
        if (holdTargetCol != null)
            holdTargetCol.enabled = true;
        */
    }

    /// <summary>
    /// Serve 또는 Hold 했던 대상과 바로 Serve 또는 Hold 를 시도하기 위함
    /// </summary>
    /// <returns></returns>
    IEnumerator ColliderEnableTime()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return null;
        GetComponent<Collider2D>().enabled = true;
    }
}
