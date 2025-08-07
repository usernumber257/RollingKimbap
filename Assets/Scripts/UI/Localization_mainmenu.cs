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

    public GameObject nickname_kor;
    public GameObject nickname_eng;

    public GameObject nicknameAlert_kor;
    public GameObject nicknameAlert_eng;

    public GameObject updateAlert_kor;
    public GameObject updateAlert_eng;

    public GameObject myRecord_kor;
    public GameObject myRecord_eng;

    public GameObject deleteAccount_kor;
    public GameObject deleteAccount_eng;

    public GameObject deleteAccountAlert_kor;
    public GameObject deleteAccountAlert_eng;

    public GameObject deleteAccount_yes_kor;
    public GameObject deleteAccount_yes_eng;

    public GameObject deleteAccountAlert_no_kor;
    public GameObject deleteAccountAlert_no_eng;

    private void Start()
    {
        Localization();

        SettingManager.Instance.OnLanguageChanged += Localization;
    }

    private void OnDisable()
    {
        SettingManager.Instance.OnLanguageChanged -= Localization;
    }

    public void Localization()
    {
        bool isKor = SettingManager.Instance.isKor;

        titleAnim.SetBool("isKor", isKor);

        gameStartText.text = isKor ? "±è¹ä ÆÈ·¯ °¡±â" : "Game Start";
        howToText.text = isKor ? "°ÔÀÓ ¹æ¹ý º¸±â" : "How to";

        howTo_kor.SetActive(isKor);
        howTo_eng.SetActive(!isKor);

        custom_kor.SetActive(isKor);
        custom_eng.SetActive(!isKor);

        decide_kor.SetActive(isKor);
        decide_eng.SetActive(!isKor);

#if UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_WIN
        ranking_kor.SetActive(isKor);
        ranking_eng.SetActive(!isKor);

        quitGame_kor.SetActive(isKor);
        quitGame_eng.SetActive(!isKor);

        nickname_kor.SetActive(isKor);
        nickname_eng.SetActive(!isKor);

        nicknameAlert_kor.SetActive(isKor);
        nicknameAlert_eng.SetActive(!isKor);

#endif
#if UNITY_IOS || UNITY_ANDROID
        updateAlert_kor.SetActive(isKor);
        updateAlert_eng.SetActive(!isKor);

        myRecord_kor.SetActive(isKor);
        myRecord_eng.SetActive(!isKor);

        deleteAccount_kor.SetActive(isKor);
        deleteAccount_eng.SetActive(!isKor);

        deleteAccountAlert_kor.SetActive(isKor);
        deleteAccountAlert_eng.SetActive(!isKor);

        deleteAccount_yes_kor.SetActive(isKor);
        deleteAccount_yes_eng.SetActive(!isKor);

        deleteAccountAlert_no_kor.SetActive(isKor);
        deleteAccountAlert_no_eng.SetActive(!isKor);
#endif

    }


}
