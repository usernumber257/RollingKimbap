using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 씬 내의 테이블에 대한 참조를 합니다.
/// </summary>
public class TableReferencer : MonoBehaviour
{
    public GameObject[] table = new GameObject[6];
    public Seat[] seats = new Seat[12];
    int showIndex = 0;

    private void Start()
    {
        for (int i = 1; i < table.Length; i++)
            table[i].gameObject.SetActive(false);

        GameManager.Seat.RegisterTableReferencer(this);
    }

    public void ShowNewSeat()
    {
        //한 테이블에 두 좌석이 있음
        showIndex++;
        table[showIndex].gameObject.SetActive(true);
    }
}
