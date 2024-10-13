using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] List<Ingredient> startItem = new List<Ingredient>();

    public void Start()
    {
        foreach (Ingredient element in startItem)
        {
            inventory.Store(element, 3);
        }
    }
}
