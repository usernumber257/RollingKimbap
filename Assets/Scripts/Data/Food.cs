using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Scriptable Object/Food", order = 1)]
public class Food : ScriptableObject
{
    [SerializeField] MyEnum.FoodType foodType;
    public MyEnum.FoodType FoodType { get { return foodType; } }

    [SerializeField] string foodName;
    public string FoodName { get { return foodName; } }

    [SerializeField] Sprite foodModel;
    public Sprite FoodModel { get { return foodModel; } }

    [SerializeField] int price;
    public int Price { get { return price; } }

    [SerializeField] List<Ingredient> ingredients;
    public List<Ingredient> Ingredients { get { return ingredients; } }
}
