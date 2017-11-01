using UnityEngine;
using UnityEngine.UI;

public class HelpUI : MonoBehaviour
{
    [Header("单选框")]
    public Toggle selectToggle;
    [Header("确认按钮")]
    public Button confirmBtn;

    [Header("教程数组")]
    public GameObject[] helpArray;

    private ItemType nowType = ItemType.Belt;

    void Start()
    {
        confirmBtn.onClick.AddListener(delegate { OnConfirmBtnClick(); });
    }

    public void ShowHelp(ItemType itemType)
    {
        helpArray[(int)nowType - 1].SetActive(false);
        helpArray[(int)itemType - 1].SetActive(true);
        nowType = itemType;
    }

    void OnConfirmBtnClick()
    {
        // 点击不再显示
        if (selectToggle.isOn == true)
        {
            switch (nowType)
            {
                case ItemType.Belt:
                    PlayerPrefs.SetInt(Consts.Help_Belt, 1);
                    break;
                case ItemType.Lift:
                    PlayerPrefs.SetInt(Consts.Help_Lift, 1);
                    break;
                case ItemType.Robot_1:
                    PlayerPrefs.SetInt(Consts.Help_Robot1, 1);
                    break;
                case ItemType.Robot_3:
                    PlayerPrefs.SetInt(Consts.Help_Robot3, 1);
                    break;
            }
            selectToggle.isOn = false;
        }

        this.gameObject.SetActive(false);
    }
}
