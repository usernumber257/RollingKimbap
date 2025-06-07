using UnityEngine;
using UnityEngine.UI;

public class StoreUI : UIBase
{
    [SerializeField] Slot slotPrefab;
    [SerializeField] Transform slotParent;
    [SerializeField] GameObject fridgeInventoryBody;
    [SerializeField] Button button;
    Slot[] slots;
    [SerializeField] Inventory fridgeInventory;

    int purchasedTable = 0;
    int tableSlotIndex;

    private void Start()
    {
        MakePool();
        MappingButton();
    }

    void MakePool()
    {
        int slotCount = PlayerStatManager.Instance.dataReferencer.sellItems.Count;
        slots = new Slot[slotCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(slotPrefab);
            slots[i].transform.parent = slotParent;
            slots[i].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

            Item curItem = PlayerStatManager.Instance.dataReferencer.sellItems[i];
            
            //아이템 UI Init
            slots[i].sprite.sprite = curItem.Model;
            slots[i].nameText.text = SettingManager.Instance.isKor ? curItem.ItemName : curItem.ItemName_eng;
            slots[i].countText.text = SettingManager.Instance.isKor ? $"{curItem.Price.ToString()}원" : $"{curItem.Price.ToString()}Won";

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
        button.onClick.AddListener(() => { UIManager.Instance.OpenUI(this); fridgeInventoryBody.SetActive(true); });
    }

    void Sell(Ingredient ingredient)
    {
        if (PlayerStatManager.Instance.CurCoin < ingredient.Price)
            return;

        SoundPlayer.Instance.Play(MyEnum.Sound.Add);
        
        fridgeInventory.Store(ingredient, 1);
        PlayerStatManager.Instance.LostCoin(ingredient.Price);
    }

    void Sell(Table table)
    {
        if (PlayerStatManager.Instance.CurCoin < table.Price)
            return;

        SoundPlayer.Instance.Play(MyEnum.Sound.Add);

        PlayerStatManager.Instance.LostCoin(table.Price);
        GameManager.Seat.UnlockSeat();
        purchasedTable++;

        //테이블은 최대 6개(1개는 기본으로 활성화), 그 이상 못 팔게 
        if (purchasedTable >= GameManager.Seat.maxSeatSize - 1)
            slots[tableSlotIndex].gameObject.SetActive(false);
    }

    public override void UIManager_Open()
    {
        base.UIManager_Open();
        fridgeInventoryBody.SetActive(true);
    }

    public override void UIManager_Close()
    {
        base.UIManager_Close();
        fridgeInventoryBody.SetActive(false);
    }
}
