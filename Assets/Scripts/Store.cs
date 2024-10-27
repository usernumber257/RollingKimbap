using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] Slot slotPrefab;
    [SerializeField] Transform slotParent;
    [SerializeField] GameObject body;
    [SerializeField] Button button;
    Slot[] slots;

    [SerializeField] Inventory fridgeInventory;

    private void Start()
    {
        MakePool();
        body.SetActive(false);

        MappingButton();
    }

    void MakePool()
    {
        int slotCount = GameManager.Data.dataReferencer.ingredients.Count;
        slots = new Slot[slotCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(slotPrefab);
            slots[i].transform.parent = slotParent;

            Ingredient curIngredient = GameManager.Data.dataReferencer.ingredients[i];
            slots[i].sprite.sprite = curIngredient.Model;
            slots[i].nameText.text = curIngredient.IngredientName;
            slots[i].countText.gameObject.SetActive(false);

            slots[i].GetComponent<Button>().onClick.AddListener(() => Sell(curIngredient));
        }
    }

    void MappingButton()
    {
        button.onClick.AddListener(() => { body.SetActive(!body.activeInHierarchy); });
    }

    void Sell(Ingredient ingredient)
    {
        fridgeInventory.Store(ingredient, 1);
        GameManager.Data.LostCoin(ingredient.Price);
    }
}
