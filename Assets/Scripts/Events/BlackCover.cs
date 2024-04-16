using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlackCover : MonoBehaviour
{
    Animator animator;
    AnimatorStateInfo state;
    public UnityEvent blackoutEvents;
    bool isExecuted = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("BlackOut") && state.normalizedTime >= 1.0f)
        {
            if (!isExecuted)
            {
                isExecuted = true;
                blackoutEvents?.Invoke();
            }
            //动画播放完成
        }
    }
}
