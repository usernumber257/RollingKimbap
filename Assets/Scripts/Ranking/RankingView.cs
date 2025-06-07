using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ·©Å·À» º¸±â À§ÇÑ ºä¾î
/// </summary>
public class RankingView : MonoBehaviour
{
    [SerializeField] GameObject rankingContentPrefab;
    [SerializeField] Transform content;

    List<GameObject> contents = new List<GameObject>();

    public void AddContent(int rank, int coin, string nickname, float playTime, int hair, int hairColor, int uniform, int hat)
    {
        GameObject newGo = Instantiate(rankingContentPrefab);
        newGo.transform.parent = content;
        newGo.transform.localScale = Vector3.one;

        newGo.GetComponent<RankingContent>().Init(rank, coin, nickname, playTime, hair, hairColor, uniform, hat);
        contents.Add(newGo);
    }

    public void Clear()
    {
        for (int i = 0; i < contents.Count; i++)
        {
            Destroy(contents[i].gameObject);
            contents.RemoveAt(i);
        }
    }
}
