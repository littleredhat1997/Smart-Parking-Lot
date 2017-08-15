using UnityEngine;
using UnityEngine.UI;

public class CompleteUI : MonoBehaviour
{
    [Header("分数文本")]
    public Text scoreText;
    [Header("Win返回按钮")]
    public Button winBackBtn;
    [Header("Over返回按钮")]
    public Button overBackBtn;

    void Start()
    {
        // 注册按钮事件
        winBackBtn.onClick.AddListener(delegate { OnBackBtnClick(); });
        overBackBtn.onClick.AddListener(delegate { OnBackBtnClick(); });
    }

    void OnBackBtnClick()
    {
        Time.timeScale = 1;
        SceneMgr.Instance.LoadScene(Consts.Scene_Main);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    /// <summary>
    /// 游戏胜利更新分数
    /// </summary>
    public void UpdateScore()
    {
        scoreText.text = GameManager.Instance.score.ToString();
    }
}
