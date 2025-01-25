using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    //캐릭터 커스터마이징
    public MyEnum.Hair curHair = MyEnum.Hair.None;
    public MyEnum.Uniform curUniform = MyEnum.Uniform.None;

    public void SetPlayerClothes(MyEnum.Hair hair, MyEnum.Uniform uniform)
    {
        curHair = hair;
        curUniform = uniform;
    }

    public void Init(int initCoin)
    {
        curCoin = initCoin;
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

    public UnityAction OnCoinChanged;

    public void EarnCoin(int howMuch)
    {
        CurCoin += howMuch;
    }

    public void LostCoin(int howMuch)
    {
        CurCoin -= howMuch;
    }

    //DataReferencer 생성
    public DataReferencer dataReferencer;

    private void OnEnable()
    {
        dataReferencer = Instantiate(Resources.Load<DataReferencer>("DataReferencer"));
    }


}
