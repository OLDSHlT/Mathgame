using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHitEvent;//受击事件，可用于调用（特定的）受击函数
    public UnityEvent<int, int> healthchanged;
    public UnityEvent deathEvent;
    public GameObject Spoil;//战利品
    private Color originalColor;
    private Color hitColor = Color.red;
    //引用
    Animator animator;
    Rigidbody2D rd;
    SpriteRenderer spriteRenderer; //精灵渲染器，用于修改被攻击时的颜色
    //属性
    [SerializeField]
    public int _maxHealth = 100;
    public int maxHealth//最大生命值
    {
        get
        { return _maxHealth; }
        private set
        { _maxHealth = value; }
    }
    [SerializeField]
    public int _Health = 100;
    public int Health//当前生命值
    {
        get { return _Health; }
        private set
        {
            value = Mathf.Max(0, value);
            _Health = value;
            healthchanged?.Invoke(_Health, maxHealth);
            if (_Health <= 0)
                isAlive = false;
            
        }
    }
    private bool _isAlive = true;
    public bool isAlive//是否存活
    {
        get { return _isAlive; }
        private set
        {
            _isAlive = value;
            animator.SetBool(AnimationString.isAlive, value);
            if (!_isAlive)
                deathEvent?.Invoke(); 
        }
    }

    public bool LockVelocity//锁定行动（硬直）
    {
        get { return animator.GetBool(AnimationString.LockVelocity); }
        private set { animator.SetBool(AnimationString.LockVelocity, value); }
    }
    //private bool isDefend { get { return animator.GetBool(AnimationString.isDefend); } }//后期用于设置能否
    public bool canKnock = true;//能否 被击退；（否/是  霸体）
    

    public bool isUnderAttackCooldown = false;//是否处于无敌状态 实际上是受击CD
    public bool isInvincible = false;
    public bool RollTriggerInvincible = false;
    public float InvincibieTime = 0.5f;//受伤后的无敌时间
    [SerializeField]
    private float Timer = 0;
    // Start is called before the first frame update
    private void Awake()//自动获取组件
    {
        animator = GetComponent<Animator>();
        rd = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;//原始颜色
        //print("start");
    }
    private void Update()
    {   //主要用于检测是否处于无敌状态
        if (RollTriggerInvincible == true)
            isUnderAttackCooldown = true;
        else if (!RollTriggerInvincible && Timer == 0)
        {
            isUnderAttackCooldown = false;
        }
        if (isUnderAttackCooldown && !RollTriggerInvincible)
        {
            Timer += Time.deltaTime;
            if (InvincibieTime <= Timer)
            {
                isUnderAttackCooldown = false;
                Timer = 0;
            }
        }
        //Console.WriteLine();
    }
    public bool Hit(int damage, Vector2 knockback)//受击判定
    {
        //if (isAlive && !isInvincible && isDefend)         //后期可用于设置防御状态
        //    {
        //    Health -= 1;
        //    damageableHitEvent?.Invoke(1, knockback);//?
                            //CharactorEvents.characterDamaged.Invoke(gameObject, 1);
        //    isInvincible = true;
        //    Timer += 0.000001f;
        //    return true;
        //}
        if (isAlive && !isUnderAttackCooldown)
        {
            if (!isInvincible)//无敌的时候不掉血，但是可以被击退
            {
                Health -= damage;
            }
            
            damageableHitEvent?.Invoke(damage, knockback);
            //animator.SetTrigger(AnimationString.HitTrigger);//后期用于设置动画器的“受击”Trigger

                            //CharactorEvents.characterDamaged.Invoke(gameObject, damage);
            isUnderAttackCooldown = true;
            Timer += 0.000001f;
            return true;
        }
        else
            return false;
    }
    public void OnHit(int damage, Vector2 knockback)//受击（可能）造成的硬直和击退
    {
        LockVelocity = true;
        if (canKnock)//若无霸体
            rd.velocity = new Vector2(knockback.x, rd.velocity.y + knockback.y);
        spriteRenderer.color = hitColor;
        Invoke("RestoreColor", 0.5f);
    }
    private void RestoreColor()
    {
        // 恢复角色的原始颜色
        spriteRenderer.color = originalColor;
    }

    public void Heal(int HealthRestore)//治疗
    {
        if (isAlive && Health < maxHealth)
        {
            int maxheal = Mathf.Max(maxHealth - Health, 0);
            int actulheal = Mathf.Min(maxheal, HealthRestore);
            Health += actulheal;
            //CharactorEvents.characterHealed(gameObject, actulheal);
            
        }
        
    }
    public void FallSpoil()//掉落战利品
    {
        Instantiate(Spoil, transform.position, transform.rotation);
    }
}
