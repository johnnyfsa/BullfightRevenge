using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodeoClownAnimator : MonoBehaviour
{
    private RodeoClownMovement rodeoClownMovement;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rodeoClownMovement = GetComponentInParent<RodeoClownMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
