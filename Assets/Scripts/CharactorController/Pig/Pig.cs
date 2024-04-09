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
    Damageable damageable;
    Animator animator;
    private AnimatorStateInfo state; //动画状态机状态

    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        this.currentTarget = wayPoint1;
        this.boxCollider2D = GetComponent<BoxCollider2D>();
        this.rb2d = GetComponent<Rigidbody2D>();
        this.damageable = GetComponent<Damageable>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(damageable.isAlive && !damageable.isUnderAttackCooldown)
        {
            Move();
        }
        if (!damageable.isAlive)
        {
            rb2d.velocity = new Vector2(0, 0);
            animator.SetBool("isDie", true);
            Die();
        }
        
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
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Damageable d = collision.GetComponent<Damageable>();//获取damageable组件
            if (collision.transform.position.x - this.transform.position.x < 0)
            {
                d.Hit(damage, new Vector2(-5, 2));
            }
            else
            {
                d.Hit(damage, new Vector2(5, 2));
            }
        }
    }
    private void Die()
    {
        //似了
        state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("die") && state.normalizedTime >= 1.0f)
        {
            //动画播放完成
            Destroy(gameObject);
        }
    }
}
