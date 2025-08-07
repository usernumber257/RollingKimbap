using UnityEngine;
using System.Collections.Generic;
using System;
using LitJson;

#if UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_WIN
using BackEnd;

/// <summary>
/// 랭킹을 다룹니다
/// </summary>
public class Leaderboard : MonoBehaviour
{
    private static Leaderboard _instance = null;

    public static Leaderboard Instance
    {
        get { if (_instance == null) _instance = new Leaderboard(); return _instance; }
    }

    string leaderboardUuid = "0196f6a6-1a75-727c-9d14-a2c3edb48d83";

    RankingView rankingView;

    public bool SaveNewPlayRecord(string nickname, int coin, float playTime, int hair, int hairColor, int uniform, int hat)
    {
        string rowIndate = string.Empty;

        Param param = new Param();

        param.Add("nickName", nickname);
        param.Add("coin", coin);
        param.Add("playTime", playTime);
        param.Add("hair", hair);
        param.Add("hairColor", hairColor);
        param.Add("uniform", uniform);
        param.Add("hat", hat);
        param.Add("playDate", DateTime.UtcNow.ToString("o"));

        //이전에 저장이 되었는지
        Where where = new Where();
        where.Equal("custom_id", GameManager.Instance.curStageId);

        Backend.GameData.Get("PlayRecordLog", where, callback =>
        {
            if (callback.IsSuccess())
            {
                var rows = callback.FlattenRows();

                if (rows.Count > 0)
                {
                    //이미 데이터가 존재하면 갱신
                    Where updateWhere = new Where();
                    updateWhere.Equal("custom_id", GameManager.Instance.curStageId);

                    Backend.GameData.Update("PlayRecordLog", updateWhere, param, updateCallback =>
                    {
                        if (updateCallback.IsSuccess())
                            Debug.Log("플레이 기록 갱신 성공");
                        else
                            Debug.LogError("갱신 실패: " + updateCallback.ToString());
                    });
                }
                else
                {
                    //데이터가 없으면 새로 추가
                    param.Add("custom_id", GameManager.Instance.curStageId);
                    Backend.GameData.Insert("PlayRecordLog", param, insertCallback =>
                    {
                        if (insertCallback.IsSuccess())
                            Debug.Log("플레이 기록 저장 성공");
                        else
                            Debug.LogError("저장 실패: " + insertCallback.ToString());
                    });
                }
            }
            else
            {
                Debug.LogError("데이터 조회 실패: " + callback.ToString());
            }
        });

        return true;
    }


    /// <summary>
    /// 랭킹을 조회해 UI 를 세팅합니다.
    /// </summary>
    public void GetLeaderboard()
    {
        if (rankingView == null)
            rankingView = GameObject.FindWithTag("RankingView")?.GetComponent<RankingView>();

        if (rankingView == null)
        {
            Debug.LogError("RankingView 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        Where where = new Where();

        Backend.GameData.Get("PlayRecordLog", where, 1000, callback =>
        {
            if (!callback.IsSuccess())
            {
                Debug.LogError("전체 플레이 기록 조회 실패: " + callback.ToString());
                return;
            }

            JsonData jsonData = callback.GetReturnValuetoJSON();
            JsonData rows = jsonData["rows"];

            if (rows.Count == 0)
            {
                Debug.Log("플레이 기록이 없습니다.");
                return;
            }

            List<PlayRecord> playRecords = new List<PlayRecord>();

            for (int i = 0; i < rows.Count; i++)
            {
                JsonData record = rows[i];

                try
                {
                    PlayRecord pr = new PlayRecord()
                    {
                        nickName = record["nickName"]["S"].ToString(),
                        coin = int.Parse(record["coin"]["N"].ToString()),
                        playTime = float.Parse(record["playTime"]["N"].ToString()),
                        hair = int.Parse(record["hair"]["N"].ToString()),
                        hairColor = int.Parse(record["hairColor"]["N"].ToString()),
                        uniform = int.Parse(record["uniform"]["N"].ToString()),
                        hat = int.Parse(record["hat"]["N"].ToString()),
                        playDate = record["playDate"]["S"].ToString()
                    };

                    playRecords.Add(pr);
                }
                catch (Exception e)
                {
                    Debug.LogError($"플레이 기록 파싱 중 오류 발생: {e.Message}");
                }
            }

            // coin 을 기준으로 내림차순 정렬을 하지만, 플레이 기록이 더 짧은 사람이 보다 윗순위를 갖게 됨
            playRecords.Sort((a, b) =>
            {
                int coinCompare = b.coin.CompareTo(a.coin);
                if (coinCompare == 0)
                {
                    return a.playTime.CompareTo(b.playTime);
                }
                return coinCompare;
            });

            // 랭킹뷰에 추가
            for (int i = 0; i < playRecords.Count; i++)
            {
                PlayRecord p = playRecords[i];
                rankingView.AddContent(i + 1, p.coin, p.nickName, p.playTime, p.hair, p.hairColor, p.uniform, p.hat);
            }
        });
    }

}
#endif
