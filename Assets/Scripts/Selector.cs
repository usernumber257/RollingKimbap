using UnityEngine;
using UnityEngine.Events;

public class Selecter : MonoBehaviour
{
    public UnityAction<bool> OnSelected;
    SelectableObject selectable;

    private void Awake()
    {
#if UNITY_IOS || UNITY_ANDROID
        MobileInputManager.Instance.confirm.onClick.AddListener(Mobile_Select);
#endif
    }


    private void OnDestroy()
    {
#if UNITY_IOS || UNITY_ANDROID
        MobileInputManager.Instance.confirm.onClick.RemoveAllListeners();
#endif
    }

    private void Update()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            Select();
#endif
    }


    public void Select()
    {
        //selectable 을 놓치지 않기 위해 hit 된 콜라이더를 다 검사하고, select 가 중복되지 않도록 가장 첫 번째로 선택되는 selectable 까지 검사 함
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null && hits[i].collider.gameObject.tag == "Selectable")
            {
                selectable = hits[i].collider.gameObject.GetComponent<SelectableObject>();

                if (selectable != null)
                    selectable.OnSelect();

                break;
            }
        }
    }

    public void Mobile_Select()
    {
        RaycastHit2D[] hits = Physics2D.CapsuleCastAll(transform.position, new Vector2(0.2f, 0.05f), CapsuleDirection2D.Vertical, 0f, Vector2.zero);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null && hits[i].collider.gameObject.tag == "Selectable")
            {
                selectable = hits[i].collider.gameObject.GetComponent<SelectableObject>();

                if (selectable != null)
                    selectable.OnSelect();

                break;
            }
        }
    }
}
