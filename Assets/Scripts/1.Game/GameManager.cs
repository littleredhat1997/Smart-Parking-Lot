using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Ready,
    Start,
    GameWin,
    GameOver,
    End
}

public class GameManager : GameSingleton<GameManager>
{
    [Header("游戏状态")]
    public GameState gameState;

    [Header("UI管理类")]
    public GameObject uiMgr;

    [Header("分数文本")]
    public Text scoreText;
    [Header("设置按钮")]
    public Button setting;
    [Header("开始按钮")]
    public Button going;
    [Header("Score界面")]
    public GameObject scorePanel;
    [Header("Item界面")]
    public GameObject itemPanel;
    [Header("GameWin界面")]
    public GameObject gameWinPanel;
    [Header("GameOver界面")]
    public GameObject gameOverPanel;

    private int _score;
    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            scoreText.text = score.ToString();
        }
    }

    public int beltNum;
    public int liftNum;
    public int robot_1Num;
    public int robot_3Num;

    public override void Awake()
    {
        // Test
        // InitNum(3);
        InitNum(Globals.Instance.level);
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.Ready:
                Debug.Log("GameState.Ready");

                Time.timeScale = 1;
                break;
            case GameState.Start:
                Debug.Log("GameState.Start");

                scorePanel.SetActive(true);
                itemPanel.SetActive(true);
                setting.gameObject.SetActive(true);
                going.gameObject.SetActive(true);
                break;
            case GameState.GameWin:
                Debug.Log("GameState.GameWin");
                gameState = GameState.End;

                Time.timeScale = 0;
                gameWinPanel.SetActive(true);

                // 播放胜利音效
                AudioMgr.Instance.PlayEffect(Consts.Audio_Win);

                // 更新UI
                uiMgr.GetComponent<CompleteUI>().UpdateScore();

                // 上传分数
                ServerMgr.Instance.Upload(Globals.Instance.username, Globals.Instance.level, score);

                // 保存最大关卡
                int maxLevel = PlayerPrefs.GetInt(Consts.Max_Level);
                maxLevel = Mathf.Max(maxLevel, Globals.Instance.level);
                PlayerPrefs.SetInt(Consts.Max_Level, maxLevel);

                break;
            case GameState.GameOver:
                Debug.Log("GameState.GameOver");
                gameState = GameState.End;

                Time.timeScale = 0;
                gameOverPanel.SetActive(true);

                // 播放失败音效
                AudioMgr.Instance.PlayEffect(Consts.Audio_Over);

                break;
            case GameState.End:

                // NONE
                Debug.Log("GameState.End");

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 更新游戏状态
    /// </summary>
    public void UpdateState()
    {
        gameState = GameState.Start;
    }

    /// <summary>
    /// 更新道具数量
    /// </summary>
    public void UpdateNum()
    {
        uiMgr.GetComponent<ItemUI>().UpdateNum();
    }

    /// <summary>
    /// 撤回道具放置
    /// </summary>
    /// <param name="type"></param>
    public void RenewItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.Belt:
                beltNum++;
                score -= Consts.Score_Belt;
                break;
            case ItemType.Lift:
                liftNum++;
                score -= Consts.Score_Lift;
                break;
            case ItemType.Robot_1:
                robot_1Num++;
                score -= Consts.Score_Robot_1;
                break;
            case ItemType.Robot_3:
                robot_3Num++;
                score -= Consts.Score_Robot_3;
                break;
            default:
                break;
        }
        UpdateNum();
    }

    void InitNum(int level)
    {
        switch (level)
        {
            // 第一关
            case 1:
                beltNum = Consts.Level_One_Belt;
                liftNum = Consts.Level_One_Lift;
                robot_1Num = Consts.Level_One_Robot_1;
                robot_3Num = Consts.Level_One_Robot_3;
                break;
            // 第二关
            case 2:
                beltNum = Consts.Level_Two_Belt;
                liftNum = Consts.Level_Two_Lift;
                robot_1Num = Consts.Level_Two_Robot_1;
                robot_3Num = Consts.Level_Two_Robot_3;
                break;
            // 第三关
            case 3:
                beltNum = Consts.Level_Three_Belt;
                liftNum = Consts.Level_Three_Lift;
                robot_1Num = Consts.Level_Three_Robot_1;
                robot_3Num = Consts.Level_Three_Robot_3;
                break;
            default:
                break;
        }
    }
}
