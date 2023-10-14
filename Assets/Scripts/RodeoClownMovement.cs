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


    private void Start()
    {
        centerOfArena = new Vector3(Random.Range(-3f, 3f), transform.position.y, Random.Range(-3f, 3f));
        //using the player name is a bad idea because it can change
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Update()
    {
        //calculate distance to player
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        distanceToCenter = Vector3.Distance(transform.position, centerOfArena);
        //if shorter than distanceToPlayer, run away from player
        if (distanceToPlayer < distanceThresholdToPlayer)
        {
            if (player.isMoving)
            {
                RunAwayFromPlayer();
            }
            else
            {
                TurnToPlayer();
            }
        }
        //if longer than distanceToPlayer, run to center of arena
        else
        {
            if (distanceToCenter > distanceThresholdToCenter)
            {
                RunToCenterOfArena();
            }
        }
    }
    private void RunAwayFromPlayer()
    {
        moveDir = transform.position - player.transform.position;
        transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.World);
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);
    }
    private void RunToCenterOfArena()
    {
        moveDir = centerOfArena - transform.position;
        transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.World);
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);
    }

    private void TurnToPlayer()
    {
        Vector3 playerDirection = player.transform.position - transform.position;
        transform.forward = Vector3.Slerp(transform.forward, playerDirection, rotationSpeed * Time.deltaTime);
    }


}
