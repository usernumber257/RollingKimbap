using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    Store store;
    Fridge fridge;

    bool storeActive = false;
    bool fridgeActive = false;

    private void Awake()
    {
        store = GameObject.FindWithTag("StoreUI").GetComponent<Store>();
        fridge = GameObject.FindWithTag("FridgeUI").GetComponent<Fridge>();
    }

    private void Update()
    {
        if (store.body.activeInHierarchy && fridgeActive == false)
            storeActive = true;
        else
            storeActive = false;

        if (fridge.combiner.activeInHierarchy && storeActive == false)
            fridgeActive = true;
        else
            fridgeActive = false;

        if (storeActive && fridge.combiner.activeInHierarchy)
        {
            store.OnESC();
            storeActive = false;
        }
        if (fridgeActive && store.body.activeInHierarchy)
        {
            fridge.OnESC();
            fridgeActive = false;
        }
    }
}
