//using BackEnd;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 다른 매니저들을 관리하며, 로그인, 난이도 조절을 합니다.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [Header("창업 비용")]
    [Range(0, 1000)]
    [SerializeField] int initCoin = 100;
    [Header("손님 출현 최소, 최대 시간")]
    [Range(1f, 30f)]
    [SerializeField] float minTime = 20f;
    [Range(2f, 60f)]
    [SerializeField] float maxTime = 30f;
    [Header("손님 기분에 따른 인기도 감소, 증가")]
    [Range(-10, 0)]
    [SerializeField] int halfAnger = -1;
    [Range(-10, 0)]
    [SerializeField] int fullAnger = -2;
    [Range(0, 20)]
    [SerializeField] int happy = 1;
    [Header("분노 타이머")]
    [Range(0.5f, 100f)]
    [SerializeField] float halfAngerTime = 45f;
    [Range(0.5f, 120f)]
    [SerializeField] float fullAngerTime = 70f;

    [ContextMenu("값 초기화")]
    public void InitValues()
    {
        initCoin = 100;
        minTime = 20f;
        maxTime = 30f;
        halfAnger = -1;
        fullAnger = -2;
        happy = 2;
        halfAngerTime = 45f;
        fullAngerTime = 70f;
    }

    static SeatingManager seat;
    public static SeatingManager Seat { get { return seat; } }

    static LevelManager level;
    public static LevelManager Level { get { return level; } }


    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += DetectSceneChange;

#if UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_WIN

        Backend.Initialize();

        string googlehash = Backend.Utils.GetGoogleHash();
        Debug.Log("구글 해시 키 : " + googlehash);
#endif

    }


    void InitManagers()
    {
        PlayerStatManager.Instance.Init(initCoin);

        if (seat == null)
            seat = CreateGameObject("SeatingManager").AddComponent<SeatingManager>();

        if (level == null)
        {
            level = CreateGameObject("LevelManager").AddComponent<LevelManager>();
            level.Init(minTime, maxTime, halfAnger, fullAnger, happy, halfAngerTime, fullAngerTime);
        }
    }

    /// <summary>
    /// 메인 씬에서 필요없는 manager 들은 삭제합니다
    /// </summary>
    void DestroyManagers()
    {
        if (seat != null)
            Destroy(seat.gameObject);

        if (level != null)
            Destroy(level.gameObject);
    }

    GameObject CreateGameObject(string name)
    {
        GameObject newGO = new GameObject();
        newGO.name = name;
        newGO.transform.parent = transform;

        return newGO;
    }

    public UnityAction OnSceneChanged;
    public Scene curScene;

    private void DetectSceneChange(Scene scene, LoadSceneMode mode)
    {
        curScene = scene;

        if (scene.name == "MainMenuScene_Window" || scene.name == "MainMenuScene_Mobile" || scene.name == "MainMenuScene_Web")
        {
            DestroyManagers();
            PlayerStatManager.Instance.ResetClothes();

            PlayerStatManager.Instance.Timer(false);

            #if UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_WIN
            Login.Instance.TempLogin();
#endif

            if (SettingManager.Instance.ControllerCanvas != null)
                SettingManager.Instance.ControllerCanvas.gameObject.SetActive(false);
        }
        else if (scene.name == "GameScene_Window" || scene.name == "GameScene_Mobile" || scene.name == "GameScene_Web")
        {
            InitManagers();
            #if UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_WIN
#if UNITY_EDITOR
            Login.Instance.TempLogin();
#endif
#endif
#if UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_WIN
            if (PlayerStatManager.Instance.nickname == "temp")
                Login.Instance.TempLogin();
            else
                Login.Instance.CustomLogin();
#endif
            PlayerStatManager.Instance.Timer(true);

            if (SettingManager.Instance.ControllerCanvas != null)
                SettingManager.Instance.ControllerCanvas.gameObject.SetActive(true);
        }

        OnSceneChanged?.Invoke();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= DetectSceneChange;
    }

    public void OnExitDetected()
    {
        PlayerStatManager.Instance.UpdateRank();
    }
}
