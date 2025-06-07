using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �κ��丮�� �˻��� ���� �� �ִ� ������ �����ݴϴ�.
/// </summary>
public class InventoryCombiner : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    [SerializeField] Food[] foods;

    Dictionary<Food, int> foodIngredients = new Dictionary<Food, int>(); //����, �κ��丮�� �ִ� ��� ��
    Dictionary<Food, bool> makeableFoods = new Dictionary<Food, bool>(); //����, ���� �� �ִ� �� ����

    //UI
    Slot[] slots;
    int slotSize = 10;
    [SerializeField] Slot slotPrefab;
    [SerializeField] Transform slotParent;

    [SerializeField] InteractableObject interactable;
    [SerializeField] GameObject body;

    int slotIndex;

    [SerializeField] Maker maker;
    [SerializeField] UIBase inventoryUI;


    private void Awake()
    {
        interactable.OnInteract += Show;

        Init();
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
        if (maker.FoodStacker.makingFood) //��������� �ִ� ���̶�� ������ ���̻� �������� ����
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
            foodIngredients[foods[i]] = 0; //�ʱ�ȭ

        foreach (Ingredient element in inventory.Slot.Keys) //�κ��丮�� �ִ� ��ᰡ
        {
            for (int i = 0; i < foods.Length; i++) //�� ������
            {
                for (int j = 0; j < foods[i].Ingredients.Count; j++) //��� �� �ϳ�����
                {
                    if (foods[i].Ingredients[j] == element)
                    {
                        if (!foodIngredients.ContainsKey(foods[i]))
                            foodIngredients.Add(foods[i], 0);

                        if (inventory.Slot[element] <= 0) //�κ��丮�� 0����� ������ ����
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
        for (int i = 0; i < foods.Length; i++) //�ʱ�ȭ
            makeableFoods[foods[i]] = false;

        foreach (Food element in foodIngredients.Keys)
        {
            if (element.Ingredients.Count == foodIngredients[element]) //��ᰡ ��� ������
            {
                if (!makeableFoods.ContainsKey(element))
                    makeableFoods.Add(element, false);

                makeableFoods[element] = true; //���� �� �ִ� ���� true

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
        slots[slotIndex].nameText.text = SettingManager.Instance.isKor ? makeableFood.ItemName : makeableFood.ItemName_eng;
        slots[slotIndex].countText.gameObject.SetActive(false);
        slots[slotIndex].gameObject.SetActive(true);

        Button button = slots[slotIndex].GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { 
            this.makeableFood = makeableFood; 
            ConsumeItems(makeableFood); 
            UIManager.Instance.CloseUI(inventoryUI); 
            SoundPlayer.Instance.Play(MyEnum.Sound.Click);
            body.SetActive(false);
        }) ;

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
