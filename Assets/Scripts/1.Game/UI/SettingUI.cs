using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [Header("设置按钮")]
    public Button settingBtn;
    [Header("返回按钮")]
    public Button backBtn;
    [Header("重置按钮")]
    public Button resetBtn;
    [Header("开始按钮")]
    public Button playBtn;
    [Header("设置面板")]
    public GameObject settingPanel;

    void Start()
    {
        // 注册按钮事件
        settingBtn.onClick.AddListener(delegate { OnSettingBtnClick(); });
        backBtn.onClick.AddListener(delegate { OnBackBtnClick(); });
        resetBtn.onClick.AddListener(delegate { OnResetBtnClick(); });
        playBtn.onClick.AddListener(delegate { OnPlayBtnClick(); });
    }

    void OnSettingBtnClick()
    {
        // 暂停游戏
        Time.timeScale = 0;
        settingPanel.SetActive(true);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    void OnBackBtnClick()
    {
        Time.timeScale = 1;
        // 返回主界面
        SceneMgr.Instance.LoadScene(Consts.Scene_Main);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    void OnResetBtnClick()
    {
        Time.timeScale = 1;
        // 重新加载场景
        SceneMgr.Instance.LoadScene(Globals.Instance.level);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    void OnPlayBtnClick()
    {
        // 开始游戏
        Time.timeScale = 1;
        settingPanel.SetActive(false);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }
}
