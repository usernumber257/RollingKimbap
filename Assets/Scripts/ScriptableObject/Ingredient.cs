using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/Item", order = 1)]
public class Ingredient : ScriptableObject
{
    MyEnum.IngredientType type;
    public MyEnum.IngredientType Type { get { return type; } }

    string ingredientName;
    public string IngredientName { get { return ingredientName; } }

    Sprite model;
    public Sprite Model { get { return model; } }
}
