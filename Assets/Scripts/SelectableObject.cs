using UnityEngine;
using UnityEngine.Events;

public class SelectableObject : MonoBehaviour
{
    public UnityAction<bool> OnSelected;

    public void OnSelect()
    {
        OnSelected?.Invoke(false);
        OnSelected?.Invoke(true);
    }
}
