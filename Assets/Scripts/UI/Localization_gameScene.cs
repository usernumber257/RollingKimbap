using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localization_gameScene : MonoBehaviour
{
    public GameObject store_kor;
    public GameObject store_eng;
    public GameObject fridge_kor;
    public GameObject fridge_eng;
    public GameObject storeViewer_kor;
    public GameObject storeViewer_eng;
    public GameObject storeViewer_fridge_kor;
    public GameObject storeViewer_fridge_eng;

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

        store_kor.SetActive(isKor);
        store_eng.SetActive(!isKor);

        fridge_kor.SetActive(isKor);
        fridge_eng.SetActive(!isKor);

        storeViewer_kor.SetActive(isKor);
        storeViewer_eng.SetActive(!isKor);

        storeViewer_fridge_kor.SetActive(isKor);
        storeViewer_fridge_eng.SetActive(!isKor);
    }
}
