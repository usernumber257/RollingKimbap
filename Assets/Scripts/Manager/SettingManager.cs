using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] Slider bgm;
    [SerializeField] Slider sfx;
    [SerializeField] AudioMixer mixer;

    public bool isKor = false;
    public UnityAction OnLanguageChanged;
    SettingCanvas settingCanvas;

    private void Awake()
    {
        settingCanvas = GameObject.FindWithTag("Setting").GetComponent<SettingCanvas>();
        settingCanvas.transform.parent = transform;

        if (Application.systemLanguage == SystemLanguage.Korean)
            isKor = true;

        Close();
    }

    public void Open()
    {
        settingCanvas.ShowBody(false);
    }

    public void Close()
    {
        settingCanvas.ShowBody(false);
    }
}
