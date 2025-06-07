using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모바일용 컨트롤러의 리사이징을 위한 캔버스
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
