using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [Header("传送带按钮")]
    public Button beltBtn;
    [Header("传送带数量")]
    public Text beltNum;

    [Header("升降台按钮")]
    public Button liftBtn;
    [Header("传送带数量")]
    public Text liftNum;

    [Header("机器人一号按钮")]
    public Button robot_1Btn;
    [Header("机器人一号数量")]
    public Text robot_1Num;
    [Header("机器人三号按钮")]
    public Button robot_3Btn;
    [Header("机器人三号")]
    public Text robot_3Num;

    void Start()
    {
        // 注册按钮事件
        beltBtn.onClick.AddListener(delegate { OnBeltBtnClick(); });

        liftBtn.onClick.AddListener(delegate { OnLiftBtnClick(); });

        robot_1Btn.onClick.AddListener(delegate { OnRobotBtn1Click(); });
        robot_3Btn.onClick.AddListener(delegate { OnRobotBtn3Click(); });

        UpdateNum();
    }

    public void UpdateNum()
    {
        // 初始化数量
        beltNum.text = GameManager.Instance.beltNum.ToString();
        liftNum.text = GameManager.Instance.liftNum.ToString();
        robot_1Num.text = GameManager.Instance.robot_1Num.ToString();
        robot_3Num.text = GameManager.Instance.robot_3Num.ToString();
    }

    /// <summary>
    /// 传送带按钮
    /// </summary>
    void OnBeltBtnClick()
    {
        if(GameManager.Instance.beltNum == 0) { return; }

        PutDownHandler.Instance.SetType(ItemType.Belt);
         AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    /// <summary>
    /// 升降台按钮
    /// </summary>
    void OnLiftBtnClick()
    {
        if (GameManager.Instance.liftNum == 0) { return; }

        PutDownHandler.Instance.SetType(ItemType.Lift);
         AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    void OnRobotBtn1Click()
    {
        if (GameManager.Instance.robot_1Num == 0) { return; }

        PutDownHandler.Instance.SetType(ItemType.Robot_1);
         AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }

    void OnRobotBtn3Click()
    {
        if (GameManager.Instance.robot_3Num == 0) { return; }

        PutDownHandler.Instance.SetType(ItemType.Robot_3);
         AudioMgr.Instance.PlayEffect(Consts.Audio_Click);
    }
}
