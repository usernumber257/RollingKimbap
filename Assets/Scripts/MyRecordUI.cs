using BackEnd;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using UnityEngine.SceneManagement;

public class MyRecordUI : MonoBehaviour
{
    [SerializeField] GameObject body;

    [SerializeField] RankingContent prefab_rankingContent;
    [SerializeField] Transform content;
    [SerializeField] GameObject alert;
    [SerializeField] Button bttn_cancel;
    [SerializeField] Button bttn_delete;

    [SerializeField] GameObject sure_kor;
    [SerializeField] GameObject sure_eng;
    [SerializeField] GameObject yes_kor;
    [SerializeField] GameObject yes_eng;
    [SerializeField] GameObject no_kor;
    [SerializeField] GameObject no_eng;

    List<RankingContent> pool = new List<RankingContent>();

    private void Awake()
    {
        body.SetActive(false);
        alert.SetActive(false);
    }

    public void OpenUI()
    {
        bool isKor = SettingManager.Instance.isKor;

        sure_kor.SetActive(isKor);
        sure_eng.SetActive(!isKor);
        yes_kor.SetActive(isKor);
        yes_eng.SetActive(!isKor);
        no_kor.SetActive(isKor);
        no_eng.SetActive(!isKor);

        ReadMyRecords();

        body.SetActive(true);
    }

    public void CloseUI()
    {
        body.SetActive(false);
    }

    void ReadMyRecords()
    {
        foreach (var element in pool)
            Destroy(element.gameObject);
        pool.Clear();

        Where where = new Where();
        where.Equal("owner_inDate", Backend.UserInDate);

        Backend.GameData.Get("PlayRecordLog", where, 100, callback =>
        {
            if (!callback.IsSuccess())
            {
                Debug.LogError("내 기록 조회 실패: " + callback.ToString());
                return;
            }

            JsonData jsonData = callback.GetReturnValuetoJSON();
            JsonData rows = jsonData["rows"];

            if (rows.Count == 0)
            {
                Debug.Log("플레이 기록이 없습니다.");
                return;
            }

            List<PlayRecord> playRecords = new List<PlayRecord>();

            for (int i = 0; i < rows.Count; i++)
            {
                JsonData record = rows[i];

                try
                {
                    PlayRecord pr = new PlayRecord()
                    {
                        customId = record["custom_id"]["S"].ToString(),
                        nickName = record["nickName"]["S"].ToString(),
                        coin = int.Parse(record["coin"]["N"].ToString()),
                        playTime = float.Parse(record["playTime"]["N"].ToString()),
                        hair = int.Parse(record["hair"]["N"].ToString()),
                        hairColor = int.Parse(record["hairColor"]["N"].ToString()),
                        uniform = int.Parse(record["uniform"]["N"].ToString()),
                        hat = int.Parse(record["hat"]["N"].ToString()),
                        playDate = record["playDate"]["S"].ToString()
                    };

                    playRecords.Add(pr);
                }
                catch (Exception e)
                {
                    Debug.LogError($"플레이 기록 파싱 중 오류 발생: {e.Message}");
                }
            }

            // 최신 순으로 정렬
            playRecords.Sort((a, b) => b.playDate.CompareTo(a.playDate));

            for (int i = 0; i < playRecords.Count; i++)
            {
                PlayRecord p = playRecords[i];

                RankingContent newGO = Instantiate(prefab_rankingContent, content);
                newGO.Init(i, p.coin, p.nickName, p.playTime, p.hair, p.hairColor, p.uniform, p.hat);
                pool.Add(newGO);

                //랭킹 숫자 감추기
                newGO.rank.gameObject.SetActive(false);

                //삭제 버튼 init
                string customIdCopy = p.customId;
                newGO.Bttn_remove.gameObject.SetActive(true);
                newGO.Bttn_remove.onClick.AddListener(() => ShowAlert(customIdCopy));
            }
        });
    }

    public void ShowAlert(string customId)
    {
        bttn_cancel.onClick.RemoveAllListeners();
        bttn_delete.onClick.RemoveAllListeners();

        bttn_cancel.onClick.AddListener(() => { alert.SetActive(false); });
        bttn_delete.onClick.AddListener(() => { DeleteMyRecord(customId); });

        alert.SetActive(true);
    }

    public void DeleteMyRecord(string customId)
    {
        Where where = new Where();
        where.Equal("custom_id", customId);

        Backend.GameData.Delete("PlayRecordLog", where, callback =>
        {
            if (callback.IsSuccess())
            {
                Debug.Log("기록 삭제 성공");
                SceneManager.LoadScene("MainMenuScene_Mobile");
            }
            else
            {
                Debug.LogError("기록 삭제 실패: " + callback.ToString());
            }
        });
    }
}