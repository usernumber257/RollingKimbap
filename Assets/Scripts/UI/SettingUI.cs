using UnityEngine;
using UnityEngine.Audio;

public class SettingUI : UIBase
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] BackToMain backToMain;


    private void Start()
    {
        SetBGM(0.2f);
        SetSFX(0.2f);
    }


    public override void UIManager_Open()
    {
        base.UIManager_Open();

        if (GameManager.Instance.curScene.name == "MainMenuScene_Mobile")
            SettingManager.Instance.ControllerCanvas.gameObject.SetActive(true);
    }

    public override void UIManager_Close()
    {
        base.UIManager_Close();

        if (GameManager.Instance.curScene.name == "MainMenuScene_Mobile")
            SettingManager.Instance.ControllerCanvas.gameObject.SetActive(false);
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

    public void SetControllerSize(float value)
    {
        PlayerPrefs.SetFloat("ControllerSize", value);
        SettingManager.Instance .ControllerCanvas.ResizeController(value);
    }

    public void SetLanguage(bool isKor)
    {
        if (SettingManager.Instance.isKor == isKor)
            return;

        SettingManager.Instance.isKor = isKor;
        SettingManager.Instance.OnLanguageChanged.Invoke();

        if (backToMain != null)
             backToMain.Back();
        UIManager.Instance.CloseUI(this);
    }
}
