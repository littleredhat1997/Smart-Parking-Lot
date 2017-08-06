using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferBelt : MonoBehaviour
{
    public float transferSpeed=2f;

    private bool isEnter = false;
    private Transform car;
    private CarMove carMove;
    private Vector3 originPos;

    void Start()
    {
        carMove = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<CarMove>();
        car = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }

    void Update()
    {
        if (isEnter)
        {
            car.Translate(transform.forward * transferSpeed * Time.deltaTime);
        }
    }

//    void OnTriggerEnter(Collider other)
//    {
//        GetComponent<BoxCollider>().isTrigger = false;
//    }

    void OnTriggerStay(Collider other)
    {
        
        if (carMove.isFirstArrive)
        {
            isEnter = true;
            TransferBeltHandle(other);
        }
    }

    void OnTriggerExit()
    {
        isEnter = false;
    }


    void TransferBeltHandle(Collider other)
    {
        //4个方向都可以传送
        if (other.tag == Tags.backTrigger || other.tag == Tags.forwardTrigger || other.tag == Tags.leftTrigger ||
            other.tag == Tags.rightTrigger)
        {
            //if (!isEnter)
            //{
            //    originPos = car.position;
            //    isEnter = true;
            //}
            //Vector3 distance = originPos - car.position;
            //Debug.Log(distance);
            //if (Vector3.Distance(originPos,car.position) < 5f)
            //    car.Translate(transform.forward * transferSpeed * Time.deltaTime);
            isEnter = true;
            car = other.transform.parent;
        }

    }

}
