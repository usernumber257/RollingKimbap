using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kimbap : MonoBehaviour
{
    public enum KimbapType { None, Original, Cheese, Kimchi, Tuna}
    public KimbapType curType;

    public GameObject seaweed;
    public GameObject rice;
    public GameObject ham;
    public GameObject burdock;
    public GameObject cucumber;
    public GameObject jidan;
    public GameObject carrot;
    public GameObject cheese;
    public GameObject perillaLeaf;
    public GameObject tuna;
    public GameObject mayo;
    public GameObject kimchi;

    int curStack = 0;

    public void StackKimbap()
    {
        curStack++;

        switch (curStack)
        {
            case 1:
                seaweed.SetActive(true);
                break;
            case 2:
                rice.SetActive(true);
                break;
            case 3:
                ham.SetActive(true);
                break;
            case 4:
                burdock.SetActive(true);
                break;
            case 5:
                cucumber.SetActive(true);
                break;
            case 6:
                jidan.SetActive(true);
                break;
            case 7:
                carrot.SetActive(true);
                break;
            case 8:
                if (curType == KimbapType.Cheese)
                {
                    cheese.SetActive(true);
                }
                else if (curType == KimbapType.Kimchi)
                {
                    kimchi.SetActive(true);
                }
                else if (curType == KimbapType.Tuna)
                {
                    perillaLeaf.SetActive(true);
                }
                break;
            case 9:
                tuna.SetActive(true);
                break;
            case 10:
                mayo.SetActive(true);
                break;
        }
    }
}
