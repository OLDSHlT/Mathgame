
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
    public float jumpForce = 10f;
    Vector2 movement = new Vector2();

    Transform viewPoint;
    TouchingDetactor touchingDetactor;
    Animator animator;
    Rigidbody2D rb2d;
    Damageable damageable;
    //跳跃时间计时器
    private float jumpTime = 0f; //按住空格起跳的时间
    private float maxJumpTime = 0.2f; //最大的跳跃时间
    private bool isJumpAllow = true;
    //各种状态的触发器
    private bool isSprinting = false;
    private bool isSprintCD = false;
    public bool isRunning = false;
    private bool isJumping = false;
    private bool isStickOnWall = false;
    private bool isWallJumping = false;

    public bool isFalling = false;

    //判断人物朝向
    private bool _isFacingLeft = false;
    public bool isFacingLeft
    {
        get { return _isFacingLeft; }
        private set
        {
            if(this.transform.localScale.x > 0)
            {
                isFacingLeft = false;
            }
            else
            {
                isFacingLeft = true;
            }
        }
    }

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.rb2d = GetComponent<Rigidbody2D>();
        this.touchingDetactor = GetComponent<TouchingDetactor>();
        this.damageable = GetComponent<Damageable>();
        this.viewPoint = transform.Find("ViewPoint");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        UpdateJumpState();
        if (!damageable.isUnderAttackCooldown && !isWallJumping && damageable.isAlive)
        {
            MovePlayer();
        }
        //overturn the spirit
        TurnCheck();
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
            isWallJumping = true;
            //当角色位于墙壁
            //给角色一个反方向斜上方的力
            if(gameObject.transform.localScale.x > 0)
            {
                //面向右
                rb2d.AddForce(new Vector2(0, 5));
                rb2d.velocity = new Vector2(-2, rb2d.velocity.y);
                movement.x = -1;
            }
            else
            {
                rb2d.AddForce(new Vector2(0, 3));
                rb2d.velocity = new Vector2(2, rb2d.velocity.y);
                movement.x = 1;
            }
            StartCoroutine(WallJumpingCounter());
        }        
        
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
            this.viewPoint.transform.localPosition = new Vector2(0, -2);//改变摄像机的位置，让画面能够看到地面
        }
        else
        {
            isFalling = false;
            this.viewPoint.transform.localPosition = new Vector2(0, 3);
        }
        if (touchingDetactor.isWall && isFalling)
        {
            isStickOnWall = true;
        }
        else
        {
            isStickOnWall = false;
        }
        
        
        animator.SetBool("isSprinting", isSprinting);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isStickOnWall", isStickOnWall);
        


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
    private IEnumerator WallJumpingCounter()
    {
        if (isWallJumping)
        {
            yield return new WaitForSeconds(0.15f);
            this.isWallJumping = false;
        }
    }
    
}
