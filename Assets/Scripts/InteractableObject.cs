using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

[RequireComponent(typeof(SelectableObject))]
[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;

    SelectableObject selectable;

    bool canInteract;
    bool isSelected;

    public UnityAction<bool> OnInteract;

    /// <summary>
    /// 만약 이게 false 라면 server를 넣어서 serve 하는 애가 될 수도 있음
    /// </summary>
    public bool canServe;

    private void Awake()
    {
        selectable = GetComponent<SelectableObject>();
    }

    private void Start()
    {
        selectable.OnSelected += Selected;
    }

    public void TryInteract(bool canInteract)
    {
        this.canInteract = canInteract;
        CanInteractEffect(canInteract);

        if (canInteract == false)
            OnInteract?.Invoke(false);
    }
    
    void Selected(bool isSelected)
    {
        this.isSelected = isSelected;

        Interact();
    }

    void Interact()
    {
        if (!isSelected || !canInteract)
            return;

        OnInteract?.Invoke(true);

        isSelected = false;
        canInteract = false;

        if (lerpColorRoutine != null)
            StopCoroutine(lerpColorRoutine);

        sprite.color = Color.white;
    }

    Coroutine lerpColorRoutine;
    void CanInteractEffect(bool doEffect)
    {
        if (doEffect)
            lerpColorRoutine = StartCoroutine(LerpColorTime());
        else
        {
            StopCoroutine(lerpColorRoutine);
            sprite.color = Color.white;
        }
    }

    float speed = 0.01f;
    Color originColor = Color.white;
    Color toColor = Color.gray;
    Color curColor;

    IEnumerator LerpColorTime()
    {
        float progress = 0f;
        bool hover = true;

        while (true)
        {
            if (hover)
            {
                if (progress < 1f)
                {
                    curColor = Color.Lerp(originColor, toColor, progress);
                    sprite.color = curColor;
                    progress += speed;
                    yield return null;
                }
                else
                {
                    hover = false;
                    progress = 0f;
                }
            }
            else
            {
                if (progress < 1f)
                {
                    curColor = Color.Lerp(toColor, originColor, progress);
                    sprite.color = curColor;
                    progress += speed;
                    yield return null;
                }
                else
                {
                    hover = true;
                    progress = 0f;
                }    
            }
        }
        
        
    }
}
