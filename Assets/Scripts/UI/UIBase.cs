using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIManager 에서 관리하기 위해 모든 UI 를 묶는 존재입니다.
/// </summary>
public class UIBase : MonoBehaviour
{
    [SerializeField] GameObject body;

    protected virtual void Awake()
    {
        UIManager.Instance.RegisterUI(this);
    }

    /// <summary>
    /// UIManager 를 통해 써줍니다.
    /// </summary>
    public virtual void UIManager_Open()
    {
        if (body != null)
            body.SetActive(true);
    }

    /// <summary>
    /// UIManager 를 통해 써줍니다.
    /// </summary>
    public virtual void UIManager_Close()
    {
        if (body != null)
            body.SetActive(false);
    }
}
