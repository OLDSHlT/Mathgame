using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatPaperController : MonoBehaviour
{
    public Transform ProducePos;
    public GameObject YHReact;
    public GameObject Bat;
    public Transform BatRebornPos;
    //Damageable BatDam;
    // Start is called before the first frame update
    void Awake()
    {
        //BatDam = Bat.GetComponent<Damageable>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BatDeath()
    {
        GameObject cloneReact = Instantiate(YHReact, YHReact.transform.position, YHReact.transform.rotation);
        Rigidbody2D rd = Bat.GetComponent<Rigidbody2D>();
        rd.gravityScale = 1f;
        Invoke("LaterReborn",2f);
        YHReact.SetActive(true);
        YHReact.transform.position=Bat.transform.position-new Vector3(0,0.5f,0);
        YHReact = cloneReact;
    }
    public void LaterReborn()
    {
        GameObject cloneBat = Instantiate(Bat, BatRebornPos.transform.position, Bat.transform.rotation);
        Bat=cloneBat;
        Rigidbody2D rd = cloneBat.GetComponent<Rigidbody2D>();
        rd.gravityScale = 0f;
    }
}
