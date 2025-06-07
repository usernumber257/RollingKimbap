using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject combinerBody; //냉장고와 인터랙션 했을 때 조합을 같이 보여줍니다. 상점에선 보여주지 않습니다.

    Slot[] slots; //슬롯 칸
    [SerializeField] Slot slotPrefab;
    [SerializeField] Transform slotParent;

    List<Ingredient> existItems = new List<Ingredient>(); //슬롯에 존재하는 아이템 목록

    [SerializeField] InteractableObject interactable;

    protected override void Awake()
    {
        base.Awake();

        inventory.OnUse += SetSlot;

        if (interactable != null)
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
            slots[i].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }

        UIManager.Instance.CloseUI(this);
    }
    void Show(bool isInterated)
    {
        if (isInterated)
            UIManager.Instance.OpenUI(this);
        else
            UIManager.Instance.CloseUI(this);
    }

    void SetSlot(Ingredient item, int count, bool isStore)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite.sprite = null;
            slots[i].nameText.text = "";
            slots[i].Count = 0;
        }

        if (inventory.Slot[item] <= 0)
            existItems.Remove(item);

        if (!existItems.Contains(item) && inventory.Slot[item] > 0)
            existItems.Add(item);

        int index = 0;
        foreach (Ingredient element in existItems)
        {
            slots[index].sprite.sprite = element.Model;
            slots[index].nameText.text = SettingManager.Instance.isKor ? element.ItemName : element.ItemName_eng;
            slots[index].Count = inventory.Slot[element];
            index++;
        }
    }

    public override void UIManager_Open()
    {
        base.UIManager_Open();

        if (combinerBody != null)
            combinerBody.SetActive(true);
    }

    public override void UIManager_Close()
    {
        base.UIManager_Close();

        if (combinerBody != null)
            combinerBody.SetActive(false);
    }

}
