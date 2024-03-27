using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public Transform wayPoint1;
    public Transform wayPoint2;
    public float speed = 4;
    public Transform currentTarget;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb2d;
    private Vector2 movement = new Vector2();

    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        this.currentTarget = wayPoint1;
        this.boxCollider2D = GetComponent<BoxCollider2D>();
        this.rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    /*
     * 判断转身
     */
    private void TurnCheck()
    {
        if (movement.x < 0 && transform.localScale.x < 0)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
        }
        else if (movement.x > 0 && transform.localScale.x > 0)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
        }
    }
    //检测是否抵达路径点
    public void CheckWayPoints()
    {
        if (boxCollider2D.OverlapPoint(currentTarget.position))
        {
            //Debug.Log("active");
            if(currentTarget == wayPoint1)
            {
                currentTarget = wayPoint2;
            }
            else
            {
                currentTarget = wayPoint1;
            }
        }
    }
    public void Move()
    {
        CheckWayPoints();
        if(this.transform.position.x < currentTarget.position.x)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //攻击玩家的代码
            Debug.Log("attack player");
        }
    }
}
