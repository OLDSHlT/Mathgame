using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBall : MonoBehaviour
{
    //Collider2D Col;
    void Awake()
    {
        //Col = Getcomponent<Collider2D>();
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
        PlayerMovementController PMC = collision.GetComponent<PlayerMovementController>();
        Rigidbody2D rd = collision.GetComponent<Rigidbody2D>();
        if(rd.velocity.y<-8f)
            rd.velocity=new Vector2(rd.velocity.x,PMC.jumpForce*1.5f);
        else if(rd.velocity.y>-8f)
            rd.velocity= new Vector2(rd.velocity.x, PMC.jumpForce );
    }
}
