using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;
using System;
using UnityEngine.Events;

public class HuanFangController : MonoBehaviour
{
    public TMP_Text[] button;
    int[] buttonNums=new int[9];
    Canvas canvas;
    void Awake()
    {
        for (int i = 0; i < button.Length; i++)
        {
            buttonNums[i] = Convert.ToInt32(button[i].text);
            //print(buttonNums[i]);
        }
        canvas= GetComponent<Canvas>();
    }

    void Start()
    {
        //Time.timeScale = 0f;
    }

    int Index1=0;
    int Index2=1;
    bool hasIndex1=false;

    //public void SetOn()
    //{
    //    if (Input.GetKey(KeyCode.E))
    //    {
    //        //canvas.gameObject.SetActive(true);
    //        Time.timeScale = 0f;

    //        canvas.enabled = true;
    //    }
    //}

    public void ButtonClick(int index)
    {
        if (!hasIndex1)
        {
            hasIndex1 = true;
            Index1 = index;
        }
        else if (hasIndex1 && index != Index1)
        {
            Index2 = index;
            Exchange();
            hasIndex1 = false;
        }
        
    }
    private void Exchange()
    {
        string ExchangeBox = button[Index1].text;
        button[Index1].text = button[Index2].text;
        buttonNums[Index1] = Convert.ToInt32(button[Index1].text);  
        button[Index2].text = ExchangeBox;
        buttonNums[Index2] = Convert.ToInt32(button[Index2].text);
        HuanFangJudge();
    }
    private void HuanFangJudge()
    {
        bool flag = true;
        for (int i = 0; i < 3; i++)//ÐÐ¼ì²â
        {
            if ((buttonNums[i * 3] + buttonNums[i * 3 + 1] + buttonNums[i * 3 + 2]) != 15)
            { flag = false; }
        }
        for (int i = 0; i < 3;  i++)//ÁÐ¼ì²â
        {
            if ((buttonNums[i] + buttonNums[i + 3] + buttonNums[i+6]) != 15)
            { flag = false; }
        }
        if ((buttonNums[0] + buttonNums[4] + buttonNums[8]) != 15)//Ð±¼ì²â159
        { flag = false; }
        if ((buttonNums[2] + buttonNums[4] + buttonNums[6]) != 15)//Ð±¼ì²â357
        { flag = false; }

        if (flag)
        {
            Time.timeScale = 1f;
            Invoke("End", 0.5f);
        }
    }
    public UnityEvent EndEvent;
    private void End()
    {
          EndEvent.Invoke();
    }
    public void Exit()
    {
        canvas.enabled=false;
    }
}
