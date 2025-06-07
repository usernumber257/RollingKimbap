using BackEnd;
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
    [Range(1f, 20f)]
    [SerializeField] float minTime = 5f;
    [Range(2f, 40f)]
    [SerializeField] float maxTime = 10f;
    [Header("손님 기분에 따른 인기도 감소, 증가")]
    [Range(-10, 0)]
    [SerializeField] int halfAnger = -2;
    [Range(-10, 0)]
    [SerializeField] int fullAnger = -3;
    [Range(0, 20)]
    [SerializeField] int happy = 2;
    [Header("분노 타이머")]
    [Range(0.5f, 60f)]
    [SerializeField] float halfAngerTime = 35f;
    [Range(0.5f, 60f)]
    [SerializeField] float fullAngerTime = 50f;

    [ContextMenu("값 초기화")]
    public void InitValues()
    {
        initCoin = 100;
        minTime = 5f;
        maxTime = 10f;
        halfAnger = -2;
        fullAnger = -3;
        happy = 2;
        halfAngerTime = 35f;
        fullAngerTime = 50f;
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

        Backend.Initialize();
        Login.Instance.TempLogin();

        //string googlehash = Backend.Utils.GetGoogleHash();
        //Debug.Log("구글 해시 키 : " + googlehash);
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

        if (scene.name == "MainMenuScene" || scene.name == "MainMenuScene_Mobile")
        {
            DestroyManagers();
            PlayerStatManager.Instance.ResetClothes();

            PlayerStatManager.Instance.Timer(false);

            Login.Instance.TempLogin();
            Leaderboard.Instance.GetLeaderboard();


            if (SettingManager.Instance.ControllerCanvas != null)
                SettingManager.Instance.ControllerCanvas.gameObject.SetActive(false);
        }
        else if (scene.name == "GameScene" || scene.name == "GameScene_Mobile")
        {
            InitManagers();
#if UNITY_EDITOR
            Login.Instance.TempLogin();
#else
            Login.Instance.CustomLogin();
            Backend.BMember.UpdateNickname(data.nickname);
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
