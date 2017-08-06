using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("移动方向")]
    public Vector3 moveDir;
    [Header("移动速度")]
    public float moveSpeed;
    [Header("速度浮动")]
    public float offsetSpeed;

    void Update()
    {
        float random = Random.Range(-offsetSpeed, offsetSpeed);
        float speed = moveSpeed + random;
        this.transform.Translate(moveDir * speed * Time.deltaTime);
    }
}
