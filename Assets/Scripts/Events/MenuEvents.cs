using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuEvents : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject producerCanvas;
    public GameObject selectGameCanvas;
    public GameObject optionCanvas;
    // Start is called before the first frame update
    void Start()
    {
        producerCanvas.SetActive(false);
        selectGameCanvas.SetActive(false);
        optionCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        //Debug.Log("Quit Game");
        Application.Quit();
    }
    public void MenuToOption()
    {
        this.menuCanvas.SetActive(false);
        this.optionCanvas.SetActive(true);
    }
    public void MenuToProducer()
    {
        this.menuCanvas.SetActive(false);
        this.producerCanvas.SetActive(true);
    }
    public void MenuToSelectGame()
    {
        this.menuCanvas.SetActive(false);
        this.selectGameCanvas.SetActive(true);
    }
}
