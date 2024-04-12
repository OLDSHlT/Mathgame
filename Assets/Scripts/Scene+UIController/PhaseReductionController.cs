using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;
using System;

public class PhaseReductionController : MonoBehaviour
{

    public TMP_Text TMP_Minuend;
    public TMP_Text TMP_Subtrahend;
    public TMP_Text TMP_Result;
    

    int _Num_Minuend;
    public int Num_Minuend
    {
        get { return _Num_Minuend; }
        private set 
        {
            //print("Min");
            _Num_Minuend = value;
            TMP_Minuend.text = value.ToString();
        }
    }

    int _Num_Subtrahend;
    public int Num_Subtrahend
    {
        get { return _Num_Subtrahend; }
        private set
        {
            //print("Sub");
            _Num_Subtrahend = value;
            TMP_Subtrahend.text = value.ToString();
            Num_Result = Num_Minuend - value;
            if (Num_Result >= 0)
            {
                TMP_Result.text = Num_Result.ToString();
            }
            else if (Num_Result < 0)
            {
                Num_Result = 0;
                TMP_Result.text = " ";
            }
        }
    }
    //int Num_Subtrahend;

    //int _Num_Result;
    //public int Num_Result { get { return _Num_Result; }
    //    private set 
    //    {
            
    //        if (Num_Result >= 0)
    //        {
    //            print("Res>0");
    //            TMP_Result.text = Num_Result.ToString();
    //        }
    //        else if(Num_Result<0)
    //        {
    //            print("Res<0");
    //            TMP_Result.text = " ";
    //            _Num_Result = 0;
    //        }
    //    }
    //}
    int Num_Result;
    void Awake()
    {
        _Num_Subtrahend= Convert.ToInt32(TMP_Subtrahend.text);
       // _Num_Result = Num_Minuend - Num_Subtrahend;
    }
    // Start is called before the first frame update
    void Start()
    {
        Num_Minuend = Convert.ToInt32(TMP_Minuend.text);
        Num_Subtrahend = Convert.ToInt32(TMP_Subtrahend.text);
        Num_Result = Num_Minuend - Num_Subtrahend;
        TMP_Result.text = Num_Result.ToString();
    }

    // Update is called once per frame
    public void Exchange1()//Subtrahend需要最后一部设置
    {
        int ExBox = Num_Minuend;
        Num_Minuend = Num_Subtrahend;
        Num_Subtrahend=ExBox;
    }
    public void Exchange2() 
    {
        int ExBox = Num_Result;
        //Num_Result = Num_Subtrahend;
        if(ExBox>0)
            Num_Subtrahend = ExBox;
    }
    
    public GameObject Slabstone;
    public GameObject GXJSMachine;
    public void Produce()
    {   if (Num_Result > 0)
        {
            GameObject flagstone=Instantiate(Slabstone, GXJSMachine.transform.position + new Vector3(5, 0, 0), transform.rotation);
            ReductionSlabstone RS=flagstone.GetComponent<ReductionSlabstone>();
            RS.reductionNumber=Num_Result;
            NewBehaviourScript NBS=GXJSMachine.GetComponent<NewBehaviourScript>();
            NBS.UpdateInput();
        }
    }

}
