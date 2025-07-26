using UnityEngine;

public class RankingButton : MonoBehaviour
{
    [SerializeField] GameObject kor;
    [SerializeField] GameObject eng;

    public Animator rankingAnim;

    private void Start()
    {
        kor.SetActive(SettingManager.Instance.isKor);
        eng.SetActive(!SettingManager.Instance.isKor);
    }

    public void Ranking()
    {
        bool result = PlayerStatManager.Instance.UpdateRank();

        if (result)
            rankingAnim.SetTrigger("Success");
        else
            rankingAnim.SetTrigger("Fail");
    }
}
