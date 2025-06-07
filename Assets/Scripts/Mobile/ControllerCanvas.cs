using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ͽ� ��Ʈ�ѷ��� ������¡�� ���� ĵ����
/// </summary>
public class ControllerCanvas : Singleton<ControllerCanvas>
{
    [SerializeField] GameObject move;
    [SerializeField] GameObject choice;

    public void ResizeController(float size)
    {
        move.transform.localScale = new Vector2(1 * size, 1 * size);
        choice.transform.localScale = new Vector2(1 * size, 1 * size);
    }
}
