using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingButton : MonoBehaviour
{
    [SerializeField] GameObject kor;
    [SerializeField] GameObject eng;

    private void Start()
    {
        kor.SetActive(SettingManager.Instance.isKor);
        eng.SetActive(!SettingManager.Instance.isKor);
    }

    public void Ranking()
    {
        PlayerStatManager.Instance.UpdateRank();
    }
}
