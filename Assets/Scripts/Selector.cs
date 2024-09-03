using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selecter : MonoBehaviour
{
    public UnityAction<bool> OnSelected;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            Select();
    }

    public void Select()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject == gameObject)
                OnSelected?.Invoke(true);
            else
                OnSelected?.Invoke(false);
        }
    }
}
