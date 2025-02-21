using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ForDebug : MonoBehaviour
{
    [SerializeField] GameObject body;

    private void Start()
    {
        body.SetActive(false);
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.RightControl))
            body.SetActive(!body.activeInHierarchy);
    }

    public void EarnCoin100()
    {
        GameManager.Data.CurCoin += 100;
    }

    public void PlusPopularity10()
    {
        GameManager.Level.Debug_SetPopularity(2);
    }

    public void MinusPopularity10()
    {
        GameManager.Level.Debug_SetPopularity(-2);
    }
}
