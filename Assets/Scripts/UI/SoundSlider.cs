using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] Slider bgm;
    [SerializeField] Slider sfx;
    [SerializeField] AudioMixer mixer;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += SetSlider;

        PlayerPrefs.SetFloat("BGM", bgm.value);
        PlayerPrefs.SetFloat("SFX", sfx.value);
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SetSlider;
    }

    void SetSlider(Scene prev, Scene next)
    {
        bgm.value = PlayerPrefs.GetFloat("BGM");
        sfx.value = PlayerPrefs.GetFloat("SFX");
    }
    
}
