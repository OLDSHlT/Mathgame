using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    Rigidbody2D rd;
    public List<Transform> waypoint;//需要手动拖动各个点位

    void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        nextwaypoint = waypoint[nextwaypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        Flight();
    }
    private int nextwaypointIndex = 0;
    Transform nextwaypoint;
    public float FlySpeed = 4f;
    float minDistanceToWayPoint = 0.1f;
    private void Flight()
    {
        Vector2 DirectionToWayPoint = (nextwaypoint.position - transform.position).normalized;
        float DistanceToWayPoint = Vector2.Distance(nextwaypoint.position, transform.position);
        rd.velocity = DirectionToWayPoint * FlySpeed;
        UpdateScale();
        if (DistanceToWayPoint >= minDistanceToWayPoint + 3.89
            && DistanceToWayPoint <= minDistanceToWayPoint + 4)//到达下一个点位之前的某处
        {
            //if (inSkill)
            //{
            //    inSkill = false;
            //    damageable.RollTriggerInvincible = false;
            //    animator.SetBool(AnimationString.Skilldone,true);
            //}
        }
        if (DistanceToWayPoint <= minDistanceToWayPoint)//达到下一个点位
        {
            nextwaypointIndex++;
            if (nextwaypointIndex >= waypoint.Count)
            {
                nextwaypointIndex = 0;
            }
            nextwaypoint = waypoint[nextwaypointIndex];

        }
    }
    private void UpdateScale()
    {
        Vector3 localScale = transform.localScale;
        if (transform.localScale.x > 0)
        {
            if (rd.velocity.x < 0)
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
        }
        else
        {
            if (rd.velocity.x > 0)
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
        }
    }
}
