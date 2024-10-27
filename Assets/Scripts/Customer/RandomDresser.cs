using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDresser : MonoBehaviour
{
    [SerializeField] List<GameObject> hairs = new List<GameObject>();
    [SerializeField] GameObject clothes;
    [SerializeField] GameObject pants;

    private void OnEnable()
    {
        GetHair();
        GetClothes();
    }

    void GetHair()
    {
        foreach (GameObject element in hairs)
            element.SetActive(false);

        int randNum = Random.Range(0, hairs.Count);

        hairs[randNum].SetActive(true);
        hairs[randNum].GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }

    void GetClothes()
    {
        int randNum = Random.Range(0, hairs.Count);
        clothes.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

        randNum = Random.Range(0, hairs.Count);
        pants.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }
}
