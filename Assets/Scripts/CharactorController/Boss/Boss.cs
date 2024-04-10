using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed = 2f;
    public DetactionArea warningZone; //设定的巡逻范围
    public float minDistance = 1f;
    public float attackCD = 1.5f;

    private AnimatorStateInfo state; //动画状态机状态
    private bool isAttackCDing = false;
    private bool isAttacking = false;
    public bool isAttackKeyFrame = false;
    public bool isTreading = false;

    public AttackModes attackMode = AttackModes.ShortRange;
    private Rigidbody2D rb2d;
    private Vector2 movement;
    private bool isPlayerInTrigger = false;
    private Animator animator;
    private Damageable damageable;
    public GameObject shortRangeAttackArea;
    public GameObject longRangeAttackArea;
    public enum AttackModes
    {
        LongRange = 0,
        ShortRange = 1
    }
    // Start is called before the first frame update
    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.damageable = GetComponent<Damageable>();
        DecideAttackMode();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.damageable.isAlive && !damageable.isUnderAttackCooldown)
        {
            Move();
        }
        UpdateStatus();
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
    private void Move()
    {

        if (warningZone != null)
        {
            float distance = 0f;
            if (warningZone.target != null)
            {
                distance = Vector2.Distance(this.transform.position, warningZone.target.transform.position);
            }
            //有巡逻范围
            if (warningZone.isTargetEnter)
            {
                if (attackMode == AttackModes.ShortRange)
                {
                    shortRangeAttackArea.SetActive(true);
                    longRangeAttackArea.SetActive(false);
                    //近战模式
                    //如果玩家不在攻击范围内
                    //走向玩家
                    if (!isPlayerInTrigger && !isAttacking || isAttackCDing && distance >= minDistance)
                    {
                        if (this.transform.position.x < warningZone.target.transform.position.x)
                        {
                            movement = new Vector2(1, 0);
                        }
                        else
                        {
                            movement = new Vector2(-1, 0);
                        }
                    }
                    else
                    {
                        AttackShortRange();
                        movement = new Vector2();
                    }
                }
                else if(attackMode == AttackModes.LongRange)
                {
                    longRangeAttackArea.SetActive(true);
                    shortRangeAttackArea.SetActive(false);
                    //脚踏
                    this.isTreading = true;
                    
                }

            }
            else
            {
                movement = new Vector2();
            }
        }

        rb2d.velocity = new Vector2(movement.x * speed, rb2d.velocity.y);
        TurnCheck();
    }
    //使用触发器判断玩家是否进入了近战攻击范围
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.isPlayerInTrigger = true;
        }
    }
    //判断玩家是否离开了近战攻击范围
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.isPlayerInTrigger = false;
        }
    }
    private void UpdateStatus()
    {
        if (movement.x != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        animator.SetBool("isAttacking", this.isAttacking);
        animator.SetBool("isTreading", this.isTreading);
        if (!damageable.isAlive)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("isWalking", false);
            Die();
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

    private void AttackShortRange()
    {
        if (!isAttackCDing)
        {
            //没有CD
            if (!isAttacking)
            {
                //没有已经开始的攻击
                this.isAttacking = true;
            }
            else
            {
                //有已经开始的攻击，判断结束
                state = animator.GetCurrentAnimatorStateInfo(0);
                if (state.IsName("attack") && state.normalizedTime >= 1.0f)
                {
                    //动画播放完毕
                    this.isAttacking = false;
                    //开始使用协程计算CD
                    this.isAttackCDing = true;
                    StartCoroutine(AttackCDControl());
                }
                if (isAttackKeyFrame)
                {
                    //造成伤害
                    if (this.isPlayerInTrigger)
                    {
                        Damageable d = warningZone.target.GetComponent<Damageable>();//获取damageable组件
                        if (warningZone.target.transform.position.x - this.transform.position.x < 0)
                        {
                            d.Hit(20, new Vector2(-5, 2));
                        }
                        else
                        {
                            d.Hit(20, new Vector2(5, 2));
                        }
                    }
                }
            }
        }
    }
    private void DecideAttackMode()
    {
        int randint = Random.Range(0, 100);
        if(randint >= 80)
        {
            this.attackMode = AttackModes.LongRange;
            if (isAttacking)
            {
                isAttacking = false;//防止卡在近战攻击
            }
        }
        else
        {
            this.attackMode = AttackModes.ShortRange;
            if (isTreading)
            {
                isTreading = false;
            }
        }
        StartCoroutine(AttackModeCD());
    }
    //计算攻击CD的协程
    private IEnumerator AttackCDControl()
    {
        if (this.isAttackCDing)
        {
            yield return new WaitForSeconds(this.attackCD);
            this.isAttackCDing = false;
        }
    }
    //计算切换攻击模式cd的协程
    private IEnumerator AttackModeCD()
    {
        yield return new WaitForSeconds(5);
        DecideAttackMode();
    }
    
}
