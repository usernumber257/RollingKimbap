using UnityEngine;

public class RankingButton : MonoBehaviour
{
#if UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_WIN

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

        Debug.Log(result);

        if (result)
            rankingAnim.SetTrigger("Success");
        else
            rankingAnim.SetTrigger("Fail");
    }
#endif
}
