using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Start()
    {
        GameManager.Data.OnCoinChanged += SetText;
        SetText();
    }

    private void OnDisable()
    {
        GameManager.Data.OnCoinChanged -= SetText;
    }

    void SetText()
    {
        text.text = GameManager.Data.CurCoin.ToString();
    }
}
