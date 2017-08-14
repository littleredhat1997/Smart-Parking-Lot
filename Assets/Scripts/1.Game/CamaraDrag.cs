using UnityEngine;

public class CamaraDrag : MonoBehaviour
{
    [Header("拉近最小值")]
    public float minDistance = 5.0f;
    [Header("拉远最大值")]
    public float maxDistance = 20.0f;
    [Header("X最大范围")]
    public float maxX = 10.0f;
    [Header("Z最大范围")]
    public float maxZ = 10.0f;
    [Header("放缩速度")]
    public float scrollSpeed = 1;
    [Header("移动速度")]
    public float moveSpeed = 1;
    [Header("旋转速度")]
    public float rotateSpeed = 5;

    private Transform player;
    private Vector3 offPos;
    private bool isFollow = false;

    void Update()
    {
        ScrollView();
        RotateView();

        if (isFollow)
        {
            this.transform.position = player.position + offPos;
        }
    }

    void ScrollView()
    {
        float distance = this.transform.position.magnitude;
        distance += Input.GetAxis("Mouse ScrollWheel") * -scrollSpeed;

        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // 改变镜头大小
        transform.position = this.transform.position.normalized * distance;
    }

    void RotateView()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h) >= 0.05f || Mathf.Abs(v) >= 0.05f)
        {
            float posX = this.transform.position.x + moveSpeed * h;
            float posY = this.transform.position.y;
            float posZ = this.transform.position.z + moveSpeed * v;

            posX = Mathf.Clamp(posX, -maxX, maxX);
            posZ = Mathf.Clamp(posZ, -maxZ, maxZ);

            // 改变镜头位置
            this.transform.position = new Vector3(posX, posY, posZ);
        }

        if (Input.GetMouseButton(1))
        {
            // 左右
            transform.RotateAround(this.transform.position, Vector3.up, rotateSpeed * Input.GetAxis("Mouse X"));

            // 上下
            transform.RotateAround(this.transform.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
        }
    }

    // 动画结束
    public void AnimEnd()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        offPos = this.transform.position - player.transform.position;

        // 游戏开始
        GameManager.Instance.gameState = GameState.Start;
    }

    // 镜头跟随
    public void FollowPlay()
    {
        isFollow = true;

        // 准备完毕
        GameManager.Instance.gameState = GameState.Run;
    }
}