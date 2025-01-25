using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] Slot slotPrefab;
    [SerializeField] Transform slotParent;
    [SerializeField] public GameObject body;
    [SerializeField] GameObject fridgeInventoryBody;
    [SerializeField] Button button;
    Slot[] slots;
    [SerializeField] Inventory fridgeInventory;

    int purchasedTable = 0;
    int tableSlotIndex;

    private void Start()
    {
        MakePool();
        body.SetActive(false);
        fridgeInventoryBody.SetActive(false);

        MappingButton();
    }

    void MakePool()
    {
        int slotCount = GameManager.Data.dataReferencer.sellItems.Count;
        slots = new Slot[slotCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(slotPrefab);
            slots[i].transform.parent = slotParent;

            Item curItem = GameManager.Data.dataReferencer.sellItems[i];
            
            //아이템 UI Init
            slots[i].sprite.sprite = curItem.Model;
            slots[i].nameText.text = curItem.ItemName;
            slots[i].countText.gameObject.SetActive(false);

            if (curItem is Ingredient)
                slots[i].GetComponent<Button>().onClick.AddListener(() => Sell((Ingredient)curItem));
            else if (curItem is Table)
            {
                tableSlotIndex = i;
                slots[i].GetComponent<Button>().onClick.AddListener(() => Sell((Table)curItem));
            }
        }
    }

    void MappingButton()
    {
        button.onClick.AddListener(() => { body.SetActive(!body.activeInHierarchy); fridgeInventoryBody.SetActive(!fridgeInventoryBody.activeInHierarchy); });
    }

    void Sell(Ingredient ingredient)
    {
        if (GameManager.Data.CurCoin < ingredient.Price)
            return;

        fridgeInventory.Store(ingredient, 1);
        GameManager.Data.LostCoin(ingredient.Price);
    }

    void Sell(Table table)
    {
        if (GameManager.Data.CurCoin < table.Price)
            return;

        GameManager.Data.LostCoin(table.Price);
        GameManager.Flow.UnlockSeat();
        purchasedTable++;

        //테이블은 최대 6개(1개는 기본으로 활성화), 그 이상 못 팔게 
        if (purchasedTable >= GameManager.Flow.maxSeatSize - 1)
            slots[tableSlotIndex].gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnESC();
    }

    public void OnESC()
    {
        if (body.activeInHierarchy)
        {
            body.SetActive(false);
            fridgeInventoryBody.SetActive(false);
        }
    }

}
