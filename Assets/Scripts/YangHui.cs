using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YangHui : MonoBehaviour
{
    public int Line = 3;
    public GameObject[] PartList;

    void Start()
    {
        //for (int i = 0; i < PartList.Length; i++)
        //{
        //    YangHuiPart yangHuiPart;
        //    yangHuiPart=PartList[i].GetCompont<YangHuiPart>();
        //    yangHuiPart.Num = YanghuiRes(2, 1);
        // }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int YanghuiRes(int a, int b)
    {
        if (a == 0 || b == 0)
            return 1;
        return YanghuiRes(a - 1, b) + YanghuiRes(a, b - 1);
    }
    Vector2 IndexChange(int OriginIndex)
    {
        int add = 0;
        Vector2 index2D=new Vector2(0,0);
        for (int i = 0; i < Line; i++)
        {
            for (int j = 0; j < Line - i; j++)
            {
                
                if (add == OriginIndex)
                {
                    index2D = new Vector2(i, j);
                    print(index2D);
                    return index2D;
                }
                add++;
            }
        }
        
        return index2D;
    }
}
