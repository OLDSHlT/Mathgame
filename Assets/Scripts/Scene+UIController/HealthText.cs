using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed=new Vector3(0,80,0);
    public float Fadetime=1.5f;
    private float Timer = 0f;
    Color startColar;
    //ÒýÓÃ
    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;
    // Start is called before the first frame update
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textTransform = GetComponent<RectTransform>();
        startColar = textMeshPro.color;
    }
    // Update is called once per frame
    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;
        Invoke("Later", 1f);
    }
    private void Later()
    {
        if (Timer < Fadetime)
        {
            Timer += Time.deltaTime;
            float newAlpha = textMeshPro.color.a * (1 - (Timer / Fadetime));
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, newAlpha);
        }
        else
            Destroy(gameObject);
    }
}
