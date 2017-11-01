using UnityEngine;

public class CarMove : MonoBehaviour
{
    [Header("车子速度")]
    public float carSpeed = 1;

    public bool isFirstStep = true;

    private Vector3 startPos;
    private float timer = 0;

    /// <summary>
    /// 内部无视 外界调用
    /// </summary>
    public bool isMoving = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // 游戏状态
        if (GameManager.Instance.gameState != GameState.Run) { return; }

        if (isFirstStep)
        {
            Move();
        }
        else
        {
            Check();
        }
    }

    void Move()
    {
        // 小车移动
        float distance = transform.position.z - startPos.z;
        if (Mathf.Abs(distance) < 1f)
        {
            transform.Translate(transform.forward * Time.deltaTime * carSpeed);
        }
        else
        {
            // 生成移动特效
            GameObject carEffect = Instantiate(Resources.Load(Consts.Effect_Car) as GameObject);
            carEffect.transform.SetParent(this.transform);
            carEffect.transform.localPosition = Vector3.zero + new Vector3(0, 50, 0);

            isFirstStep = false;
        }
    }

    void Check()
    {
        // 射线检测
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo))
        {
            // 格子
            if (hitinfo.collider.tag == Consts.Grid)
            {
                timer += Time.deltaTime;
                if (timer > 3.0f)
                {
                    Debug.Log("GameOver");
                    GameManager.Instance.gameState = GameState.GameOver;
                }
            }
            else
            {
                timer = 0;
            }

            // 终点
            if (hitinfo.collider.tag == Consts.Des)
            {
                Debug.Log("GameWin");
                GameManager.Instance.gameState = GameState.GameWin;
            }
        }
        else
        {
            Debug.Log("GameOver");
            GameManager.Instance.gameState = GameState.GameOver;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        timer = 0;

        // 障碍
        if (other.tag == Consts.Car)
        {
            Debug.Log("GameOver");
            GameManager.Instance.gameState = GameState.GameOver;
        }
    }

    void OnTriggerExit(Collider other)
    {
        timer = 0;
    }
}
