using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("移动方向")]
    public Vector3 moveDir;
    [Header("移动速度")]
    public float moveSpeed;

    void Update()
    {
        this.transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }
}
