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
    /// 트리거 안에 들어간 상태에서, 다른 오브젝트와 중첩으로 트리거 됐을 경우를 대비하기 위함
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        InteractableObject newInteractObj = collision.gameObject.GetComponent<InteractableObject>();

        if (newInteractObj != null && newInteractObj != curInteractObj)
        {
            // 기존 interactObj 를 비활성화 하고
            if (curInteractObj != null)
                curInteractObj.TryInteract(false);

            // 새로운 애로
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
