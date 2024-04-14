using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public bool isOpen = false;
    public Transform recoverPoint;
    private Vector3 target;
    // Start is called before the first frame update
    void Start() 
    {
        if(recoverPoint != null)
        {
            target = new Vector3(recoverPoint.position.x, recoverPoint.position.y, recoverPoint.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            MoveDoor();
        }
    }
    public void OpenDoor()
    {
        isOpen = true;
    }
    private void MoveDoor()
    {
        if (recoverPoint != null)
        {
            transform.position = Vector3.Lerp(transform.position, target, 1 * Time.deltaTime);
        }
    }
}
