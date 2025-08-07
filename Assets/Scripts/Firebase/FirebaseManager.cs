using System.Collections;
using System.Threading.Tasks;
using BackEnd;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Google;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{
    FirebaseAuth auth;
    FirebaseUser user;

    [SerializeField] private GameObject LoginScreen;
    [SerializeField] private GameObject MainMenuScreen;

    private bool isGoogleSignInConfigured = false;

    private const string WebClientId = "128655055093-oda38me45kvh37o82v2tuotuvjh2j641.apps.googleusercontent.com";

    private void Awake()
    {
        // GoogleSignIn Configuration는 앱 시작 시 1회만 설정
        GoogleSignIn.Configuration = new GoogleSignInConfiguration
        {
            WebClientId = FirebaseManager.WebClientId,
            RequestIdToken = true,
            UseGameSignIn = false,
            RequestEmail = true
        };
        isGoogleSignInConfigured = true;
    }

    private void Start()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        Login.Instance.TempLogin();
        LoginScreen.SetActive(false);
        MainMenuScreen.SetActive(true);
return;
#endif

        InitFirebase();
        StartCoroutine(CheckLoginStatus());
    }

    void InitFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                Debug.Log("Firebase Auth initialized successfully.");
            }
            else
            {
                Debug.LogError("Could not resolve Firebase dependencies: " + task.Result);
            }
        });
    }

    IEnumerator CheckLoginStatus()
    {
        float timeout = 5f;
        while (auth == null && timeout > 0f)
        {
            timeout -= Time.deltaTime;
            yield return null;
        }

        if (auth != null && auth.CurrentUser != null)
        {
            user = auth.CurrentUser;

            LoginScreen.SetActive(false);
            MainMenuScreen.SetActive(true);

            Login.Instance.CustomLogin();
        }
        else
        {
            LoginScreen.SetActive(true);
            MainMenuScreen.SetActive(false);
        }
    }

    public void GoogleSignInClick()
    {
        if (!isGoogleSignInConfigured)
        {
            Debug.LogError("GoogleSignIn not configured!");
            return;
        }

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                foreach (var e in task.Exception.InnerExceptions)
                {
                    var signInException = e as GoogleSignIn.SignInException;
                    if (signInException != null)
                    {
                        Debug.Log("\nSignIn Error: " + signInException.Status + " - " + signInException.Message); 
                    }
                    else
                    {
                        Debug.Log("\nOther Exception: " + e.Message);
                    }
                }
            }
            else if (task.IsCanceled)
            {
                Debug.Log("SignIn Canceled: ");
            }
            else
            {
                OnGoogleAuthenticatedFinished(task);
            }
        });
    }

    void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Google Authentication failed or canceled");
            return;
        }

        GoogleSignInUser userGoogle = task.Result;

        Debug.Log("구글 로그인 성공! 이메일: " + userGoogle.Email);
        Debug.Log("구글 로그인 성공! ID: " + userGoogle.UserId);

        string googleEmail = userGoogle.Email;
        string googleId = userGoogle.UserId;

        Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(userGoogle.IdToken, null);

        auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(authTask =>
        {
            if (authTask.IsCanceled || authTask.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync error: " + authTask.Exception);
                return;
            }

            user = auth.CurrentUser;

            //구글 이메일을 아이디로 설정하기 위해 PlayerPrefs 에 저장
            PlayerPrefs.SetString("UserId", googleEmail);
            PlayerPrefs.Save();

            LoginScreen.SetActive(false);
            MainMenuScreen.SetActive(true);

#if UNITY_IOS || UNITY_ANDROID
            Login.Instance.CustomLogin();
#endif
        });
    }

    public void DeleteAccount()
    {
        Debug.Log("회원 탈퇴 시도");
        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(googleTask =>
        {
            if (googleTask.IsCanceled || googleTask.IsFaulted)
            {
                Debug.LogError("Google 재로그인 실패: " + googleTask.Exception);
                return;
            }

            GoogleSignInUser googleUser = googleTask.Result;
            string idToken = googleUser.IdToken;

            Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

            //Firebase 재인증
            user.ReauthenticateAsync(credential).ContinueWithOnMainThread(authTask =>
            {
                if (authTask.IsCanceled || authTask.IsFaulted)
                {
                    Debug.LogError("Firebase 재인증 실패: " + authTask.Exception);
                    return;
                }

                Debug.Log("Firebase 재인증 성공");

                //Firebase 계정 삭제
                user.DeleteAsync().ContinueWithOnMainThread(deleteTask =>
                {
                    if (deleteTask.IsCanceled || deleteTask.IsFaulted)
                    {
                        Debug.LogError("Firebase 계정 삭제 실패: " + deleteTask.Exception);
                        return;
                    }

                    Debug.Log("Firebase 계정 삭제 성공");

                    //뒤끝 계정 삭제
                    Backend.BMember.WithdrawAccount(backendCallback =>
                    {
                        if (backendCallback.IsSuccess())
                        {
                            Debug.Log("뒤끝 계정 탈퇴 성공");
                            Backend.BMember.Logout();

                            Application.Quit();
                        }
                        else
                        {
                            Debug.LogError("뒤끝 계정 탈퇴 실패: " + backendCallback.ToString());
                        }
                    });
                });
            });
        });
    }
}