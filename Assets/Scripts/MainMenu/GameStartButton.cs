using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    public void GameStart()
    {
#if UNITY_STANDALONE_WIN
        SceneManager.LoadScene("GameScene_Window");
#endif
#if UNITY_IOS || UNITY_ANDROID
        SceneManager.LoadScene("GameScene_Mobile");
#endif
#if UNITY_WEBGL
        SceneManager.LoadScene("GameScene_Web");
#endif
    }
}
