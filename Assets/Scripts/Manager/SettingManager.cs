using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class SettingManager : MonoBehaviour
{
    public bool isKor = false;
    public UnityAction OnLanguageChanged;
    SettingCanvas settingCanvas;

    private void Awake()
    {
        settingCanvas = GameObject.FindWithTag("Setting").GetComponent<SettingCanvas>();
        settingCanvas.transform.parent = transform;

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
