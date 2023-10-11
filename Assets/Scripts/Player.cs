using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event EventHandler OnLivesChanged;
    [SerializeField] InputManager inputManager;
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] int numLives;

    public bool isMoving = false;
    private bool canMove;

    public int NumLives { get => numLives; set => numLives = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = inputManager.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float moveDistance = Time.deltaTime * speed;
        isMoving = moveDir != Vector3.zero;
        float playerRadius = 1.0f;
        float playerHeight = 2.0f;
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //if the player is making a diagonal movement and hits a wall, it should try to decompose the movement so it cam move sideways whilst hugging the wall
        //if the player hit a wall
        if (!canMove)
        {
            //check if the player cam move on the x axis
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized; //normalize, so it does not go slower when hugging the wall
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                //check if the player can move on the Z axis
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);

    }

    public bool IsWalking()
    {
        return isMoving;
    }

    public void AddLives(int livesToAdd)
    {
        NumLives += livesToAdd;
        if (NumLives >= 0)
        {
            OnLivesChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}
