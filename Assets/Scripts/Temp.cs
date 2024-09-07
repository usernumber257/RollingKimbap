using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Ingredient item;
    public void Func()
    {
        inventory.Store(item, 1);
    }
}
