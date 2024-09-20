using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteFood : MonoBehaviour
{
    [Header("Kimbap")]
    public Food originalKimbap;
    public Food cheeseKimbap;
    public Food kimchiKimbap;
    public Food tunaKimbap;

    public MyEnum.FoodType foodType;
    public SpriteRenderer sprite;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Init(MyEnum.FoodType foodType)
    {
        this.foodType = foodType;
        
        switch (this.foodType)
        {
            case MyEnum.FoodType.OriginalKimbap:
                sprite.sprite = originalKimbap.FoodModel;
                break;
            case MyEnum.FoodType.CheeseKimbap:
                sprite.sprite = cheeseKimbap.FoodModel;
                break;
            case MyEnum.FoodType.KimchiKimbap:
                sprite.sprite = kimchiKimbap.FoodModel;
                break;
            case MyEnum.FoodType.TunaKimbap:
                sprite.sprite = tunaKimbap.FoodModel;
                break;
        }
    }

    public void Disappear()
    {
        animator.SetTrigger("Disappear");
        destoryRoutine = StartCoroutine(DestroyTime());
    }

    Coroutine destoryRoutine;
    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
