using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// ������ ���õ� ������Ʈ���� �ٷ�ϴ�.
/// </summary>
public class SettingManager : Singleton<SettingManager>
{
    [SerializeField] Slider bgm;
    [SerializeField] Slider sfx;
    [SerializeField] AudioMixer mixer;

    public bool isKor = false;
    public UnityAction OnLanguageChanged;

    [SerializeField] SettingUI settingCanvas;
    public SettingUI SettingCanvas => settingCanvas;

    [SerializeField] ControllerCanvas controllerCanvas;
    public ControllerCanvas ControllerCanvas => controllerCanvas;

    protected override void Awake()
    {
        base.Awake();

        if (Application.systemLanguage == SystemLanguage.Korean)
            isKor = true;
    }
}
