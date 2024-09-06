using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Interactor))]
public class PlayerServer : Server
{
    Interactor interactor;
    public InteractableObject interactableObj;

    public void Awake()
    {
        interactor = GetComponent<Interactor>();
        interactor.OnTryInteract += TryServe;
    }

    void TryServe(InteractableObject obj)
    {
        if (!obj.canServe)
            return;

        interactableObj = obj;
        obj.OnInteract += Hold;
    }

    void Hold()
    {
        Hold(interactableObj.gameObject);
    }
}
