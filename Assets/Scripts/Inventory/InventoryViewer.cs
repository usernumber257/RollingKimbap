using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryViewer : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    Slot[] slots;
    [SerializeField] Slot slotPrefab;
    [SerializeField] Transform slotParent;

    //몇 번째 칸에 뭐가 들었는지 알기 위함
    Dictionary<Ingredient, int> numberData = new Dictionary<Ingredient, int>();

    int usage = 0;

    [SerializeField] InteractableObject interactable;
    [SerializeField] GameObject body;

    private void Awake()
    {
        inventory.OnUse += SetSlot;
        interactable.OnInteract += Show;

        Init();
    }

    void Init()
    {
        slots = new Slot[inventory.SlotSize];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(slotPrefab);
            slots[i].transform.parent = slotParent;
        }

        body.SetActive(false);
    }

    void Show(bool isInterated)
    {
        body.SetActive(isInterated);
    }

    void SetSlot(Ingredient item, int count, bool isStore)
    {
        if (isStore)
        {
            if (!numberData.ContainsKey(item))
                numberData.Add(item, usage++); //한 번도 추가 된 적 없는 아이템이면 슬롯 하나를 가져감
        }
        else
        {
            if (!numberData.ContainsKey(item))
            {
                Debug.Log($"we don't have slot for {item.name} but you're trying to remove");
                return;
            }
        }

        slots[numberData[item]].sprite.sprite = item.Model;
        slots[numberData[item]].nameText.text = item.IngredientName;
        slots[numberData[item]].Count = isStore ? slots[numberData[item]].Count + count : slots[numberData[item]].Count - count;
    }
}
