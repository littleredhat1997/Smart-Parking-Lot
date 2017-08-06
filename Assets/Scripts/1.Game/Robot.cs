using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {

    [Header("机器人移动距离")]
    public int movDistance = 1;
    [Header("推拉速度")]
    public int pushPullSpeed = 1;

    private CarMove carMove;
    private Vector3 originPos;
    private GameObject flagGrid = null;//路径标识
    private Transform player;

    void Start()
    {
        originPos = transform.position;
        carMove = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<CarMove>();
        flagGrid = transform.Find("flagGrid").gameObject;
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }

    private bool isEnter = false;
    private bool needDestory = false;

    void OnTriggerStay(Collider other)
    {
        if (carMove.isFirstArrive)
        {
            RobotHandle(other);
        }
    }

    void Update()
    {
        if (carMove.isStart == true && flagGrid!=null) {
            flagGrid.layer = LayerMask.NameToLayer("Default");
        }
    }
/*
    void Update()
    {
        if(isEnter && carMove.isFirstArrive && !carMove.isArrive)
        {
            float distanceX = originPos.x - transform.position.x;
            //float distanceY = originPos.y - transform.position.y;
            float distanceZ = originPos.z - transform.position.z;
           // Debug.Log("distanceX" + distanceX);
            //Debug.Log("distanceZ" + distanceZ);
            if (Mathf.Abs(distanceX) < movDistance && Mathf.Abs(distanceZ) < 0.6f)
            {
                //车和robot一起运动
                player.Translate(-transform.right * pushPullSpeed * Time.deltaTime);

                transform.position = player.position + new Vector3(0, 0, -0.5f);
            }
            else if (Mathf.Abs(distanceZ) < movDistance && Mathf.Abs(distanceX) < 0.6f)
            {
                player.Translate(-transform.right * pushPullSpeed * Time.deltaTime);

                transform.position = player.position + new Vector3(0, 0, -0.5f);
            }
            else if (Mathf.Abs(distanceX) >= movDistance || Mathf.Abs(distanceZ) >= movDistance)
            {
                //needDestory = true;
                //Destroy(gameObject);
                //carMove.isArrive = true;
            }
        }
    }*/


    void OnTriggerEnter(Collider other)
    {
        //carMove.isArrive = false;
        //originPos = transform.position;
        if (flagGrid != null)
            flagGrid.transform.SetParent(null);
    }


    void OnTriggerExit(Collider other)
    {
        
        isEnter = false;
        if (flagGrid != null)
            Destroy(flagGrid);
        //if (needDestory)
            Destroy(gameObject);
    }

    void RobotHandle(Collider other)
    {
        if (other.tag == Tags.leftTrigger || other.tag == Tags.rightTrigger || other.tag == Tags.forwardTrigger || other.tag == Tags.backTrigger)
        {
            
            float distanceX = originPos.x - transform.position.x;
            float distanceZ = originPos.z - transform.position.z;
            //Debug.Log("distanceX" + distanceX);
           //Debug.Log("distanceZ"+distanceZ);
            if (Mathf.Abs(distanceX) < movDistance && Mathf.Abs(distanceZ) < 1f)
            {
                //车和robot一起运动
                player.Translate(-transform.right*pushPullSpeed*Time.deltaTime);
                transform.position = player.position+new Vector3(0,0,-0.5f);
            }else if(Mathf.Abs(distanceZ) < movDistance-0.5f && Mathf.Abs(distanceX) < 1f)
            {
                player.Translate(-transform.right * pushPullSpeed * Time.deltaTime);

                transform.position = player.position + new Vector3(0, 0, -0.5f);
            }
            //else if(Mathf.Abs(distanceX) >= movDistance || Mathf.Abs(distanceZ) >= movDistance+0.5f)
           // else
           // {
           //     needDestory = true;
           //     Destroy(gameObject);
           // }
 
        }
    }
    //void RobotHandle(Collider other)
    //{
    //    if (other.tag == Tags.forwardTrigger)//拉一格
    //    {
    //        movDistance = 1;
    //        isEnter = true;
    //    }
    //    else if (other.tag == Tags.backTrigger)//推三格
    //    {
    //        movDistance = 3;
    //        isEnter = true;
    //    }
    //    if (isEnter)
    //    {
    //        float distance = originPos.z - transform.position.z;
    //        if (Mathf.Abs(distance) < movDistance)
    //        {
    //            车和robot一起运动
    //            other.transform.parent.Translate(other.transform.forward * pushPullSpeed * Time.deltaTime);
    //            transform.Translate(other.transform.forward * pushPullSpeed * Time.deltaTime);
    //        }

    //    }
    //}
}
