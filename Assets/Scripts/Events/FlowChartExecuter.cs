using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowChartExecuter : MonoBehaviour
{
    public string blockName;
    public Flowchart flowchart;
    public bool isLoop = false;
    
    bool isExecuted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isLoop || !isExecuted)
            {
                flowchart.ExecuteBlock(blockName);
                isExecuted = true;
            }
        }
    }
}
