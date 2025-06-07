using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Start()
    {
        PlayerStatManager.Instance.OnCoinChanged += SetText;
        SetText();
    }

    private void OnDisable()
    {
        PlayerStatManager.Instance.OnCoinChanged -= SetText;
    }

    void SetText()
    {
        text.text = PlayerStatManager.Instance.CurCoin.ToString();
    }
}
