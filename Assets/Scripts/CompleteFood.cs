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

    public Food myFood;
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
                sprite.sprite = originalKimbap.Model;
                myFood = originalKimbap;
                break;
            case MyEnum.FoodType.CheeseKimbap:
                sprite.sprite = cheeseKimbap.Model;
                myFood = cheeseKimbap;
                break;
            case MyEnum.FoodType.KimchiKimbap:
                sprite.sprite = kimchiKimbap.Model;
                myFood = kimchiKimbap;
                break;
            case MyEnum.FoodType.TunaKimbap:
                sprite.sprite = tunaKimbap.Model;
                myFood = tunaKimbap;
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
