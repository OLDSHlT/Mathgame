using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    //private bool LockVelocity { get { return _lockvelocity; }
    //    set { //animator.setbool(animationstring.lockvelocity)=value;
    //          }
    //    }//是否锁定速度
    // //以上参数后期要关联动画器
    public bool LockVelocity=false;
    public float MoveSpeed = 1.5f;
    Collider2D Collider;
    Rigidbody2D rd;
    TouchingDetactor touchingDetactor;
    void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        touchingDetactor = GetComponent<TouchingDetactor>();
        Collider = GetComponent<Collider2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isJumping)
            JumpTimer -= Time.deltaTime;
        if (!LockVelocity)
        {
            {
                float x = Input.GetAxisRaw("Horizontal");
                float y = Input.GetAxisRaw("Vertical");
                if (x > 0 && transform.localScale.x < 0)
                {

                    TurnScale();
                }
                else if (x < 0 && transform.localScale.x > 0)
                {

                    TurnScale();
                }
                rd.velocity = new Vector2(MoveSpeed * x, rd.velocity.y);
            }//行走逻辑
            Jump();
            //if (touchingDetactor.isWall &&!touchingDetactor.isGrounded)
                //WallJump();
        }
        Dash();
        DashTimer -=Time.deltaTime;
        DashCDTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        
    }

    public float FallingSpeed = 3f;
    //void Falling()
    //{
    //    if (!touchingDetactor.isGrounded && !LockVelocity && !isJumping)
    //        rd.velocity = new Vector2(rd.velocity.x, FallingSpeed*-1f);
    //}//固定下落速度而非引擎自带的重力加速度

    public void TurnScale()
    {
        gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
    }

    public float JumpSpeed = 5f;
    bool isJumping=false;
    float JumpTimer = 0f;
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&  touchingDetactor.isGrounded)
        {
            JumpTimer = 0.5f;
            isJumping=true;
        }
        if (Input.GetKey(KeyCode.Space) &&  isJumping)
        {
            //animator.SetBool....
            rd.velocity = new Vector2(rd.velocity.x * 0.75f, JumpSpeed);
        }
        if (Input.GetKeyUp(KeyCode.Space) || JumpTimer < 0f)
        {
            isJumping = false;
            JumpTimer = 0f;
        }
    }
    public Vector2 WallJumpSpeed = new Vector2(0.5f,3);
    void WallJump()
    {
        if ( Input.GetKeyDown(KeyCode.Space) && !LockVelocity)
        {
            //if{ }
            {
                rd.velocity = new Vector2(transform.localScale.x * -1*WallJumpSpeed.x, WallJumpSpeed.y);
                LockVelocity = true;
                Invoke("LaterWallJump", 0.2f);
            }
        }
    }
    void LaterWallJump()
    {
        LockVelocity = false;
    }

    public float DashSpeed = 5f;
    float DashCDTimer = 0f;
    public float DashCD = 1.3f;
    float DashTimer = 0f;
    bool isDashing=false;
    void Dash()
    {  
        if (Input.GetKeyDown(KeyCode.W) && DashCDTimer < 0f &&!isDashing &&!LockVelocity)
        {   isDashing = true;
            DashTimer = 0.35f;
            DashCDTimer = DashCD;
            LockVelocity=true;//后期可通过动画器设置
            rd.velocity = new Vector2(transform.localScale.x * DashSpeed, 0f);
        }
        if (DashTimer < 0 &&isDashing)
        {
            isDashing = false;
            LockVelocity=false;
            rd.velocity = new Vector2(0, 0);
        }
    }
}
