using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FoodStacker))]
public abstract class Maker : MonoBehaviour
{
    Coroutine minigameRoutine;

    [SerializeField] Sprite keyboard_W;
    [SerializeField] Sprite keyboard_S;
    [SerializeField] Sprite keyboard_A;
    [SerializeField] Sprite keyboard_D;

    [SerializeField] SpriteRenderer commandUI;

    public UnityAction OnKeyDown;
    public UnityAction OnClear;

    FoodStacker foodStacker;
    public FoodStacker FoodStacker { get { return foodStacker; } }

    [SerializeField] Inventory fridgeInventory;

    public void Awake()
    {
        foodStacker = GetComponent<FoodStacker>();
    }

    private void Start()
    {
        commandUI.gameObject.SetActive(false);
    }

    public void StartMake(Food food)
    {
        foodStacker.curFood = food;
        foodStacker.makingFood = true;
    }

    public void StopMake()
    {
        commandUI.gameObject.SetActive(false);
        foodStacker.makingFood = false;

        if (minigameRoutine != null)
            StopCoroutine(minigameRoutine);

        //만들기를 취소하면 인벤토리에 다시 재료들이 원상복구 되게
        if (foodStacker.curFood == null)
            return;

        foreach (Ingredient element in foodStacker.curFood.Ingredients)
            fridgeInventory.Store(element, 1);
    }

    //키보드 미니게임 ---------------------------------

    enum Keyboard { Up, Down, Left, Right }
    public void Minigame_WASD(int keyCount)
    {
        commandUI.gameObject.SetActive(true);
        minigameRoutine = StartCoroutine(Minigame_KeyboardTIme(keyCount));
    }

    IEnumerator Minigame_KeyboardTIme(int keyCount)
    {
        List<int> choosedNums = RandomNumberChoicer.Dice(keyCount, 0, 4);

        int index = 0;

        while (true)
        {
            if (index >= keyCount)
                break;

            int curChoosedNum = choosedNums[index];

            switch (curChoosedNum)
            {
                case 0:
                    commandUI.sprite = keyboard_W;

                    index = Keyboard_W() ? ++index : index;

                    if (Keyboard_W())
                        OnKeyDown?.Invoke();

                    break;
                case 1:
                    commandUI.sprite = keyboard_S;

                    index = Keyboard_S() ? ++index : index;

                    if (Keyboard_S())
                        OnKeyDown?.Invoke();

                    break;
                case 2:
                    commandUI.sprite = keyboard_A;

                    index = Keyboard_A() ? ++index : index;

                    if (Keyboard_A())
                        OnKeyDown?.Invoke();

                    break;
                case 3:
                    commandUI.sprite = keyboard_D;

                    index = Keyboard_D() ? ++index : index;
                    
                    if (Keyboard_D())
                        OnKeyDown?.Invoke();

                    break;
            }

            yield return null;
        }

        commandUI.gameObject.SetActive(false);
        OnClear?.Invoke();
    }

    bool Keyboard_W()
    {
        return Input.GetKeyDown(KeyCode.W);
    }

    bool Keyboard_S()
    {
        return Input.GetKeyDown(KeyCode.S);
    }

    bool Keyboard_A()
    {
        return Input.GetKeyDown(KeyCode.A);
    }

    bool Keyboard_D()
    {
        return Input.GetKeyDown(KeyCode.D);
    }

    //휠 미니게임 ---------------------------------
    public void Minigame_Wheel()
    {

    }

    bool MouseWheel_Up()
    {
        return Input.GetAxis("Mouse ScrollWheel") > 0f;
    }

    bool MouseWheel_Down()
    {
        return Input.GetAxis("Mouse ScrollWheel") < 0f;
    }
}
