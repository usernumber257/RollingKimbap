using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// holder �� ��ȣ�ۿ��Ͽ� holder �� ��� �ִ� ������Ʈ�� �ٸ� holder ���� �ǳ�
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
            holdTarget.OnInteract -= Hold; //���� Ÿ�ٰ��� �̺�Ʈ ����

        curHolder.alreadyHold = false;
        holdTarget.OnInteract += Hold;
    }

    void Hold(bool isInteracted)
    {
        if (!isInteracted || holdTarget == null)
        {
            //���� Ÿ�ٰ��� �̺�Ʈ ������ ����
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

        //�ٸ� holder �� ���� �ǳ׹���
        myholder.Hold(holdTarget.GetComponent<Holder>().Give());
        myholder.alreadyHold = true;

        holdTarget.OnInteract -= Hold;

        StartCoroutine(ColliderEnableTime());

        /*
        Collider2D holdTargetCol = holdTarget.GetComponent<Collider2D>(); //������� �� �ٸ� �繰�� Interact �� �ǰ�
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
            //���� Ÿ�ٰ��� �̺�Ʈ ������ ����
            if (serveTarget != null)
                serveTarget.OnInteract -= Serve;

            return;
        }

        serveTarget.GetComponent<Holder>().Hold(myholder.Give());
        OnServe?.Invoke();

        serveTarget.OnInteract -= Serve;

        StartCoroutine(ColliderEnableTime());

        /*
        Collider2D holdTargetCol = holdTarget.GetComponent<Collider2D>(); //�ٸ� �繰 ���� �������� �� �ٽ� �� �� �ֵ��� �ݶ��̴� ���ֱ�
        if (holdTargetCol != null)
            holdTargetCol.enabled = true;
        */
    }

    /// <summary>
    /// Serve �Ǵ� Hold �ߴ� ���� �ٷ� Serve �Ǵ� Hold �� �õ��ϱ� ����
    /// </summary>
    /// <returns></returns>
    IEnumerator ColliderEnableTime()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return null;
        GetComponent<Collider2D>().enabled = true;
    }
}
