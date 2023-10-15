using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodeoClownMovement : MonoBehaviour
{
    private Player player;
    [SerializeField] float speed = 2f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float distanceThresholdToPlayer = 5f;
    [SerializeField] float distanceThresholdToCenter = 3.0f;

    private Vector3 moveDir;
    private float distanceToPlayer;
    private float distanceToCenter;
    private Vector3 centerOfArena;

    private bool canDance;

    public bool CanDance { get => canDance; set => canDance = value; }

    private void Start()
    {
        centerOfArena = new Vector3(UnityEngine.Random.Range(-3f, 3f), transform.position.y, UnityEngine.Random.Range(-3f, 3f));
        //using the player name is a bad idea because it can change
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        moveDir = Vector3.zero;
        //calculate distance to player
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //calculate the distance to the center of the arena
        distanceToCenter = Vector3.Distance(transform.position, centerOfArena);
        //if shorter than distanceToPlayer, run away from player
        if (distanceToPlayer < distanceThresholdToPlayer)
        {
            //player is moving so it's better to run away, runing animation
            if (player.isMoving)
            {
                RunAwayFromPlayer();
            }
            //player is standing still so it's better to turn and face the player, idle animation
            else
            {
                TurnToPlayer();
            }
        }
        //if longer than distanceToPlayer, run to center of arena
        else
        {
            /*the center of the arena has a radius of distanceThresholdToCenter, 
            if it's out of the area defined by this radius, it should move towards the center*/
            if (distanceToCenter > distanceThresholdToCenter)
            {
                RunToCenterOfArena();
            }
            //if the enemy is inside the area defined by the radius, it should dance
            else if (distanceToCenter <= distanceThresholdToCenter)
            {
                canDance = true;
            }
        }

    }
    private void RunAwayFromPlayer()
    {
        canDance = false;
        moveDir = transform.position - player.transform.position;
        transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.World);
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);
    }
    private void RunToCenterOfArena()
    {
        canDance = false;
        moveDir = centerOfArena - transform.position;
        transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.World);
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);
    }

    private void TurnToPlayer()
    {
        Vector3 playerDirection = player.transform.position - transform.position;
        transform.forward = Vector3.Slerp(transform.forward, playerDirection, rotationSpeed * Time.deltaTime);
    }

    public bool IsMoving()
    {
        if (moveDir == Vector3.zero)
        {
            return false;
        }
        return true;
    }
}
