using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HP;
    public int maxHP = 0;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        this.HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReduceHealth(int damage)
    {
        this.HP -= damage;
        if(HP <= 0)
        {
            this.isDead = true;
        }
    }
    public void IncreaseHealth(int hp)
    {
        this.HP += hp;
        if(this.HP >= maxHP)
        {
            this.HP = maxHP;
        }
    }
    
}
