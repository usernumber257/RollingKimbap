using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Interactor))]
[RequireComponent(typeof(Holder))]
public class Server: MonoBehaviour
{
    Interactor interactor;

    public Holder serveTarget;
    public InteractableObject holdTarget;

    Holder myholder;

    public void Awake()
    {
        interactor = GetComponent<Interactor>();
        myholder = GetComponent<Holder>();

        interactor.OnTryInteract += TryHold;
        interactor.OnTryInteract += TryServe;
    }

    void TryHold(InteractableObject obj)
    {
        if (obj.canServe)
        {
            holdTarget = obj;
            obj.OnInteract += Hold;
        }
        else
        {
            obj.OnInteract += Serve;
        }

    }

    void Hold(bool isInterated)
    {
        if (!isInterated)
            return;

        myholder.Hold(holdTarget.gameObject);
    }

    void TryServe(InteractableObject obj)
    {
        if (obj.canServe)
            return;

        serveTarget = obj.GetComponent<Holder>();
    }

    void Serve(bool isInterated)
    {
        if (!isInterated)
            return;

        if (serveTarget == null || holdTarget == null)
            return;

        serveTarget.Hold(holdTarget.gameObject);
        myholder.holdingObj = null;
    }
}
