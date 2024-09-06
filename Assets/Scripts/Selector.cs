using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selecter : MonoBehaviour
{
    public UnityAction<bool> OnSelected;
    SelectableObject selectable;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Select();
    }

    public void Select()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            selectable = hit.collider.gameObject.GetComponent<SelectableObject>();

            if (selectable != null)
                selectable.OnSelected?.Invoke(true);
        }
    }
}
