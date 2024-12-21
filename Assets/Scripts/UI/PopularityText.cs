using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopularityText : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Start()
    {
        GameManager.Level.OnPopularityChanged += SetText;
        SetText();
    }

    private void OnDisable()
    {
        GameManager.Level.OnPopularityChanged -= SetText;
    }

    void SetText()
    {
        text.text = $"{GameManager.Level.Popularity}";
    }
}
