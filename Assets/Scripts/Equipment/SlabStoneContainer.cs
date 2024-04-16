using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

//容纳所有约分石板的容器

public class SlabStoneContainer : MonoBehaviour
{
    private List<ReductionSlabstone> slabstones = new List<ReductionSlabstone>();
    public ReductionSlabstone selectedSlabStone = null;
    int currentIndex;//
    public UnityEvent slabstonePick;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R) && selectedSlabStone != null)
        //    SwitchIndex();

    }
    public void AddSlabStone(ReductionSlabstone slabstone)
    {
        slabstones.Add(slabstone);
        if(selectedSlabStone == null || slabstones.Count == 0)
        {
            selectedSlabStone = slabstone;
            currentIndex = 0;
            
        }
    }
    public void RemoveSlabStone(ReductionSlabstone slabstone)
    {
        int index = slabstones.IndexOf(slabstone); // 找到要移除的 SlabStone 在列表中的索引
        if (index != -1)
        {
            slabstones.Remove(slabstone); // 移除 SlabStone

            // 更新 currentIndex
            if (index < currentIndex)
            {
                currentIndex--; // 如果被移除的 SlabStone 在当前选中的 SlabStone 之前，则需要将 currentIndex 减一
            }
            else if (index == currentIndex)
            {
                currentIndex = 0; // 如果被移除的 SlabStone 正好是当前选中的 SlabStone，则将 currentIndex 设为 0
                if (slabstones.Count > 0)
                {
                    selectedSlabStone = slabstones[currentIndex]; // 更新选中的 SlabStone
                }
                else
                {
                    selectedSlabStone = null; // 如果列表为空，则选中的 SlabStone 为 null
                }
            }
        }
    }
    //public GameObject SlabStoneUI;
    public void SwitchSelectSlabStone()
    {   if (slabstones.Count == 1)//启动时
            slabstonePick?.Invoke();
        if (slabstones.Count == 0)
        {
            selectedSlabStone = null; // 如果列表为空，则选中的 SlabStone 为 null
            currentIndex = -1;
            return;
        }

        // 增加 currentIndex，如果达到列表的末尾，则回到列表的开头
        currentIndex = (currentIndex + 1) % slabstones.Count;
        selectedSlabStone = slabstones[currentIndex];
        int Num = selectedSlabStone.GetReductionNumber();
        ChosenNum = Num.ToString();
        TextChange();
    }

    string ChosenNum;
    public void SwitchIndex()
    {

        {
            if (currentIndex >= slabstones.Count-1 || slabstones.Count == 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
        }

        int Num = slabstones[currentIndex].GetReductionNumber();
        ChosenNum = Num.ToString();
        TextChange();
    }
    string[] TextNum = new string[] { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
    string actualText;
    public TMP_Text SlabText;
    private void TextChange()
    {
        actualText = "";
        char[] Nums = new char[ChosenNum.Length];
        for (int i = 0; i < Nums.Length; i++)
        {
            Nums[i] = ChosenNum[i];
            char actualChar = Convert.ToChar(TextNum[Convert.ToInt32(Nums[i])-48]);
            actualText = actualText.PadRight(i+1, actualChar);
            //print(actualChar);
        }
        SlabText.text = actualText;
        
    }
}
