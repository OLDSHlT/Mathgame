using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Damageable damageable;
    AnimatorStateInfo state;
    Animator animator;
    PlayerMovementController movementController;

    public bool isAttacking = false;
    private bool isAttackCD = false;
    public bool isRecovering = false;
    private bool isRecoverCD = false;

    public float attackCD = 0.5f;
    public float recoverDuration = 1f;
    public float recoverCD = 0.5f;


    void Start()
    {
        this.damageable = GetComponent<Damageable>();
        this.animator = GetComponent<Animator>();
        this.movementController = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        UpdateStatus();
    }

    void FixedUpdate()
    {
        
    }
    private void UpdateStatus()
    {
        animator.SetBool("isRecovering", isRecovering);
    }
    private void UpdateInput()
    {
        if (Input.GetKey(KeyCode.X) || isAttacking)
        {
            //当点击攻击或者正在攻击时调用
            Attack();
        }
        if (Input.GetKey(KeyCode.Q) && !this.isRecoverCD && !this.isRecovering && !movementController.isRunning)
        {
            this.isRecovering = true;
            StartCoroutine(RecoverCounter());
        }
        
    }
    //攻击方法
    private void Attack()
    {
        if (!isAttacking && !isAttackCD)
        {
            //没有正在进行的攻击且没有CD
            this.isAttacking = true;
            if (UnityEngine.Random.Range(0, 2) == 0)
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
            if (state.IsName("attack1") && state.normalizedTime >= 1.0f)
            {
                //播放完毕
                this.isAttacking = false;
                animator.SetBool("attack1", false);
                isAttackCD = true;
                StartCoroutine(AttackCounter());
            }
            else if (state.IsName("attack2") && state.normalizedTime >= 1.0f)
            {
                //播放完毕
                this.isAttacking = false;
                animator.SetBool("attack2", false);
                isAttackCD = true;
                StartCoroutine(AttackCounter());
            }
        }
    }
    //计算攻击CD的协程
    private IEnumerator AttackCounter()
    {
        if (isAttackCD)
        {
            yield return new WaitForSeconds(this.attackCD);
            this.isAttackCD = false;
        }
    }
    //计算恢复的CD协程
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
