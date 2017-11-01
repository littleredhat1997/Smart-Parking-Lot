using UnityEngine;

public class Robot : MonoBehaviour
{
    [Header("移动距离")]
    public int movDistance = 1;
    [Header("移动速度")]
    public int robotSpeed = 2;

    private Vector3 originPos;
    private GameObject flagGrid;

    private bool isEnter = false;
    private Transform car;
    private CarMove carMove;
    private bool isPlay = false;

    void Start()
    {
        originPos = transform.position;
        flagGrid = transform.parent.Find("flagGrid").gameObject;

        car = GameObject.FindGameObjectWithTag(Consts.Player).transform;
        carMove = car.GetComponent<CarMove>();
    }

    void Update()
    {
        if (isEnter)
        {
            if (transform.right == Vector3.right || transform.right == Vector3.left)
            {
                // 横向运动
                float distance = transform.position.x - originPos.x;
                if (Mathf.Abs(distance) < movDistance)
                {
                    car.Translate(transform.right * robotSpeed * Time.deltaTime);
                    transform.position = car.position + new Vector3(0, 0, -0.5f);
                }
                else
                {
                    Destroy(flagGrid);
                    Destroy(gameObject);
                    carMove.isMoving = false;
                }
            }
            else if (transform.right == Vector3.forward || transform.right == Vector3.back)
            {
                // 纵向运动
                float distance = transform.position.z - (originPos.z - 0.5f);
                if (Mathf.Abs(distance) < movDistance)
                {
                    car.Translate(transform.right * robotSpeed * Time.deltaTime);
                    transform.position = car.position + new Vector3(0, 0, -0.5f);
                }
                else
                {
                    Destroy(flagGrid);
                    Destroy(gameObject);
                    carMove.isMoving = false;
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!isPlay) { AudioMgr.Instance.PlayEffect(Consts.Audio_Robot); isPlay = true; }

        if (!carMove.isMoving && !carMove.isFirstStep)
        {
            if (other.tag == Consts.Player)
            {
                isEnter = true;
                carMove.isMoving = true;
                // 设置坐标为道具中心
                car.transform.position = new Vector3(this.transform.position.x,
                    car.transform.position.y, car.transform.position.z);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        isEnter = false;
        carMove.isMoving = false;
    }
}
