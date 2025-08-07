using BackEnd;
using System.Collections.Generic;
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
    [SerializeField] Animator nicknameAlert;

    protected override void Awake()
    {
        base.Awake();
        
        next.onClick.AddListener(() => {
            if (string.IsNullOrWhiteSpace(inputField.text))
            {
                SoundPlayer.Instance.Play(MyEnum.Sound.Cancel);
                return;
            }

            if (inputField.text == "tempUser257")
            {
                SoundPlayer.Instance.Play(MyEnum.Sound.Accept);
                UIManager.Instance.CloseUI(this);
                UIManager.Instance.OpenUI(customPlayer);
                return;
            }

            /*
            BackendReturnObject bro = Backend.BMember.CheckNicknameDuplication(inputField.text);

            if (bro.IsSuccess())
            {
                SoundPlayer.Instance.Play(MyEnum.Sound.Accept);
                PlayerStatManager.Instance.nickname = inputField.text;
                UIManager.Instance.CloseUI(this);
                UIManager.Instance.OpenUI(customPlayer);
            }
            else
                nicknameAlert.SetTrigger("Play");
            */

            SoundPlayer.Instance.Play(MyEnum.Sound.Accept);
            PlayerStatManager.Instance.nickname = inputField.text;
            UIManager.Instance.CloseUI(this);
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
