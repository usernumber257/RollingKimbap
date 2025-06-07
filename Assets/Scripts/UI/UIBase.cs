using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIManager ���� �����ϱ� ���� ��� UI �� ���� �����Դϴ�.
/// </summary>
public class UIBase : MonoBehaviour
{
    [SerializeField] GameObject body;

    protected virtual void Awake()
    {
        UIManager.Instance.RegisterUI(this);
    }

    /// <summary>
    /// UIManager �� ���� ���ݴϴ�.
    /// </summary>
    public virtual void UIManager_Open()
    {
        if (body != null)
            body.SetActive(true);
    }

    /// <summary>
    /// UIManager �� ���� ���ݴϴ�.
    /// </summary>
    public virtual void UIManager_Close()
    {
        if (body != null)
            body.SetActive(false);
    }
}
