using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    Vector2 movement = new Vector2();
    Animator animator;
    Rigidbody2D rb2d;
    bool isRunning = false;

    public bool isOnWayPoint = false;//是否抵达目标路径点
    public bool isMoving = false;
    public Transform wayPoint;
    public float speed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        if(this.movement.x != 0)
        {
            this.isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        animator.SetBool("isRunning", isRunning);
    }
    void UpdateMovement()
    {
        if (isMoving)
        {
            if(wayPoint != null)
            {
                //路径点不为空
                if (this.transform.position.x < wayPoint.position.x)
                {
                    //向右
                    movement = new Vector2(1, 0);
                }
                else
                {
                    //向左
                    movement = new Vector2(-1, 0);
                }
                
                rb2d.velocity = new Vector2(movement.x * speed, rb2d.velocity.y);
                TurnCheck();
            }
            else
            {
                Stop();
            }
        }
        else
        {
            Stop();
        }
    }
    public void SetWayPoint(Transform transform)
    {
        this.wayPoint = transform;
        this.isOnWayPoint = false;
    }
    private void TurnCheck()
    {
        if (movement.x > 0 && transform.localScale.x < 0)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
        }
        else if (movement.x < 0 && transform.localScale.x > 0)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
        }
    }
    private void Stop()
    {
        movement = new Vector2();
        rb2d.velocity = new Vector2();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.transform == wayPoint)
        {
            this.wayPoint = null;
            isMoving = false;
            isOnWayPoint = true;
        }
    }
}
