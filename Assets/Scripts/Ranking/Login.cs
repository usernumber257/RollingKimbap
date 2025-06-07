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
    /// 게임에 들어가기 전 랭킹을 보기위해 이미 존재하는 아이디로 임시 로그인합니다. 또한 에디터에서 디버깅을 위해 쓰입니다.
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
        }
        else
        {
            Debug.LogError("임시 로그인이 실패했습니다. : " + login);
        }

        Backend.BMember.UpdateNickname(tempUser);
    }

    /// <summary>
    /// 입력된 닉네임과 겹치지 않는 id 를 부여받아 로그인합니다.
    /// </summary>
    public void CustomLogin()
    {
        string customID = (System.Guid.NewGuid().ToString("N")).Substring(0, 14);

        BackendReturnObject sign = Backend.BMember.CustomSignUp(customID, "0000");

        if (sign.IsSuccess())
        {
            Debug.Log("회원가입에 성공했습니다");
        }

        Debug.Log("로그인을 요청합니다.");

        var login = Backend.BMember.CustomLogin(customID, "0000");

        if (login.IsSuccess())
        {
            Debug.Log("로그인이 성공했습니다. : " + login);
        }
        else
        {
            Debug.LogError("로그인이 실패했습니다. : " + login);
        }
    }
}
