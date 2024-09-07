using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Interactor))]
public class PlayerServer : Server
{
    Interactor interactor;

    public Server serveTarget;
    public InteractableObject holdTarget;

    public void Awake()
    {
        interactor = GetComponent<Interactor>();
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

    void Hold()
    {
        Hold(holdTarget.gameObject);
    }

    void TryServe(InteractableObject obj)
    {
        if (obj.canServe)
            return;

        serveTarget = obj.GetComponent<Server>();
    }

    new void Serve()
    {
        if (serveTarget == null || holdTarget == null)
            return;

        serveTarget.Hold(holdTarget.gameObject);
    }
}
