using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingSlider : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    [SerializeField] Slider bgm;
    [SerializeField] Slider sfx;

    [SerializeField] Slider controllerSize;

    private void Start()
    {
        GameManager.Instance.OnSceneChanged += SetSlider;
    }

    void SetSlider()
    {
        bgm.value = PlayerPrefs.GetFloat("BGM");
        sfx.value = PlayerPrefs.GetFloat("SFX");
        controllerSize.value = PlayerPrefs.GetFloat("ControllerSize");
    }
    
}
