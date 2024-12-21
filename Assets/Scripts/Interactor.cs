using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactor : MonoBehaviour
{
    public InteractableObject curInteractObj;
    public UnityAction<InteractableObject> OnTryInteract;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractableObject newInteractObj = collision.gameObject.GetComponent<InteractableObject>();

        if (newInteractObj != null)
        {
            curInteractObj = newInteractObj;
            curInteractObj.TryInteract(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractableObject newInteractObj = collision.gameObject.GetComponent<InteractableObject>();

        if (newInteractObj != null)
            newInteractObj.TryInteract(false);
    }
}
