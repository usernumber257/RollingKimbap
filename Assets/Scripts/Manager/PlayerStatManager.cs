//using BackEnd;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

/// <summary>
/// ĳ���� Ŀ��, ����, ����, �÷��� �ð� �����͸� ��� �ֽ��ϴ�.
/// </summary>
public class PlayerStatManager : Singleton<PlayerStatManager>
{
    public string nickname = "";

    //ĳ���� Ŀ���͸���¡
    public MyEnum.Hair curHair = MyEnum.Hair.None;
    public MyEnum.HairColor curHairColor = MyEnum.HairColor.gray;
    public MyEnum.Uniform curUniform = MyEnum.Uniform.None;
    public MyEnum.Hat curHat = MyEnum.Hat.None;

    public DataReferencer dataReferencer;

    /// <summary>
    /// ���ο� ������ ���� ��ϵ� ���� �����մϴ�.
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

    //���� ������
    public int foodCount = 4;

    //���� ������
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

    //�÷��� Ÿ�� ���
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
