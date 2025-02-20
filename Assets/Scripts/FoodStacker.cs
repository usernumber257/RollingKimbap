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

    Vector3 spawnPos = new Vector3(0f, 0.02f, 0f);

    Holder holder;

    public bool makingFood;

    AudioSource[] cook;
    AudioSource done;

    private void Awake()
    {
        holder = GetComponent<Holder>();

        cook = new AudioSource[3];

        cook[0] = GameObject.FindWithTag("Sounds").transform.GetChild(4).GetComponent<AudioSource>();
        cook[1] = GameObject.FindWithTag("Sounds").transform.GetChild(5).GetComponent<AudioSource>();
        cook[2] = GameObject.FindWithTag("Sounds").transform.GetChild(6).GetComponent<AudioSource>();
        done = GameObject.FindWithTag("Sounds").transform.GetChild(7).GetComponent<AudioSource>();
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
        if (curFood == null || !makingFood)
            return;

        Debug.Log($"{curFood.name} 쌓기 시작");

        if (curStack < curFood.Ingredients.Count)
        {
            cook[Random.Range(0, 3)].Play();

            pool[curStack].sprite = curFood.Ingredients[curStack].Model;
            pool[curStack].gameObject.SetActive(true);
        }
        
        curStack++;
    }

    public void Complete()
    {
        done.Play();

        Debug.Log($"{curFood.name} 완료");

        CompleteFood newFood = Instantiate(Resources.Load<CompleteFood>("CompleteFood"));
        newFood.Init(curFood.FoodType);

        holder.Hold(newFood.gameObject);

        //holder.Hold(newFood.gameObject);
        //holder.Hold(gameObject);

        Stop();
    }

    public void Stop()
    {
        DisactiveAllIngredients();

        curFood = null;
        makingFood = false;

        curStack = 0;
    }

    void DisactiveAllIngredients()
    {
        for (int i = 0; i < poolSize; i++)
            pool[i].gameObject.SetActive(false);
    }
}
