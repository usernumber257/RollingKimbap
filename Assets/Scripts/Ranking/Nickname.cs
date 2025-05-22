using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Nickname : MonoBehaviour
{
    [SerializeField] GameObject text_eng;
    [SerializeField] GameObject text_kor;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button next;
    [SerializeField] AudioSource negative;
    [SerializeField] AudioSource positive;
    [SerializeField] GameObject customPlayer;

    private void Awake()
    {
        next.onClick.AddListener(() => {
            if (string.IsNullOrWhiteSpace(inputField.text))
            {
                negative.Play();
                return;
            }

            positive.Play();
            GameManager.Data.nickname = inputField.text;
            gameObject.SetActive(false);
            customPlayer.SetActive(true);
        });
    }

    private void OnEnable()
    {
        text_eng.SetActive(false);
        text_kor.SetActive(false);

        if (GameManager.Setting.isKor)
            text_kor.SetActive(true);
        else
            text_eng.SetActive(true);
    }

    private void OnDestroy()
    {
        next.onClick.RemoveAllListeners();
    }
}
