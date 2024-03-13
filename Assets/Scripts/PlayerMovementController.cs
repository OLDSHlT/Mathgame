using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 8.0f;
    Vector2 movement = new Vector2();
    private TouchingDetactor touchingDetactor;

    Animator animator;
    string animationState = "AnimationState";
    Rigidbody2D rb2d;

    enum CharStates
    {
        idle = 0,
        running = 1,
        attack1 = 2,
        attack2 = 3,
        recover = 4,
        jumping = 5,
    }
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
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        //overturn the spirit
        if (Input.GetKey(KeyCode.Space) && touchingDetactor.isGrounded){
            rb2d.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
        }
        TurnCheck();
        rb2d.velocity = new Vector2(movement.x * movementSpeed, rb2d.velocity.y);
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
            animator.SetInteger(animationState, (int)CharStates.running);
        }
        else if (Input.GetKey(KeyCode.Space) || !touchingDetactor.isGrounded)
        {
            animator.SetInteger(animationState, (int)CharStates.jumping);
        }
        else if (Input.GetKey(KeyCode.X))
        {
            animator.SetInteger(animationState, (int)CharStates.attack1);
        }
        else
        {
            animator.SetInteger(animationState, (int)CharStates.idle);
        }
    }
}