using UnityEngine;
using BackEnd;

/// <summary>
/// �α����� �ٷ�ϴ�
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
    /// ���ӿ� ���� �� ��ŷ�� �������� �̹� �����ϴ� ���̵�� �ӽ� �α����մϴ�. ���� �����Ϳ��� ������� ���� ���Դϴ�.
    /// </summary>
    public void TempLogin()
    {
        BackendReturnObject sign = Backend.BMember.CustomSignUp(tempUser, "0000");

        if (sign.IsSuccess())
        {
            Debug.Log("�ӽ� ���̵� ���� �Ϸ�");
        }

        Debug.Log("�ӽ� �α����� ��û�մϴ�.");

        var login = Backend.BMember.CustomLogin(tempUser, "0000");

        if (login.IsSuccess())
        {
            Debug.Log("�ӽ� �α����� �����߽��ϴ�. : " + login);
        }
        else
        {
            Debug.LogError("�ӽ� �α����� �����߽��ϴ�. : " + login);
        }

        Backend.BMember.UpdateNickname(tempUser);
    }

    /// <summary>
    /// �Էµ� �г��Ӱ� ��ġ�� �ʴ� id �� �ο��޾� �α����մϴ�.
    /// </summary>
    public void CustomLogin()
    {
        string customID = (System.Guid.NewGuid().ToString("N")).Substring(0, 14);

        BackendReturnObject sign = Backend.BMember.CustomSignUp(customID, "0000");

        if (sign.IsSuccess())
        {
            Debug.Log("ȸ�����Կ� �����߽��ϴ�");
        }

        Debug.Log("�α����� ��û�մϴ�.");

        var login = Backend.BMember.CustomLogin(customID, "0000");

        if (login.IsSuccess())
        {
            Debug.Log("�α����� �����߽��ϴ�. : " + login);
        }
        else
        {
            Debug.LogError("�α����� �����߽��ϴ�. : " + login);
        }
    }
}
