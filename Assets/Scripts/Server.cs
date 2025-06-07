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
            return;

        curHolder.alreadyHold = false;

        holdTarget.OnInteract += Hold;
    }

    void Hold(bool isInteracted)
    {
        if (!isInteracted || holdTarget == null)
        {
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
        GetComponent<Collider2D>().enabled = false; //Hold 하면 다시 바로 Serve 할 수 있게
        GetComponent<Collider2D>().enabled = true;

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
            if (serveTarget != null)
                serveTarget.OnInteract -= Serve;

            return;
        }

        serveTarget.GetComponent<Holder>().Hold(myholder.Give());
        OnServe?.Invoke();

        serveTarget.OnInteract -= Serve;

        GetComponent<Collider2D>().enabled = false; //Serve 하면 다시 바로 Hold 할 수 있게
        GetComponent<Collider2D>().enabled = true;

        /*
        Collider2D holdTargetCol = holdTarget.GetComponent<Collider2D>(); //다른 사물 위에 놓여졌을 땐 다시 들 수 있도록 콜라이더 켜주기
        if (holdTargetCol != null)
            holdTargetCol.enabled = true;
        */
    }
}
