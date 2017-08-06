using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [Header("图片列表")]
    public Image[] levelImgs;
    [Header("按钮列表")]
    public Button[] levelBtns;

    void OnEnable()
    {
        int maxLevel = PlayerPrefs.GetInt(Consts.Max_Level);
        
        // 目前已开发的关卡
        maxLevel = Mathf.Min(maxLevel, Consts.MAXLEVEL - 1);

        for (int i = 0; i <= maxLevel; ++i)
        {
            // 注册按钮事件 跳转指定场景
            int index = i + 1;
            levelBtns[i].onClick.AddListener(delegate { LoadScene(index); });
        }
        for (int i = maxLevel + 1; i < levelImgs.Length; ++i)
        {
            levelImgs[i].sprite = Resources.Load("level_lock", new Sprite().GetType()) as Sprite;
        }
    }

    void LoadScene(int index)
    {
        // 镜头停止
        GameObject.FindGameObjectWithTag(Tags.mainCamera).GetComponent<CameraAnim>().isOk = true;

        Globals.Instance.level = index;
        SceneMgr.Instance.LoadSceneAsnc(index);
        this.gameObject.SetActive(false);
    }
}
