using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractableObject interactAllower = collision.gameObject.GetComponent<InteractableObject>();

        if (interactAllower != null)
        {
            interactAllower.TryInteract(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractableObject interactAllower = collision.gameObject.GetComponent<InteractableObject>();

        if (interactAllower != null)
        {
            interactAllower.TryInteract(false);
        }
    }
}
