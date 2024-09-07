using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Holder))]
public class FoodStacker : MonoBehaviour
{
    [Header("Kimbap")]
    public Food originalKimbap;
    public Food cheeseKimbap;
    public Food kimchiKimbap;
    public Food tunaKimbap;

    int curStack = 0;

    MyEnum.FoodType curFoodType;
    public MyEnum.FoodType CurFoodType { get { return curFoodType; } }

    Food curFood;
    public Food CurFood { get { return curFood; } }

    SpriteRenderer[] pool;
    int poolSize = 10;

    Vector3 spawnPos = new Vector3(0f, 0.2f, 0f);

    Holder holder;

    private void Awake()
    {
        holder = GetComponent<Holder>();
    }

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

    public void InitFood(MyEnum.FoodType curFoodType)
    {
        switch (curFoodType)
        {
            case MyEnum.FoodType.OriginalKimbap:
                curFood = originalKimbap;
                break;
            case MyEnum.FoodType.CheeseKimbap:
                curFood = cheeseKimbap;
                break;
            case MyEnum.FoodType.KimchiKimbap:
                curFood = kimchiKimbap;
                break;
            case MyEnum.FoodType.TunaKimbap:
                curFood = tunaKimbap;
                break;
        }
    }

    public void StackIngredients()
    {
        if (curFood == null)
            return;

        pool[curStack].sprite = curFood.Ingredients[curStack].Model;
        pool[curStack].gameObject.SetActive(true);

        curStack++;
    }

    public void Complete()
    {
        DisactiveAllIngredients();

        GameObject newFood = Instantiate(Resources.Load<GameObject>("CompleteFood"));
        newFood.gameObject.name = curFoodType.ToString();

        holder.Hold(newFood);
    }

    void DisactiveAllIngredients()
    {
        for (int i = 0; i < poolSize; i++)
            pool[i].gameObject.SetActive(false);
    }
}
