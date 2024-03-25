using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectGameEvent : MonoBehaviour
{
    /**
     * 给每个选择关卡的按钮配置该组件
     * 当组件值为-1时为测试关卡
     * 0-3为游戏的四个关卡
     */
    public int sceneNumber;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void goToGame()
    {
        if(sceneNumber == -1)
        {
            SceneManager.LoadScene("TestScene");
        }
    }

}
