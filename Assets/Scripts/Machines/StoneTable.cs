using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTable : MonoBehaviour
{
    // Start is called before the first frame update
    Transform UI;
    public bool isActive = false;
    void Start()
    {
        UI = transform.Find("UI");
        UI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
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
    private void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //退出仪器
            Deactivation();
        }
    }
}
