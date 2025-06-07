using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �� ���� ���̺� ���� ������ �մϴ�.
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
        //�� ���̺� �� �¼��� ����
        showIndex++;
        table[showIndex].gameObject.SetActive(true);
    }
}
