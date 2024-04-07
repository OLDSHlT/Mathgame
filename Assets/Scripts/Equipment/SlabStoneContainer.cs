using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//容纳所有约分石板的容器

public class SlabStoneContainer : MonoBehaviour
{
    public List<ReductionSlabstone> slabstones;
    public ReductionSlabstone selectedSlabStone = null;
    private int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        slabstones = new List<ReductionSlabstone>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void SwitchSelectSlabStone()
    {
        if (slabstones.Count == 0)
        {
            selectedSlabStone = null; // 如果列表为空，则选中的 SlabStone 为 null
            currentIndex = -1;
            return;
        }

        // 增加 currentIndex，如果达到列表的末尾，则回到列表的开头
        currentIndex = (currentIndex + 1) % slabstones.Count;
        selectedSlabStone = slabstones[currentIndex];
    }
}
