using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAnimator : MonoBehaviour
{
    private const string SPEED = "Speed_f";
    private const string EAT = "Eat_b";
    private Animator animator;
    [SerializeField] Player playerController;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.isMoving)
        {
            animator.SetFloat(SPEED, 0);
            animator.SetBool(EAT, true);
        }
        else
        {
            animator.SetFloat(SPEED, 1);
            animator.SetBool(EAT, false);
            animator.Play("Locomotion");
        }
    }
}
