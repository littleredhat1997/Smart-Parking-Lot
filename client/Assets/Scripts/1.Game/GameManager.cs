using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Ready,
    Start,
    Run, // 游戏运行
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
    [Header("游戏胜利界面")]
    public GameObject gameWinPanel;
    [Header("游戏结束界面")]
    public GameObject gameOverPanel;

    private Transform _carTrans;
    public Transform carTrans
    {
        get
        {
            if (_carTrans == null)
            {
                _carTrans = GameObject.FindGameObjectWithTag(Consts.Player).transform;
            }
            return _carTrans;
        }
    }

    private int _score;
    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            // 更新UI
            scoreText.text = score.ToString();
        }
    }

    private int _beltNum;
    public int beltNum
    {
        get { return _beltNum; }
        set
        {
            _beltNum = value;
            // 更新UI
            uiMgr.GetComponent<ItemUI>().UpdateNum();
        }
    }

    private int _liftNum;
    public int liftNum
    {
        get { return _liftNum; }
        set
        {
            _liftNum = value;
            // 更新UI
            uiMgr.GetComponent<ItemUI>().UpdateNum();
        }
    }

    private int _robot1Num;
    public int robot1Num
    {
        get { return _robot1Num; }
        set
        {
            _robot1Num = value;
            // 更新UI
            uiMgr.GetComponent<ItemUI>().UpdateNum();
        }
    }

    private int _robot3Num;
    public int robot3Num
    {
        get { return _robot3Num; }
        set
        {
            _robot3Num = value;
            // 更新UI
            uiMgr.GetComponent<ItemUI>().UpdateNum();
        }
    }

    void Start()
    {
        int level = Globals.Instance.level;
        beltNum = Consts.Level[level, 0];
        liftNum = Consts.Level[level, 1];
        robot1Num = Consts.Level[level, 2];
        robot3Num = Consts.Level[level, 3];
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.Ready:

                // NONE
                Debug.Log("GameState.Ready");

                break;
            case GameState.Start:
                Debug.Log("GameState.Start");

                scorePanel.SetActive(true);
                itemPanel.SetActive(true);
                setting.gameObject.SetActive(true);
                going.gameObject.SetActive(true);
                break;
            case GameState.Run:

                // NONE
                Debug.Log("GameState.Run");

                break;
            case GameState.GameWin:
                Debug.Log("GameState.GameWin");
                gameState = GameState.End;

                gameWinPanel.SetActive(true);

                // 生成胜利特效
                GameObject winEffect = Instantiate(Resources.Load(Consts.Effect_Win) as GameObject);
                winEffect.transform.position = carTrans.position;

                // 播放胜利音效
                AudioMgr.Instance.PlayEffect(Consts.Audio_Win);

                // 更新UI
                uiMgr.GetComponent<CompleteUI>().UpdateScore();

                // 上传分数
                ServerMgr.Instance.Upload(Globals.Instance.username, Globals.Instance.level, score);

                // 保存关卡
                int maxLevel = Mathf.Max(PlayerPrefs.GetInt(Consts.Max_Level), Globals.Instance.level);
                PlayerPrefs.SetInt(Consts.Max_Level, maxLevel);

                break;
            case GameState.GameOver:
                Debug.Log("GameState.GameOver");
                gameState = GameState.End;

                gameOverPanel.SetActive(true);

                // 生成失败特效
                GameObject overEffect = Instantiate(Resources.Load(Consts.Effect_Over) as GameObject);
                overEffect.transform.position = carTrans.position + new Vector3(0, 5, 0);

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
    /// 检查道具
    /// </summary>
    /// <param name="type"></param>
    public bool CheckItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.NULL:
                return false;
            case ItemType.Belt:
                return beltNum != 0 ? true : false;
            case ItemType.Lift:
                return liftNum != 0 ? true : false;
            case ItemType.Robot_1:
                return robot1Num != 0 ? true : false;
            case ItemType.Robot_3:
                return robot3Num != 0 ? true : false;
            default:
                return false;
        }
    }

    /// <summary>
    /// 使用道具
    /// </summary>
    /// <param name="type"></param>
    public bool UseItem(ItemType type)
    {
        score += Consts.Score[(int)type];
        switch (type)
        {
            case ItemType.Belt:
                return --beltNum != 0 ? true : false;
            case ItemType.Lift:
                return --liftNum != 0 ? true : false;
            case ItemType.Robot_1:
                return --robot1Num != 0 ? true : false;
            case ItemType.Robot_3:
                return --robot3Num != 0 ? true : false;
            default:
                return false;
        }
    }

    /// <summary>
    /// 撤回道具
    /// </summary>
    /// <param name="type"></param>
    public void RenewItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.Belt:
                beltNum++;
                break;
            case ItemType.Lift:
                liftNum++;
                break;
            case ItemType.Robot_1:
                robot1Num++;
                break;
            case ItemType.Robot_3:
                robot3Num++;
                break;
            default:
                break;
        }
        score -= Consts.Score[(int)type];
    }
}
