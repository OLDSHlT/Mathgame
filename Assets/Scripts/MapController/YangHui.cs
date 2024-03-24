using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YangHui : MonoBehaviour
{
    int Line;
    public YangHuiPart[] PartList;

    void Start()
    {
        int add = 0,a=1;
        
        while (add<PartList.Length)
        {
            add += a;
            Line = a;
            a++;
        }
        //print(Line);
        System.Random random = new System.Random();
        Vector2 iRandom2d =new Vector2 (random.Next(1,3), random.Next(1, 3));
        for (int i = 0; i < PartList.Length; i++)
        {
            
            Vector2 Index2D = IndexChange(i)+iRandom2d;
            PartList[i].Num = YanghuiRes(Convert.ToInt32(Index2D.x), Convert.ToInt32(Index2D.y));
            
        }
        PartList[0].Num = YanghuiRes(Convert.ToInt32(iRandom2d.x), Convert.ToInt32(iRandom2d.y));


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
                    //print(index2D);
                    return index2D;
                }
                add++;
            }
        }
        
        return index2D;
    }
}
