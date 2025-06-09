using BackEnd;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// �ٸ� �Ŵ������� �����ϸ�, �α���, ���̵� ������ �մϴ�.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [Header("â�� ���")]
    [Range(0, 1000)]
    [SerializeField] int initCoin = 100;
    [Header("�մ� ���� �ּ�, �ִ� �ð�")]
    [Range(1f, 20f)]
    [SerializeField] float minTime = 20f;
    [Range(2f, 40f)]
    [SerializeField] float maxTime = 30f;
    [Header("�մ� ��п� ���� �α⵵ ����, ����")]
    [Range(-10, 0)]
    [SerializeField] int halfAnger = -1;
    [Range(-10, 0)]
    [SerializeField] int fullAnger = -2;
    [Range(0, 20)]
    [SerializeField] int happy = 1;
    [Header("�г� Ÿ�̸�")]
    [Range(0.5f, 100f)]
    [SerializeField] float halfAngerTime = 45f;
    [Range(0.5f, 120f)]
    [SerializeField] float fullAngerTime = 70f;

    [ContextMenu("�� �ʱ�ȭ")]
    public void InitValues()
    {
        initCoin = 100;
        minTime = 8f;
        maxTime = 15f;
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

        Backend.Initialize();

        string googlehash = Backend.Utils.GetGoogleHash();
        Debug.Log("���� �ؽ� Ű : " + googlehash);
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
    /// ���� ������ �ʿ���� manager ���� �����մϴ�
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

            if (SettingManager.Instance.ControllerCanvas != null)
                SettingManager.Instance.ControllerCanvas.gameObject.SetActive(false);
        }
        else if (scene.name == "GameScene" || scene.name == "GameScene_Mobile")
        {
            InitManagers();

#if UNITY_EDITOR
            Login.Instance.TempLogin();
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
