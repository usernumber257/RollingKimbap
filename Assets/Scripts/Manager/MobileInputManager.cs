using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileInputManager : Singleton<MobileInputManager>
{
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
