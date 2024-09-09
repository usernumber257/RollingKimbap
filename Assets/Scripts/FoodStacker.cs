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

    public Food curFood;

    SpriteRenderer[] pool;
    int poolSize = 10;

    Vector3 spawnPos = new Vector3(0f, 0.2f, 0f);

    Holder holder;

    public bool canMakeFood;

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

    public void StackIngredients()
    {
        if (curFood == null || !canMakeFood)
            return;

        if (curStack < curFood.Ingredients.Count)
        {
            pool[curStack].sprite = curFood.Ingredients[curStack].Model;
            pool[curStack].gameObject.SetActive(true);
        }
        
        curStack++;
    }

    public void Complete()
    {
        CompleteFood newFood = Instantiate(Resources.Load<CompleteFood>("CompleteFood"));
        newFood.gameObject.name = curFood.FoodName;
        newFood.sprite.sprite = curFood.FoodModel; 

        holder.Hold(newFood.gameObject);

        Stop();
    }

    public void Stop()
    {
        DisactiveAllIngredients();

        curFood = null;
        canMakeFood = false;

        curStack = 0;
    }

    void DisactiveAllIngredients()
    {
        for (int i = 0; i < poolSize; i++)
            pool[i].gameObject.SetActive(false);
    }
}
