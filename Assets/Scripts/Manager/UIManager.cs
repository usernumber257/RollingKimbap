using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI ���� �����մϴ�.
/// </summary>
public class UIManager : Singleton<UIManager>
{
    List<UIBase> uis = new List<UIBase>();
    UIBase curOpenUI;

    protected override void Awake()
    {
        base.Awake();

        //������� ��� cancel ��ư�� ���� �� ���� UI �� �ݽ��ϴ�.
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
        //������ ȯ���� ��� ESC �� ���� �� ���� UI�� �ݽ��ϴ�.
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
