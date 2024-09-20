using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    static DataManager data;
    public static DataManager Data { get { return data; } }

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
    }

    GameObject CreateGameObject(string name)
    {
        GameObject newGO = new GameObject();
        newGO.name = name;
        newGO.transform.parent = transform;

        return newGO;
    }
}
