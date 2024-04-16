using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePlane : MonoBehaviour
{
    BoxCollider2D col;
    Animator animator;

    public bool _Touched = false;
    public bool Touched { get { return _Touched; }
        private set 
        { 
            _Touched = value;
            //animator.SetBool("Touched",value);
        }
    }
    float Timer=0f;
    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetactTouch();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Touched = true;
    }
    private void DetactTouch()
    {
        if (Touched)
        {
            Timer += Time.deltaTime;
            if (Timer>=0.6f)
            { 
                animator.enabled=true;
                Timer = 0f;
                Touched = false;
            }
        }
    }
    public void SetNegtive()
    {
        animator.enabled = false;
    }
}
