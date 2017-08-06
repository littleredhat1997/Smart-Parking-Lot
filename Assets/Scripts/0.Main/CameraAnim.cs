using System.Collections.Generic;
using UnityEngine;

public class CameraAnim : MonoBehaviour
{
    [Header("环境数组")]
    public GameObject[] envs;

    [Header("移动速度")]
    public float moveSpeed;
    [Header("移动长度")]
    public float length;

    public bool isOk = false;

    // 环境队列
    private Queue<GameObject> envQueue = new Queue<GameObject>();
    // 当前位置下标
    private int index = 0;

    void Start()
    {
        // 初始化队列
        envQueue.Enqueue(envs[0]);
        envQueue.Enqueue(envs[1]);
        envQueue.Enqueue(envs[2]);
    }

    void Update()
    {
        if(isOk) { return; }

        this.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);

        // 生成新物体
        if (this.transform.position.x >= (index + 1) * length)
        {
            // 队首出队
            GameObject first = envQueue.Dequeue();

            // 更新位置
            first.transform.position = new Vector3(length * (index + 3), 0, 0);

            // 重新入队
            envQueue.Enqueue(first);

            // 更新下标
            index++;
        }
    }
}
