using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCombiner : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    [SerializeField] Food[] foods;

    Dictionary<Food, int> foodIngredients = new Dictionary<Food, int>(); //음식, 인벤토리에 있는 재료 수
    Dictionary<Food, bool> makeableFoods = new Dictionary<Food, bool>(); //음식, 만들 수 있는 지 여부

    //UI
    Slot[] slots;
    int slotSize = 10;
    [SerializeField] Slot slotPrefab;
    [SerializeField] Transform slotParent;

    [SerializeField] InteractableObject interactable;
    [SerializeField] GameObject body;

    int slotIndex;

    [SerializeField] Maker maker;

    AudioSource click;


    private void Awake()
    {
        interactable.OnInteract += Show;

        Init();

        click = GameObject.FindWithTag("Sounds").transform.GetChild(0).GetComponent<AudioSource>();
    }

    void Init()
    {
        slots = new Slot[slotSize];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(slotPrefab);
            slots[i].transform.parent = slotParent;
            slots[i].gameObject.SetActive(false);
            slots[i].transform.localScale = new Vector3(2f, 2f, 2f);
        }
        body.SetActive(false);
    }

    void Show(bool isInterated)
    {
        if (maker.FoodStacker.makingFood) //만들어지고 있는 중이라면 조합을 더이상 보여주지 않음
        {
            body.SetActive(false);
            return;
        }

        body.SetActive(isInterated);

        ResetSlots();
        Combine();
        SetMakeable();
    }

    void ResetSlots()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].gameObject.SetActive(false);

        slotIndex = 0;
    }

    void Combine()
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

                        if (inventory.Slot[element] <= 0) //인벤토리에 0개라면 모이지 않음
                            continue;
                        else
                            foodIngredients[foods[i]]++;
                    }
                }
            }
        }
    }

    void SetMakeable()
    {
        for (int i = 0; i < foods.Length; i++) //초기화
            makeableFoods[foods[i]] = false;

        foreach (Food element in foodIngredients.Keys)
        {
            if (element.Ingredients.Count == foodIngredients[element]) //재료가 모두 모였으면
            {
                if (!makeableFoods.ContainsKey(element))
                    makeableFoods.Add(element, false);

                makeableFoods[element] = true; //만들 수 있는 음식 true

                ShowMakeable(element);
            }
        }
    }

    Food _makeableFood;
    Food makeableFood { get { return _makeableFood; } 
        set 
        {
            _makeableFood = value;
            StartMake();
        }
    }

    void ShowMakeable(Food makeableFood)
    {
        slots[slotIndex].sprite.sprite = makeableFood.Model;
        slots[slotIndex].nameText.text = makeableFood.ItemName;
        slots[slotIndex].countText.gameObject.SetActive(false);
        slots[slotIndex].gameObject.SetActive(true);

        Button button = slots[slotIndex].GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { this.makeableFood = makeableFood; ConsumeItems(makeableFood); body.SetActive(false); click.Play(); }) ;

        slotIndex++;
    }

    void StartMake()
    {
        maker.StartMake(makeableFood);
    }

    void ConsumeItems(Food makeableFood)
    {
        foreach (Ingredient element in makeableFood.Ingredients)
            inventory.Take(element, 1);
    }
}
