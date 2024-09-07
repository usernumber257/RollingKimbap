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
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null && hits[i].collider.gameObject.tag == "Selectable")
            {
                selectable = hits[i].collider.gameObject.GetComponent<SelectableObject>();

                if (selectable != null)
                    selectable.OnSelected?.Invoke(true);
            }
        }
    }
}
