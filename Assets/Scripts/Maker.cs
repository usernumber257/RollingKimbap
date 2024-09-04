using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public abstract class Maker : MonoBehaviour
{
    Coroutine minigameRoutine;

    [SerializeField] Sprite keyboard_up;
    [SerializeField] Sprite keyboard_down;
    [SerializeField] Sprite keyboard_left;
    [SerializeField] Sprite keyboard_right;

    [SerializeField] SpriteRenderer commandUI;

    public UnityAction OnKeyDown;

    private void Start()
    {
        commandUI.gameObject.SetActive(false);
    }

    public abstract void Make();

    //키보드 미니게임 ---------------------------------

    enum Keyboard { Up, Down, Left, Right }
    public void Minigame_Keyboard(int keyCount)
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
                    commandUI.sprite = keyboard_up;

                    index = Keyboard_Up() ? ++index : index;

                    if (Keyboard_Up())
                        OnKeyDown?.Invoke();

                    break;
                case 1:
                    commandUI.sprite = keyboard_down;

                    index = Keyboard_Down() ? ++index : index;

                    if (Keyboard_Down())
                        OnKeyDown?.Invoke();

                    break;
                case 2:
                    commandUI.sprite = keyboard_left;

                    index = Keyboard_Left() ? ++index : index;

                    if (Keyboard_Left())
                        OnKeyDown?.Invoke();

                    break;
                case 3:
                    commandUI.sprite = keyboard_right;

                    index = Keyboard_Right() ? ++index : index;
                    
                    if (Keyboard_Right())
                        OnKeyDown?.Invoke();

                    break;
            }

            yield return null;
        }

        commandUI.gameObject.SetActive(false);
        Debug.Log("clear!");
    }

    bool Keyboard_Up()
    {
        return Input.GetKeyDown(KeyCode.UpArrow);
    }

    bool Keyboard_Down()
    {
        return Input.GetKeyDown(KeyCode.DownArrow);
    }

    bool Keyboard_Left()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }

    bool Keyboard_Right()
    {
        return Input.GetKeyDown(KeyCode.RightArrow);
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
