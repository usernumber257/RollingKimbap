using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomPlayer : MonoBehaviour
{
    public bool isIngame = false;

    public List<GameObject> hairs = new List<GameObject>();
    public List<GameObject> uniforms = new List<GameObject>();
    public List<GameObject> hats = new List<GameObject>();

    private void Start()
    {
        if (!isIngame)
            return;

        if (GameManager.Data.curHair != 0)
            hairs[(int)GameManager.Data.curHair - 1].gameObject.SetActive(true);

        if (GameManager.Data.curUniform != 0)
            uniforms[(int)GameManager.Data.curUniform - 1].gameObject.SetActive(true);

        if (GameManager.Data.curHat != 0)
            hats[(int)GameManager.Data.curHat - 1].gameObject.SetActive(true);

        ChangeHairColor((int)GameManager.Data.curHairColor);
    }

    public void OffAllHairs(int value)
    {
        foreach (GameObject element in hairs)
            element.SetActive(false);

        GameManager.Data.curHair = (MyEnum.Hair)value;
    }

    public void ChangeHairColor(int hairColor)
    {
        Color color = new Color(200f, 200f, 200f, 255f);

        switch ((MyEnum.HairColor)hairColor)
        {
            case MyEnum.HairColor.gray:
                color = new Color(1f, 1f, 1f, 1f);
                break;
            case MyEnum.HairColor.black:
                color = new Color(0.23f, 0.23f, 0.23f, 1f);
                break;
            case MyEnum.HairColor.brown:
                color = new Color(0.47f, 0.39f, 0.27f, 1f);
                break;
            case MyEnum.HairColor.red:
                color = new Color(0.98f, 0.35f, 0.35f, 1f);
                break;
            case MyEnum.HairColor.orange:
                color = new Color(0.98f, 0.66f, 0.35f, 1f);
                break;
            case MyEnum.HairColor.yellow:
                color = new Color(0.98f, 0.82f, 0.35f, 1f);
                break;
            case MyEnum.HairColor.green:
                color = new Color(0.58f, 0.86f, 0.66f, 1f);
                break;
            case MyEnum.HairColor.blue:
                color = new Color(0.58f, 0.86f, 0.98f, 1f);
                break;
            case MyEnum.HairColor.navy:
                color = new Color(0.39f, 0.54f, 0.62f, 1f);
                break;
            case MyEnum.HairColor.purple:
                color = new Color(0.74f, 0.58f, 0.86f, 1f);
                break;
            case MyEnum.HairColor.pink:
                color = new Color(0.98f, 0.74f, 0.78f, 1f);
                break;
        }

        GameManager.Data.curHairColor = (MyEnum.HairColor)hairColor;

        foreach (GameObject element in hairs)
        {
            Image image = element.GetComponent<Image>();

            if (image != null)
                image.color = color;
            else
                element.GetComponent<SpriteRenderer>().material.color = color;
        }
    }

    public void OffAllUniforms(int value)
    {
        foreach (GameObject element in uniforms)
            element.SetActive(false);

        GameManager.Data.curUniform = (MyEnum.Uniform)value;
    }

    public void OffAllHats(int value)
    {
        foreach (GameObject element in hats)
            element.SetActive(false);

        GameManager.Data.curHat = (MyEnum.Hat)value;
    }
}
