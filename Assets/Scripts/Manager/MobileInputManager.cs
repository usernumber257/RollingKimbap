using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileInputManager : MonoBehaviour
{
    private static MobileInputManager instance;

    public static MobileInputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<MobileInputManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(MobileInputManager).Name);
                    instance = go.AddComponent<MobileInputManager>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject); // 이미 인스턴스가 존재하면 삭제
            return;
        }
        instance = this as MobileInputManager;
    }

    public ButtonPressDetector up;
    public ButtonPressDetector down;
    public ButtonPressDetector left;
    public ButtonPressDetector right;

    public ButtonPressDetector up_left;
    public ButtonPressDetector up_right;
    public ButtonPressDetector down_left;
    public ButtonPressDetector down_right;

    public Button confirm;
    public Button cancel;
}
