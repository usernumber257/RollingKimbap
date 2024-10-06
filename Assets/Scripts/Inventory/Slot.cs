using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image sprite;
    int count;
    public int Count { get { return count; } set { count = value; UpdateCountText(value); } }

    [SerializeField] TMP_Text countText;
    public TMP_Text nameText;

    void UpdateCountText(int value)
    {
        countText.text = value.ToString();
    }
}
