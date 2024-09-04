using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kimbap : MonoBehaviour
{
    public enum KimbapType { None, Original, Cheese, Kimchi, Tuna}
    public KimbapType curType;

    [Header("Ingredients")]
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

    [Header("Kimbap")]
    public GameObject originalKimbap;
    public GameObject cheeseKimbap;
    public GameObject kimchiKimbap;
    public GameObject tunaKimbap;

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

    public void Complete()
    {
        DisactiveAllIngredients();

        switch (curType)
        {
            case KimbapType.Original:
                originalKimbap.SetActive(true);
                break;
            case KimbapType.Cheese:
                cheeseKimbap.SetActive(true);
                break;
            case KimbapType.Kimchi:
                kimchiKimbap.SetActive(true);
                break;
            case KimbapType.Tuna:
                tunaKimbap.SetActive(true);
                break;
        }
    }

    void DisactiveAllIngredients()
    {
        seaweed.SetActive(false);
        rice.SetActive(false);
        ham.SetActive(false);
        burdock.SetActive(false);
        cucumber.SetActive(false);
        jidan.SetActive(false);
        carrot.SetActive(false);
        cheese.SetActive(false);
        perillaLeaf.SetActive(false);
        tuna.SetActive(false);
        mayo.SetActive(false);
        kimchi.SetActive(false);
    }

    void DisactiveAllKimbap()
    {
        originalKimbap.SetActive(false);
        cheeseKimbap.SetActive(false);
        kimchiKimbap.SetActive(false);
        tunaKimbap.SetActive(false);
    }
}
