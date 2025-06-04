using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Localization_mainmenu: MonoBehaviour
{
    public Animator titleAnim;
    public TMP_Text gameStartText;
    public TMP_Text howToText;
    
    public GameObject howTo_kor;
    public GameObject howTo_eng;
    
    public GameObject custom_kor;
    public GameObject custom_eng;
    
    public GameObject decide_kor;
    public GameObject decide_eng;

    public GameObject ranking_kor;
    public GameObject ranking_eng;

    public GameObject quitGame_kor;
    public GameObject quitGame_eng;

    private void Start()
    {
        Localization();

        GameManager.Setting.OnLanguageChanged += Localization;
    }

    private void OnDisable()
    {
        GameManager.Setting.OnLanguageChanged -= Localization;
    }

    public void Localization()
    {
        bool isKor = GameManager.Setting.isKor;

        titleAnim.SetBool("isKor", isKor);

        gameStartText.text = isKor ? "±è¹ä ÆÈ·¯ °¡±â" : "Game Start";
        howToText.text = isKor ? "°ÔÀÓ ¹æ¹ý º¸±â" : "How to";

        howTo_kor.SetActive(isKor);
        howTo_eng.SetActive(!isKor);

        custom_kor.SetActive(isKor);
        custom_eng.SetActive(!isKor);

        decide_kor.SetActive(isKor);
        decide_eng.SetActive(!isKor);

        ranking_kor.SetActive(isKor);
        ranking_eng.SetActive(!isKor);
#if UNITY_IOS || UNITY_ANDROID
        quitGame_kor.SetActive(isKor);
        quitGame_eng.SetActive(!isKor);
#endif
    }

    
}
