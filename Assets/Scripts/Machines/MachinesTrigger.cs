using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MachinesTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    //可交互物体的触发器
    public bool isPlayerInTrigger = false;
    public UnityEvent activeEvent;
    public UnityEvent enterEvent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                activeEvent?.Invoke();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            enterEvent?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
}
