using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetactionArea : MonoBehaviour
{
    private Collider2D detactZone;
    public Transform target;
    public bool isTargetEnter = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 检查进入触发器的碰撞体是否是目标的游戏对象
        if (collider.transform.gameObject == target.gameObject)
        {
            isTargetEnter = true;
            //Debug.Log("The target entered the trigger!");
            // 在这里执行目标进入触发器的逻辑
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        // 检查进入触发器的碰撞体是否是目标的游戏对象
        if (collider.transform.gameObject == target.gameObject)
        {
            isTargetEnter = false;
            // 在这里执行目标进入触发器的逻辑
        }
    }
}
