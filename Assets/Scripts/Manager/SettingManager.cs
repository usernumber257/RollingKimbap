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

    private void Awake()
    {
        Close();
    }

    public void Open()
    {
        GameObject.FindWithTag("Setting").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }

    public void Close()
    {
        GameObject.FindWithTag("Setting").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    }

}
