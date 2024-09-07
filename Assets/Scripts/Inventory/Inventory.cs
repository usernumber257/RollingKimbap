using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    /// <summary>
    /// Ingredient 재료, int 수량
    /// </summary>
    Dictionary<Ingredient, int> slot = new Dictionary<Ingredient, int>();
    public Dictionary<Ingredient, int> Slot = new Dictionary<Ingredient, int>();

    int slotSize = 20;
    public int SlotSize { get { return slotSize; } }

    /// <summary>
    /// bool true 는 Store, false 는 Take
    /// </summary>
    public UnityAction<Ingredient, int, bool> OnUse;
    
    public void Take(Ingredient item, int count)
    {
        if (!slot.ContainsKey(item) || slot[item] <= 0)
        {
            Debug.Log($"{gameObject.name}'s inventory: {item} doesn't exist!");
            return;
        }

        slot[item] -= count;
        OnUse?.Invoke(item, count, false);
    }

    public void Store(Ingredient item, int count)
    {
        if (slot.Count >= slotSize)
        {
            Debug.Log($"{gameObject.name}'s inventory: slot is full!");
            return;
        }

        if (!slot.ContainsKey(item))
            slot.Add(item, 0);

        slot[item] += count;
        OnUse?.Invoke(item, count, true);
    }

}
