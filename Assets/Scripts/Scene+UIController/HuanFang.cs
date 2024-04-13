using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuanFang : MonoBehaviour
{
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetOn()
    {   if(Input.GetKey(KeyCode.E))
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    
}
