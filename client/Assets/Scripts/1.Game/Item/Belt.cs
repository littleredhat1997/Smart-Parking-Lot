using UnityEngine;

public class Belt : MonoBehaviour
{
    [Header("移动速度")]
    public float beltSpeed = 2.0f;

    private bool isEnter = false;
    private Transform car;
    private CarMove carMove;
    private bool isPlay = false;

    void Start()
    {
        car = GameObject.FindGameObjectWithTag(Consts.Player).transform;
        carMove = car.GetComponent<CarMove>();
    }

    void Update()
    {
        if (isEnter)
        {
            car.Translate(transform.forward * beltSpeed * Time.deltaTime);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!isPlay) { AudioMgr.Instance.PlayEffect(Consts.Audio_Belt); isPlay = true; }

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

    void OnTriggerExit()
    {
        isEnter = false;
        carMove.isMoving = false;
    }
}
