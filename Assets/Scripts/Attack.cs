using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider2D))]
public class Attack : MonoBehaviour
{

    private Collider2D colliderAttack;
    public int attackDemage = 10;
    public Vector2 knockback;
    // Start is called before the first frame update
    private void Awake()
    {
        colliderAttack = GetComponent<Collider2D>();

    }
    //private void OnTriggerEnter(Collision other)
    //{
    //    Damageable damageable = other.GetComponent<Damageable>();
    //    if (damageable != null)
    //    {
    //        damageable.Hit(attackDemage);
    //        Debug.Log(o)
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null && transform.parent != null)
        {
            Vector2 knockbackVector = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.Hit(attackDemage, knockbackVector);
        }
        else if (damageable != null)
        {
            bool gotHit = damageable.Hit(attackDemage, Vector2.zero);
        }

    }
}
