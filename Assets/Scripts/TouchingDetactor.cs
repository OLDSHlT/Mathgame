using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDetactor : MonoBehaviour
{
    public ContactFilter2D castFilter;
    private float groundDistance = 0.05f;
    private float wallDistance = 0.2f;
    //private float ceilingDistance = 0.25f;
    //[SerializeField]
    //private float canClimbCheckDistance = 0.7f;


    CapsuleCollider2D touchingCollider;
    //public Transform canEdgeClimbposition;
    LayerMask MaskGround;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    //RaycastHit2D[] RayTestHits = new RaycastHit2D[5];//²âÊÔÓÃ

    private Animator animator;
    [SerializeField]
    private bool _isGrounded;
    public bool isGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            //animator.SetBool(AnimationString.isGround, value);
        }
    }
    [SerializeField]
    private bool _isWall;
    private Vector2 WallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool isWall
    {
        get
        {
            return _isWall;
        }
        private set
        {
            _isWall = value;
            //animator.SetBool(AnimationString.isWall, value);
        }
    }
    //private bool _isceiling;
    //public bool isceiling
    //{
    //    get
    //    {
    //        return _isceiling;
    //    }
    //    private set
    //    {
    //        _isceiling = value;
    //        animator.setbool(animationstring.isceiling, value);
    //    }
    //}

    //private bool _canEdgeClimb;
    //public bool canEdgeClimb
    //{
    //    get
    //    {
    //        return _canEdgeClimb;
    //    }
    //    private set
    //    {
    //        _canEdgeClimb = value;
    //        if(gameObject.tag=="Player")
    //            animator.SetBool(AnimationString.canEdgeClimb, value);
    //    }
    //}

    private void Awake()
    {
        touchingCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        //MaskGround = 1 << 7;
        //MaskGround = LayerMask.GetMask("Ground","UI");

    }
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = touchingCollider.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        isWall = touchingCollider.Cast(WallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        //isWall = Physics2D.Raycast(transform.position, WallCheckDirection, wallDistance, MaskGround);
        //isCeiling = touchingCollider.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
        //canEdgeClimb = Physics2D.Raycast(canEdgeClimbposition.position, WallCheckDirection, canClimbCheckDistance,MaskGround);
    }
}
