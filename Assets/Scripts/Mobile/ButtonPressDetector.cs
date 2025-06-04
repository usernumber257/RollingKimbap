using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonPressDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool canPressed = false;
    public bool isPressed = false;

    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            if (canPressed)
                isPressed = true;
        }
        else
        {
            canPressed = false;
            isPressed = false;
        }
    }
  
    public void OnPointerEnter(PointerEventData eventData)
    {
        canPressed = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canPressed = false;
        isPressed = false;
    }
}
