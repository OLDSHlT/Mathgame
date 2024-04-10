using fractionProcessor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFraction : MonoBehaviour
{
    MathStickLoader stickLoader;
    Transform shield;
    Damageable damageable;
    FractionProcessor fractionProcessor;//分数生成器

    public SlabStoneContainer playerFraction;
    public int numerator; //分子
    public int denominator; //分母

    public Image numHundred; //分子
    public Image numTen;
    public Image numUnit;
    public Image denHundred; //分母
    public Image denTen;
    public Image denUnit;
    // Start is called before the first frame update
    void Start()
    {
        fractionProcessor = new FractionProcessor(new BigFractionGenerator());

        shield = transform.Find("Shield");
        stickLoader = GetComponent<MathStickLoader>();
        damageable = GetComponent<Damageable>();

        Transform canvas = transform.Find("FractionDisplay");
        Transform img1 = canvas.Find("NumHundred");
        Transform img2 = canvas.Find("NumTen");
        Transform img3 = canvas.Find("NumUnit");
        Transform img4 = canvas.Find("DenHundred");
        Transform img5 = canvas.Find("DenTen");
        Transform img6 = canvas.Find("DenUnit");

        numHundred = img1.GetComponent<Image>();
        numTen = img2.GetComponent<Image>();
        numUnit = img3.GetComponent<Image>();
        denHundred = img4.GetComponent<Image>();
        denTen = img5.GetComponent<Image>();
        denUnit = img6.GetComponent<Image>();

        SetCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFraction();
        SetCanvas();
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
    private void SetCanvas()
    {
        DisplayMathStick(fractionProcessor.GetDivisor() / 100, numHundred, 1); //分子
        DisplayMathStick((fractionProcessor.GetDivisor() / 10) % 10, numTen, 0);
        DisplayMathStick(fractionProcessor.GetDivisor() % 10, numUnit, 1);
        DisplayMathStick(fractionProcessor.GetDividend() / 100, denHundred, 1);
        DisplayMathStick((fractionProcessor.GetDividend() / 10) % 10, denTen, 0);
        DisplayMathStick(fractionProcessor.GetDividend() % 10, denUnit, 1);
    }
    private void DisplayMathStick(int number, Image img, int mode)
    {
        //0 vertical
        //1 horizontal
        if (mode == 0)
        {
            DisplayMathStickVertical(number, img);
        }
        else
        {
            DisplayMathStickHorizontal(number, img);
        }
    }
    private void DisplayMathStickHorizontal(int number , Image img)
    {
        switch (number)
        {
            case 1:
                img.sprite = stickLoader.horizontalSticks[0];
                break;
            case 2:
                img.sprite = stickLoader.horizontalSticks[1];
                break;
            case 3:
                img.sprite = stickLoader.horizontalSticks[2];
                break;
            case 4:
                img.sprite = stickLoader.horizontalSticks[3];
                break;
            case 5:
                img.sprite = stickLoader.horizontalSticks[4];
                break;
            case 6:
                img.sprite = stickLoader.horizontalSticks[5];
                break;
            case 7:
                img.sprite = stickLoader.horizontalSticks[6];
                break;
            case 8:
                img.sprite = stickLoader.horizontalSticks[7];
                break;
            case 9:
                img.sprite = stickLoader.horizontalSticks[8];
                break;
            default:
                img.sprite = stickLoader.emptyStick;
                break;
        }
    }
    private void DisplayMathStickVertical(int number, Image img)
    {
        switch (number)
        {
            case 1:
                img.sprite = stickLoader.verticalSticks[0];
                break;
            case 2:
                img.sprite = stickLoader.verticalSticks[1];
                break;
            case 3:
                img.sprite = stickLoader.verticalSticks[2];
                break;
            case 4:
                img.sprite = stickLoader.verticalSticks[3];
                break;
            case 5:
                img.sprite = stickLoader.verticalSticks[4];
                break;
            case 6:
                img.sprite = stickLoader.verticalSticks[5];
                break;
            case 7:
                img.sprite = stickLoader.verticalSticks[6];
                break;
            case 8:
                img.sprite = stickLoader.verticalSticks[7];
                break;
            case 9:
                img.sprite = stickLoader.verticalSticks[8];
                break;
            default:
                img.sprite = stickLoader.emptyStick;
                break;
        }
    }
}
