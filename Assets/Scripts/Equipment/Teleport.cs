using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;
    public Transform targetObject;
    public GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TeleportObject()
    {
        targetObject.transform.position = destination.transform.position;
    }
    public void DisableBackground()
    {
        if(background != null)
        {
            background.SetActive(false);
        }
    }
}
