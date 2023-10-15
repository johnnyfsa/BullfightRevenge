using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodeoClownAnimator : MonoBehaviour
{
    private const string IDLE_SEXY_DANCE = "Idle_SexyDance";
    private const string CAN_DANCE = "CanDance_b";
    private const string IS_STATIC = "Static_b";
    private const string SPEED = "Speed_f";
    private RodeoClownMovement rodeoClownMovement;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rodeoClownMovement = GetComponentInParent<RodeoClownMovement>();
        animator = GetComponent<Animator>();
        animator.SetBool(IS_STATIC, false);
    }

    // Update is called once per frame
    void Update()
    {
        //if the rodeClown is moving the animator should play the Run animation set the speed to 1 and it cannot dance
        if (rodeoClownMovement.IsMoving())
        {
            animator.SetFloat(SPEED, 1);
            animator.SetBool(CAN_DANCE, false);
        }
        //else if the rodeoClown is not moving the animator should set the speed to 0
        else
        {
            animator.SetFloat(SPEED, 0);
            //if the rodeoClown is not moving and canDance is true, than the rodeoClown should dance
            if (rodeoClownMovement.CanDance)
            {
                animator.SetBool(CAN_DANCE, true);
            }
            //else if the rodeoclown is not moving and canDance is false, than the rodeoClown should not dance
            else if (!rodeoClownMovement.CanDance)
            {
                animator.SetBool(CAN_DANCE, false);
            }
        }
    }
}
