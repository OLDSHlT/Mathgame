
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 8.0f;
    public float sprintCD = 1.5f;
    public float sprintDuration = 0.1f;
    public float attackCD = 0.5f;
    public float recoverDuration = 1f;
    public float recoverCD = 0.5f;
    public float jumpForce = 10f;
    Vector2 movement = new Vector2();
    private TouchingDetactor touchingDetactor;

    Animator animator;
    Rigidbody2D rb2d;
    AnimatorStateInfo state;
    //跳跃时间计时器
    private float jumpTime = 0f; //按住空格起跳的时间
    private float maxJumpTime = 0.2f; //最大的跳跃时间
    private bool isJumpAllow = true;
    //各种状态的触发器
    private bool isAttackCD = false;
    private bool isSprinting = false;
    private bool isSprintCD = false;
    private bool isRunning = false;
    private bool isJumping = false;
    private bool isStickOnWall = false;
    private bool isRecovering = false;
    private bool isRecoverCD = false;
    private bool isAttacking = false;
    public bool isFalling = false;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.rb2d = GetComponent<Rigidbody2D>();
        this.touchingDetactor = GetComponent<TouchingDetactor>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        UpdateJumpState();
        MovePlayer();
    }
    private void FixedUpdate()
    {
        //MovePlayer();
    }
    private void MovePlayer()
    {
        //冲刺
        if (Input.GetKey(KeyCode.LeftShift) && !isSprintCD)
        {
            this.isSprinting = true;
            //启动奔跑CD的协程
            StartCoroutine(SprintCounter());
        }
        //真正的速度
        //当冲刺时速度是原速度的五倍
        float actualSpeed = this.movementSpeed;
        if (isSprinting)
        {
            actualSpeed = actualSpeed * 5;
        }
        else
        {
            actualSpeed = this.movementSpeed;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        //转换为单位向量
        //movement.Normalize();
        movement.y = rb2d.velocity.y;
        //jump
        if (Input.GetKey(KeyCode.Space) && isJumpAllow)
        {
            movement.y = jumpForce;
        }else if (Input.GetKeyUp(KeyCode.Space) && jumpTime < maxJumpTime)
        {
            //当在跳跃时间小于最大跳跃时间时松开了跳跃键
            movement.y = 0;
        }
        //蹬墙跳
        if (Input.GetKeyDown(KeyCode.Space) && !touchingDetactor.isGrounded && touchingDetactor.isWall)
        {
            //当角色位于墙壁
            //给角色一个反方向斜上方的力
            if(gameObject.transform.localScale.x > 0)
            {
                //面向右
                rb2d.AddForce(new Vector2(0, 3));
                rb2d.velocity = new Vector2(-5, rb2d.velocity.y);
                movement.x = -5;
            }
            else
            {
                rb2d.AddForce(new Vector2(0, 3));
                rb2d.velocity = new Vector2(5, rb2d.velocity.y);
                movement.x = 5;
            }
        }
        //overturn the spirit
        TurnCheck();
        
        
        rb2d.velocity = new Vector2(movement.x * actualSpeed, movement.y);
        
        
    }
    private void UpdateJumpState()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //松开空格时锁定跳跃 防止二段跳
            this.isJumpAllow = false;
        }else if (Input.GetKey(KeyCode.Space))
        {
            //按住空格时
            if (touchingDetactor.isGrounded)
            {
                //在地面时
                this.isJumpAllow = true;
            }
            else if (!touchingDetactor.isGrounded && jumpTime < maxJumpTime)
            {
                //没在地面，但是没达到最大跳跃时间
                jumpTime = jumpTime + Time.deltaTime; //累加时间
                //this.isJumpAllow = true;
            }
            else
            {
                //没在地面且超过最大跳跃时间
                this.isJumpAllow = false;
            }
            
        }
        else if(touchingDetactor.isGrounded)
        {
            //在地面时
            this.jumpTime = 0f;
            isJumpAllow = true;
        }
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
    private void UpdateState()
    {
        if(movement.x != 0 && touchingDetactor.isGrounded)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        if (!touchingDetactor.isGrounded)
        {
            //还得判断是否在下落（待修改）
            this.isJumping = true;
        }
        else
        {
            this.isJumping = false;
        }
        if (rb2d.velocity.y <= 0 && !touchingDetactor.isGrounded)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
        if (Input.GetKey(KeyCode.Q) && !this.isRecoverCD && !this.isRunning && !this.isRecovering)
        {
            this.isRecovering = true;
            StartCoroutine(RecoverCounter());
        }
        
        animator.SetBool("isSprinting", isSprinting);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isStickOnWall", isStickOnWall);
        animator.SetBool("isRecovering", isRecovering);

        if (Input.GetKey(KeyCode.X) || isAttacking)
        {
            //当点击攻击或者正在攻击时调用
            Attack();
        }

    }
    //攻击方法
    private void Attack()
    {
        if (!isAttacking && !isAttackCD)
        {
            //没有正在进行的攻击且没有CD
            this.isAttacking = true;
            if(Random.Range(0,2) == 0)
            {
                animator.SetBool("attack1", true);
            }
            else
            {
                animator.SetBool("attack2", true);
            }
        }
        else
        {
            //有正在进行的动画，判断是否播放完毕
            state = animator.GetCurrentAnimatorStateInfo(0);
            if(state.IsName("attack1") && state.normalizedTime >= 1.0f)
            {
                //播放完毕
                this.isAttacking = false;
                animator.SetBool("attack1", false);
                isAttackCD = true;
                StartCoroutine(AttackCounter());
            }
            else if(state.IsName("attack2") && state.normalizedTime >= 1.0f)
            {
                //播放完毕
                this.isAttacking = false;
                animator.SetBool("attack2", false);
                isAttackCD = true;
                StartCoroutine(AttackCounter());
            }
        }
    }
    //协程用于计时
    private IEnumerator SprintCounter()
    {
        if (isSprinting)
        {
            yield return new WaitForSeconds(this.sprintDuration);
            this.isSprinting = false;
            this.isSprintCD = true;
            yield return new WaitForSeconds(this.sprintCD);
            this.isSprintCD = false;
        }
    }
    private IEnumerator RecoverCounter()
    {
        if (isRecovering)
        {
            yield return new WaitForSeconds(this.recoverDuration);
            this.isRecovering = false;
            this.isRecoverCD = true;
            yield return new WaitForSeconds(this.recoverCD);
            this.isRecoverCD = false;
        }
    }
    private IEnumerator AttackCounter()
    {
        if (isAttackCD)
        {
            yield return new WaitForSeconds(this.attackCD);
            this.isAttackCD = false;
        }
    }
}
