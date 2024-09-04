using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandomNumberChoicer
{
    public static List<int> Dice(int count, int minInclusive, int maxExclusive, List<int> excludeNums = null)
    {
        List<int> choosedNums = new List<int>();
        int curNum;

        while (choosedNums.Count < count)
        {
            if (excludeNums == null) //제외시켜야 할 숫자를 설정하지 않았을 경우
                curNum = Random.Range(minInclusive, maxExclusive);
            else //제외시켜야하는 숫자가 설정이 된다면, 해당 숫자가 나오지 않을 때까지 random range 를 돌림. 
                do { curNum = Random.Range(minInclusive, maxExclusive); } while (excludeNums.Exists(excludeNum => excludeNum == curNum));

            choosedNums.Add(curNum);
            Debug.Log(curNum);
        }

        return choosedNums;
    }
}
