using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Transform PlayerPosStart;
    Vector3 SpritePosStart;
    public Transform TargetPosX;//最远点
    public Transform TargetPosY;//最高点
    float TotalDistanceX;//玩家初始位置和图片终点的x距离
    float TotalDistanceY;//玩家初始位置和图片终点的y距离
    public Transform PosX1;
    public Transform PosX2;
    public Transform PosY1;
    public Transform PosY2;
    //关于Y方向的视差设置
    public bool usingYParallax=false;//是否启用Y方向上的视差
    public float ParallaxY = 0.5f;//Y方向视差的修正系数
    //public bool isX = true;
    void Awake()
    {
        PlayerPosStart = GameObject.Find("Main Camera").GetComponent<Transform>();
        TotalDistanceX=TargetPosX.transform.position.x-PlayerPosStart.transform.position.x;
        TotalDistanceY = TargetPosY.transform.position.y - PlayerPosStart.transform.position.y;
        SpritePosStart = transform.position;
    }
    float Wide;//移动到目标位置后，原本某一位于背景Pos1的视角会移动到背景Pos2
    float High;
    void Start()
    {   
        Wide = PosX2.transform.position.x - PosX1.transform.position.x;    
        High = PosY2.transform.position.y - PosY1.transform.position.y;
    }

    float ToTargetDistanceX;
    float ToTargetDistanceY;
    void Update()
    {
        Transform PlayerPos = GameObject.Find("Main Camera").GetComponent<Transform>();
        ToTargetDistanceX = TargetPosX.transform.position.x - PlayerPos.transform.position.x;//玩家到目标的x距离
        ToTargetDistanceY = TargetPosY.transform.position.y - PlayerPos.transform.position.y;//玩家到目标的y距离
        float ParallaxDistanceX = (1 - ToTargetDistanceX / TotalDistanceX) * Wide;//视差x距离
        float ParallaxDistanceY = (1 - ToTargetDistanceX / TotalDistanceX) * High;//视差y距离
        transform.position = SpritePosStart +new Vector3(( -1*ParallaxDistanceX + (TotalDistanceX-ToTargetDistanceX)),0,0);//X视差

        if (usingYParallax)//Y视差
        {
            transform.position = SpritePosStart
            + new Vector3((-1 * ParallaxDistanceX + (TotalDistanceX - ToTargetDistanceX)), (-1 * ParallaxDistanceY + TotalDistanceY - ToTargetDistanceY) * ParallaxY, 0);
        }
        else 
        {
            transform.position = SpritePosStart+ new Vector3((-1 * ParallaxDistanceX + (TotalDistanceX - ToTargetDistanceX)), 0, 0);
        }
    }
}
