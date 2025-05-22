using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static MyEnum;

public class RankingView : MonoBehaviour
{
    [SerializeField] GameObject rankingContentPrefab;
    [SerializeField] Transform content;

    public void AddContent(int rank, int coin, string nickname, float playTime, int hair, int hairColor, int uniform, int hat)
    {
        GameObject newGo = Instantiate(rankingContentPrefab);
        newGo.transform.parent = content;
        newGo.transform.localScale = Vector3.one;

        newGo.GetComponent<RankingContent>().Init(rank, coin, nickname, playTime, hair, hairColor, uniform, hat);
    }
}
