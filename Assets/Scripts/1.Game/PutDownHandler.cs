using System;
using UnityEngine;

public enum ItemType
{
    Belt,
    Lift,
    Robot_1,
    Robot_3
}

public class PutDownHandler : GameSingleton<PutDownHandler>
{
    public GameObject greenGridPrefab_1;
    public GameObject greenGridPrefab_3;
    public GameObject greenGridPrefab_5;

    [Header("传送带")]
    public GameObject belt;
    [Header("升降梯")]
    public GameObject lift;
    [Header("机器人一号")]
    public GameObject robot_1;
    [Header("机器人三号")]
    public GameObject robot_3;

    private bool isEnter = false;
    private GameObject grid = null;//生成的绿色格子
    private GameObject transferGo = null;//生成的道具
    private Vector3 mouseOriginPos;//鼠标初始pos

    // 放置类型
    private ItemType _type;

    private GameObject item;
    private int length;

    // 保存最后一个物体，方便回退
    private ItemType lastType;

    public void SetType(ItemType type)
    {
        _type = type;
        switch (type)
        {
            case ItemType.Belt:
                item = belt;
                length = 5;
                break;
            case ItemType.Lift:
                item = lift;
                length = 1;
                break;
            case ItemType.Robot_1:
                item = robot_1;
                length = 1;
                break;
            case ItemType.Robot_3:
                item = robot_3;
                length = 1;
                break;
            default:
                break;
        }
        Destroy(grid);
        isEnter = false;
    }

    void Update()
    {
        PutdownHandle(item, length);

        if (Input.GetKeyDown(KeyCode.Escape) && transferGo != null)
        {
            Destroy(transferGo);
            GameManager.Instance.RenewItem(lastType);
        }
    }

    /// <summary>
    /// 处理放置道具事件（每帧调用）
    /// </summary>
    /// <param name="itemPrefab">道具Prefab</param>
    /// <param name="len">道具长度</param>
    void PutdownHandle(GameObject itemPrefab, int len)
    {
        if(itemPrefab == null ) { return; }
       // Debug.Log(itemPrefab.name + "  " + len);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        if (Physics.Raycast(ray, out info))
        {
            if (info.collider.tag == Tags.grid)
            {
                if (!isEnter)//判断是否首次进入可放置区域
                {
                    GameObject greenGrid = null;
                    if (len == 1)
                    {
                        greenGrid = greenGridPrefab_1;
                    }
                    else if (len == 3)
                    {
                        greenGrid = greenGridPrefab_3;
                    }
                    else if (len == 5)
                    {
                        greenGrid = greenGridPrefab_5;
                    }
                    grid = Instantiate(greenGrid, info.collider.transform.position + new Vector3(0, 0.5f, 0),
                        Quaternion.identity);
                    isEnter = true;
                }
                else
                {
                    //在可放置区域内时grid跟随鼠标移动
                    if (grid != null)
                        grid.transform.position = info.collider.transform.position + new Vector3(0, 0.5f, 0);
                }
                if (Input.GetMouseButtonDown(0) && grid != null)
                {
                    mouseOriginPos = Input.mousePosition;
                    Vector3 spawnPos = grid.transform.position;
                    transferGo = Instantiate(itemPrefab, spawnPos, grid.transform.rotation);
                    //更改道具的layer,无视射线穿透
                    transferGo.layer = LayerMask.NameToLayer("Ignore Raycast");
                    Destroy(grid);//放下后销毁绿色格子
                    isEnter = false;

                    // 使用道具
                    switch(_type)
                    {
                        case ItemType.Belt:
                            if(--GameManager.Instance.beltNum == 0) { isEnter = true; }
                            GameManager.Instance.score += Consts.Score_Belt;
                            break;
                        case ItemType.Lift:
                            if (--GameManager.Instance.liftNum == 0) { isEnter = true; }
                            GameManager.Instance.score += Consts.Score_Lift;
                            break;
                        case ItemType.Robot_1:
                            if (--GameManager.Instance.robot_1Num == 0) { isEnter = true; }
                            GameManager.Instance.score += Consts.Score_Robot_1;
                            break;
                        case ItemType.Robot_3:
                            if (--GameManager.Instance.robot_3Num == 0) { isEnter = true; }
                            GameManager.Instance.score += Consts.Score_Robot_3;
                            break;
                        default:
                            break;
                    }
                    // 更新道具数量
                    GameManager.Instance.UpdateNum();
                    // 允许再次放置
                    // isEnter = false;
                    lastType = _type;
                }


                //计算鼠标偏移值，
                float offsetX = mouseOriginPos.x - Input.mousePosition.x;
                float offsetY = mouseOriginPos.y - Input.mousePosition.y;

                if (Mathf.Abs(offsetX) > 0.1f || Mathf.Abs(offsetY) > 0.1f)
                {
                    //恢复道具的layer
                    if (transferGo != null)
                        transferGo.layer = LayerMask.NameToLayer("Default");
                }


                if (Input.GetMouseButtonDown(1))
                {
                    //控制道具和绿色方格的旋转
                    if (transferGo != null && isEnter)
                    {
                        //计算鼠标偏移值，一定范围内（道具上空）才可以旋转
                        offsetX = mouseOriginPos.x - Input.mousePosition.x;
                        offsetY = mouseOriginPos.y - Input.mousePosition.y;

                        if (Mathf.Abs(offsetX) < 1f || Mathf.Abs(offsetY) < 1f)
                            transferGo.transform.Rotate(Vector3.up, 90);
                        else
                        {
                            //恢复道具的layer
                            if (transferGo != null)
                                transferGo.layer = LayerMask.NameToLayer("Default");
                        }
                    }
                    if(grid!=null)
                        grid.transform.Rotate(Vector3.up, 90);
                }
            }
            else
            {
                if (grid != null)
                {
                    Destroy(grid);
                    isEnter = false;
                    //恢复道具的layer
                    if (transferGo != null)
                        transferGo.layer = LayerMask.NameToLayer("Default");
                }
            }
        }
    }
}
