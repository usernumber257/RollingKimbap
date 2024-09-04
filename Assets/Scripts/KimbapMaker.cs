using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class KimbapMaker : Maker
{
    [SerializeField] Kimbap kimbap;

    InteractableObject interactableObject;

    int originalKimbap = 7;
    int cheeseKimbap = 8;
    int kimchiKimbap = 8;
    int tunaKimbap = 10;

    int kimbapStack = -1;

    private void Awake()
    {
        interactableObject = GetComponent<InteractableObject>();
        interactableObject.OnInteract += Make;
    }

    public override void Make()
    {
        OnKeyDown += kimbap.StackKimbap;
        kimbap.curType = Kimbap.KimbapType.Original;
        Minigame_Keyboard(originalKimbap);
    }

    
}
