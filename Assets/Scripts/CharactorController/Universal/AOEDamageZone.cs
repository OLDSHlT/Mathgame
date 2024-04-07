using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamageZone : MonoBehaviour
{
    public List<GameObject> enemyList;
    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        enemyList.Add(collision.gameObject);
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        enemyList.Remove(collision.gameObject);
        
    }
}
