using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodeoClownMovement : MonoBehaviour
{
    private Transform player;
    [SerializeField] float speed = 2f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float distanceThreshold = 5f;
    private float distanceToPlayer;
    private void Start()
    {
        //using the player name is a bad idea because it can change
        player = GameObject.Find("Player").transform;
    }
    private void Update()
    {
        //calculate distance to player
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //if shorter than distanceToPlayer run away from player
        if (distanceToPlayer < distanceThreshold)
        {
            RunAwayFromPlayer();
        }
        //if longer than distanceToPlayer run to center of arena
        else
        {
            RunToCenterOfArena();
        }

    }
    private void RunAwayFromPlayer()
    {
        Vector3 awayDirection = transform.position - player.position;
        transform.Translate(awayDirection.normalized * speed * Time.deltaTime, Space.World);
        transform.forward = Vector3.Slerp(transform.forward, awayDirection, rotationSpeed * Time.deltaTime);
    }
    private void RunToCenterOfArena()
    {
        Vector3 centerOfArena = new Vector3(0, transform.position.y, 0);
        Vector3 directionToCenter = centerOfArena - transform.position;
        transform.Translate(directionToCenter.normalized * speed * Time.deltaTime, Space.World);
        transform.forward = Vector3.Slerp(transform.forward, directionToCenter, rotationSpeed * Time.deltaTime);
    }
}
