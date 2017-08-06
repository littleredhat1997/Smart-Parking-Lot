using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LiftType
{
    Up,
    Down
}

public class Lift : MonoBehaviour
{
    [Header("电梯类型")]
    public LiftType type=LiftType.Up;
    [Header("上升/下降速度")]
    public float liftSpeed = 2f;
    [Header("上升/下降高度")] 
    public int height=3; 

    private bool isArrive = false;
    private Vector3 originPos;
    private CarMove carMove;
    private Transform player;

    void Start()
    {
        originPos = this.transform.position;
        carMove = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<CarMove>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }

    void OnTriggerEnter(Collider other)
    {
        // 播放音效
        AudioMgr.Instance.PlayEffect(Consts.Audio_Lift);
    }

    void OnTriggerStay(Collider other)
    {
        //if(carMove.isFirstArrive)
            LiftHandle(other);
    }

    private bool isEnter = false;
    void LiftHandle(Collider other)
    {
        if (other.tag == Tags.backTrigger || other.tag == Tags.forwardTrigger || other.tag == Tags.leftTrigger || other.tag == Tags.rightTrigger)
        //if (other.tag == Tags.forwardTrigger)
        {
            float distance = originPos.y - transform.position.y;

 /*           if (Mathf.Abs(distance) < 3f)
            {
                player.Translate(transform.up * liftSpeed * Time.deltaTime);
                transform.position = player.position - new Vector3(0, 0, 0.5f);
            }
 */           

            if (!isEnter)
            {
                player.position = transform.position +new Vector3(0,0.2f,0);
                isEnter = true;
            }
            

            if (Mathf.Abs(distance) < 3f)
            {
                if (type == LiftType.Up)
                {
                    player.Translate(Vector3.up * liftSpeed * Time.deltaTime);
                    transform.position = player.position;
                    //transform.Translate(Vector3.up * liftSpeed * Time.deltaTime);
                }
                else
                {
                    other.transform.Translate(-Vector3.up*liftSpeed*Time.deltaTime);
                    transform.Translate(-transform.up*liftSpeed*Time.deltaTime);
                }
            }
 
        }
    }
}
