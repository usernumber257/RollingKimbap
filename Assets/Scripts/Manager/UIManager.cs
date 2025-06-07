using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI 들을 관리합니다.
/// </summary>
public class UIManager : Singleton<UIManager>
{
    List<UIBase> uis = new List<UIBase>();
    UIBase curOpenUI;

    protected override void Awake()
    {
        base.Awake();

        //모바일의 경우 cancel 버튼을 누를 시 현재 UI 를 닫습니다.
#if UNITY_IOS || UNITY_ANDROID
        MobileInputManager.Instance.cancel.onClick.AddListener(CloseCurrentUI);
#endif
    }

    private void Start()
    {
        CloseAllUI();
    }

    private void Update()
    {
        //윈도우 환경의 경우 ESC 를 누를 시 현재 UI를 닫습니다.
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseCurrentUI();
    }

    public void OpenUI(UIBase ui)
    {
        if (curOpenUI != null)
            curOpenUI.UIManager_Close();

        ui.UIManager_Open();

        curOpenUI = ui;
    }

    public void CloseUI(UIBase ui)
    {
        curOpenUI = null;

        ui.UIManager_Close();
    }

    public void RegisterUI(UIBase ui)
    {
        if (!uis.Contains(ui))
            uis.Add(ui);
    }

    void CloseCurrentUI()
    {
        if (curOpenUI != null)
            curOpenUI.UIManager_Close();
    }

    void CloseAllUI()
    {
        for (int i = 0; i < uis.Count; i++)
        {
            if (uis[i] != null)
                uis[i].UIManager_Close();
        }
    }

}
