using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [Header("帮助面板")]
    public GameObject helpPanel;

    [Header("传带按钮")]
    public Button beltBtn;
    [Header("传带数量")]
    public Text beltNum;

    [Header("电梯按钮")]
    public Button liftBtn;
    [Header("电梯数量")]
    public Text liftNum;

    [Header("机器一号按钮")]
    public Button robot_1Btn;
    [Header("机器一号数量")]
    public Text robot_1Num;
    [Header("机器三号按钮")]
    public Button robot_3Btn;
    [Header("机器三号")]
    public Text robot_3Num;

    void Start()
    {
        // 注册按钮事件
        beltBtn.onClick.AddListener(delegate { OnBeltBtnClick(); });

        liftBtn.onClick.AddListener(delegate { OnLiftBtnClick(); });

        robot_1Btn.onClick.AddListener(delegate { OnRobotBtn1Click(); });
        robot_3Btn.onClick.AddListener(delegate { OnRobotBtn3Click(); });
    }

    public void UpdateNum()
    {
        beltNum.text = GameManager.Instance.beltNum.ToString();
        liftNum.text = GameManager.Instance.liftNum.ToString();
        robot_1Num.text = GameManager.Instance.robot1Num.ToString();
        robot_3Num.text = GameManager.Instance.robot3Num.ToString();
    }

    void OnBeltBtnClick()
    {
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);

        int isShow = PlayerPrefs.GetInt(Consts.Help_Belt);
        if (isShow == 0)
        {
            helpPanel.SetActive(true);
            helpPanel.GetComponent<HelpUI>().ShowHelp(ItemType.Belt);
        }

        ItemHandler.Instance.SetType(GameManager.Instance.CheckItem(ItemType.Belt) ? ItemType.Belt : ItemType.NULL);
    }

    void OnLiftBtnClick()
    {
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);

        int isShow = PlayerPrefs.GetInt(Consts.Help_Lift);
        if (isShow == 0)
        {
            helpPanel.SetActive(true);
            helpPanel.GetComponent<HelpUI>().ShowHelp(ItemType.Lift);
        }

        ItemHandler.Instance.SetType(GameManager.Instance.CheckItem(ItemType.Lift) ? ItemType.Lift : ItemType.NULL);
    }

    void OnRobotBtn1Click()
    {
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);

        int isShow = PlayerPrefs.GetInt(Consts.Help_Robot1);
        if (isShow == 0)
        {
            helpPanel.SetActive(true);
            helpPanel.GetComponent<HelpUI>().ShowHelp(ItemType.Robot_1);
        }

        ItemHandler.Instance.SetType(GameManager.Instance.CheckItem(ItemType.Robot_1) ? ItemType.Robot_1 : ItemType.NULL);
    }

    void OnRobotBtn3Click()
    {
        AudioMgr.Instance.PlayEffect(Consts.Audio_Click);

        int isShow = PlayerPrefs.GetInt(Consts.Help_Robot3);
        if (isShow == 0)
        {
            helpPanel.SetActive(true);
            helpPanel.GetComponent<HelpUI>().ShowHelp(ItemType.Robot_3);
        }

        ItemHandler.Instance.SetType(GameManager.Instance.CheckItem(ItemType.Robot_3) ? ItemType.Robot_3 : ItemType.NULL);
    }
}
