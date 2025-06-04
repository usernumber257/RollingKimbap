using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("â�� ���")]
    [Range(0, 1000)]
    [SerializeField] int initCoin = 100;
    [Header("�մ� ���� �ּ�, �ִ� �ð�")]
    [Range(1f, 20f)]
    [SerializeField] float minTime = 5f;
    [Range(2f, 40f)]
    [SerializeField] float maxTime = 10f;
    [Header("�մ� ��п� ���� �α⵵ ����, ����")]
    [Range(-10, 0)]
    [SerializeField] int halfAnger = -2;
    [Range(-10, 0)]
    [SerializeField] int fullAnger = -3;
    [Range(0, 20)]
    [SerializeField] int happy = 2;
    [Header("�г� Ÿ�̸�")]
    [Range(0.5f, 60f)]
    [SerializeField] float halfAngerTime = 35f;
    [Range(0.5f, 60f)]
    [SerializeField] float fullAngerTime = 50f;

    [ContextMenu("�� �ʱ�ȭ")]
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


    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    static DataManager data;
    public static DataManager Data { get { return data; } }

    static FlowManager flow;
    public static FlowManager Flow { get { return flow; } }

    static LevelManager level;
    public static LevelManager Level { get { return level; } }

    static UIManager ui;
    public static UIManager UI { get { return ui; } }

    static SettingManager setting;
    public static SettingManager Setting { get { return setting; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += DetectSceneChange;

        if (data == null)
            data = CreateGameObject("DataManager").AddComponent<DataManager>();

        if (setting == null)
            setting = CreateGameObject("SettingManager").AddComponent<SettingManager>();

        Backend.Initialize();
        Login.Instance.TempLogin();

        string googlehash = Backend.Utils.GetGoogleHash();

        Debug.Log("���� �ؽ� Ű : " + googlehash);
    }


    void InitManagers()
    {
        data.Init(initCoin);

        if (flow == null)
            flow = CreateGameObject("FlowManager").AddComponent<FlowManager>();

        if (level == null)
        {
            level = CreateGameObject("LevelManager").AddComponent<LevelManager>();
            level.Init(minTime, maxTime, halfAnger, fullAnger, happy, halfAngerTime, fullAngerTime);
        }
        else
        {
            Debug.Log(level);
        }

        if (ui == null)
            ui = CreateGameObject("UIManager").AddComponent<UIManager>();
    }

    void DestroyManagers()
    {
        if (flow != null)
            Destroy(flow.gameObject);

        if (level != null)
            Destroy(level.gameObject);

        if (ui != null)
            Destroy(ui.gameObject);
    }

    GameObject CreateGameObject(string name)
    {
        GameObject newGO = new GameObject();
        newGO.name = name;
        newGO.transform.parent = transform;

        return newGO;
    }

    private void DetectSceneChange(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenuScene" || scene.name == "MainMenuScene_Mobile")
        {
            DestroyManagers();
            data.ResetClothes();

            data.Timer(false);

            Login.Instance.TempLogin();
            Leaderboard.Instance.GetLeaderboard();
        }
        else if (scene.name == "GameScene" || scene.name == "GameScene_Mobile")
        {
            InitManagers();
            Login.Instance.CustomLogin();
            Backend.BMember.UpdateNickname(data.nickname);

            data.Timer(true);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= DetectSceneChange;
    }

    public void OnExitDetected()
    {
        data.UpdateRank();
    }

}
