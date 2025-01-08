using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/Ingredient", order = 1)]
public class Ingredient : Item
{
    [SerializeField] MyEnum.IngredientType type;
    public MyEnum.IngredientType Type { get { return type; } }
}
