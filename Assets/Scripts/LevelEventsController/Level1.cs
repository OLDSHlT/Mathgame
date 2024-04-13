using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    //第一关的事件控制器
    public Thief thief;
    public PlayerMovementController player;
    public Transform thiefWayPoint1;
    public Transform thiefWayPoint2;
    public Flowchart flowchart;

    public int state = -1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCounter(5));
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 0)
        {
            Stage0();
        }
        else if(state == 1)
        {
            Stage1();
        }
        else if(state == 2)
        {
            Stage2();
        }
    }
    void Stage0()
    {
        if (thief.isOnWayPoint)
        {
            //go to stage 1
            state += 1;
            if(flowchart != null)
            {
                flowchart.ExecuteBlock("StartDialog");
            }
        }
    }
    void Stage1()
    {
        //等对话结束后前往下一个目标点
        if (flowchart.GetBooleanVariable("start_end"))
        {
            state += 1;
            thief.SetWayPoint(thiefWayPoint2);
            thief.isMoving = true;
        }
    }
    void Stage2()
    {
        if (thief.isOnWayPoint)
        {
            state += 1;
            //进入stage3
            //盗贼被禁用
            thief.gameObject.SetActive(false);
        }
    }
    private IEnumerator StartCounter(float time)
    {
        yield return new WaitForSeconds(time);
        state = 0;
        if(thief != null)
        {
            thief.SetWayPoint(thiefWayPoint1);
            thief.isMoving = true;
        }
    }
}
