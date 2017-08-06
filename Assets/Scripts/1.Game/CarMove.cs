using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public float carSpeed = 1;
    public bool isGround = false;
    public bool isStart = false;
    public bool isFirstArrive = false;
    public bool isArrive = true;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // 玩家准备完毕，可以开始移动
    public void StartGame()
    {
        isStart = true;
    }

    void Move()
    {
        //小车向前走一格
        float distance = transform.position.z - startPos.z;
        if (Mathf.Abs(distance) < 1f)
            transform.Translate(transform.forward*Time.deltaTime*carSpeed);
        else
        {
            isFirstArrive = true;
        }
    }

    void Update()
    {
        if (isStart)//玩家放置好物体后点击开始键后置isStart为true
        {
            if (!isFirstArrive)
                Move();
            else
            {
                checkIsGround();//射线检测是否落地
            }
        }
    }

    private float timer=0;
    void checkIsGround()
    {
        // 游戏进行中
        if(GameManager.Instance.gameState !=GameState.Start) { return; }

        //射线检测是否落地
        Ray ray =new Ray(transform.position,Vector3.down);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo))
        {
            Debug.Log(hitinfo.collider.tag);
            if (hitinfo.collider.tag == Tags.grid)
            {
                timer += Time.deltaTime;
                isGround = true;
                if (timer > 3.0f)
                {
                    Debug.Log("game over");
                    timer = 0;

                    GameManager.Instance.gameState = GameState.GameOver;
                } 
            }
            else
            {
                isGround = false;
            }
            if (hitinfo.collider.tag == Tags.des)
            {
                //到达终点
                Debug.Log("win");
                GameManager.Instance.gameState = GameState.GameWin;
            }
        }
        else
        {
            //从空中掉落
            Debug.Log("game over");
           GameManager.Instance.gameState = GameState.GameOver;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        timer = 0;
        if (other.tag == Tags.car)
        {
            //撞车
            Debug.Log("game over");
            GameManager.Instance.gameState = GameState.GameOver;
        }
    }

    void OnTriggerExit(Collider other)
    {
        timer = 0;
    }

}
