using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YangHuiPart : MonoBehaviour
{   public int _Num;
    public int Num { set 
    {    //print(value);
         _Num = value; } 
    }
    public bool CanReact=false;

    public UnityEvent YangHuiReactEvent;
    Collider2D collider;
    // Start is called before the first frame update
    
    void Awake()
    {
        collider = GetComponent<Collider2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   if(CanReact)
            YangHuiReactEvent?.Invoke();
    }

    public void ReactTest()
    {
        print("ReactTest");
    }
}
