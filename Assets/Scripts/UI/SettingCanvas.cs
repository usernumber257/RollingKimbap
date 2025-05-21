using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingCanvas : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] GameObject body;
    [SerializeField] BackToMain backToMain;

    public void ShowBody(bool isOpen)
    {
        body.SetActive(isOpen);
    }

    public void SetBGM(float value)
    {
        float temp = Mathf.Log10(value) * 20;

        mixer.SetFloat("BGM", temp);
        PlayerPrefs.SetFloat("BGM", value);
    }

    public void SetSFX(float value)
    {
        float temp = Mathf.Log10(value) * 20;

        mixer.SetFloat("SFX", temp);
        PlayerPrefs.SetFloat("SFX", value);
    }

    public void SetLanguage(bool isKor)
    {
        GameManager.Setting.isKor = isKor;
        GameManager.Setting.OnLanguageChanged.Invoke();

        backToMain.Back();
        ShowBody(false);
    }
}
