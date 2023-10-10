using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyMoveSideways : MonoBehaviour
{

    [SerializeField] float speed = 1f;

    private bool canMove = true;

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        speed *= -1.0f;
    }




}
