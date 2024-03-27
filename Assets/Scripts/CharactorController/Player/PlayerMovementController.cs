using System;
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
    public float attackDuration = 1f;
    public float attackCD = 0.5f;
    public float recoverDuration = 1f;
    public float recoverCD = 0.5f;
    public float jumpHeight = 2f;
    Vector2 movement = new Vector2();
    private TouchingDetactor touchingDetactor;

    Animator animator;
    Rigidbody2D rb2d;
    //跳跃时间计时器
    private float jumpTime = 0f;
    private bool isJumpTouch = false;
    private float maxJumpTime = 0.2f;
    //各种状态的触发器
    private bool isSprinting = false;
    private bool isSprintCD = false;
    private bool isRunning = false;
    private bool isJumping = false;
    private bool isStickOnWall = false;
    private bool isRecovering = false;
    private bool isRecoverCD = false;

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
        movement.Normalize();
        //jump
        if (Input.GetKey(KeyCode.Space) && !touchingDetactor.isWall)
        {
            if (touchingDetactor.isGrounded)
            {
                rb2d.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
            }
            //待修改
            else if(jumpTime < maxJumpTime)
            {
                Jump(movement.x * actualSpeed);
            }
            if(jumpTime < maxJumpTime)
            {
                jumpTime += Time.deltaTime;
            }
        }
        //蹬墙跳
        if (Input.GetKeyDown(KeyCode.Space) && !touchingDetactor.isGrounded && touchingDetactor.isWall)
        {
            rb2d.AddForce(Vector2.up * 8f, ForceMode2D.Impulse); 
        }
        //overturn the spirit
        TurnCheck();
        rb2d.velocity = new Vector2(movement.x * actualSpeed, rb2d.velocity.y);
    }
    private void Jump(float movement)
    {
        rb2d.velocity = new Vector2(movement, 10f);
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
            this.isJumping = true;
        }
        else
        {
            this.isJumping = false;
            //重置跳跃时间
            jumpTime = 0f;
        }
        if(Input.GetKey(KeyCode.Q) && !this.isRecoverCD && !this.isRunning && !this.isRecovering)
        {
            this.isRecovering = true;
            StartCoroutine(RecoverCounter());
        }
        
        animator.SetBool("isSprinting", isSprinting);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isStickOnWall", isStickOnWall);
        animator.SetBool("isRecovering", isRecovering);

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
}
