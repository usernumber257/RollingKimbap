using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMain : MonoBehaviour
{
    public void Back()
    {
#if UNITY_STANDALONE_WIN
        SceneManager.LoadScene("MainMenuScene_Window");
#endif

#if UNITY_IOS || UNITY_ANDROID
        SceneManager.LoadScene("MainMenuScene_Mobile");
#endif
#if UNITY_WEBGL
        SceneManager.LoadScene("MainMenuScene_Web");
#endif
    }
}
