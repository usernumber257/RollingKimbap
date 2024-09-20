using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    [SerializeField] Holder chair;
    [SerializeField] Holder place;

    Customer myCustomer;
    MyEnum.FoodType myOrder;

    public void OnCurstomerSit(Customer customer)
    {
        myCustomer = customer;
        myOrder = customer.MyOrder;

        place.OnHold += CompareOrder;
    }

    void CompareOrder(GameObject go)
    {
        CompleteFood readiedFood = go.GetComponent<CompleteFood>();

        if (readiedFood == null)
            return;

        if (readiedFood.foodType != myOrder)
            return;
        else
            Debug.Log("Correct food");
    }
}
