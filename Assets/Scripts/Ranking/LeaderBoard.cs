using UnityEngine;
using BackEnd;

/// <summary>
/// ��ŷ�� �ٷ�ϴ�
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

        Backend.Leaderboard.User.UpdateMyDataAndRefreshLeaderboard("�������� uuid", "���̺� �̸�", "������ row inDate", param, callback =>
        {
            var rankBro = Backend.Leaderboard.User.UpdateMyDataAndRefreshLeaderboard(leaderboardUuid, tableName, rowIndate, param);
            if (rankBro.IsSuccess())
            {
                Debug.Log("�������� ��� ����");
            }
            else
            {
                Debug.Log("�������� ��� ���� : " + rankBro);
            }
        });
    }

    /// <summary>
    /// ��ŷ�� ��ȸ�� UI �� �����մϴ�.
    /// </summary>
    public void GetLeaderboard()
    {
        BackEnd.Leaderboard.BackendUserLeaderboardReturnObject bro = null;

        bro = Backend.Leaderboard.User.GetLeaderboard(leaderboardUuid, 100);

        // ��ŷ ��ȸ ��.
        if (bro.IsSuccess() == false)
        {
            Debug.Log("��ŷ ��ȸ ����");
            return;
        }

        Debug.Log("�������� �� ���� ��� �� : " + bro.GetTotalCount());

        //UI �� ���÷��� �ϱ�
        if (rankingView == null)
            rankingView = GameObject.FindWithTag("RankingView").GetComponent<RankingView>();

        foreach (BackEnd.Leaderboard.UserLeaderboardItem item in bro.GetUserLeaderboardList())
        {
            if (item.nickname == Login.Instance.tempUser) //���θ޴�, ������� ���� �ӽ� ���� ���̵�� �������� �ʽ��ϴ�.
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
