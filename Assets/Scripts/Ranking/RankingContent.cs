using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 랭킹 뷰어에 들어갈 데이터를 보여주는 UI
/// </summary>
public class RankingContent : MonoBehaviour
{
    [SerializeField] TMP_Text rank;
    [SerializeField] TMP_Text nickname;
    [SerializeField] TMP_Text playTime;
    [SerializeField] TMP_Text coin;

    [Header("Model")]
    public List<GameObject> hairs = new List<GameObject>();
    public List<GameObject> uniforms = new List<GameObject>();
    public List<GameObject> hats = new List<GameObject>();

    public void Init(int rank, int coin, string nickname, float playTime, int hair, int hairColor, int uniform, int hat)
    {
        this.rank.text = rank.ToString("D2");

        if (rank == 1)
            this.rank.color = Color.yellow;
        else if (rank == 2)
            this.rank.color = Color.cyan;
        else if (rank == 3)
            this.rank.color = Color.gray;

        this.nickname.text = nickname;

        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(playTime);
        this.playTime.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

        this.coin.text = coin.ToString();

        if (hair != 0)
            hairs[hair - 1].SetActive(true);
        if (uniform != 0)
            uniforms[uniform - 1].SetActive(true);
        if (hat != 0)
            hats[hat - 1].SetActive(true);
        ChangeHairColor(hairColor);
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

        foreach (GameObject element in hairs)
        {
            Image image = element.GetComponent<Image>();

            if (image != null)
                image.color = color;
            else
                element.GetComponent<SpriteRenderer>().material.color = color;
        }
    }
}
