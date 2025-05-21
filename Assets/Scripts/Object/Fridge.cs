using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    [SerializeField] GameObject viewer;
    [SerializeField] public GameObject combiner;

    private void Update()
    {
        if (viewer.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
            OnESC();
    }

    public void OnESC()
    {
        if (viewer.activeInHierarchy)
        {
            viewer.SetActive(false);
            combiner.SetActive(false);
        }
    }
}
