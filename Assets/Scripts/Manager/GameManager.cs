using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    static DataManager data;
    public static DataManager Data { get { return data; } }

    static FlowManager flow;
    public static FlowManager Flow { get { return flow; } }

    static LevelManager level;
    public static LevelManager Level { get { return level; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        InitManagers();
    }

    void InitManagers()
    {
        if (data == null)
            data = CreateGameObject("DataManager").AddComponent<DataManager>();

        if (flow == null)
            flow = CreateGameObject("FlowManager").AddComponent<FlowManager>();

        if (level == null)
            level = CreateGameObject("LevelManager").AddComponent<LevelManager>();
    }

    GameObject CreateGameObject(string name)
    {
        GameObject newGO = new GameObject();
        newGO.name = name;
        newGO.transform.parent = transform;

        return newGO;
    }
}
