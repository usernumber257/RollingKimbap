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
        //selectable �� ��ġ�� �ʱ� ���� hit �� �ݶ��̴��� �� �˻��ϰ�, select �� �ߺ����� �ʵ��� ���� ù ��°�� ���õǴ� selectable ���� �˻� ��
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
