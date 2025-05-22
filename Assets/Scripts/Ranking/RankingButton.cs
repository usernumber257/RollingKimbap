using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingButton : MonoBehaviour
{
    [SerializeField] GameObject kor;
    [SerializeField] GameObject eng;

    private void Start()
    {
        kor.SetActive(GameManager.Setting.isKor);
        eng.SetActive(!GameManager.Setting.isKor);
    }

    public void Ranking()
    {
        GameManager.Data.UpdateRank();
    }
}
