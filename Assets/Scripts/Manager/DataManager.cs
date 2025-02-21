using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    //캐릭터 커스터마이징
    public MyEnum.Hair curHair = MyEnum.Hair.None;
    public MyEnum.HairColor curHairColor = MyEnum.HairColor.gray;
    public MyEnum.Uniform curUniform = MyEnum.Uniform.None;
    public MyEnum.Hat curHat = MyEnum.Hat.None;

    public void SetPlayerClothes(MyEnum.Hair value)
    {
        curHair = value;
    }
    public void SetPlayerClothes(MyEnum.HairColor value)
    {
        curHairColor = value;
    }
    public void SetPlayerClothes(MyEnum.Uniform value)
    {
        curUniform = value;
    }
    public void SetPlayerClothes(MyEnum.Hat value)
    {
        curHat = value;
    }


    public void Init(int initCoin)
    {
        CurCoin = initCoin;
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
