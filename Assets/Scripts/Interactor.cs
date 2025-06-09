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
            OnTryInteract?.Invoke(curInteractObj);
        }
    }

    /// <summary>
    /// Ʈ���� �ȿ� �� ���¿���, �ٸ� ������Ʈ�� ��ø���� Ʈ���� ���� ��츦 ����ϱ� ����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        InteractableObject newInteractObj = collision.gameObject.GetComponent<InteractableObject>();

        if (newInteractObj != null && newInteractObj != curInteractObj)
        {
            // ���� interactObj �� ��Ȱ��ȭ �ϰ�
            if (curInteractObj != null)
                curInteractObj.TryInteract(false);

            // ���ο� �ַ�
            curInteractObj = newInteractObj;
            curInteractObj.TryInteract(true);
            OnTryInteract?.Invoke(curInteractObj);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractableObject newInteractObj = collision.gameObject.GetComponent<InteractableObject>();

        if (newInteractObj != null)
            newInteractObj.TryInteract(false);
    }
}
