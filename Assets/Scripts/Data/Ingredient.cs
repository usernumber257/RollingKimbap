using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/Ingredient", order = 1)]
public class Ingredient : ScriptableObject
{
    [SerializeField] MyEnum.IngredientType type;
    public MyEnum.IngredientType Type { get { return type; } }

    [SerializeField] string ingredientName;
    public string IngredientName { get { return ingredientName; } }

    [SerializeField] Sprite model;
    public Sprite Model { get { return model; } }
}
