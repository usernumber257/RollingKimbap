using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class InventoryCombiner : MonoBehaviour
{
    Inventory inventory;

    [SerializeField] Food[] foods;

    Dictionary<Food, int> foodIngredients = new Dictionary<Food, int>(); //음식, 인벤토리에 있는 재료 수
    Dictionary<Food, bool> makeableFoods = new Dictionary<Food, bool>(); //음식, 만들 수 있는 지 여부

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    public void Combine()
    {
        for (int i = 0; i < foods.Length; i++)
            foodIngredients[foods[i]] = 0; //초기화

        foreach (Ingredient element in inventory.Slot.Keys) //인벤토리에 있는 재료가
        {
            for (int i = 0; i < foods.Length; i++) //한 음식의
            {
                for (int j = 0; j < foods[i].Ingredients.Count; j++) //재료 중 하나인지
                {
                    if (foods[i].Ingredients[j] == element)
                    {
                        if (!foodIngredients.ContainsKey(foods[i]))
                            foodIngredients.Add(foods[i], 0);

                        foodIngredients[foods[i]]++;
                    }
                }
            }
        }

        SetMakeable();
    }

    void SetMakeable()
    {
        for (int i = 0; i < foods.Length; i++)
            makeableFoods[foods[i]] = false; //초기화

        foreach (Food element in foodIngredients.Keys)
        {
            if (element.Ingredients.Count == foodIngredients[element]) //재료가 모두 모였으면
            {
                if (!makeableFoods.ContainsKey(element))
                    makeableFoods.Add(element, false);

                makeableFoods[element] = true; //만들 수 있는 음식 true
            }
        }
    }
}
