using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDresser : MonoBehaviour
{
    [SerializeField] List<GameObject> hairs = new List<GameObject>();

    private void OnEnable()
    {
        foreach (GameObject element in hairs)
            element.SetActive(false);

        int randNum = Random.Range(0, hairs.Count);

        hairs[randNum].SetActive(true);
        hairs[randNum].GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }
}
