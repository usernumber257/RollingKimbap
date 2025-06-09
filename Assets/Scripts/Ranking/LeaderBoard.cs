using UnityEngine;
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

    private void OnEnable()
    {
        rankingView = GameObject.FindWithTag("RankingView").GetComponent<RankingView>();
        GetLeaderboard();
    }

    public void UpdateLeaderboard(int score, string extraData)
    {
        string tableName = "PlayerRanking";
        string rowIndate = string.Empty;

        Param param = new Param();
        param.Add("Coin", score);
        param.Add("ExtraData", extraData);

        var bro = Backend.GameData.Get("PlayerRanking", new Where());

        if (bro.IsSuccess() == false)
        {
            Debug.LogError(bro);
            return;
        }

        if (bro.FlattenRows().Count > 0)
        {
            rowIndate = bro.FlattenRows()[0]["inDate"].ToString();
        }
        else
        {
            var bro2 = Backend.GameData.Insert(tableName, param);

            if (bro2.IsSuccess())
            {
                rowIndate = bro2.GetInDate();
            }
            else
            {
                Debug.LogError(bro2);
                return;
            }
        }

        if (rowIndate == string.Empty)
        {
            return;
        }

        Backend.Leaderboard.User.UpdateMyDataAndRefreshLeaderboard("리더보드 uuid", "테이블 이름", "갱신할 row inDate", param, callback =>
        {
            var rankBro = Backend.Leaderboard.User.UpdateMyDataAndRefreshLeaderboard(leaderboardUuid, tableName, rowIndate, param);
            if (rankBro.IsSuccess())
            {
                Debug.Log("리더보드 등록 성공");
            }
            else
            {
                Debug.Log("리더보드 등록 실패 : " + rankBro);
            }
        });
    }

    /// <summary>
    /// 랭킹을 조회해 UI 를 세팅합니다.
    /// </summary>
    public void GetLeaderboard()
    {
        BackEnd.Leaderboard.BackendUserLeaderboardReturnObject bro = null;

        bro = Backend.Leaderboard.User.GetLeaderboard(leaderboardUuid, 100);

        // 랭킹 조회 후.
        if (bro.IsSuccess() == false)
        {
            Debug.Log("랭킹 조회 실패");
            return;
        }

        Debug.Log("리더보드 총 유저 등록 수 : " + bro.GetTotalCount());

        //UI 에 디스플레이 하기
        if (rankingView == null)
            rankingView = GameObject.FindWithTag("RankingView").GetComponent<RankingView>();

        foreach (BackEnd.Leaderboard.UserLeaderboardItem item in bro.GetUserLeaderboardList())
        {
            if (item.nickname == Login.Instance.tempUser) //메인메뉴, 디버깅을 위한 임시 유저 아이디는 보여주지 않습니다.
                continue;

            string[] extraData = item.extraData.Split("|");

            UserData userData = new UserData();

            userData.spentTime = float.Parse(extraData[0].ToString());
            userData.hair = int.Parse(extraData[1].ToString());
            userData.hairColor = int.Parse(extraData[2].ToString());
            userData.uniform = int.Parse(extraData[3].ToString());
            userData.hat = int.Parse(extraData[4].ToString());

            rankingView.AddContent(int.Parse(item.rank), int.Parse(item.score), item.nickname, userData.spentTime, userData.hair, userData.hairColor, userData.uniform, userData.hat);
        }
    }
}
