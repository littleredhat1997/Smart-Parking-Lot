using UnityEngine;
using UnityEngine.EventSystems;

public enum ItemType
{
    NULL,
    Belt,
    Lift,
    Robot_1,
    Robot_3
}

public class ItemHandler : GameSingleton<ItemHandler>
{
    [Header("一格预设")]
    public GameObject greenGridPrefab_1;
    [Header("三格预设")]
    public GameObject greenGridPrefab_3;
    [Header("五格预设")]
    public GameObject greenGridPrefab_5;

    [Header("传送带")]
    public GameObject belt;
    [Header("升降梯")]
    public GameObject lift;
    [Header("机器人一号")]
    public GameObject robot_1;
    [Header("机器人三号")]
    public GameObject robot_3;

    // 格子
    private GameObject gridGo = null;
    // 道具
    private GameObject itemGo = null;

    // 道具类型
    private ItemType itemType = ItemType.NULL;
    // 道具预设
    private GameObject itemPrefab = null;
    // 格子预设
    private GameObject gridPrefab = null;

    /// <summary>
    /// 最后一个道具
    /// </summary>
    private ItemType lastType = ItemType.NULL;

    /// <summary>
    /// 最后一个位置
    /// </summary>
    private Vector3 lastPos = new Vector3(100, 100, 100);

    /// <summary>
    /// 当前鼠标位置
    /// </summary>
    private Vector3 mouseOriginPos = Vector3.zero;

    /// <summary>
    /// 是否首次放置
    /// </summary>
    private bool isCanEnter = false;

    void Update()
    {
        // 游戏运行中
        if (GameManager.Instance.gameState == GameState.Run)
        {
            if (gridGo != null) { Destroy(gridGo); }
            return;
        }

        // ESC取消放置
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (itemGo != null)
            {
                Destroy(itemGo);
                GameManager.Instance.RenewItem(lastType);
            }
            lastType = ItemType.NULL;
            lastPos = new Vector3(100, 100, 100);
            mouseOriginPos = Vector3.zero; ;
        }

        // 游戏进行中且鼠标不在UI控件上
        if (GameManager.Instance.gameState == GameState.Start && !EventSystem.current.IsPointerOverGameObject())
        {
            // 右键调整道具
            RigthClickHandle();

            if (!GameManager.Instance.CheckItem(itemType)) { return; }

            // 左键放置道具
            LeftClickHandle();
        }
    }

    /// <summary>
    /// 左键放置道具（每帧调用）
    /// </summary>
    void LeftClickHandle()
    {
        // 鼠标发出射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        if (Physics.Raycast(ray, out info))
        {
            if (info.collider.tag == Tags.Grid)
            {
                // 是否首次放置
                if (isCanEnter)
                {
                    // 实例格子实体
                    gridGo = Instantiate(gridPrefab, info.collider.transform.position + new Vector3(0, 0.5f, 0),
                        Quaternion.identity);
                    isCanEnter = false;
                }
                else
                {
                    // 跟随鼠标移动
                    if (gridGo != null) { gridGo.transform.position = info.collider.transform.position + new Vector3(0, 0.5f, 0); }
                }

                // 按下左键
                if (Input.GetMouseButtonDown(0))
                {
                    // 尚无道具放置
                    if (itemType == ItemType.NULL) { return; }

                    // 防止重复放置
                    if (info.collider.transform.position == lastPos) { return; }

                    // 生成道具
                    itemGo = Instantiate(itemPrefab, gridGo.transform.position, gridGo.transform.rotation);
                    // 无视射线
                    itemGo.layer = LayerMask.NameToLayer("Ignore Raycast");
                    // 销毁格子
                    Destroy(gridGo);

                    // 最后一个道具
                    lastType = itemType;
                    // 最后一个位置
                    lastPos = info.collider.transform.position;
                    // 当前鼠标位置
                    mouseOriginPos = Input.mousePosition;

                    // 使用道具
                    isCanEnter = GameManager.Instance.UseItem(itemType);
                    // 消耗完毕
                    if (!isCanEnter) { itemType = ItemType.NULL; }
                }
            }
            else
            {
                // 移除格子实体
                if (gridGo != null)
                {
                    Destroy(gridGo);
                    isCanEnter = true;
                }
                // 恢复道具层级
                if (itemGo != null)
                {
                    itemGo.layer = LayerMask.NameToLayer("Default");
                }
            }
        }
    }

    /// <summary>
    /// 右键调整道具（每帧调用）
    /// </summary>
    void RigthClickHandle()
    {
        // 按下右键
        if (Input.GetMouseButtonDown(1))
        {
            // 控制格子
            if (gridGo != null)
            {
                // 控制电梯方向
                if (itemType == ItemType.Lift) {; }
                // 控制其它旋转
                else { gridGo.transform.Rotate(Vector3.up, 90); }
            }
            // 控制道具
            if (itemGo != null)
            {
                float offsetX = mouseOriginPos.x - Input.mousePosition.x;
                float offsetY = mouseOriginPos.y - Input.mousePosition.y;
                // 一定范围旋转
                if (Mathf.Abs(offsetX) < 1.0f || Mathf.Abs(offsetY) < 1.0f)
                {
                    // 控制电梯方向
                    if (lastType == ItemType.Lift) { itemGo.GetComponent<Lift>().ChangeDir(); }
                    // 控制其它旋转
                    else { itemGo.transform.Rotate(Vector3.up, 90); }
                }
                // 恢复道具层级
                else
                {
                    itemGo.layer = LayerMask.NameToLayer("Default");
                }
            }
        }
    }

    public void SetType(ItemType type)
    {
        itemType = type;
        switch (type)
        {
            case ItemType.Belt:
                itemPrefab = belt;
                gridPrefab = greenGridPrefab_5;
                break;
            case ItemType.Lift:
                itemPrefab = lift;
                gridPrefab = greenGridPrefab_1;
                break;
            case ItemType.Robot_1:
                itemPrefab = robot_1;
                gridPrefab = greenGridPrefab_1;
                break;
            case ItemType.Robot_3:
                itemPrefab = robot_3;
                gridPrefab = greenGridPrefab_3;
                break;
            case ItemType.NULL:
                itemPrefab = null;
                gridPrefab = null;
                break;
            default:
                break;
        }
        Destroy(gridGo);
        isCanEnter = true;
    }
}
