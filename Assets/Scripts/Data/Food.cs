using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Scriptable Object/Food", order = 1)]
public class Food : Item
{
    [SerializeField] MyEnum.FoodType foodType;
    public MyEnum.FoodType FoodType { get { return foodType; } }

    [SerializeField] List<Ingredient> ingredients;
    public List<Ingredient> Ingredients { get { return ingredients; } }
}
