using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutMapHurt : MonoBehaviour
{
    BoxCollider2D collider;
    public Transform ReturnPos;
    public int FallDamage = 20;
    void Awake()
    {
        collider=  GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        bool gotHit = damageable.Hit(FallDamage, Vector2.zero);
        collision.transform.position = ReturnPos.transform.position;
    }
}
