using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ObjectDetector 와 상호작용하는 클래스로, 플레이어가 오브젝트의 앞/뒤 어느쪽에 있느냐에 따라 오브젝트의 sortingOrder 가 조정 됨
/// </summary>
public class ObjectLayer : MonoBehaviour
{
    [SerializeField] Renderer sprite;
    [SerializeField] bool hasPriority;

    int playerSortingOrder = 5;

    public void SetLayer(bool setTop)
    {
        if (setTop)
            sprite.sortingOrder = hasPriority ? playerSortingOrder + 2 : playerSortingOrder + 1;
        else
            sprite.sortingOrder = hasPriority ? playerSortingOrder - 1 : playerSortingOrder - 2;
    }
}
