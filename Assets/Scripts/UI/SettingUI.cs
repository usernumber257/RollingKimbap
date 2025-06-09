using UnityEngine;
using UnityEngine.Audio;

public class SettingUI : UIBase
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] BackToMain backToMain;


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

        Debug.Log(PlayerPrefs.GetFloat("BGM"));
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
        SettingManager.Instance.isKor = isKor;
        SettingManager.Instance.OnLanguageChanged.Invoke();

        if (backToMain != null)
             backToMain.Back();
        UIManager.Instance.CloseUI(this);
    }
}
