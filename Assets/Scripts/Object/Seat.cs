using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Seat : MonoBehaviour
{
    [SerializeField] Holder chair;
    [SerializeField] Holder place;
    [SerializeField] SpriteRenderer chairModel;
    [SerializeField] Transform navPivotParent;
    public Transform[] navPivot;

    MyEnum.FoodType myFoodType;
    public Func<bool> OnFoodReadied;
    public MyEnum.FoodType MyFoodType { get { return myFoodType; } set { myFoodType = value; OnFoodReadied?.Invoke(); } }
    
    CompleteFood readiedFood;
    public CompleteFood ReadiedFood { get { return readiedFood; } }

    private void Awake()
    {
        SetNavPivot();
    }

    void SetNavPivot()
    {
        int childCount = navPivotParent.childCount;

        if (childCount == 0)
            return;

        navPivot = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
            navPivot[i] = navPivotParent.GetChild(i);
    }

    public void Sit(GameObject go)
    {
        chair.Hold(go);
        place.OnHold += CompareOrder;

        //레이어 작업
        chairModel.sortingOrder = -1;
    }

    public void Leave()
    {
        //레이어 작업
        chairModel.sortingOrder = 0;
    }

    void CompareOrder(GameObject go)
    {
        readiedFood = go.GetComponent<CompleteFood>();

        if (readiedFood == null)
            return;

        myFoodType = readiedFood.foodType;
    }

    public void Clear()
    {
        readiedFood = null;
    }
}
