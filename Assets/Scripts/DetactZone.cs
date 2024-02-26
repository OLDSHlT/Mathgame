using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetactionZone : MonoBehaviour
{
    public Transform selftransform;
    public UnityEvent noCollidersRemain;
    public UnityEvent EnterEvent;
    public UnityEvent InStayEvent;
    public UnityEvent StayEvent;
    public List<Collider2D> collider2Ds = new List<Collider2D>();
    public bool InTriggerStay = false;
    public bool isTriggerEnter = false;
    public bool isTriggerExit = false;
    public bool forStay = false;
    Collider2D col;
    // Start is called before the first frame update
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collider2Ds.Add(collision);
        if (isTriggerEnter)
        {
            EnterEvent?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collider2Ds.Remove(collision);
        if (collider2Ds.Count <= 0&& isTriggerExit)
        {
            noCollidersRemain?.Invoke();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)//
    {

        if (InTriggerStay)//主要用于转向
        {
            Vector2 DirectionVector = (collision.transform.position - transform.position).normalized;//取得目标到本体的向量
            //Debug.Log(DirectionVector);
            if (DirectionVector.x < 0 && selftransform.localScale.x > 0)//主体背对目标时才能触发
            {
                InStayEvent?.Invoke();//基本上都是用转向函数
            }
            else if (DirectionVector.x > 0 && selftransform.localScale.x < 0)//下同
            {
                InStayEvent?.Invoke();
            }
        }
        if (forStay)//用于转向以外，在目标待在区域内时每一帧不断调用的事件，可能暂时用不到
        {
            StayEvent?.Invoke();
        }
    }
}
