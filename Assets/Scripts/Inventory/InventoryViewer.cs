using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class InventoryViewer : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    Slot[] slots; //½½·Ô Ä­
    [SerializeField] Slot slotPrefab;
    [SerializeField] Transform slotParent;

    List<Ingredient> existItems = new List<Ingredient>(); //½½·Ô¿¡ Á¸ÀçÇÏ´Â ¾ÆÀÌÅÛ ¸ñ·Ï

    int usage = 0;

    [SerializeField] InteractableObject interactable;
    [SerializeField] public GameObject body;

    private void Awake()
    {
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

        body.SetActive(false);
    }
    void Show(bool isInterated)
    {
        body.SetActive(isInterated);
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
            slots[index].nameText.text = element.ItemName;
            slots[index].Count = inventory.Slot[element];
            index++;
        }
    }

}
