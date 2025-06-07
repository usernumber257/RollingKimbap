using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 손님의 인내 시간, 만족도, 가게의 인기도 등 게임의 난이도와 관련 된 데이터를 다룹니다.
/// </summary>
public class LevelManager : MonoBehaviour
{
    public void Init(float minTime, float maxTime, int halfAnger, int fullAnger, int happy, float halfAngerTime, float fullAngerTime)
    {
        visitTime_min = minTime;
        visitTime_max = maxTime;

        popularity_halfAnger = halfAnger;
        popularity_fullAnger = fullAnger;
        popularity_happy = happy;

        this.halfAngerTime = halfAngerTime;
        this.fullAngerTime = fullAngerTime;
    }

    //popularity
    int popularity = 50;
    public int Popularity { get { return popularity; } set { popularity = value; if (popularity < 0) popularity = 0; if (popularity >= 100) popularity = 100; } }

    int popularity_halfAnger = -2;
    int popularity_fullAnger = -3;
    int popularity_happy = 2;

    public UnityAction OnPopularityChanged;

    /// <summary>
    /// 손님들이 마지막에 갖게 된 감정으로 가게의 인기도를 증가/감소 시킵니다.
    /// </summary>
    /// <param name="customersEmotion"></param>
    public void SetPopularity(Customer.Emotions customersEmotion)
    {
        switch (customersEmotion)
        {
            case Customer.Emotions.None:
                break;
            case Customer.Emotions.HalfAnger:
                Popularity += popularity_halfAnger;
                break;
            case Customer.Emotions.FullAnger:
                Popularity += popularity_fullAnger;
                break;
            case Customer.Emotions.Happy:
                Popularity += popularity_happy;
                break;
        }

        OnPopularityChanged?.Invoke(); //UI 설정을 위함
        SetVisitTime(); //인기도 증감에 따라 다음 손님의 방문 시간도 조절
    }

    public void Debug_SetPopularity(int value)
    {
        Popularity += value;

        OnPopularityChanged?.Invoke();
        SetVisitTime();
    }

    //손님이 방문하는 시간 값
    [SerializeField] float visitTime_min;
    public float VisitTime_min { get { return visitTime_min; } }

    [SerializeField] float visitTime_max;
    public float VisitTime_max { get { return visitTime_max; } }

    /// <summary>
    /// 인기도에 따라 다음 손님의 방문 시간 값을 결정
    /// </summary>
    void SetVisitTime()
    {
        visitTime_min = Mathf.Lerp(10f, 0.2f, Popularity / 100f);
        visitTime_max = Mathf.Lerp(20f, 0.4f, Popularity / 100f);
    }

    //손님의 인내심 값
    float halfAngerTime = 15f;
    public float HalfAngerTime { get { return halfAngerTime; } }
    float fullAngerTime = 20f;
    public float FullAngerTime { get { return fullAngerTime; } }

}
