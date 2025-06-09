//using BackEnd;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

/// <summary>
/// 캐릭터 커마, 음식, 수익, 플레이 시간 데이터를 담고 있습니다.
/// </summary>
public class PlayerStatManager : Singleton<PlayerStatManager>
{
    public string nickname = "";

    //캐릭터 커스터마이징
    public MyEnum.Hair curHair = MyEnum.Hair.None;
    public MyEnum.HairColor curHairColor = MyEnum.HairColor.gray;
    public MyEnum.Uniform curUniform = MyEnum.Uniform.None;
    public MyEnum.Hat curHat = MyEnum.Hat.None;

    public DataReferencer dataReferencer;

    /// <summary>
    /// 새로운 게임을 위해 등록된 옷을 리셋합니다.
    /// </summary>
    public void ResetClothes()
    {
        curHair = MyEnum.Hair.None;
        curHairColor = MyEnum.HairColor.gray;
        curUniform = MyEnum.Uniform.None;
        curHat = MyEnum.Hat.None;
    }

    public void Init(int initCoin)
    {
        CurCoin = initCoin;

        if (dataReferencer == null)
        {
            dataReferencer = Instantiate(Resources.Load<DataReferencer>("DataReferencer"));
            dataReferencer.transform.parent = transform;
        }
    }

    //음식 데이터
    public int foodCount = 4;

    //수익 데이터
    int curCoin = 50;
    public int CurCoin { 
        get { return curCoin; } 
        set 
        { 
            curCoin = value;
            OnCoinChanged?.Invoke();

            if (curCoin < 0) 
                curCoin = 0;
        } 
    }

    public void UpdateRank()
    {
        #if UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_WIN
        Leaderboard.Instance.UpdateLeaderboard(curCoin, $"{spentTime}|{(int)curHair}|{(int)curHairColor}|{(int)curUniform}|{(int)curHat}");
#endif
    }

    public UnityAction OnCoinChanged;

    public void EarnCoin(int howMuch)
    {
        CurCoin += howMuch;
    }

    public void LostCoin(int howMuch)
    {
        CurCoin -= howMuch;
    }

    //플레이 타임 기록
    bool timer = false;
    float spentTime = 0f;
    float oneSec = 0f;

    private void Update()
    {
        if (!timer)
            return;

        oneSec += Time.deltaTime;

        if (oneSec >= 1f)
        {
            spentTime += oneSec;
            oneSec = 0f;
        }
    }

    public void Timer(bool on)
    {
        timer = on;
        spentTime = 0f;
    }

}
