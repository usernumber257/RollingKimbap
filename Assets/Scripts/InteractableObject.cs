using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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
    /// ���� �̰� false ��� server�� �־ serve �ϴ� �ְ� �� ���� ����
    /// </summary>
    //public bool canBeHolded;

    private void Awake()
    {
        selectable = GetComponent<SelectableObject>();
    }

    private void OnEnable()
    {
        selectable.OnSelected -= Selected;
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
        if (this.isSelected && isSelected)
        {
            //���� ��ȣ�ۿ� �����ϰ�
            this.isSelected = false;
        }

        this.isSelected = isSelected;

        Interact();
    }

    void Interact()
    {
        if (!isSelected || !canInteract)
            return;

        OnInteract?.Invoke(true);
        SoundPlayer.Instance.Play(MyEnum.Sound.Click);

        /* ��ȣ�ۿ� �Ǹ� ��ȣ�ۿ� ������ ���°� �����ż� �ٽ� �ݸ����� �����ٰ� ���Ծ���ϴµ�, ���ŷο� �� ���Ƽ� ����
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

    float speed = 1.5f;
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
                    progress += speed * Time.deltaTime;
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
                    progress += speed * Time.deltaTime;
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
