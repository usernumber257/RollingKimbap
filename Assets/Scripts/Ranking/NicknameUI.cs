using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NicknameUI : UIBase
{
    [SerializeField] GameObject text_eng;
    [SerializeField] GameObject text_kor;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button next;
    [SerializeField] UIBase customPlayer;

    protected override void Awake()
    {
        base.Awake();
        
        next.onClick.AddListener(() => {
            if (string.IsNullOrWhiteSpace(inputField.text))
            {
                SoundPlayer.Instance.Play(MyEnum.Sound.Cancel);
                return;
            }

            SoundPlayer.Instance.Play(MyEnum.Sound.Accept);
            PlayerStatManager.Instance.nickname = inputField.text;
            UIManager.Instance.OpenUI(customPlayer);
        });

    }

    private void OnEnable()
    {
        text_eng.SetActive(false);
        text_kor.SetActive(false);

        if (SettingManager.Instance.isKor)
            text_kor.SetActive(true);
        else
            text_eng.SetActive(true);
    }

    private void OnDestroy()
    {
        next.onClick.RemoveAllListeners();
    }
}
