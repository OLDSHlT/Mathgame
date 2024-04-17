using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YangHuiReact : MonoBehaviour
{
    public bool isReall;
    public YangHuiPart Target;
    public int standNum;
    public TMP_Text TMPself;
    public TMP_Text TMPBat;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start()
    {
        if (isReall)
        {
            standNum = Target.accurateNum;
            TMPself.text = standNum.ToString();
            TMPBat.text = standNum.ToString();
        }
        else
        {
            System.Random random = new System.Random();
            standNum = Target.accurateNum + random.Next(-3, 3);
            TMPself.text = standNum.ToString();
            TMPBat.text = standNum.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReactTest()
    {
        print(1);
    }
}
