using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class YangHuiPart : MonoBehaviour
{   public int accurateNum;
    //public string TextNum;
    public bool CanReact=false;

    public UnityEvent YangHuiReactEvent;
    Collider2D collider;
    public TMP_Text TMP;
    // Start is called before the first frame update
    
    void Awake()
    {
        collider = GetComponent<Collider2D>();
        
        
    }
    void Start()
    {
        if (CanReact)
            TMP.text = "";
        else
        {
            //TextNum = accurateNum.ToString();
            TMP.text = accurateNum.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   if(CanReact)
        { YangHuiReact YHReact = collision.GetComponent<YangHuiReact>();
            int ReactNum = YHReact.standNum;
            TMP.text=ReactNum.ToString();
            if (ReactNum == accurateNum)
            {
                YangHuiReactEvent?.Invoke();
                CanReact = false;
            }

            Destroy(collision.gameObject);
        }
    }

    
}
