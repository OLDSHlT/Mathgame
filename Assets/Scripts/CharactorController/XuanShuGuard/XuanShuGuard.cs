using fractionProcessor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XuanShuGuard : MonoBehaviour
{
    public GameObject player;
    public float speed = 2f;
    public float detactDistance = 100f;
    public float attackCD = 1.5f;

    private AnimatorStateInfo state;
    private bool isAttackCDing = false;
    private bool isAttacking = false;

    private FractionProcessor fractionProcesser;
    private attackModes attackMode = attackModes.ShortRange;
    private Rigidbody2D rb2d;
    private Vector2 movement;
    private bool isPlayerInTrigger = false;
    private Animator animator;
    private enum attackModes{
        LongRange = 0,
        ShortRange = 1
    }
    // Start is called before the first frame update
    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        int randomInt = Random.Range(0, 10);
        if(randomInt >= 5)
        {
            fractionProcesser = new FractionProcessor(new FractionGeneratorWith3());
        }
        else
        {
            fractionProcesser = new FractionProcessor(new FractionGeneratorWith2());
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
        float distance = Vector2.Distance(this.transform.position, player.transform.position);
        if(distance <= detactDistance)
        {
            //��ս����
            //�����Ҳ��ڹ�����Χ��
            //�������
            if(attackMode == attackModes.ShortRange)
            {
                if (!isPlayerInTrigger && !isAttacking || isAttackCDing)
                {
                    if (this.transform.position.x < this.player.transform.position.x)
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
        }
        else
        {
            movement = new Vector2();
        }
        rb2d.velocity = new Vector2(movement.x * speed, rb2d.velocity.y);
        TurnCheck();
    }
    //ʹ�ô������ж�����Ƿ�����˽�ս������Χ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.isPlayerInTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.isPlayerInTrigger = false;
        }
    }
    private void UpdateStatus()
    {
        if(movement.x != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        animator.SetBool("isAttacking", this.isAttacking);
    }
    private void AttackShortRange()
    {
        if (!isAttackCDing)
        {
            //û��CD
            if (!isAttacking)
            {
                //û���Ѿ���ʼ�Ĺ���
                this.isAttacking = true;
            }
            else
            {
                //���Ѿ���ʼ�Ĺ������жϽ���
                state = animator.GetCurrentAnimatorStateInfo(0);
                if(state.IsName("attack") && state.normalizedTime >= 1.0f)
                {
                    //�����������
                    this.isAttacking = false;
                    //��ʼʹ��Э�̼���CD
                    this.isAttackCDing = true;
                    StartCoroutine(AttackCDControl());
                }
            }
        }
    }
    private IEnumerator AttackCDControl()
    {
        if (this.isAttackCDing)
        {
            yield return new WaitForSeconds(this.attackCD);
            this.isAttackCDing = false;
        }
    }
}