using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactor : MonoBehaviour
{
    public InteractableObject curInteractObj;
    public UnityAction<InteractableObject> OnTryInteract;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        curInteractObj = collision.gameObject.GetComponent<InteractableObject>();

        if (curInteractObj != null)
        {
            OnTryInteract?.Invoke(curInteractObj);
            curInteractObj.TryInteract(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        curInteractObj = collision.gameObject.GetComponent<InteractableObject>();

        if (curInteractObj != null)
        {
            curInteractObj.TryInteract(false);
        }
    }
}
