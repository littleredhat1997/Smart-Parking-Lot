using UnityEngine;

public class Belt : MonoBehaviour
{
    [Header("移动速度")]
    public float beltSpeed = 2.0f;

    private bool isEnter = false;
    private Transform car;
    private CarMove carMove;

    void Start()
    {
        car = GameObject.FindGameObjectWithTag(Tags.Player).transform;
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
        if (!carMove.isMoving && !carMove.isFirstStep)
        {
            if (other.tag == Tags.Player)
            {
                isEnter = true;
                carMove.isMoving = true;
            }
        }
    }

    void OnTriggerExit()
    {
        isEnter = false;
        carMove.isMoving = false;
    }
}
