using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas finishUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnLevelFinish()
    {
        //Time.timeScale = 0f;
        if(finishUI != null)
        {
            finishUI.gameObject.SetActive(true);
            finishUI.gameObject.GetComponent<Animator>().SetTrigger("active");
        }
    }
}
