using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Selecter))]
[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;

    Selecter selecter;

    bool canInteract;
    bool isSelected;

    public UnityAction OnInteract;

    public bool canServe;

    private void Awake()
    {
        selecter = GetComponent<Selecter>();
    }

    private void Start()
    {
        selecter.OnSelected += Interact;
    }

    public void TryInteract(bool canInteract)
    {
        this.canInteract = canInteract;
        CanInteractEffect(canInteract);
    }
    
    void Interact(bool isSelected)
    {
        this.isSelected = isSelected;
    }

    private void Update()
    {
        if (isSelected && canInteract)
        {
            OnInteract?.Invoke();

            isSelected = false;
            canInteract = false;

            if (lerpColorRoutine != null)
                StopCoroutine(lerpColorRoutine);

            sprite.color = Color.white;
        }
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
        bool toCyan = true;

        while (true)
        {
            if (toCyan)
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
                    toCyan = false;
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
                    toCyan = true;
                    progress = 0f;
                }    
            }
        }
        
        
    }


}
