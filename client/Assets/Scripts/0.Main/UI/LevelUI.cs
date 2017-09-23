using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [Header("图片列表")]
    public Image[] levelImgs;
    [Header("按钮列表")]
    public Button[] levelBtns;

    public void InitLevel(int selectChapter)
    {
        // 第一关卡
        int from = 1; for (int i = 0; i < selectChapter; i++) { from += Consts.Chapter[i]; }
        // 当前章节最大关卡
        int maxLevel = Consts.Chapter[selectChapter];

        for (int i = 0; i < maxLevel; i++)
        {
            // 注册按钮事件
            int index = from + i;
            levelBtns[i].enabled = true;
            levelBtns[i].onClick.AddListener(delegate { LoadScene(index); });
        }
        for (int i = maxLevel; i < levelImgs.Length; i++)
        {
            // 设置关卡精灵
            levelBtns[i].enabled = false;
            levelImgs[i].sprite = Resources.Load("sprite/level_lock", new Sprite().GetType()) as Sprite;
        }
    }

    void LoadScene(int index)
    {
        // 镜头停止
        GameObject.FindGameObjectWithTag(Consts.MainCamera).GetComponent<CameraAnim>().isOk = true;

        Globals.Instance.level = index;
        SceneMgr.Instance.LoadSceneAsnc(index);
        this.gameObject.SetActive(false);
    }
}
