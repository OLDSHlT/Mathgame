using fractionProcessor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XuanShuFractionComponent : MonoBehaviour
{
    public Image numeratorTen; //分子十位
    public Image numeratorUnit; //分子个位
    public Image denominatorTen; //分母十位
    public Image denominatorUnit; //分母个位
    private FractionProcessor fractionProcessor;//分数生成器
    public int numerator;
    public int denominator;
    public SlabStoneContainer playerFraction;
    Damageable damageable;
    MathStickLoader mathSticks;

    Transform shield;

    // Start is called before the first frame update
    void Start()
    {
        Transform canvas = transform.Find("FractionDisplay");
        Transform img1 = canvas.Find("Numerator-Ten");
        Transform img2 = canvas.Find("Denominator-Ten");
        Transform img3 = canvas.Find("Numerator-Unit");
        Transform img4 = canvas.Find("Denominator-Unit");
        this.numeratorTen = img1.GetComponent<Image>();
        this.denominatorTen = img2.GetComponent<Image>();
        this.numeratorUnit = img3.GetComponent<Image>();
        this.denominatorUnit = img4.GetComponent<Image>();
        this.damageable = GetComponent<Damageable>();
        this.shield = transform.Find("Shield");
        this.mathSticks = GetComponent<MathStickLoader>();

        int randomInt = Random.Range(0, 10);
        //根据不同情况随机生成2或者3的生成器
        if (randomInt >= 5)
        {
            fractionProcessor = new FractionProcessor(new FractionGeneratorWith3());
        }
        else
        {
            fractionProcessor = new FractionProcessor(new FractionGeneratorWith2());
        }

        SetCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFraction();
    }
    private void UpdateFraction()
    {
        this.numerator = fractionProcessor.GetDivisor();
        this.denominator = fractionProcessor.GetDividend();
        if (fractionProcessor.IsSimplestFraction())
        {
            //最简分数
            damageable.isInvincible = false;
            this.shield.gameObject.SetActive(false);
        }
        else
        {
            damageable.isInvincible = true;
        }
        
    }
    void SetCanvas()
    {
        //根据不同的数使用不同的算筹图片
        DisplayMathStick(fractionProcessor.GetDivisor() / 10, this.numeratorTen, 0); //分子十位
        DisplayMathStick(fractionProcessor.GetDivisor() % 10, this.numeratorUnit, 1); //分子个位
        DisplayMathStick(fractionProcessor.GetDividend() / 10, this.denominatorTen, 0); //分母十位
        DisplayMathStick(fractionProcessor.GetDividend() % 10, this.denominatorUnit, 1); //分母个位
        //this.numeratorTen.sprite = LoadSpriteFromPath("Assets/Art/MathSticks/1-horizontal.png");
    }

    private void DisplayMathStick(int number , Image img , int mode)
    {
        //0 vertical
        //1 horizontal
        if(mode == 0)
        {
            DisplayMathStickVertical(number, img);
        }
        else
        {
            DisplayMathStickHorizontal(number, img);
        }
    }
    private void DisplayMathStickVertical(int number, Image img)
    {
        switch (number)
        {
            case 1:
                img.sprite = mathSticks.verticalSticks[0];
                break;
            case 2:
                img.sprite = mathSticks.verticalSticks[1];
                break;
            case 3:
                img.sprite = mathSticks.verticalSticks[2];
                break;
            case 4:
                img.sprite = mathSticks.verticalSticks[3];
                break;
            case 5:
                img.sprite = mathSticks.verticalSticks[4];
                break;
            case 6:
                img.sprite = mathSticks.verticalSticks[5];
                break;
            case 7:
                img.sprite = mathSticks.verticalSticks[6];
                break;
            case 8:
                img.sprite = mathSticks.verticalSticks[7];
                break;
            case 9:
                img.sprite = mathSticks.verticalSticks[8];
                break;
            default:
                img.sprite = mathSticks.emptyStick;
                break;
        }
    }
    private void DisplayMathStickHorizontal(int number, Image img)
    {
        switch (number)
        {
            case 1:
                img.sprite = mathSticks.horizontalSticks[0];
                break;
            case 2:
                img.sprite = mathSticks.horizontalSticks[1];
                break;
            case 3:
                img.sprite = mathSticks.horizontalSticks[2];
                break;
            case 4:
                img.sprite = mathSticks.horizontalSticks[3];
                break;
            case 5:
                img.sprite = mathSticks.horizontalSticks[4];
                break;
            case 6:
                img.sprite = mathSticks.horizontalSticks[5];
                break;
            case 7:
                img.sprite = mathSticks.horizontalSticks[6];
                break;
            case 8:
                img.sprite = mathSticks.horizontalSticks[7];
                break;
            case 9:
                img.sprite = mathSticks.horizontalSticks[8];
                break;
            default:
                img.sprite = mathSticks.emptyStick;
                break;
        }
    }

    public void OnHit(int damage, Vector2 knockback)//计算玄数
    {
        if(playerFraction != null)
        {
            //Debug.Log("fraction counter");
            if (playerFraction.selectedSlabStone != null)
            {
                fractionProcessor.Reduction(playerFraction.selectedSlabStone.reductionNumber);
            }
        }
        SetCanvas();//更新画布
    }
}
