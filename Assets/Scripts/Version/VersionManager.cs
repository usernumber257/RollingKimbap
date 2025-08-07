using BackEnd;
using System;
using UnityEngine;
using UnityEngine.UI;

public class VersionManager : MonoBehaviour
{
    [SerializeField] GameObject updateUIbody;
    [SerializeField] Button bttn_goToStore;
    [SerializeField] Button bttn_close;

    private void Start()
    {
        updateUIbody.SetActive(false);

        //에디터, 윈도우, 웹은 버전 조회 안 해두 됨
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_WEBGL
        return;
#endif 

        CheckVersion();

        bttn_goToStore.onClick.AddListener(OpenStoreLink);
        bttn_close.onClick.AddListener(() => { updateUIbody.SetActive(false); });
    }

    void CheckVersion()
    {
        Version client = new Version(Application.version);

        Backend.Utils.GetLatestVersion(callback => { 
            if (callback.IsSuccess() == false)
            {
                //정보 조회 실패
                return;
            }

            var version = callback.GetReturnValuetoJSON()["version"].ToString();
            Version server = new Version(version);

            var result = server.CompareTo(client);

            if (result == 0) //로컬과 서버 버전 같음
                return;
            else if (result < 0) //서버 버전이 작음(검수 중)
                return;

            OpenUpdateUI();
        });
    }

    void OpenUpdateUI()
    {
        updateUIbody.SetActive(true);
    }

    string playStoreLink = "market://details?id=com.User257.RollingKimbap";

    void OpenStoreLink()
    {
#if UNITY_ANDROID
        Application.OpenURL(playStoreLink);
#endif
    }
}
