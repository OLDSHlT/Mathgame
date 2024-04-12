using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//更相减损术仪器脚本
public class NewBehaviourScript : MonoBehaviour
{
    public bool isActive = false;
    // Start is called before the first frame update
    public Canvas UI;
    void Start()
    {
        UI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                UpdateInput();
        }
    }
    public void OnActive()//仪器被激活的回调函数
    {
        Time.timeScale = 0f;
        this.isActive = true;
        UI.gameObject.SetActive(true);
    }
    void Deactivation()//仪器关闭
    {
        Time.timeScale = 1.0f;
        this.isActive = false;
        UI.gameObject.SetActive(false);
    }
    public void UpdateInput()
    {
        
        {
            //退出仪器
            Deactivation();
        }
    }
}
