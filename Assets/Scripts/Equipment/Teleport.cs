using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;
    public Transform targetObject;
    public GameObject background;
    public Camera mainCamera;
    public CinemachineVirtualCamera virtualCamera;
    public Collider2D newBoundary;
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
    public void TeleportCamera()
    {
        if(mainCamera != null)
        {
            mainCamera.transform.position = destination.transform.position;
        }
        if(virtualCamera != null)
        {
            virtualCamera.transform.position = destination.transform.position;
        }
    }
    public void ChangeCameraBoundary()
    {
        if(newBoundary != null)
        {
            virtualCamera.gameObject.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = newBoundary;
        }
    }
    
}
