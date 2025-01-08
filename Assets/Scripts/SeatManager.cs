using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatManager : MonoBehaviour
{
    public GameObject[] table = new GameObject[6];
    public Seat[] seats = new Seat[12];
    int showIndex = 0;

    private void Start()
    {
        for (int i = 1; i < table.Length; i++)
            table[i].gameObject.SetActive(false);
    }

    public void ShowNewSeat()
    {
        //한 테이블에 두 좌석이 있음
        showIndex++;
        table[showIndex].gameObject.SetActive(true);
    }
}
