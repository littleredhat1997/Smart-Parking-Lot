using UnityEngine;

public class Lift : MonoBehaviour
{
    [Header("电梯方向")]
    public Vector3 dir = Vector3.up;
    [Header("上升/下降速度")]
    public float liftSpeed = 2.0f;
    [Header("上升/下降高度")]
    public float movDistance = 3.0f;

    private Vector3 originPos;

    private Transform car;
    private CarMove carMove;

    void Start()
    {
        originPos = transform.position;

        car = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        carMove = car.GetComponent<CarMove>();
    }

    void OnTriggerStay(Collider other)
    {
        float distance = transform.position.y - originPos.y;
        if (Mathf.Abs(distance) >= movDistance) { return; }

        if (!carMove.isFirstStep)
        {
            if (other.tag == Tags.Player)
            {
                transform.Translate(dir * liftSpeed * Time.deltaTime);
                car.position = transform.position + new Vector3(0, 0.05f, 0);
            }
        }
    }

    /// <summary>
    /// 改变电梯方向
    /// </summary>
    public void ChangeDir()
    {
        if (dir == Vector3.up)
        {
            dir = Vector3.down;
            this.GetComponentInChildren<TextMesh>().text = "Down";
        }
        else if (dir == Vector3.down)
        {
            dir = Vector3.up;
            this.GetComponentInChildren<TextMesh>().text = "Up";
        }
    }
}
