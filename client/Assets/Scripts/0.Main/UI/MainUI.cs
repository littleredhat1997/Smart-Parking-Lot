using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [Header("确认按钮")]
    public Button confirmBtn;
    [Header("输入框")]
    public InputField inputField;
    [Header("输入面板")]
    public GameObject inputPanel;

    [Header("开始按钮")]
    public Button startBtn;
    [Header("章节关闭按钮")]
    public Button chapterClose;
    [Header("章节面板")]
    public GameObject chapterPanel;
    [Header("关卡关闭按钮")]
    public Button levelClose;
    [Header("关卡面板")]
    public GameObject levelPanel;

    [Header("排行按钮")]
    public Button rankBtn;
    [Header("排行关闭按钮")]
    public Button rankClose;
    [Header("排行面板")]
    public GameObject rankPanel;

    [Header("选项按钮")]
    public Button optionBtn;
    [Header("选项关闭按钮")]
    public Button optionClose;
    [Header("选项面板")]
    public GameObject optionPanel;

    [Header("退出按钮")]
    public Button quitBtn;

    [Header("音量滑块")]
    public Slider musicSlider;
    [Header("音效滑块")]
    public Slider effectSlider;

    void Start()
    {
        // 注册按钮事件
        confirmBtn.onClick.AddListener(delegate { OnConfirmBtnClick(); });
        startBtn.onClick.AddListener(delegate { OnStartBtnClick(); });
        chapterClose.onClick.AddListener(delegate { OnChapterCloseBtnClick(); });
        levelClose.onClick.AddListener(delegate { OnLevelCloseClick(); });
        rankBtn.onClick.AddListener(delegate { OnRankBtnClick(); });
        rankClose.onClick.AddListener(delegate { OnRankCloseClick(); });
        optionBtn.onClick.AddListener(delegate { OnOptionBtnClick(); });
        optionClose.onClick.AddListener(delegate { OnOptionCloseClick(); });
        quitBtn.onClick.AddListener(delegate { OnQuitBtnClick(); });

        // 注册滑块事件
        musicSlider.onValueChanged.AddListener(delegate { OnMusicSliderChange(); });
        effectSlider.onValueChanged.AddListener(delegate { OnEffectSliderChange(); });

        // 读取音量存档 默认0.5
        float musicVolume = PlayerPrefs.GetFloat(Consts.Music_Volume, 0.5f);
        musicSlider.value = musicVolume;
        AudioMgr.Instance.SetMusicVolume(musicVolume);

        // 读取音效存档 默认0.5
        float effectVolume = PlayerPrefs.GetFloat(Consts.Effect_Volume, 0.5f);
        effectSlider.value = effectVolume;
        AudioMgr.Instance.SetEffectVolume(effectVolume);
    }

    /// <summary>
    /// 确认
    /// </summary>
    void OnConfirmBtnClick()
    {
        string username = inputField.text;

        if (string.IsNullOrEmpty(username)) { return; }

        Globals.Instance.username = username;
        inputPanel.SetActive(false);

        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    /// <summary>
    /// 开始
    /// </summary>
    void OnStartBtnClick()
    {
        chapterPanel.SetActive(true);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    void OnChapterCloseBtnClick()
    {
        chapterPanel.SetActive(false);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    void OnLevelCloseClick()
    {
        levelPanel.SetActive(false);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    /// <summary>
    /// 排行
    /// </summary>
    void OnRankBtnClick()
    {
        rankPanel.SetActive(true);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    void OnRankCloseClick()
    {
        rankPanel.SetActive(false);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    /// <summary>
    /// 选项
    /// </summary>
    void OnOptionBtnClick()
    {
        optionPanel.SetActive(true);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    void OnOptionCloseClick()
    {
        optionPanel.SetActive(false);
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    /// <summary>
    /// 退出
    /// </summary>
    void OnQuitBtnClick()
    {
        Application.Quit();
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    /// <summary>
    /// 音量控制
    /// </summary>
    void OnMusicSliderChange()
    {
        float value = musicSlider.value;
        AudioMgr.Instance.SetMusicVolume(value);
        PlayerPrefs.SetFloat(Consts.Music_Volume, value);
    }

    /// <summary>
    /// 音效控制
    /// </summary>
    void OnEffectSliderChange()
    {
        float value = effectSlider.value;
        AudioMgr.Instance.SetEffectVolume(value);
        PlayerPrefs.SetFloat(Consts.Effect_Volume, value);
    }
}
