using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] List<Ingredient> item = new List<Ingredient>();
    public void Start()
    {
        foreach (Ingredient element in item)
        {
            inventory.Store(element, 2);
        }
    }
}
