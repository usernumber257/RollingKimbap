using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Scriptable Object/Food", order = int.MaxValue)]
public class Food : ScriptableObject
{
    public enum FoodType { None, OriginalKimbap, CheeseKimbap, KimchiKimbap, TunaKimbap }

    [SerializeField] string foodName;
    public string FoodName { get { return foodName; } }

    [SerializeField] Sprite foodModel;
    public Sprite FoodModel { get { return foodModel; } }

    [SerializeField] int price;
    public int Price { get { return price; } }

    [SerializeField] List<Sprite> ingredients;
    public List<Sprite> Ingredients { get { return ingredients; } }
}
