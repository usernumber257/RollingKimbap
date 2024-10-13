using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Seat : MonoBehaviour
{
    [SerializeField] Holder chair;
    [SerializeField] Holder place;

    MyEnum.FoodType myFoodType;
    public Func<bool> OnFoodReadied;
    public MyEnum.FoodType MyFoodType { get { return myFoodType; } set { myFoodType = value; OnFoodReadied?.Invoke(); } }
    
    CompleteFood readiedFood;
    public CompleteFood ReadiedFood { get { return readiedFood; } }

    public void Sit(GameObject go)
    {
        chair.Hold(go);
        place.OnHold += CompareOrder;
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
