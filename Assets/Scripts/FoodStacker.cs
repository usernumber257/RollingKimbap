using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodStacker : MonoBehaviour
{
    [Header("Kimbap")]
    public Food originalKimbap;
    public Food cheeseKimbap;
    public Food kimchiKimbap;
    public Food tunaKimbap;

    int curStack = 0;
    public Food.FoodType curFoodType;
    Food curFood;

    SpriteRenderer[] pool;
    int poolSize = 10;

    Vector3 spawnPos = new Vector3(0f, 0.2f, 0f);

    private void Start()
    {
        MakePool();
    }

    void MakePool()
    {
        pool = new SpriteRenderer[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            SpriteRenderer newGo = new GameObject().AddComponent<SpriteRenderer>();
            newGo.transform.parent = transform;
            newGo.transform.localPosition = spawnPos;
            newGo.gameObject.SetActive(false);
            newGo.sortingOrder = 10 + i; 
            pool[i] = newGo;
        }
    }

    public void InitFood(Food.FoodType curFoodType)
    {
        switch (curFoodType)
        {
            case Food.FoodType.OriginalKimbap:
                curFood = originalKimbap;
                break;
            case Food.FoodType.CheeseKimbap:
                curFood = cheeseKimbap;
                break;
            case Food.FoodType.KimchiKimbap:
                curFood = kimchiKimbap;
                break;
            case Food.FoodType.TunaKimbap:
                curFood = tunaKimbap;
                break;
        }
    }

    public void StackIngredients()
    {
        if (curFood == null)
            return;

        pool[curStack].sprite = curFood.Ingredients[curStack];
        pool[curStack].gameObject.SetActive(true);

        curStack++;
    }

    public void Complete()
    {
        DisactiveAllIngredients();

        pool[0].sprite = curFood.FoodModel;
        pool[0].gameObject.SetActive(true);
    }

    void DisactiveAllIngredients()
    {
        for (int i = 0; i < poolSize; i++)
            pool[i].gameObject.SetActive(false);
    }
}
