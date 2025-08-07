#if UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_WIN
using UnityEngine;
using BackEnd;

/// <summary>
/// 로그인을 다룹니다
/// </summary>
public class Login
{
    private static Login _instance = null;

    public static Login Instance
    {
        get { if (_instance == null) _instance = new Login(); return _instance; }
    }

    public string tempUser = "tempUser257";

    /// <summary>
    /// 임시 로그인입니다. 디버깅용으로 쓰입니다
    /// </summary>
    public void TempLogin()
    {
        BackendReturnObject sign = Backend.BMember.CustomSignUp(tempUser, "0000");

        if (sign.IsSuccess())
        {
            Debug.Log("임시 아이디 생성 완료");
        }

        Debug.Log("임시 로그인을 요청합니다.");

        var login = Backend.BMember.CustomLogin(tempUser, "0000");

        if (login.IsSuccess())
        {
            Debug.Log("임시 로그인이 성공했습니다. : " + login);

            Leaderboard.Instance.GetLeaderboard();
        }
        else
        {
            Debug.LogError("임시 로그인이 실패했습니다. : " + login);
        }

        Backend.BMember.UpdateNickname(tempUser);
    }

    /// <summary>
    /// 사용자는 구글 메일로 로그인을 시도하며, 구글 메일을 아이디로 등록합니다.
    /// </summary>
    public void CustomLogin()
    {
        string myID = PlayerPrefs.GetString("UserId");

        BackendReturnObject sign = Backend.BMember.CustomSignUp(myID, "0000");

        if (sign.IsSuccess())
        {
            Debug.Log("회원가입에 성공했습니다");
        }

        Debug.Log("로그인을 요청합니다.");

        var login = Backend.BMember.CustomLogin(myID, "0000");

        if (login.IsSuccess())
        {
            Debug.Log("로그인이 성공했습니다. : " + login);

            Leaderboard.Instance.GetLeaderboard();
        }
        else
        {
            Debug.LogError("로그인이 실패했습니다. : " + login);
        }
    }
}
#endif
