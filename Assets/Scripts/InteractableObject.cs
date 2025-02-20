using System.Collections;
using System.Collections.Generic;
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
    //public bool canBeHolded;

    AudioSource click;

    private void Awake()
    {
        selectable = GetComponent<SelectableObject>();

        click = GameObject.FindWithTag("Sounds").transform.GetChild(0).GetComponent<AudioSource>();
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
        click.Play();

        /* 상호작용 되면 상호작용 가능한 상태가 해제돼서 다시 콜리젼을 나갔다가 들어왔어야하는데, 번거로운 거 같아서 없앰
        isSelected = false;
        canInteract = false;

        if (sprite == null)
            return;

        if (lerpColorRoutine != null)
            StopCoroutine(lerpColorRoutine);

        sprite.color = Color.white;
        */
    }

    Coroutine lerpColorRoutine;
    void CanInteractEffect(bool doEffect)
    {
        if (sprite == null)
            return;

        if (doEffect)
            lerpColorRoutine = StartCoroutine(LerpColorTime());
        else
        {
            if (lerpColorRoutine != null)
                StopCoroutine(lerpColorRoutine);
            sprite.color = Color.white;
        }
    }

    float speed = 0.005f;
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
