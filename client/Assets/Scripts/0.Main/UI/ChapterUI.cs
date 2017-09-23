using UnityEngine;
using UnityEngine.UI;

public class ChapterUI : MonoBehaviour
{
    [Header("章节名字")]
    public Text chapterName;
    [Header("左章节图")]
    public Image chapterLImgs;
    [Header("中章节图")]
    public Image chapterMImgs;
    [Header("右章节图")]
    public Image chapterRImgs;
    [Header("左按钮")]
    public Button lBtns;
    [Header("右按钮")]
    public Button rBtns;
    [Header("关闭按钮")]
    public Button closeBtn;

    [Header("关卡面板")]
    public GameObject levelPanel;

    int maxChapter = 0;
    int selectChapter = 0;

    void Start()
    {
        chapterMImgs.gameObject.GetComponent<Button>().onClick.AddListener(delegate { OnChapterBtnClick(); });
        lBtns.onClick.AddListener(delegate { OnLeftBtnClick(); });
        rBtns.onClick.AddListener(delegate { OnRightBtnClick(); });

        // int maxChapter = PlayerPrefs.GetInt(Consts.Max_Chapter);
        maxChapter = Consts.Chapter.Length - 1;
        // 目前已开发的章节
        maxChapter = Mathf.Min(maxChapter, Consts.Chapter.Length - 1);
        UpdateChapter();
    }

    void UpdateChapter()
    {
        chapterName.text = Consts.ChapterName[selectChapter];

        chapterLImgs.enabled = true;
        chapterMImgs.sprite = Resources.Load("sprite/" + selectChapter, new Sprite().GetType()) as Sprite;
        chapterRImgs.enabled = true;

        if (selectChapter - 1 < 0) { chapterLImgs.enabled = false; }
        else { chapterLImgs.sprite = Resources.Load("sprite/" + (selectChapter - 1), new Sprite().GetType()) as Sprite; }

        if (selectChapter + 1 > maxChapter) { chapterRImgs.enabled = false; }
        else { chapterRImgs.sprite = Resources.Load("sprite/" + (selectChapter + 1), new Sprite().GetType()) as Sprite; }
    }

    void OnLeftBtnClick()
    {
        selectChapter++;
        if (selectChapter > maxChapter) { selectChapter = maxChapter; }
        UpdateChapter();
    }

    void OnRightBtnClick()
    {
        selectChapter--;
        if (selectChapter < 0) { selectChapter = 0; }
        UpdateChapter();
    }

    void OnChapterBtnClick()
    {
        levelPanel.SetActive(true);
        levelPanel.GetComponent<LevelUI>().InitLevel(selectChapter);
    }
}
